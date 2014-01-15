using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace testApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            FP.OnScreenKeyboard osk = new FP.OnScreenKeyboard(textBox1);
            osk.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //keybd_event(vbKeyControl, 0, 0, 0);
            //keybd_event(vbKeyEscape, 0, 0, 0);
            //keybd_event(vbKeyControl, 0, KEYEVENTF_KEYUP, 0);
            //keybd_event(vbKeyEscape, 0, KEYEVENTF_KEYUP, 0);
            keybd_event(vbKeyLWin, 0, 0, 0);
            keybd_event(vbKeyLWin, 0, KEYEVENTF_KEYUP, 0);
        }

        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);
        private const uint KEYEVENTF_KEYUP = 2;
        private const byte vbKeyControl = 17;
        private const byte vbKeyEscape = 27;
        private const byte vbKeyLWin = 91;

    }
}
