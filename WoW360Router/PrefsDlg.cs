using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DavidNikdel
{
  public partial class PrefsDlg : Form
  {
    private Properties.Settings m_settings = null;

    internal PrefsDlg(Properties.Settings set)
    {
      InitializeComponent();
      m_settings = set;
    }

    private void Cancel_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void PrefsDlg_Load(object sender, EventArgs e)
    {
      m_mute.Checked = m_settings.MuteSound;
      m_autorunUseTilde.Checked = m_settings.AutoRunUseTilde;
      m_StickyBars.Checked = m_settings.StickyBars;
      m_AssistMode.Checked = m_settings.AssistMode;
      m_PetControls.Checked = m_settings.PetControl;
      m_UseAltModifier.Checked = m_settings.UseAltModifier;
      m_useMouseModeToggle.Checked = m_settings.UseMouseModeToggle;
      //m_invertMouse.Checked = m_settings.InvertMouse;
      //m_lt2dp.Checked = m_settings.LThumbToDPad;

      double LSens = m_settings.LookSensitivity;
      m_lookSense.Value = (int)(50.0 + 25.0 * Math.Log(LSens, 2.0));
      double MSens = m_settings.MouseSensitivity;
      m_mouseSense.Value = (int)(50.0 + 25.0 * Math.Log(MSens, 2.0));

      m_swapTabShift.SelectedIndex = m_settings.SwapShiftAndTarget ? 1 : 0;
      m_SwapSticks.SelectedIndex = m_settings.SwapSticks ? 1 : 0;
    }

    private void Ok_Click(object sender, EventArgs e)
    {
      m_settings.MuteSound = m_mute.Checked;
      m_settings.AutoRunUseTilde = m_autorunUseTilde.Checked;
      m_settings.StickyBars = m_StickyBars.Checked;
      m_settings.AssistMode = m_AssistMode.Checked;
      m_settings.PetControl = m_PetControls.Checked;
      m_settings.UseAltModifier = m_UseAltModifier.Checked;
      m_settings.UseMouseModeToggle = m_useMouseModeToggle.Checked;
      //m_settings.InvertMouse = m_invertMouse.Checked;
      //m_settings.LThumbToDPad = m_lt2dp.Checked;
      m_settings.MouseSensitivity = Math.Pow(2.0, ((double)m_mouseSense.Value - 50.0) / 25.0);
      m_settings.LookSensitivity = Math.Pow(2.0, ((double)m_lookSense.Value - 50.0) / 25.0);
      m_settings.SwapShiftAndTarget = (m_swapTabShift.SelectedIndex == 1);
      m_settings.SwapSticks = (m_SwapSticks.SelectedIndex == 1);
      m_settings.Save();

      this.DialogResult = DialogResult.OK;
      this.Close();
    }
  }
}