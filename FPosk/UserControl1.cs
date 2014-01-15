using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices; 

namespace FP
{
    public delegate void OnDrag(object sender, MouseEventArgs e);

    internal partial class KeyBoardControl : UserControl
    {
        Control _textBox;
        bool capLock = false;
        bool shift = false;
        bool ctrl = false;
        bool alt = false;

        public event OnDrag DragKeyBoard;

        protected virtual void OnDrag(MouseEventArgs e)
        {
            if (DragKeyBoard != null)
                DragKeyBoard(this, e);
        }

        public KeyBoardControl(Control textBox)
        {
            InitializeComponent();
            _textBox = textBox;
            try
            {
                foreach (Control btn in flowLayoutPanel1.Controls)
                {
                    if (btn is Button)
                    {
                        if (btn.Text.Length == 1 && char.GetUnicodeCategory(btn.Text[0]) != System.Globalization.UnicodeCategory.OtherSymbol && char.GetUnicodeCategory(btn.Text[0]) != System.Globalization.UnicodeCategory.MathSymbol)
                        {
                            btn.Click += new EventHandler(btn_Click);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        void btn_Click(object sender, EventArgs e)
        {
            try
            {
                Button l = sender as Button;
                setCursor();
                SendKeys.Send(l.Text);                
                if (shift)
                {
                    shift = !shift;
                    button30.ForeColor = Color.Black;
                    toLower();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void c_MouseLeave(object sender, EventArgs e)
        {
            Label l = sender as Label;
            l.BackColor = SystemColors.Control;
        }

        void c_MouseEnter(object sender, EventArgs e)
        {
            Label l = sender as Label;
            l.BackColor = Color.LightGreen;
        }

        void c_Click(object sender, EventArgs e)
        {
            try
            {
                Label l = sender as Label;
                setCursor();
                SendKeys.Send(l.Text);
                if (shift)
                {
                    shift = !shift;
                    toLower();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        void setCursor()
        {
            _textBox.Focus();
        }

        void toUpper()
        {
            foreach (Control c in flowLayoutPanel1.Controls)
            {
                if (c is Button)
                {
                    Button l = c as Button;
                    if (l.Text.Length == 1)
                    {
                        if (char.IsLetter(l.Text[0]))
                        {
                            l.Text = l.Text.ToUpper();
                        }
                    }
                }
            }
        }


        void toLower()
        {
            foreach (Control c in flowLayoutPanel1.Controls)
            {
                if (c is Button)
                {
                    Button l = c as Button;
                    if (l.Text.Length == 1)
                    {
                        if (char.IsLetter(l.Text[0]))
                        {
                            l.Text = l.Text.ToLower();
                        }
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            setCursor();
            SendKeys.Send("{ESC}");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            setCursor();
            SendKeys.Send("{TAB}");
        }

        private void button24_Click(object sender, EventArgs e)
        {
            setCursor();
            SendKeys.Send("{BACKSPACE}");
        }

        private void button26_Click(object sender, EventArgs e)
        {
            setCursor();
            SendKeys.Send("{DELETE}");

        }

        private void button31_Click(object sender, EventArgs e)
        {
            setCursor();
            SendKeys.Send("{ENTER}");
        }

        private void button43_Click(object sender, EventArgs e)
        {
            try
            {
                setCursor();
                SendKeys.Send("{UP}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button42_Click(object sender, EventArgs e)
        {
            try
            {
                setCursor();
                SendKeys.Send("{DOWN}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button54_Click(object sender, EventArgs e)
        {
            try
            {
                setCursor();
                SendKeys.Send("{RIGHT}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button52_Click(object sender, EventArgs e)
        {
            try
            {
                setCursor();
                SendKeys.Send("{LEFT}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void KeyBoardControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                //label50.Text = this.PointToScreen(e.Location).ToString();
                OnDrag(e);
            }
        }

        private void button27_Click(object sender, EventArgs e)
        {
            capLock = !capLock;
            if (capLock)
            {
                button27.ForeColor = Color.Green;
                toUpper();
            }
            else
            {
                button27.ForeColor = Color.Black;
                toLower();
            }
        }

        private void button51_Click(object sender, EventArgs e)
        {
            try
            {
                setCursor();
                SendKeys.Send(" ");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button30_Click(object sender, EventArgs e)
        {
            shift = !shift;
            if (shift)
            {
                button30.ForeColor = Color.Green;
                toUpper();
            }
            else
            {
                button30.ForeColor = Color.Black;
                toLower();
            }
        }

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool PostMessage(HandleRef hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        const uint WM_KEYDOWN = 0x0100;
        const uint WM_KEYUP = 0x0101;
        const uint WM_CHAR = 0x0102;

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern int GetLastError();

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern int FormatMessage(int dwFlags,
            string lpSource,
            int dwMessageId,
            int dwLanguageId,
            StringBuilder lpBuffer,
            int nSize,
            string[] Arguments);

        public static string GetLastErrorMessage()
        {
            StringBuilder strLastErrorMessage = new StringBuilder(255);
            int ret2 = GetLastError();
            int dwFlags = 4096;

            int ret3 = FormatMessage(dwFlags,
                null,
                ret2,
                0,
                strLastErrorMessage,
                strLastErrorMessage.Capacity,
                null);

            return strLastErrorMessage.ToString();
        }


        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);
        private const uint KEYEVENTF_KEYUP = 2;
        private const byte vbKeyControl = 17;
        private const byte vbKeyEscape = 27;
        private const byte vbKeyLWin = 91;
        private void button16_Click(object sender, EventArgs e)
        {

            //bool b = PostMessage(new HandleRef(this, this._textBox.Handle), WM_KEYDOWN, new IntPtr((int)Keys.RWin), new IntPtr(1));
            //MessageBox.Show(GetLastErrorMessage());

            //keybd_event(vbKeyControl, 0, 0, 0);
            //keybd_event(vbKeyEscape, 0, 0, 0);
            //keybd_event(vbKeyControl, 0, KEYEVENTF_KEYUP, 0);
            //keybd_event(vbKeyEscape, 0, KEYEVENTF_KEYUP, 0);
            keybd_event(vbKeyLWin, 0, 0, 0);
            keybd_event(vbKeyLWin, 0, KEYEVENTF_KEYUP, 0);

        }

        private void button40_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn.ForeColor == SystemColors.ControlText)
            {
                btn.ForeColor = Color.Green;
                ctrl = true;
            }
            else
            {
                btn.ForeColor = SystemColors.ControlText;
                ctrl = false;
            }
        }

        private void button53_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn.ForeColor == SystemColors.ControlText)
            {
                btn.ForeColor = Color.Green;
                alt = true;
            }
            else
            {
                btn.ForeColor = SystemColors.ControlText;
                alt = false;
            }
        }

        private void button55_Click(object sender, EventArgs e)
        {
            if (this.Parent is ToolStripDropDown)
            {
                ToolStripDropDown t = this.Parent as ToolStripDropDown;
                t.Close();
            }
        }

    }
}
