using System;
using System.Collections.Generic;
using System.Windows.Automation;

namespace Winspector.Models
{
    public class UIAElement
    {
        public string Name { get; private set; }
        public string ClassName { get; private set; }
        public string AutomationId { get; private set; }
        public string ProcessId { get; private set; }
        public string ControlType { get; private set; }
        public Dictionary<string, string> Properties { get; private set; }

        public UIAElement(AutomationElement element)
        {
            if (element == null) return;

            Properties = new Dictionary<string, string>();

            try
            {
                // Basic information
                Name = element.Current.Name;
                ClassName = element.Current.ClassName;
                AutomationId = element.Current.AutomationId;
                ProcessId = element.Current.ProcessId.ToString();
                ControlType = element.Current.ControlType.ProgrammaticName;

                // Add the basic properties to the dictionary
                Properties["Name"] = Name;
                Properties["ClassName"] = ClassName;
                Properties["AutomationId"] = AutomationId;
                Properties["ProcessId"] = ProcessId;
                Properties["ControlType"] = ControlType;

                // Fetch and store all supported properties
                var supportedProperties = element.GetSupportedProperties();
                foreach (var property in supportedProperties)
                {
                    object propertyValue = element.GetCurrentPropertyValue(property);
                    Properties[property.ProgrammaticName] = propertyValue?.ToString() ?? "N/A";
                }
            }
            catch (ElementNotAvailableException)
            {
                // Handle the exception if the element is not available anymore
                Properties["Error"] = "Element not available";
            }
            catch (Exception ex)
            {
                // General exception handling
                Properties["Error"] = $"Exception: {ex.Message}";
            }
        }

        public static AutomationElement GetElementFromPoint(int x, int y)
        {
            try
            {
                var point = new System.Windows.Point(x, y);
                return AutomationElement.FromPoint(point);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }
    }
}
