using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;

namespace FP
{
    public class OnScreenKeyboard
    {
        PopupWindow osk;
        Control _textBox;
        Point mouseClickLocation;
        KeyBoardControl keyboard;

        /// <summary>
        /// Pass in a TextBox control.
        /// </summary>
        /// <param name="sender">TextBox</param>
        public OnScreenKeyboard(Control textBox)
        {
            _textBox = textBox;
            keyboard = new KeyBoardControl(_textBox);
            osk = new PopupWindow(keyboard);
            osk.Leave += new EventHandler(osk_Leave);
            osk.BackColor = SystemColors.ActiveCaption;
            keyboard.DragKeyBoard += new OnDrag(keyboard_DragKeyBoard);
            keyboard.MouseDown += new MouseEventHandler(keyboard_MouseClick);
            _textBox.Leave += new EventHandler(_textBox_Leave);
        }

        void keyboard_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseClickLocation = e.Location;
            }
        }


        void keyboard_DragKeyBoard(object sender, MouseEventArgs e)
        {
            //Point oskLocation = new Point(osk.Left, osk.Top);
            Point mouseLocation = osk.PointToScreen(e.Location);
            Point moveLocation = new Point(mouseLocation.X - mouseClickLocation.X, mouseLocation.Y - mouseClickLocation.Y);
            osk.Show(moveLocation);
           
        }

        void osk_Leave(object sender, EventArgs e)
        {
            osk.Close();
        }

        void _textBox_Leave(object sender, EventArgs e)
        {
            osk.Close();
        }

        public void Show()
        {
            osk.Show(_textBox.Parent, new Point(_textBox.Location.X, _textBox.Location.Y+_textBox.Height));

        }
    }

    /// <summary>
    /// A simple popup window that can host any System.Windows.Forms.Control
    /// </summary>
    internal class PopupWindow : System.Windows.Forms.ToolStripDropDown
    {
        private System.Windows.Forms.Control _content;
        private System.Windows.Forms.ToolStripControlHost _host;

        public PopupWindow(System.Windows.Forms.Control content)
        {
            //Basic setup...
            this.SetStyle(ControlStyles.Selectable, false);
            this.AutoSize = false;
            this.DoubleBuffered = true;
            this.ResizeRedraw = true;
            this.AutoClose = false; /////send key will not work when set to true. 

            this._content = content;
            this._host = new System.Windows.Forms.ToolStripControlHost(content);

            //Positioning and Sizing
            this.MinimumSize = content.MinimumSize;
            this.MaximumSize = content.Size;
            this.Size = content.Size;
            content.Location = Point.Empty;

            //Add the host to the list
            this.Items.Add(this._host);

        }

    }


}
