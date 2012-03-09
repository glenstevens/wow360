using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Media;
using Microsoft.DirectX;
using XInputDotNet;

namespace DavidNikdel
{
  public partial class WoW360Router : Form, IDisposable
  {
    private volatile UInt32 m_hWnd = 0;
    private volatile bool m_done = false;
    private volatile bool m_pause = true;
    private volatile int m_nPadIndex = 0;
    private Thread m_thread = null;
    private GamePad m_pad = new GamePad();
    private SoundPlayer m_chimeSnd = null;
    private SoundPlayer m_boopSnd = null;
    private Bitmap m_controller = null;
    private Bitmap m_about = null;
    private Bitmap m_keymap = null;
    private double m_lookSensMulti = NORMAL_LOOK;
    private double m_mouseSensMulti = NORMAL_MOUSE;
    private Rectangle m_wndExt;
    private Point m_wndOrig;
    private bool m_fLMouseDown = false;
    private bool m_fRMouseDown = false;
    private bool m_LTriggerDown = false;
    private bool m_RTriggerDown = false;
    private bool m_btnQDown = false;
    private bool m_btnWDown = false;
    private bool m_btnEDown = false;
    private bool m_btnSDown = false;
    private bool m_shiftToggle = false;
    private bool m_readyToToggle = true;

    private Vector2 m_home;
    private Properties.Settings m_settings = new Properties.Settings();

    private const double NORMAL_LOOK = 15.0f;
    private const double NORMAL_MOUSE = 25.0f;

    public enum KeyPressType
    {
      Up,
      Down,
      Both
    }
    public enum MouseButton
    {
      Left,
      Right
      // no middle mouse support yet
    }

    public const UInt32 BTN_TARGET = 0x00100000;
    public const UInt32 BTN_SHIFT = 0x00200000;
    public const UInt32 BTN_RIGHT_TRIGGER = 0x00010000;
    public const UInt32 BTN_LEFT_TRIGGER = 0x00020000;

    #region Constructor + Dispose
    public WoW360Router()
    {
      InitializeComponent();
      this.SetStyle(ControlStyles.UserPaint, true);
      System.Resources.ResourceManager rm = new System.Resources.ResourceManager(
          "DavidNikdel.Properties.Resources",
          System.Reflection.Assembly.GetExecutingAssembly());

      // load resources
      m_controller = rm.GetObject("controller") as Bitmap;
      m_about = rm.GetObject("about") as Bitmap;
      m_keymap = rm.GetObject("keymap") as Bitmap;
      m_chimeSnd = new SoundPlayer(rm.GetStream("ding"));
      m_boopSnd = new SoundPlayer(rm.GetStream("boop"));

      // load preferences
      m_lookSensMulti = NORMAL_LOOK * m_settings.LookSensitivity;
      m_mouseSensMulti = NORMAL_MOUSE * m_settings.MouseSensitivity;
      m_home = new Vector2(m_settings.CenterOffsetX, m_settings.CenterOffsetY); // mouse center offset (in percent of quarter screen)
    }

    public new void Dispose()
    {
      if (this.BackgroundImage != null)
        this.BackgroundImage.Dispose();
      this.BackgroundImage = null;
      if (m_controller != null)
        m_controller.Dispose();
      m_controller = null;
      if (m_about != null)
        m_about.Dispose();
      m_about = null;
      if (m_keymap != null)
        m_keymap.Dispose();
      m_keymap = null;
      if (m_chimeSnd != null)
        m_chimeSnd.Dispose();
      m_chimeSnd = null;
      if (m_boopSnd != null)
        m_boopSnd.Dispose();
      m_boopSnd = null;

      base.Dispose();
    }
    #endregion

    #region Windows Unmanaged Stuff
    public const UInt32 WM_KEYDOWN = 0x0100;
    public const UInt32 WM_CHAR = 0x0102;
    public const UInt32 WM_KEYUP = 0x0101;
    public const UInt32 WM_LBUTTONDOWN = 0x0201;
    public const UInt32 WM_LBUTTONUP = 0x0202;
    public const UInt32 WM_MBUTTONDOWN = 0x0207;
    public const UInt32 WM_MBUTTONUP = 0x0208;
    public const UInt32 WM_RBUTTONDOWN = 0x0204;
    public const UInt32 WM_RBUTTONUP = 0x0205;
    public const UInt32 WM_MOUSEMOVE = 0x0200;

    public delegate bool EnumCallBack(UInt32 hWnd, UInt32 lParam);

