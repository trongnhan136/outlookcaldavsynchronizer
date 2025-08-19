using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private async void btLogin_Click(object sender, EventArgs e)
        {
            String domain = txtDomain.Text;
            String userId = txtUserID.Text;
            String password = txtPassword.Text;
            updateControl(false);
             var resutl =  await _viewModel.DoUpdateOptionWithData(domain, userId, password);
            updateControl(true);
            if (resutl)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }

        }
    }
}
