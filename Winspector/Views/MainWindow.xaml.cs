using System;
using System.Windows;
using Winspector.ViewModels;

namespace Winspector.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MouseHook.Start();
            MouseHook.MouseAction += OnMouseMoved;

            this.DataContext = new MainViewModel();
        }

        private async void OnMouseMoved(object sender, EventArgs e)
        {
            var viewModel = this.DataContext as MainViewModel;
            if (viewModel != null)
            {
                await viewModel.PopulateDataGridAsync();
            }
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            var viewModel = this.DataContext as MainViewModel;
            if (viewModel != null)
            {
                viewModel.CancelTasks();
            }

            MouseHook.Stop();

            base.OnClosing(e);
        }
    }
}