    [DllImport("user32.dll")]
    public static extern bool EnumWindows(EnumCallBack x, int y);
    [DllImport("user32.dll")]
    public static extern void GetWindowText(UInt32 hWnd, StringBuilder s, UInt32 nMaxCount);
    [DllImport("user32.dll")]
    public static extern bool PostMessage(UInt32 hWnd, UInt32 msg, UInt32 wParam, UInt32 lParam);
    [DllImport("user32.dll")]
    public static extern UInt32 SendMessage(UInt32 hWnd, UInt32 msg, UInt32 wParam, UInt32 lParam);
    [DllImport("user32.dll")]
    public static unsafe extern bool ClientToScreen(UInt32 hWnd, Point* lpPoint);
    [DllImport("user32.dll")]
    public static unsafe extern bool GetClientRect(UInt32 hWnd, Rectangle* lpRect);

    private static bool WarcraftCB(UInt32 hWnd, UInt32 lParam)
    {
      StringBuilder sbName = new StringBuilder(1024);
      GetWindowText(hWnd, sbName, (UInt32)sbName.Capacity);
      if (sbName.ToString() == "World of Warcraft")
      //if (sbName.ToString() == "Untitled - Notepad")
      {
        WoW360Router f = (WoW360Router)Form.FromHandle((IntPtr)lParam);
        f.m_hWnd = hWnd;
      }
      return true;
    }
    #endregion

    #region WoW Link
    private void OnLostWindow()
    {
      // pause (if we are not already)
      this.Paused = true;
      // ditch the window handle
      m_hWnd = 0;

      // update the WowLink status
      m_wowStatus.Visible = false;
    }

    private void OnGotWindow()
    {
      Rectangle ext;
      Point orig;

      // figure out how big the window is
      unsafe
      {
        GetClientRect(m_hWnd, &ext);
        ClientToScreen(m_hWnd, &orig);
      }
      m_wndExt = ext;
      m_wndOrig = orig;

      FakeMouse(MouseButton.Left, KeyPressType.Up);
      m_fLMouseDown = false;
      FakeMouse(MouseButton.Right, KeyPressType.Up);
      m_fRMouseDown = false;

      // update the WowLink status
      m_wowStatus.Visible = true;
    }

    private bool FindWindow()
    {
      // get a handle to the world of warcraft window
      OnLostWindow();
      EnumWindows(new EnumCallBack(WarcraftCB), (int)this.Handle);
      if (m_hWnd > 0)
      {
        OnGotWindow();
        return true;
      }
      return false;
    }

    public void FakeMouseMove(int x, int y)
    {
      if (m_hWnd <= 0 || m_pause)
        return;
      Cursor.Position = new Point(Cursor.Position.X + x, Cursor.Position.Y + y);
    }

    public void FakeMouse(MouseButton b, KeyPressType t)
    {
      if (m_hWnd <= 0 || m_pause)
        return;
      if (t == KeyPressType.Both)
      {
        UInt32 pos = (UInt32)((Cursor.Position.Y << 16) | Cursor.Position.X);
        if (b == MouseButton.Left)
        {
          // left click
          SendMessage(m_hWnd, WM_MOUSEMOVE, 0x01, pos);
          SendMessage(m_hWnd, WM_LBUTTONDOWN, 0x01, pos);
          SendMessage(m_hWnd, WM_LBUTTONUP, 0x01, pos);
          m_fLMouseDown = false;
        }
        else
        {
          // right click
          SendMessage(m_hWnd, WM_MOUSEMOVE, 0x02, pos);
          SendMessage(m_hWnd, WM_RBUTTONDOWN, 0x02, pos);
          SendMessage(m_hWnd, WM_RBUTTONUP, 0x02, pos);
          m_fRMouseDown = false;
        }
        return;
      }

      try
      {
        bool btnDown = (b == MouseButton.Left) ? m_fLMouseDown : m_fRMouseDown;
        if (t == KeyPressType.Down || t == KeyPressType.Both && !btnDown)
        {
          // figure out what message to send
          UInt32 command = 0;
          UInt32 wParam = 0;
          switch (b)
          {
            case MouseButton.Right:
              command = WM_RBUTTONDOWN;
              wParam = 0x02;
              break;
            default:
              command = WM_LBUTTONDOWN;
              wParam = 0x01;
              break;
          }

          // send the message
          if (!PostMessage(m_hWnd, command, wParam, (UInt32)((Cursor.Position.Y << 16) | Cursor.Position.X)))
            throw new Exception("WoW window lost.");
          if (b == MouseButton.Left)
            m_fLMouseDown = true;
          else
            m_fRMouseDown = true;
        }
        if (t == KeyPressType.Up || t == KeyPressType.Both && btnDown)
        {
          // figure out what message to send
          UInt32 command = 0;
          UInt32 wParam = 0;
          switch (b)
          {
            case MouseButton.Right:
              command = WM_RBUTTONUP;
              wParam = 0x02;
              break;
            default:
              command = WM_LBUTTONUP;
              wParam = 0x01;
              break;
          }
          // send the message
          if (!PostMessage(m_hWnd, command, wParam, (UInt32)((Cursor.Position.Y << 16) | Cursor.Position.X)))
            throw new Exception("WoW window lost.");
          if (b == MouseButton.Left)
            m_fLMouseDown = false;
          else
            m_fRMouseDown = false;
        }
      }
      catch (Exception e)
      {
        m_statusTxt.Text = e.Message;
        // something went wrong (assume we lost the window
        OnLostWindow();
      }
    }

