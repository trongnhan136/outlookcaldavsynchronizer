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

            cbSyncInterval.DataSource = AvailableSyncIntervals;
            cbSyncInterval.DisplayMember = "Name";
            cbSyncInterval.ValueMember = "Value";
            cbSyncInterval.SelectedValue = viewModel.selectedOption.Model.SynchronizationIntervalInMinutes;
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            _viewModel.selectedOption.Model.SynchronizationIntervalInMinutes = (int)cbSyncInterval.SelectedValue;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
