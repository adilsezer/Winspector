using System.ComponentModel;

namespace Winspector.Models
{
    public class DataGridItem : INotifyPropertyChanged
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
                    NotifyPropertyChanged(nameof(ElementName));
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
                    NotifyPropertyChanged(nameof(ElementValue));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}