    public void FakeKey(Keys k, KeyPressType t)
    {
      if (m_hWnd <= 0 || m_pause)
        return;
      try
      {
        UInt32 key = (UInt32)(k & Keys.KeyCode);
        if (t == KeyPressType.Down || t == KeyPressType.Both)
        {
          if (!PostMessage(m_hWnd, WM_KEYDOWN, key, 0x00000000))
            throw new Exception("WoW window lost.");
        }
        if (t == KeyPressType.Up || t == KeyPressType.Both)
        {
          if (!PostMessage(m_hWnd, WM_KEYUP, key, 0xC0000000))
            throw new Exception("WoW window lost.");
        }
      }
      catch (Exception e)
      {
        m_statusTxt.Text = e.Message;
        // something went wrong (assume we lost the window
        OnLostWindow();
      }
    }

    private void CenterCursor()
    {
      if (m_hWnd <= 0 || m_pause)
        return;

      // position acordingly
      Point location = new Point(m_wndOrig.X + (int)((1.0f + m_home.X) * (float)m_wndExt.Width / 2.0f),
          m_wndOrig.Y + (int)((1.0f + m_home.Y) * (float)m_wndExt.Height / 2.0f)); // center the cursor

      Cursor.Position = location;
      SendMessage(m_hWnd, WM_MOUSEMOVE, 0x0, (UInt32)((location.Y << 16) | location.X));
    }
    #endregion

    #region Gamepad Functions
    private void ScanForGamePads()
    {
      // scan the game pads to see which are available
      bool fFoundPadIdx = false;
      for (int i = 0; i < 4; i++)
      {
        GamePad gp = XInput.GetPadState(i);
        if (gp != null)
        {
          m_cMenu.DropDownItems[i].Enabled = true;
          if (m_nPadIndex == i)
          {
            fFoundPadIdx = true;
            m_cMenu.DropDownItems[i].Image = m_controller;
          }
          else
            m_cMenu.DropDownItems[i].Image = null;
        }
        else
        {
          m_cMenu.DropDownItems[i].Enabled = false;
          m_cMenu.DropDownItems[i].Image = null;
        }
      }

      if (!fFoundPadIdx)
      {
        // pick the first one
        m_nPadIndex = -1;
        for (int i = 0; i < 4; i++)
        {
          if (m_cMenu.DropDownItems[i].Enabled)
          {
            m_cMenu.DropDownItems[i].Image = m_controller;
            m_nPadIndex = i;
            break;
          }
        }

        // none available
        if (m_nPadIndex < 0)
        {
          m_nPadIndex = 0;
          m_statusTxt.Text = "No X360 controllers detected.";
        }
      }
    }


    private void ControllerPollThread()
    {
      while (!m_done)
      {
        // sleep for a bit
        Thread.Sleep(20);

        // only check the selected controller
        GamePad gp = XInput.GetPadState(m_nPadIndex);
        if (gp == null)
          continue; // no game pad in this slot

        // Handle the preference for left or right target mode.
        if (m_settings.SwapShiftAndTarget)
        {
          if ((gp.Buttons & GamePad.BTN_RIGHT_SHOULDER) != 0)
            gp.Buttons |= BTN_TARGET;
          if (m_settings.UseMouseModeToggle)
          {
            if ((gp.Buttons & GamePad.BTN_LEFT_SHOULDER) != 0)
            {
              // Do not toggle more than once while the shoulder is depressed, allow again after it has been released
              if (m_readyToToggle) m_shiftToggle = !m_shiftToggle;
              m_readyToToggle = false;
            }
            else
            {
              m_readyToToggle = true;
            }
          }
          else
          {
            // Original functionality
            if ((gp.Buttons & GamePad.BTN_LEFT_SHOULDER) != 0)
            {
              gp.Buttons |= BTN_SHIFT;
            }
          }
        }
        else
        {
          if (m_settings.UseMouseModeToggle)
          {
            if ((gp.Buttons & GamePad.BTN_RIGHT_SHOULDER) != 0)
            {
              // Do not toggle more than once while the shoulder is depressed, allow again after it has been released
              if (m_readyToToggle) m_shiftToggle = !m_shiftToggle;
              m_readyToToggle = false;
            }
            else
            {
              m_readyToToggle = true;
            }
          }
          else
          {
            // Original functionality
            if ((gp.Buttons & GamePad.BTN_RIGHT_SHOULDER) != 0)
            {
              gp.Buttons |= BTN_SHIFT;
            }
          }
          if ((gp.Buttons & GamePad.BTN_LEFT_SHOULDER) != 0)
            gp.Buttons |= BTN_TARGET;
        }

        // update the "mouse" using the analog sticks
        UpdateMouseMove(gp);
        // Grab our analog values from the controller
        AnalogMovement(gp);

        // check for button changes
        UInt32 BtnDown = gp.Buttons & ~m_pad.Buttons;
        UInt32 BtnUp = ~gp.Buttons & m_pad.Buttons;

        // update the pad and handle changes
        if (BtnUp != 0)
        {
          OnButton(BtnUp, KeyPressType.Up);
        }
        m_pad = gp;
        if (BtnDown != 0)
        {
          OnButton(BtnDown, KeyPressType.Down);
        }
      }
    }

