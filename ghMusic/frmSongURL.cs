using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace migh.player
{
    public partial class frmSongURL : Form
    {
        public frmSongURL()
        {
            InitializeComponent();
        }
        public List<string> URLs = new List<string>();
        private void listSongURL_DoubleClick(object sender, EventArgs e)
        {
            if(listSongURL.SelectedItems.Count == 1)
            {
                try
                {
                    Clipboard.SetText(listSongURL.SelectedItem.ToString());
                }
                catch
                {
                    
                }
            }
        }

        private void frmSongURL_Load(object sender, EventArgs e)
        {
            refresh();
        }
        void refresh()
        {
            listSongURL.DataSource = null;
            List<string> l = new List<string>();
            foreach(string s in URLs)
            {
                if(s.ToLower().Contains(txtSearch.Text.ToLower()))
                {
                    l.Add(s);
                }
            }
            lblCount.Text = l.Count.ToString();
            listSongURL.DataSource = l;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            refresh();
        }
    }
}
