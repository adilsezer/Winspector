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

            // Start the mouse hook
            MouseHook.Start();

            // Hook up the mouse action event to the OnMouseMoved method
            MouseHook.MouseAction += OnMouseMoved;

            // Set the DataContext to the MainViewModel for data binding
            this.DataContext = new MainViewModel();
        }

        private async void OnMouseMoved(object sender, EventArgs e)
        {
            // Access the ViewModel and trigger the PopulateDataGridAsync method when the mouse moves
            var viewModel = this.DataContext as MainViewModel;
            if (viewModel != null)
            {
                // Await the async method to ensure proper task handling
                await viewModel.PopulateDataGridAsync();
            }
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            // Cancel background tasks in the ViewModel when the window is closing
            var viewModel = this.DataContext as MainViewModel;
            if (viewModel != null)
            {
                viewModel.CancelTasks();
            }

            // Stop the mouse hook when the window is closing
            MouseHook.Stop();

            base.OnClosing(e);
        }
    }
}
