using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using migh.api;

namespace migh.player
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        public string Username = "";
        private void frmLogin_Load(object sender, EventArgs e)
        {

        }
        public Library lib = new Library();
        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            validate();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            validate();
        }
        private void validate()
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            if (User.name_exists(lib.user_list, username))
            {
                foreach (User user in lib.user_list)
                {
                    string realpass = Tools.DecodeStringFromBase64(user.password);
                    if (user.name.ToLower() == username && realpass == password)
                    {
                        Username = username;
                        this.DialogResult = DialogResult.OK;
                    }
                }
            }
        }
        private void Exitbtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
