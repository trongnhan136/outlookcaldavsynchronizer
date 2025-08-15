
using log4net;
using Microsoft.Office.Tools.Ribbon;
using System;
using CalDavSynchronizer.Utilities;

namespace CalDavSynchronizer
{
    public partial class HanbiroCaldavSynchronizerRibbon
    {

        private static readonly ILog s_logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private void HanbiroCaldavSynchronizerRibbon_Load(object sender, RibbonUIEventArgs e)
        {
            ThisAddIn.SynchronizationFailedWhileReportsFormWasNotVisible += SynchronizationFailedWhileReportsFormWasNotVisible;
            ThisAddIn.StatusChanged += ThisAddIn_StatusChanged;
            ThisAddIn.SyncProfileChanged += ThisAddIn_SyncProfileChanged;

            updateControl();
        }

        private void updateControl()
        {
            ComponentContainer.EnsureSynchronizationContext();
            var hasAccount = ThisAddIn.ComponentContainer.hasCaldavAccount();
            this.btLogin.Visible = !hasAccount;
            this.btLogout.Visible = hasAccount;
            this.btConfig.Visible = hasAccount;
        }

        private void ThisAddIn_SyncProfileChanged(object sender, EventArgs e)
        {
            updateControl();
        }

        private void ThisAddIn_StatusChanged(object sender, Scheduling.SchedulerStatusEventArgs e)
        {
            //SynchronizeNowButton.Enabled = !e.IsRunning;
        }

        private void SynchronizationFailedWhileReportsFormWasNotVisible(object sender, EventArgs e)
        {
            //ReportsButton.Image = Resources.SyncError;
        }

        private async void btLogin_Click(object sender, RibbonControlEventArgs e)
        {

            try 
            {
                ComponentContainer.EnsureSynchronizationContext();
                await ThisAddIn.ComponentContainer.ShowHanbiroLogin();
            }
            catch (Exception x)
            {
                ExceptionHandler.Instance.DisplayException(x, s_logger);
            }
        }

        private async void btConfig_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                ComponentContainer.EnsureSynchronizationContext();
                await ThisAddIn.ComponentContainer.ShowHanbiroLogin();
            }
            catch (Exception x)
            {
                ExceptionHandler.Instance.DisplayException(x, s_logger);
            }
        }

        private void btLogout_Click(object sender, RibbonControlEventArgs e)
        {

        }
    }
}
