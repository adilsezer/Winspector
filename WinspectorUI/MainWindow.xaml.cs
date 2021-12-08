using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MousePositioners;

namespace WinspectorUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<DataGridItem> DataGridItems = new ObservableCollection<DataGridItem>();

        public MainWindow()
        {
            InitializeComponent();

            MouseHook.Start();
            MouseHook.MouseAction += new EventHandler(Event);

            elementGrid.ItemsSource = DataGridItems;
        }

        private async void Event(object sender, EventArgs e)
        {
            ObservableCollection<DataGridItem> dataGridTaskCollection = await Task.Run(() => { return DataGridItem.PopulateDataGridItem(); });
            DataGridItems.Clear();
            dataGridTaskCollection.ToList().ForEach(item => DataGridItems.Add(item));
        }
    }
}