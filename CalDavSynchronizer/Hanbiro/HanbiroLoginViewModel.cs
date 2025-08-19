using CalDavSynchronizer.Contracts;
using CalDavSynchronizer.Globalization;
using CalDavSynchronizer.Implementation;
using CalDavSynchronizer.Implementation.ComWrappers;
using CalDavSynchronizer.ProfileTypes;
using CalDavSynchronizer.Properties;
using CalDavSynchronizer.Ui;
using CalDavSynchronizer.Ui.Options;
using CalDavSynchronizer.Ui.Options.Models;
using CalDavSynchronizer.Ui.Options.ViewModels;
using CalDavSynchronizer.Utilities;
using Google.GData.Extensions;
using log4net;
using Microsoft.Office.Interop.Outlook;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CalDavSynchronizer.Hanbiro
{
    public class HanbiroLoginViewModel : ModelBase, IOptionsViewModelParent, ISynchronizationProfilesViewModel
    {
        private static readonly ILog s_logger = LogManager.GetLogger(MethodInfo.GetCurrentMethod().DeclaringType);

        private readonly ObservableCollection<IOptionsViewModel> _options = new ObservableCollection<IOptionsViewModel>();
        private readonly IProfileTypeRegistry _profileTypeRegistry;
        private readonly IReadOnlyDictionary<IProfileType, IProfileModelFactory> _profileModelFactoriesByType;
        private readonly bool _expandAllSyncProfiles;
        private readonly IUiService _uiService;
        
        private readonly Func<Guid, string> _profileDataDirectoryFactory;
        private readonly IOptionTasks _optionTasks;

        public event EventHandler RequestBringIntoView;
        public event EventHandler<CloseEventArgs> CloseRequested;


        public HanbiroLoginViewModel(
           bool expandAllSyncProfiles,
           Func<Guid, string> profileDataDirectoryFactory,
           IUiService uiService,
           IOptionTasks optionTasks,
           IProfileTypeRegistry profileTypeRegistry,
           Func<IOptionsViewModelParent, IProfileType, IProfileModelFactory> profileModelFactoryFactory,
           IViewOptions viewOptions)
        {
            _optionTasks = optionTasks;
            ViewOptions = viewOptions;
            _profileDataDirectoryFactory = profileDataDirectoryFactory;
            _uiService = uiService;
            if (profileDataDirectoryFactory == null)
                throw new ArgumentNullException(nameof(profileDataDirectoryFactory));
            if (optionTasks == null) throw new ArgumentNullException(nameof(optionTasks));
            if (viewOptions == null) throw new ArgumentNullException(nameof(viewOptions));

            _expandAllSyncProfiles = expandAllSyncProfiles;

            _profileTypeRegistry = profileTypeRegistry;
            _profileModelFactoriesByType = profileTypeRegistry.AllTypes.ToDictionary(t => t, t => profileModelFactoryFactory(this, t));

            SaveCommand = new DelegateCommand(shouldSaveNewOptions => Close((bool)shouldSaveNewOptions));
        }

        public IViewOptions ViewOptions { get; }

        public Contracts.Options[] GetOptionsCollection()
        {
            return _options.Where(o => !o.IsMultipleOptionsTemplateViewModel).Select(o => o.Model.CreateData()).ToArray();
        }

        public OneTimeChangeCategoryTask[] GetOneTimeTasks()
        {
            var oneTimeTasks = new List<OneTimeChangeCategoryTask>();
            foreach (var options in _options.Where(o => !o.IsMultipleOptionsTemplateViewModel))
                options.Model.AddOneTimeTasks(oneTimeTasks.Add);
            return oneTimeTasks.ToArray();
        }

        public void RequestRemoval(IOptionsViewModel viewModel)
        {

        }

        public void RequestAdd(IReadOnlyCollection<OptionsModel> options)
        {
           
        }

        public void ShowProfile(Guid value)
        {
            
        }

        public void BringToFront()
        {
            RequestBringIntoView?.Invoke(this, EventArgs.Empty);
        }


        public void SetOptionsCollection(Contracts.Options[] value, Guid? initialSelectedProfileId = null)
        {
            _options.Clear();

            foreach (var data in value)
            {
                var profileType = _profileTypeRegistry.DetermineType(data);
                var profileModelFactory = _profileModelFactoriesByType[profileType];
                _options.Add(profileModelFactory.CreateViewModel(profileModelFactory.CreateModelFromData(data)));
            }

           
        }

        private async Task<bool> TestConnectionAsync(OptionsModel model)
        {
            try
            {
                var result = await _optionTasks.TestHanbiroWebDavConnection(model);
                if(result.Type == "error")
                {
                    MessageBox.Show(result.Message, OptionTasks.ConnectionTestCaption);
                    return false;
                }
                return true;
            }
            catch (System.Exception x)
            {
                s_logger.Error("Exception while testing the connection.", x);
                string message = null;
                for (System.Exception ex = x; ex != null; ex = ex.InnerException)
                    message += ex.Message + Environment.NewLine;
                MessageBox.Show(message, OptionTasks.ConnectionTestCaption);
            }
            return false;
        }

        private OutlookFolderDescriptor CreateCalendarFolder(string newCalendarName)
        {
            GenericComObjectWrapper<Folder> defaultCalendarFolder = new GenericComObjectWrapper<Folder>(Globals.ThisAddIn.Application.Session.GetDefaultFolder(OlDefaultFolders.olFolderCalendar) as Folder);
            GenericComObjectWrapper<Folder> newCalendarFolder = null;
            try
            {
                // Use existing folder if it does exist
                newCalendarFolder = new GenericComObjectWrapper<Folder>(defaultCalendarFolder.Inner.Folders[newCalendarName] as Folder);
            }
            catch
            {
                // Create missing folder
                newCalendarFolder = new GenericComObjectWrapper<Folder>(defaultCalendarFolder.Inner.Folders.Add(newCalendarName, OlDefaultFolders.olFolderCalendar) as Folder);
                // Make sure it has not been renamed to "name (this computer only)"
                newCalendarFolder.Inner.Name = newCalendarName;
            }

            // use the selected folder for syncing with kolab
            return new OutlookFolderDescriptor(newCalendarFolder.Inner.EntryID, newCalendarFolder.Inner.StoreID, newCalendarFolder.Inner.DefaultItemType, newCalendarFolder.Inner.Name, 0);
        }


        private OutlookFolderDescriptor CreateContactFolder(string newAddressBookName)
        {
            GenericComObjectWrapper<Folder> defaultAddressBookFolder = new GenericComObjectWrapper<Folder>(Globals.ThisAddIn.Application.Session.GetDefaultFolder(OlDefaultFolders.olFolderContacts) as Folder);
            GenericComObjectWrapper<Folder> newAddressBookFolder = null;
            try
            {
                newAddressBookFolder = new GenericComObjectWrapper<Folder>(defaultAddressBookFolder.Inner.Folders[newAddressBookName] as Folder);
            }
            catch
            {
                newAddressBookFolder = new GenericComObjectWrapper<Folder>(defaultAddressBookFolder.Inner.Folders.Add(newAddressBookName, OlDefaultFolders.olFolderContacts) as Folder);
                newAddressBookFolder.Inner.Name = newAddressBookName;
            }
            return new OutlookFolderDescriptor(newAddressBookFolder.Inner.EntryID, newAddressBookFolder.Inner.StoreID, newAddressBookFolder.Inner.DefaultItemType, newAddressBookFolder.Inner.Name, 0);
        }

        public async Task<bool> DoUpdateOptionWithData(String domain, String userId, String password)
        {
            string folderName = userId + " (" + domain + ")";
            if (_options.Count <= 0)
            {
                var folder = CreateCalendarFolder(folderName);
                var option = new Contracts.Options
                {
                    ConflictResolution = ConflictResolution.Automatic,
                    DaysToSynchronizeInTheFuture = 365,
                    DaysToSynchronizeInThePast = 60,
                    SynchronizationIntervalInMinutes = 0,
                    SynchronizationMode = SynchronizationMode.MergeInBothDirections,
                    Name = domain,
                    Id = Guid.NewGuid(),
                    Inactive = false,
                    PreemptiveAuthentication = true,
                    ForceBasicAuthentication = true,
                    ProxyOptions = new ProxyOptions() { ProxyUseDefault = true },
                    IsChunkedSynchronizationEnabled = true,
                    ChunkSize = 100,
                    ServerAdapterType = ServerAdapterType.WebDavHttpClientBased,
                    ProfileTypeOrNull = "Generic",
                    
                    OutlookFolderEntryId = folder.EntryId,
                    OutlookFolderStoreId = folder.StoreId,
                    EnableChangeTriggeredSynchronization = true,
                };
                option.ExtraData = domain;
                option.CalenderUrl = String.Format("https://{0}:15201/{1}@{2}/calendar/", domain, userId, domain);
                option.UserName = String.Format("{0}@{1}", userId, domain);
                option.Password = SecureStringUtility.ToSecureString(password);
                var profileType = _profileTypeRegistry.DetermineType(option);
                var profileModelFactory = _profileModelFactoriesByType[profileType];
                var model = profileModelFactory.CreateViewModel(profileModelFactory.CreateModelFromData(option));
              
                _options.Add(model);
            }

            var profileModel = _options[0];
            //profileModel.Model.CalenderUrl = "";
            var result = await TestConnectionAsync(profileModel.Model);
            return result;
        }

        public IOptionsViewModel selectedOption
        {
            get
            {
                return _options[0];
            }
        }

        public ICommand SaveCommand { get; }

        private void Close(bool shouldSaveNewOptions)
        {
            CloseRequested?.Invoke(this, new CloseEventArgs(shouldSaveNewOptions));
        }
    }
}
