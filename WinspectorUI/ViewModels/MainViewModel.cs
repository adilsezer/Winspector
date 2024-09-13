using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Winspector.Models;

namespace Winspector.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        // ObservableCollection for binding to the DataGrid
        public ObservableCollection<DataGridItem> DataGridItems { get; private set; }

        private readonly CancellationTokenSource _cancellationTokenSource;

        public MainViewModel()
        {
            DataGridItems = new ObservableCollection<DataGridItem>(); // Initialize the collection
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public async Task PopulateDataGridAsync()
        {
            var token = _cancellationTokenSource.Token;

            await Task.Run(() =>
            {
                try
                {
                    // Fetch mouse position and element
                    var mousePosition = MousePosition.GetMousePosition();
                    var element = UIAElement.GetElementFromPoint(mousePosition.Item1, mousePosition.Item2);

                    if (element == null || token.IsCancellationRequested)
                    {
                        return; // Exit if no element or the task is cancelled
                    }

                    var uiElement = new UIAElement(element);

                    // Use the dispatcher to update UI
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        if (!token.IsCancellationRequested) // Check again before updating UI
                        {
                            DataGridItems.Clear();
                            foreach (var property in uiElement.Properties)
                            {
                                if (property.Key != null && property.Value != null)
                                {
                                    DataGridItems.Add(new DataGridItem
                                    {
                                        ElementName = property.Key,
                                        ElementValue = property.Value
                                    });
                                }
                            }
                        }
                    });
                }
                catch (OperationCanceledException)
                {
                    // Task was canceled, handle gracefully if needed
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }, token);
        }

        public void CancelTasks()
        {
            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource.Dispose();
            }
        }
    }
}
