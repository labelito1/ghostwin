using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JsonTools;

namespace migh.player
{
    public partial class frmSettings : Form
    {
        public frmSettings()
        {
            InitializeComponent();
        }

        int TogMove;
        int MValX;
        int MValY;

        public int R = 0;
        public int G = 0;
        public int B = 0;

        public Color Color;

        private void btnChooseColor_Click(object sender, EventArgs e)
        {
            ColorDialog.ShowDialog(this);
            Color = ColorDialog.Color;
            TopPanel.BackColor = Color;
            RightPanel.BackColor = Color;
            BottomPanel.BackColor = Color;
            LeftPanel.BackColor = Color;
        }

        private void TopPanel_MouseDown(object sender, MouseEventArgs e)
        {
            this.ActiveControl = null;
            TogMove = 1;
            MValX = e.X;
            MValY = e.Y;
        }

        private void TopPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (TogMove == 1)
            {
                this.SetDesktopLocation(MousePosition.X - MValX, MousePosition.Y - MValY);
            }
        }

        private void TopPanel_MouseUp(object sender, MouseEventArgs e)
        {
            TogMove = 0;
        }

        private void Exitbtn_MouseDown(object sender, MouseEventArgs e)
        {
            Exitbtn.Image = Properties.Resources.close2_mouse_pressed;
        }

        private void Exitbtn_MouseEnter(object sender, EventArgs e)
        {
            Exitbtn.Image = Properties.Resources.close2_mouse;
        }

        private void Exitbtn_MouseLeave(object sender, EventArgs e)
        {
            Exitbtn.Image = Properties.Resources.close2;
        }

        private void Exitbtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
