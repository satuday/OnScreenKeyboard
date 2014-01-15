using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace OnScreenKeyboardControl
{
    public partial class KeyBoardControl : UserControl
    {
        Control _textBox;
        bool capLock = false;
        bool shift = false;

        public KeyBoardControl(Control textBox)
        {
            InitializeComponent();
            _textBox = textBox;

            foreach (Control c in tableLayoutPanel1.Controls)
            {
                if (c is Label)
                {               
                    if(c.Text.Length == 1 && char.GetUnicodeCategory(c.Text[0]) != System.Globalization.UnicodeCategory.OtherSymbol)
                    c.Click += new EventHandler(c_Click);
                }
            }

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
                    label45.ForeColor = Color.Black;
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
            foreach (Control c in tableLayoutPanel1.Controls)
            {
                if (c is Label)
                {
                    Label l = c as Label;
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
            foreach (Control c in tableLayoutPanel1.Controls)
            {
                if (c is Label)
                {
                    Label l = c as Label;
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


        private void label34_Click(object sender, EventArgs e)
        {
            capLock = !capLock;
            if (capLock)
            {
                label34.ForeColor = Color.Green;
                toUpper();
            }
            else
            {
                label34.ForeColor = Color.Black;
                toLower();
            }
            

        }

        private void label47_Click(object sender, EventArgs e)
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

        private void label11_Click(object sender, EventArgs e)
        {
            try
            {
                setCursor();
                SendKeys.Send("{BACKSPACE}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label46_Click(object sender, EventArgs e)
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

        private void label45_Click(object sender, EventArgs e)
        {
            shift = !shift;
            if (shift)
            {
                label45.ForeColor = Color.Green;
                toUpper();
            }
            else
            {
                label45.ForeColor = Color.Black;
                toLower();
            }


        }

        private void label44_Click(object sender, EventArgs e)
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

        private void label22_Click(object sender, EventArgs e)
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

        private void label48_Click(object sender, EventArgs e)
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

        private void label32_Click(object sender, EventArgs e)
        {
            try
            {
                setCursor();
                SendKeys.Send("{ENTER}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label49_Click(object sender, EventArgs e)
        {

        }

    }
}