    private void AnalogMovement(GamePad g)
    {
      float x = 0f;
      float y = 0f;
      if (m_settings.SwapSticks)
      {
        x = g.ThumbR.X;
        y = g.ThumbR.Y;
      }
      else
      {
        x = g.ThumbL.X;
        y = g.ThumbL.Y;
      }
      if (x > 0.5f)
      {
        FakeKey(Keys.E, KeyPressType.Down); // Strife Right
        m_btnEDown = true;
      }
      else if (x < -0.5f)
      {
        FakeKey(Keys.Q, KeyPressType.Down); // Strife Left
        m_btnQDown = true;
      }
      else
      {
        // Send up keys to stop moving if no input
        if (m_btnEDown)
        {
          FakeKey(Keys.E, KeyPressType.Up);
          m_btnEDown = false;
        }
        if (m_btnQDown)
        {
          FakeKey(Keys.Q, KeyPressType.Up);
          m_btnQDown = false;
        }
      }
      if (y > 0.5f)
      {
        FakeKey(Keys.W, KeyPressType.Down); // Move Forward
        m_btnWDown = true;
      }
      else if (y < -0.5f)
      {
        FakeKey(Keys.S, KeyPressType.Down); // Move Backwards
        m_btnSDown = true;
      }
      else
      {
        // Send up keys to stop moving if no input
        if (m_btnWDown)
        {
          FakeKey(Keys.W, KeyPressType.Up);
          m_btnWDown = false;
        }
        if (m_btnSDown)
        {
          FakeKey(Keys.S, KeyPressType.Up);
          m_btnSDown = false;
        }
      }

      // Evaluate if the left and right triggers are pulled as shift modifiers.
      m_RTriggerDown = (g.RightTrigger > 0.5f) ? true : false;
      m_LTriggerDown = (g.LeftTrigger > 0.5f) ? true : false;
    }

