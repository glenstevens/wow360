using System;
using System.Runtime.InteropServices;
using Microsoft.DirectX;

namespace XInputDotNet
{
	public class GamePad
    {
        public UInt32 dwPacketNumber;
        public float LeftTrigger;
        public float RightTrigger;
        public Vector2 ThumbL;
        public Vector2 ThumbR;
        public UInt32 Buttons;

        public const UInt32 BTN_DPAD_UP = 0x0001;
        public const UInt32 BTN_DPAD_DOWN = 0x0002;
        public const UInt32 BTN_DPAD_LEFT = 0x0004;
        public const UInt32 BTN_DPAD_RIGHT = 0x0008;
        public const UInt32 BTN_START = 0x0010;
        public const UInt32 BTN_BACK = 0x0020;
        public const UInt32 BTN_LEFT_THUMB = 0x0040;
        public const UInt32 BTN_RIGHT_THUMB = 0x0080;
        public const UInt32 BTN_LEFT_SHOULDER = 0x0100;
        public const UInt32 BTN_RIGHT_SHOULDER = 0x0200;
        public const UInt32 BTN_A = 0x1000;
        public const UInt32 BTN_B = 0x2000;
        public const UInt32 BTN_X = 0x4000;
        public const UInt32 BTN_Y = 0x8000;
	};

	public class HeadSetDevice
	{
		public Guid guidHeadphones;
		public Guid guidMicrophone;
	}

	public class XInput
    {
        [DllImport("XInput9_1_0.dll")]
        private static unsafe extern int XInputGetState(int idx, XINPUT_STATE* state);
        [DllImport("XInput9_1_0.dll")]
        private static unsafe extern int XInputSetState(int idx, XINPUT_VIBRATION* state);
        [DllImport("XInput9_1_0.dll")]
        private static unsafe extern int XInputGetDSoundAudioDeviceGuids(int idx, Guid* mic, Guid* phones);

        private struct XINPUT_GAMEPAD
        {
            public UInt16 wButtons;
            public byte bLeftTrigger;
            public byte bRightTrigger;
            public Int16 sThumbLX;
            public Int16 sThumbLY;
            public Int16 sThumbRX;
            public Int16 sThumbRY;
        }

        private struct XINPUT_STATE
        {
            public UInt32 dwPacketNumber;
            public XINPUT_GAMEPAD Gamepad;
        }

        private struct XINPUT_VIBRATION
        {
            public UInt16 wLeftMotorSpeed;
            public UInt16 wRightMotorSpeed;
        }

        private static float apply_thresh(int val, int thresh, int max)
        {
            if (val < thresh && val > -thresh)
                return 0.0f;
            if (val < 0)
                return (float)(val + thresh) / (float)(max - thresh);
            return (float)(val - thresh) / (float)(max - thresh);
        }

        private static Vector2 apply_thresh_2d(int xval, int yval, int thresh, int max)
        {
            Vector2 pos = new Vector2((float)xval, (float)yval);
            if (pos.LengthSq() < thresh * thresh)
                return new Vector2(0.0f, 0.0f);

            Vector2 n = pos;
            n.Normalize();
            n *= (float)thresh;
            pos -= n;
            pos *= 1.0f / (float)(max - thresh);
            return pos;
        }

		public static GamePad GetPadState(int nUserIdx)
        {
            XINPUT_STATE st = new XINPUT_STATE();
            unsafe
            {
                if (XInputGetState(nUserIdx, &st) != 0)
                    return null;
            }

            GamePad gp = new GamePad();
            gp.dwPacketNumber = st.dwPacketNumber;
            gp.Buttons = st.Gamepad.wButtons;
            gp.LeftTrigger = apply_thresh(st.Gamepad.bLeftTrigger, XInput.TriggerThresh, XInput.TriggerMax);
            gp.RightTrigger = apply_thresh(st.Gamepad.bRightTrigger, XInput.TriggerThresh, XInput.TriggerMax);
            //if (m_settings.LThumbToDPad)
            //{
                gp.ThumbL = apply_thresh_2d(st.Gamepad.sThumbLX, st.Gamepad.sThumbLY, XInput.LeftThumbThresh, XInput.ThumbstickMax);
                gp.ThumbR = apply_thresh_2d(st.Gamepad.sThumbRX, st.Gamepad.sThumbRY, XInput.RightThumbThresh, XInput.ThumbstickMax);
            /*}
            else
            {
                gp.ThumbL = apply_thresh_2d(st.Gamepad.sThumbLX, st.Gamepad.sThumbLY, XInput.LeftThumbThresh, XInput.ThumbstickMax);
                gp.ThumbR = apply_thresh_2d(st.Gamepad.sThumbRX, st.Gamepad.sThumbRY, XInput.RightThumbThresh, XInput.ThumbstickMax);
            }*/
            return gp;
        }

		public static bool SetVibration(int nUserIdx, float fLowFreqMotorSpeed, float fHiFreqMotorSpeed)
        {
            XINPUT_VIBRATION state = new XINPUT_VIBRATION();
            state.wLeftMotorSpeed = (ushort)(65535.0f * fLowFreqMotorSpeed);
            state.wRightMotorSpeed = (ushort)(65535.0f * fHiFreqMotorSpeed);

            int res = 0;
            unsafe
            {
                res = XInputSetState(nUserIdx, &state);
            }
            return (res == 0);
        }

		public static HeadSetDevice	GetAudio(int nUserIdx)
        {
            Guid soundOut = new Guid();
            Guid soundIn = new Guid();
            unsafe
            {
                if (XInputGetDSoundAudioDeviceGuids(nUserIdx, &soundOut, &soundIn) != 0)
                    return null;
            }

            HeadSetDevice dev = new HeadSetDevice();
            dev.guidHeadphones = soundOut;
            dev.guidMicrophone = soundIn;
            return dev;
        }

        public const UInt16 VibrationMax = 0xFFFF;
		public const byte TriggerMax = 255;
        public const UInt16 ThumbstickMax = 32767; // min is actually -32768 but meh

        public const byte TriggerThresh = 30;
        public const UInt16 LeftThumbThresh = 7849;
        public const UInt16 RightThumbThresh = 8689;
	}
}