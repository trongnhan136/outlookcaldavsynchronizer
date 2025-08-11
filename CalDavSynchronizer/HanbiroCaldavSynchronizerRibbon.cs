using CalDavSynchronizer.Properties;
using log4net;
using Microsoft.Office.Tools.Ribbon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalDavSynchronizer
{
    public partial class HanbiroCaldavSynchronizerRibbon
    {

        private static readonly ILog s_logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private void HanbiroCaldavSynchronizerRibbon_Load(object sender, RibbonUIEventArgs e)
        {
            ThisAddIn.SynchronizationFailedWhileReportsFormWasNotVisible += SynchronizationFailedWhileReportsFormWasNotVisible;
            ThisAddIn.StatusChanged += ThisAddIn_StatusChanged;
        }

        private void ThisAddIn_StatusChanged(object sender, Scheduling.SchedulerStatusEventArgs e)
        {
            //SynchronizeNowButton.Enabled = !e.IsRunning;
        }

        private void SynchronizationFailedWhileReportsFormWasNotVisible(object sender, EventArgs e)
        {
            //ReportsButton.Image = Resources.SyncError;
        }
    }
}
