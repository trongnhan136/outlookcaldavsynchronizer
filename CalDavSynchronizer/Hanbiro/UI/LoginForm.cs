using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace CalDavSynchronizer.Hanbiro.UI
{
    public partial class LoginForm : Form
    {
        private HanbiroLoginViewModel _viewModel;
        public LoginForm(HanbiroLoginViewModel viewModel)
        {
            InitializeComponent();
            this._viewModel = viewModel;
            //txtDomain.Text = "n.hanbiro.com";
            //txtUserID.Text = "nhannt";
            //txtPassword.Text = "H4nbjr0!23456789";
        }

        void updateControl(bool enable)
        {
            btLogin.Enabled = enable;
            txtDomain.Enabled = enable;
            txtUserID.Enabled = enable;
            txtPassword.Enabled = enable;
            if (enable)
            {
                Cursor = Cursors.Default;
              
            }
            else
            {
                Cursor = Cursors.WaitCursor;
            }
        }

        private async void DoLogin()
        {

            String domain = txtDomain.Text;
            String userId = txtUserID.Text;
            String password = txtPassword.Text;
            updateControl(false);
            var result = await _viewModel.DoUpdateOptionWithData(domain, userId, password);
            updateControl(true);
            if (result)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btLogin_Click(object sender, EventArgs e)
        {
            DoLogin();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DoLogin();
                e.SuppressKeyPress= true;
            }
        }
    }
}
