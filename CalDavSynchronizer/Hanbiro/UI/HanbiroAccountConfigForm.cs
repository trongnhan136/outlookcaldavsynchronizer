using CalDavSynchronizer.Ui;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CalDavSynchronizer.Globalization;
using System;

namespace CalDavSynchronizer.Hanbiro.UI
{
    public partial class HanbiroAccountConfigForm : Form
    {

        private HanbiroLoginViewModel _viewModel;

        public IList<Item<int>> AvailableSyncIntervals =>
            new[] { new Item<int>(0, Strings.Get($"Manual only")) }
                .Union(Enumerable.Range(1, 2).Select(i => new Item<int>(i, i.ToString())))
                .Union(Enumerable.Range(1, 12).Select(i => i * 5).Select(i => new Item<int>(i, i.ToString())))
                .Union(Enumerable.Range(1, 3).Select(i => (int)Math.Pow(2, i) * 60).Select(i => new Item<int>(i, (i / 60).ToString() + Strings.Get($"hours"))))
                .Union(new[] { new Item<int>(720, Strings.Get($"12 hours")), new Item<int>(1440, Strings.Get($"1 day")) }).ToList();
        public HanbiroAccountConfigForm(HanbiroLoginViewModel viewModel)
        {
            InitializeComponent();
            this._viewModel = viewModel;

            var currentProfile = viewModel.CurrentHanProfile;

            cbSyncInterval.DataSource = AvailableSyncIntervals;
            cbSyncInterval.DisplayMember = "Name";
            cbSyncInterval.ValueMember = "Value";
            cbSyncInterval.SelectedValue = currentProfile != null ? currentProfile.Model.SynchronizationIntervalInMinutes : 0;

            tbPastDay.Text = currentProfile.Model.DaysToSynchronizeInThePast.ToString();
            tbFutureDay.Text = currentProfile.Model.DaysToSynchronizeInTheFuture.ToString();

            cbShowReport.Checked = viewModel.ShowReport;
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            var calendarProfile = _viewModel.FindCalendarProfile();
            var cardProfile = _viewModel.FindContactProfile();

            if(calendarProfile != null)
            {
                calendarProfile.Model.SynchronizationIntervalInMinutes = (int)cbSyncInterval.SelectedValue;
                calendarProfile.Model.DaysToSynchronizeInThePast = int.Parse(tbPastDay.Text);
                calendarProfile.Model.DaysToSynchronizeInTheFuture = int.Parse(tbFutureDay.Text);
            }

            if (cardProfile != null)
            {
                cardProfile.Model.SynchronizationIntervalInMinutes = (int)cbSyncInterval.SelectedValue;
                cardProfile.Model.DaysToSynchronizeInThePast = int.Parse(tbPastDay.Text);
                cardProfile.Model.DaysToSynchronizeInTheFuture = int.Parse(tbFutureDay.Text);
            }

            _viewModel.ShowReport = cbShowReport.Checked;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void tbPastDay_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void tbFutureDay_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