    private void OnButton(UInt32 mask, KeyPressType t)
    {
      // button keys are all front loaded
      if (t == KeyPressType.Down)
      {
        if (m_LTriggerDown && m_RTriggerDown) // Check if both triggers are pulled
        {
          if ((mask & GamePad.BTN_A) != 0) //Assist Party member 1
          {
            FakeKey(Keys.F2, KeyPressType.Both);
            if (m_settings.AssistMode) FakeKey(Keys.F, KeyPressType.Both);
          }
          if ((mask & GamePad.BTN_B) != 0) //Assist Party member 2
          {
            FakeKey(Keys.F3, KeyPressType.Both);
            if (m_settings.AssistMode) FakeKey(Keys.F, KeyPressType.Both);
          }
          if ((mask & GamePad.BTN_X) != 0) //Assist Party member 3
          {
            FakeKey(Keys.F4, KeyPressType.Both);
            if (m_settings.AssistMode) FakeKey(Keys.F, KeyPressType.Both);
          }
          if ((mask & GamePad.BTN_Y) != 0) //Assist Party member 4
          {
            FakeKey(Keys.F5, KeyPressType.Both);
            if (m_settings.AssistMode) FakeKey(Keys.F, KeyPressType.Both);
          }
          if (m_settings.StickyBars)
          {
            if ((mask & GamePad.BTN_DPAD_UP) != 0)
            {
              // Scroll action bar up
              FakeKey(Keys.ShiftKey, KeyPressType.Down);
              FakeKey(Keys.Up, KeyPressType.Both);
              FakeKey(Keys.ShiftKey, KeyPressType.Up);
            }
            if ((mask & GamePad.BTN_DPAD_DOWN) != 0)
            {
              // Scroll action bar down
              FakeKey(Keys.ShiftKey, KeyPressType.Down);
              FakeKey(Keys.Down, KeyPressType.Both);
              FakeKey(Keys.ShiftKey, KeyPressType.Up);
            }
          }
        }
        else if (m_LTriggerDown) // Check if Left Trigger is pulled
        {
          if ((mask & GamePad.BTN_A) != 0)
            FakeKey(Keys.D5, KeyPressType.Both);
          if ((mask & GamePad.BTN_B) != 0)
            FakeKey(Keys.D6, KeyPressType.Both);
          if ((mask & GamePad.BTN_X) != 0)
            FakeKey(Keys.D7, KeyPressType.Both);
          if ((mask & GamePad.BTN_Y) != 0)
            FakeKey(Keys.D8, KeyPressType.Both);

          if (m_settings.PetControl)
          {
            if ((mask & GamePad.BTN_DPAD_LEFT) != 0)
            {
              FakeKey(Keys.ControlKey, KeyPressType.Down);
              FakeKey(Keys.D1, KeyPressType.Both);
              FakeKey(Keys.ControlKey, KeyPressType.Up);
            }
            if ((mask & GamePad.BTN_DPAD_UP) != 0)
            {
              FakeKey(Keys.ControlKey, KeyPressType.Down);
              FakeKey(Keys.D2, KeyPressType.Both);
              FakeKey(Keys.ControlKey, KeyPressType.Up);
            }
            if ((mask & GamePad.BTN_DPAD_RIGHT) != 0)
            {
              FakeKey(Keys.ControlKey, KeyPressType.Down);
              FakeKey(Keys.D3, KeyPressType.Both);
              FakeKey(Keys.ControlKey, KeyPressType.Up);
            }
          }
        }
        else if (m_RTriggerDown) // Check if Right Trigger is pulled
        {
          if ((mask & GamePad.BTN_A) != 0)
            FakeKey(Keys.D9, KeyPressType.Both);
          if ((mask & GamePad.BTN_B) != 0)
            FakeKey(Keys.D0, KeyPressType.Both);
          if ((mask & GamePad.BTN_X) != 0)
            FakeKey(Keys.OemMinus, KeyPressType.Both);
          if ((mask & GamePad.BTN_Y) != 0)
            FakeKey(Keys.Oemplus, KeyPressType.Both);
          if (m_settings.UseAltModifier && (mask & GamePad.BTN_DPAD_RIGHT) != 0)
            FakeKey(Keys.M, KeyPressType.Both);

          if (m_settings.PetControl)
          {
            if ((mask & GamePad.BTN_DPAD_LEFT) != 0)
            {
              FakeKey(Keys.ControlKey, KeyPressType.Down);
              FakeKey(Keys.D4, t);
              FakeKey(Keys.ControlKey, KeyPressType.Up);
            }
            if ((mask & GamePad.BTN_DPAD_UP) != 0)
            {
              FakeKey(Keys.ControlKey, KeyPressType.Down);
              FakeKey(Keys.D5, t);
              FakeKey(Keys.ControlKey, KeyPressType.Up);
            }
            if ((mask & GamePad.BTN_DPAD_RIGHT) != 0)
            {
              FakeKey(Keys.ControlKey, KeyPressType.Down);
              FakeKey(Keys.D6, t);
              FakeKey(Keys.ControlKey, KeyPressType.Up);
            }
            if ((mask & GamePad.BTN_DPAD_DOWN) != 0)
            {
              FakeKey(Keys.ControlKey, KeyPressType.Down);
              FakeKey(Keys.D7, t);
              FakeKey(Keys.ControlKey, KeyPressType.Up);
            }
          }
          // left trigger targets
          if ((mask & BTN_TARGET) != 0)
          {
            FakeKey(Keys.ControlKey, KeyPressType.Down);
            FakeKey(Keys.Tab, KeyPressType.Both);
            FakeKey(Keys.ControlKey, KeyPressType.Up);
          }
        }
        else
        {
          if ((mask & GamePad.BTN_A) != 0)
            FakeKey(Keys.D1, KeyPressType.Both);
          if ((mask & GamePad.BTN_B) != 0)
            FakeKey(Keys.D2, KeyPressType.Both);
          if ((mask & GamePad.BTN_X) != 0)
            FakeKey(Keys.D3, KeyPressType.Both);
          if ((mask & GamePad.BTN_Y) != 0)
            FakeKey(Keys.D4, KeyPressType.Both);

          // left trigger targets
          if ((mask & BTN_TARGET) != 0)
          {
            FakeKey(Keys.Tab, KeyPressType.Both);
          }

          if (!m_settings.UseAltModifier && (mask & GamePad.BTN_DPAD_LEFT) != 0)
          {
            // Open Map
            FakeKey(Keys.M, KeyPressType.Both);
          }
          if ((mask & GamePad.BTN_DPAD_RIGHT) != 0)
          {
            // Open Bag
            FakeKey(Keys.B, KeyPressType.Both);
          }
          if (!m_settings.StickyBars)
          {
            if ((mask & GamePad.BTN_DPAD_UP) != 0)
            {
              // Scroll action bar up
              FakeKey(Keys.ShiftKey, KeyPressType.Down);
              FakeKey(Keys.Up, KeyPressType.Both);
              FakeKey(Keys.ShiftKey, KeyPressType.Up);
            }
            if ((mask & GamePad.BTN_DPAD_DOWN) != 0)
            {
              // Scroll action bar down
              FakeKey(Keys.ShiftKey, KeyPressType.Down);
              FakeKey(Keys.Down, KeyPressType.Both);
              FakeKey(Keys.ShiftKey, KeyPressType.Up);
            }
          }
        }
      }

      if (m_settings.UseMouseModeToggle)
      {
        if (m_shiftToggle)
        {
          FakeMouse(MouseButton.Right, KeyPressType.Up);
        }
        else
        {
          CenterCursor();
          FakeMouse(MouseButton.Right, KeyPressType.Down);
        }
      }
      else
      {
        // center cursor on exit mousemove mode
        if (IsShiftPressed(mask))//((mask & BTN_SHIFT) != 0) //was BTN_RIGHT_SHOULDER
        {
          if (t == KeyPressType.Up)
          {
            CenterCursor();
            FakeMouse(MouseButton.Right, KeyPressType.Down);
          }
          else
            FakeMouse(MouseButton.Right, KeyPressType.Up);
        }
      }

      // only right/left click in mousemove mode
      if (IsShiftPressed(m_pad.Buttons))//((m_pad.Buttons & BTN_SHIFT) != 0) //was GamePad.BTN_RIGHT_SHOULDER
      {
        if (t == KeyPressType.Up)
        {
          if ((mask & GamePad.BTN_RIGHT_THUMB) != 0)
            FakeMouse(MouseButton.Right, KeyPressType.Both);
          if ((mask & GamePad.BTN_LEFT_THUMB) != 0)
            FakeMouse(MouseButton.Left, KeyPressType.Both);
        }
        //if ((mask & GamePad.BTN_RIGHT_THUMB) != 0)
        //  FakeMouse(MouseButton.Right, t);
        //if ((mask & GamePad.BTN_LEFT_THUMB) != 0)
        //  FakeMouse(MouseButton.Left, t);
      }
      else
      {
        // otherwise, right thumbclick is Space (Jump)
        if ((mask & GamePad.BTN_RIGHT_THUMB) != 0)
        {
          FakeKey(Keys.Space, t);
        }
        if ((mask & GamePad.BTN_LEFT_THUMB) != 0)
          FakeKey((m_settings.AutoRunUseTilde ? Keys.Oemtilde : Keys.NumLock), t);
      }

      if (m_settings.StickyBars && (!m_LTriggerDown || !m_RTriggerDown))
      {
        if ((mask & GamePad.BTN_DPAD_UP) != 0)
        {
          // Press shift when sticky bars enabled.
          FakeKey(Keys.ShiftKey, t);
        }
        if ((mask & GamePad.BTN_DPAD_DOWN) != 0)
        {
          // Press control when sticky bars enabled.
          FakeKey(Keys.ControlKey, t);
        }
        if (m_settings.UseAltModifier && (mask & GamePad.BTN_DPAD_LEFT) != 0)
        {
          // Press alt when sticky bars enabled.
          FakeKey(Keys.Menu, t);
        }
      }


      // Back = Esc
      if ((mask & GamePad.BTN_BACK) != 0)
        FakeKey(Keys.Escape, t);

      // check for pause being pressed
      if ((mask & GamePad.BTN_START) != 0 && t == KeyPressType.Up)
      {
        if ((m_pad.Buttons & GamePad.BTN_DPAD_UP) != 0) // up is held down
          FakeKey(Keys.NumLock, KeyPressType.Both); // autorun
        else
          this.Paused = !this.Paused; // toggle pause mode
      }
    }

