using System;
using CalDavSynchronizer.Contracts;
using CalDavSynchronizer.Globalization;
using CalDavSynchronizer.Ui;
using CalDavSynchronizer.Utilities;
using log4net;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Outlook;
using Exception = System.Exception;


//HanbiroCalDavSynchronizerToolbar
namespace CalDavSynchronizer
{
    internal class HanbiroCalDavSynchronizerToolbar
    {
        private static readonly ILog s_logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly CommandBarButton _toolBarBtnSyncNow;
        private readonly CommandBarButton _toolBarBtnLogin;
        private readonly CommandBarButton _toolBarBtnConfig;
        private readonly CommandBarButton _toolBarBtnLogout;

    
        // ReSharper restore PrivateFieldCanBeConvertedToLocalVariable

        private CommandBar _toolBar;

        public HanbiroCalDavSynchronizerToolbar(Explorer explorer, object missing, bool wireClickEvents)
        {
            _toolBar = explorer.CommandBars.Add("CalDav Synchronizer", MsoBarPosition.msoBarTop, false, true);

            _toolBarBtnLogin = (CommandBarButton)_toolBar.Controls.Add(1, missing, missing, missing, missing);
            _toolBarBtnLogin.Style = MsoButtonStyle.msoButtonIconAndCaption;
            _toolBarBtnLogin.Caption = Strings.Get($"Login");
            _toolBarBtnLogin.FaceId = 222; // builtin icon: hand hovering above a property list
            _toolBarBtnLogin.Tag = Strings.Get($"Login");
            //_toolBarBtnOptions.Visible = false;
            if (wireClickEvents)
                _toolBarBtnLogin.Click += ToolBarBtn_Login_OnClick;


            _toolBarBtnConfig = (CommandBarButton)_toolBar.Controls.Add(1, missing, missing, missing, missing);
            _toolBarBtnConfig.Style = MsoButtonStyle.msoButtonIconAndCaption;
            _toolBarBtnConfig.Caption = Strings.Get($"Config");
            _toolBarBtnConfig.FaceId = 222; // builtin icon: hand hovering above a property list
            _toolBarBtnConfig.Tag = Strings.Get($"Config");
            if (wireClickEvents)
                _toolBarBtnConfig.Click += ToolBarBtn_Config_OnClick;

            _toolBarBtnSyncNow = (CommandBarButton)_toolBar.Controls.Add(1, missing, missing, missing, missing);
            _toolBarBtnSyncNow.Style = MsoButtonStyle.msoButtonIconAndCaption;
            _toolBarBtnSyncNow.Caption = Strings.Get($"Synchronize now");
            _toolBarBtnSyncNow.FaceId = 222; // builtin icon: lightning hovering above a calendar table
            _toolBarBtnSyncNow.Tag = Strings.Get($"Start a manual synchronization of all active profiles.");
            if (wireClickEvents)
                _toolBarBtnSyncNow.Click += ToolBarBtn_SyncNow_OnClick;

            _toolBarBtnLogout = (CommandBarButton)_toolBar.Controls.Add(1, missing, missing, missing, missing);
            _toolBarBtnLogout.Style = MsoButtonStyle.msoButtonIconAndCaption;
            _toolBarBtnLogout.Caption = Strings.Get($"Logout");
            _toolBarBtnLogout.FaceId = 222; // builtin icon: blue round sign with "i" letter
            _toolBarBtnLogout.Tag = Strings.Get($"Logout");
            if (wireClickEvents)
                _toolBarBtnLogout.Click += ToolBarBtn_Logout_OnClick;


            _toolBar.Visible = true;
        }

        private void ManualSynchronize()
        {
            try
            {
                _toolBarBtnSyncNow.Enabled = false;
                try
                {
                    ComponentContainer.EnsureSynchronizationContext();
                    ThisAddIn.ComponentContainer.SynchronizeNowAsync();
                }
                finally
                {
                    _toolBarBtnSyncNow.Enabled = true;
                }
            }
            catch (Exception x)
            {
                ExceptionHandler.Instance.DisplayException(x, s_logger);
            }
        }

        private void ToolBarBtn_Login_OnClick(CommandBarButton Ctrl, ref bool CancelDefault)
        {
            ToolBarBtn_Login_OnClick();
        }

        private async void ToolBarBtn_Login_OnClick()
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


        private void ToolBarBtn_Config_OnClick(CommandBarButton Ctrl, ref bool CancelDefault)
        {
            ToolBarBtn_Config_OnClick();
        }

        private async void ToolBarBtn_Config_OnClick()
        {
            try
            {
                ComponentContainer.EnsureSynchronizationContext();
                await ThisAddIn.ComponentContainer.ShowHanbiroConfig();
            }
            catch (Exception x)
            {
                ExceptionHandler.Instance.DisplayException(x, s_logger);
            }
        }

        private void ToolBarBtn_Logout_OnClick(CommandBarButton Ctrl, ref bool CancelDefault)
        {
            ToolBarBtn_Logout_OnClick();
        }

        private async void ToolBarBtn_Logout_OnClick()
        {
            try
            {
                ComponentContainer.EnsureSynchronizationContext();
                await ThisAddIn.ComponentContainer.LogoutHanbiroAccount();
            }
            catch (Exception x)
            {
                ExceptionHandler.Instance.DisplayException(x, s_logger);
            }
        }

        private void ToolBarBtn_SyncNow_OnClick(CommandBarButton Ctrl, ref bool CancelDefault)
        {
            ManualSynchronize();
        }

        public void updateControl()
        {
        ComponentContainer.EnsureSynchronizationContext();
            var hasAccount = ThisAddIn.ComponentContainer.hasCaldavAccount();
            this._toolBarBtnLogin.Visible = !hasAccount;
            this._toolBarBtnSyncNow.Visible = hasAccount;
            this._toolBarBtnLogout.Visible = hasAccount;
            this._toolBarBtnConfig.Visible = hasAccount;
        }

        public ToolbarSettings Settings
        {
            get
            {
                return new ToolbarSettings
                {
                    Top = _toolBar.Top,
                    Left = _toolBar.Left,
                    Visible = _toolBar.Visible,
                    Position = _toolBar.Position,
                    RowIndex = _toolBar.RowIndex
                };
            }
            set
            {
                _toolBar.Position = value.Position;

                _toolBar.RowIndex = value.RowIndex;

                _toolBar.Top = value.Top;

                _toolBar.Left = value.Left;

                _toolBar.Visible = value.Visible;
            }
        }
    }
}
