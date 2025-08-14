using System;
using System.ComponentModel;
using System.Windows;
using CalDavSynchronizer.Globalization;
using CalDavSynchronizer.Ui.Options.ViewModels;

namespace CalDavSynchronizer.Hanbiro
{
    /// <summary>
    /// Interaction logic for HanbiroLoginWindow.xaml
    /// </summary>
    public partial class HanbiroLoginWindow : Window
    {
        public HanbiroLoginWindow()
        {
            InitializeComponent();
            DataContextChanged += HanbiroLoginWindow_DataContextChanged;
            Closing += OnWindowClosing;
        }

        private void HanbiroLoginWindow_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is HanbiroLoginViewModel newViewModel)
            {
                newViewModel.CloseRequested += ViewModel_CloseRequested;
            }

            if (e.OldValue is HanbiroLoginViewModel oldViewModel)
            {
                oldViewModel.CloseRequested -= ViewModel_CloseRequested;
            }
        }

        private void ViewModel_CloseRequested(object sender, CloseEventArgs e)
        {
            DialogResult = e.IsAcceptedByUser;
        }

        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            //if (DataContext is HanbiroLoginViewModel viewModel)
            //{
            //    if (!DialogResult.HasValue)
            //    {
            //        var result = MessageBox.Show(Strings.Get($"Do you want to save profiles?"), ComponentContainer.MessageBoxTitle, MessageBoxButton.YesNo);
            //        DialogResult = (result == MessageBoxResult.Yes);
            //    }

            //    if (DialogResult.Value)
            //    {
            //        e.Cancel = !viewModel.Validate();
            //    }
            //}
        }
    }
}