    private bool IsShiftPressed(uint mask)
    {
      if (m_settings.UseMouseModeToggle)
      {
        return m_shiftToggle;
      }
      else
      {
        return ((mask & BTN_SHIFT) != 0); // Original check
      }
    }

    private void UpdateMouseMove(GamePad now)
    {
      Vector2 zero = new Vector2(0.0f, 0.0f);

      if (m_settings.SwapSticks)
      {
        if (IsShiftPressed(now.Buttons))//((now.Buttons & BTN_SHIFT) != 0) // mousemove -  //was GamePad.BTN_RIGHT_SHOULDER
          FakeMouseMove((int)(now.ThumbL.X * Math.Abs(now.ThumbL.X) * m_mouseSensMulti), (int)(now.ThumbL.Y * Math.Abs(now.ThumbL.Y) * -m_mouseSensMulti));
        else if (!(now.ThumbL == zero) && m_fRMouseDown) // mouselook
          FakeMouseMove((int)(now.ThumbL.X * m_lookSensMulti), (int)(now.ThumbL.Y * -m_lookSensMulti));
      }
      else
      {
        // move the mouse
        if (IsShiftPressed(now.Buttons))//((now.Buttons & BTN_SHIFT) != 0) // mousemove -  //was GamePad.BTN_RIGHT_SHOULDER
          FakeMouseMove((int)(now.ThumbR.X * Math.Abs(now.ThumbR.X) * m_mouseSensMulti), (int)(now.ThumbR.Y * Math.Abs(now.ThumbR.Y) * -m_mouseSensMulti));
        else if (!(now.ThumbR == zero) && m_fRMouseDown) // mouselook
          FakeMouseMove((int)(now.ThumbR.X * m_lookSensMulti), (int)(now.ThumbR.Y * -m_lookSensMulti));
      }
    }
    #endregion

