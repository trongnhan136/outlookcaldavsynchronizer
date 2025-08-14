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

        private void btLogin_Click(object sender, EventArgs e)
        {
            String domain = txtDomain.Text;
            String userId = txtUserID.Text;
            String password = txtPassword.Text;
            _viewModel.doUpdateOptionWithData(domain, userId, password);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
