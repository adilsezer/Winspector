using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Automation;
using Winspector.Models;

namespace Winspector
{
    public class DataGridItem
    {
        private string elementName;
        private string elementValue;

        public string ElementName
        {
            get { return elementName; }
            set
            {
                if (elementName != value)
                {
                    elementName = value;
                    NotifyPropertyChanged("ElementName");
                }
            }
        }

        public string ElementValue
        {
            get { return elementValue; }
            set
            {
                if (elementValue != value)
                {
                    elementValue = value;
                    NotifyPropertyChanged("ElementValue");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public static ObservableCollection<DataGridItem> PopulateDataGridItem()
        {
            ObservableCollection<DataGridItem> dataGridCollection = new ObservableCollection<DataGridItem>();
            Tuple<int, int> mousePosition = MousePosition.GetMousePosition();
            AutomationElement element = UIAElement.GetElementFromPoint(mousePosition.Item1, mousePosition.Item2);
            UIAElement uiElement = new UIAElement(element);

            foreach (var prop in uiElement.GetType().GetProperties())
            {
                dataGridCollection.Add(new DataGridItem { ElementName = prop.Name, ElementValue = prop.GetValue(uiElement, null)?.ToString() });
            }
            return dataGridCollection;
        }
    }
}