    #region Pause Functions
    private void SendPauseMessage(bool pause)
    {
      if (m_hWnd <= 0)
        return;

      // bring the mouse buttons up
      UInt32 pos = (UInt32)((Cursor.Position.Y << 16) | Cursor.Position.X);
      if (m_fRMouseDown)
      {
        SendMessage(m_hWnd, WM_RBUTTONUP, 0x00, pos);
        m_fRMouseDown = false;
        Thread.Sleep(50);
      }
      if (m_fLMouseDown)
      {
        SendMessage(m_hWnd, WM_LBUTTONUP, 0x00, pos);
        m_fLMouseDown = false;
        Thread.Sleep(50);
      }

      // position acordingly
      Point location;
      if (pause)
        location = new Point(m_wndOrig.X + 5, m_wndOrig.Y + m_wndExt.Height - 5);  // bottom left
      else
        location = new Point(m_wndOrig.X + m_wndExt.Width - 5, m_wndOrig.Y + m_wndExt.Height - 5);  // bottom right

      // move the cursor
      Cursor.Position = location;
      pos = (UInt32)((location.Y << 16) | location.X);

      // send the correct messages
      if (pause)
      {
        // send key-ups as necessary
        if ((m_pad.Buttons & GamePad.BTN_DPAD_LEFT) != 0)
          SendMessage(m_hWnd, WM_KEYUP, (UInt32)(Keys.Q & Keys.KeyCode), 0xC0000000);
        if ((m_pad.Buttons & GamePad.BTN_DPAD_RIGHT) != 0)
          SendMessage(m_hWnd, WM_KEYUP, (UInt32)(Keys.E & Keys.KeyCode), 0xC0000000);
        if ((m_pad.Buttons & GamePad.BTN_DPAD_UP) != 0)
          SendMessage(m_hWnd, WM_KEYUP, (UInt32)(Keys.W & Keys.KeyCode), 0xC0000000);
        if ((m_pad.Buttons & GamePad.BTN_DPAD_DOWN) != 0)
          SendMessage(m_hWnd, WM_KEYUP, (UInt32)(Keys.S & Keys.KeyCode), 0xC0000000);

        // left click on the lower left corner
        SendMessage(m_hWnd, WM_MOUSEMOVE, 0x00, pos);
        SendMessage(m_hWnd, WM_LBUTTONDOWN, 0x01, pos);
        SendMessage(m_hWnd, WM_LBUTTONUP, 0x01, pos);

        // after pausing or unpausing, we need to sleep to give the UI a second to catch up
        Thread.Sleep(500);

        // recenter the cursor, but don't send the mouse move
        Cursor.Position = new Point(m_wndOrig.X + m_wndExt.Width / 2, m_wndOrig.Y + m_wndExt.Height / 2); // center the cursor
      }
      else
      {
        // right click on the lower right corner
        SendMessage(m_hWnd, WM_MOUSEMOVE, 0x00, pos);
        SendMessage(m_hWnd, WM_RBUTTONDOWN, 0x02, pos);
        SendMessage(m_hWnd, WM_RBUTTONUP, 0x02, pos);

        // after pausing or unpausing, we need to sleep to give the UI a second to catch up
        Thread.Sleep(500);

        // recenter the cursor
        CenterCursor();

        // right mouse defaults to down
        SendMessage(m_hWnd, WM_RBUTTONDOWN, 0x02, pos);
        m_fRMouseDown = true;
      }
    }

