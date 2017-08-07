using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace migh.player
{
    public partial class frmDownload : Form
    {
        public frmDownload()
        {
            InitializeComponent();
        }
        int TogMove;
        int MValX;
        int MValY;

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

        private void Exitbtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void listDownload_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void frmDownload_Load(object sender, EventArgs e)
        {
            listDownload.BeginUpdate();
            listDownload.DataSource = frmPlayer.DownloadList;
            listDownload.EndUpdate();
        }

        private void UpdateList(object sender, EventArgs e)
        {
            listDownload.BeginUpdate();
            listDownload.DataSource = null;
            listDownload.DataSource = frmPlayer.DownloadList;
            listDownload.EndUpdate();
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

        private void btnCancelAll_Click(object sender, EventArgs e)
        {
            frmPlayer.CancellAllDownloads();
        }

        private void btnCancelCurrent_Click(object sender, EventArgs e)
        {
            frmPlayer.CancelCurrentDownload();
        }

        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            if(!Directory.Exists(Application.StartupPath + "\\Descargas\\"))
            {
                Directory.CreateDirectory(Application.StartupPath + "\\Descargas\\");
            }
            Process.Start(@Application.StartupPath + "\\Descargas\\");
        }

        private void btnUpdateList_Click(object sender, EventArgs e)
        {
            listDownload.BeginUpdate();
            listDownload.DataSource = null;
            listDownload.DataSource = frmPlayer.DownloadList;
            listDownload.EndUpdate();
        }
    }
}