    public bool Paused
    {
      set
      {
        if (value == m_pause)
          return;

        if (value)
        {
          // send key to hide the x360 UI, then pause
          SendPauseMessage(true);
          m_pause = true;

          // take care of the mechanics of pausing
          m_pauseMenu.Image = m_pauseMenu.DropDownItems[0].Image;
          m_statusTxt.Text = "Router paused.";
          if (!m_settings.MuteSound)
            m_boopSnd.Play(); // play the chime for unpausing the controller
        }
        else
        {
          if (m_hWnd <= 0)
          {
            m_statusTxt.Text = "Error: No WoW window.";
            return; // can't unpause if we don't have a window
          }

          // unpause, then send key to activate the x360 UI
          m_pause = false;
          SendPauseMessage(false);

          // take care of the mechanics of pausing
          m_pauseMenu.Image = m_pauseMenu.DropDownItems[1].Image;
          m_statusTxt.Text = "Router unpaused.";
          if (!m_settings.MuteSound)
            m_chimeSnd.Play(); // play the chime for unpausing the controller
        }
      }
      get
      {
        return m_pause;
      }
    }
    #endregion

    #region Windows Messages
    private void OnLoad(object sender, EventArgs e)
    {
      m_statusTxt.Text = FindWindow() ? "Window Acquired" : "WoW window not found.";
      ScanForGamePads();
      m_thread = new Thread(new ThreadStart(this.ControllerPollThread));
      m_thread.Name = "ControllerPoll Thread";
      m_thread.Priority = ThreadPriority.AboveNormal;
      m_thread.Start();
      m_timer.Enabled = true;
    }

    private void OnClosing(object sender, FormClosingEventArgs e)
    {
      m_timer.Enabled = false;
      OnLostWindow();
      m_done = true;
    }

    private void OnTimer(object sender, EventArgs e)
    {
      if (m_done)
        return;
      if (m_hWnd == 0)
      {
        if (FindWindow())
          m_statusTxt.Text = "Window Acquired";
      }
      else
      {
        // check and make sure the window's still up
        StringBuilder sbName = new StringBuilder(1024);
        GetWindowText(m_hWnd, sbName, (UInt32)sbName.Capacity);
        if (sbName.ToString() != "World of Warcraft")
        //if (sbName.ToString() != "Untitled - Notepad")
        {
          OnLostWindow();
          m_statusTxt.Text = "WoW window lost.";
        }
      }
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void scanToolStripMenuItem_Click(object sender, EventArgs e)
    {
      ScanForGamePads();
    }

    private void m_c1Menu_Click(object sender, EventArgs e)
    {
      m_nPadIndex = 0;
      m_c1Menu.Image = m_controller;
      m_c2Menu.Image = null;
      m_c3Menu.Image = null;
      m_c4Menu.Image = null;
    }

    private void m_c2Menu_Click(object sender, EventArgs e)
    {
      m_nPadIndex = 1;
      m_c1Menu.Image = null;
      m_c2Menu.Image = m_controller;
      m_c3Menu.Image = null;
      m_c4Menu.Image = null;
    }

    private void m_c3Menu_Click(object sender, EventArgs e)
    {
      m_nPadIndex = 2;
      m_c1Menu.Image = null;
      m_c2Menu.Image = null;
      m_c3Menu.Image = m_controller;
      m_c4Menu.Image = null;
    }

    private void m_c4Menu_Click(object sender, EventArgs e)
    {
      m_nPadIndex = 3;
      m_c1Menu.Image = null;
      m_c2Menu.Image = null;
      m_c3Menu.Image = null;
      m_c4Menu.Image = m_controller;
    }

    private void keyMappingsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      using (ImageDlg dlg = new ImageDlg(m_keymap))
      {
        dlg.ShowDialog();
      }
    }

    private void aboutX360RouterToolStripMenuItem_Click(object sender, EventArgs e)
    {
      using (ImageDlg dlg = new ImageDlg(m_about))
      {
        dlg.ShowDialog();
      }
    }

    private void pauseOff_Click(object sender, EventArgs e)
    {
      this.Paused = false;
    }

    private void pauseOn_Click(object sender, EventArgs e)
    {
      this.Paused = true;
    }

    private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
    {
      using (PrefsDlg dlg = new PrefsDlg(m_settings))
      {
        dlg.ShowDialog();
        m_lookSensMulti = NORMAL_LOOK * m_settings.LookSensitivity;
        m_mouseSensMulti = NORMAL_MOUSE * m_settings.MouseSensitivity;
      }
    }
    #endregion

  }
}
