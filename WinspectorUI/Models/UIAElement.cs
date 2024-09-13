﻿using System;
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
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element), "AutomationElement cannot be null");
            }

            Properties = new Dictionary<string, string>();

            try
            {
                // Safely access each property and handle potential nulls
                Name = SafeGetProperty(() => element.Current.Name, "N/A");
                ClassName = SafeGetProperty(() => element.Current.ClassName, "N/A");
                AutomationId = SafeGetProperty(() => element.Current.AutomationId, "N/A");
                ProcessId = SafeGetProperty(() => element.Current.ProcessId.ToString(), "N/A");
                ControlType = SafeGetProperty(() => element.Current.ControlType.ProgrammaticName, "N/A");

                // Add basic properties to the dictionary
                Properties["Name"] = Name;
                Properties["Class Name"] = ClassName;
                Properties["Automation ID"] = AutomationId;
                Properties["Process ID"] = ProcessId;
                Properties["Control Type"] = ControlType;

                // Fetch and store all supported properties
                var supportedProperties = element.GetSupportedProperties();
                foreach (var property in supportedProperties)
                {
                    object propertyValue = SafeGetProperty(() => element.GetCurrentPropertyValue(property), "N/A");
                    Properties[property.ProgrammaticName] = propertyValue?.ToString() ?? "N/A";
                }
            }
            catch (ElementNotAvailableException)
            {
                Properties["Error"] = "Element not available";
            }
            catch (Exception ex)
            {
                Properties["Error"] = $"Exception: {ex.Message}";
            }
        }

        // Helper method to safely get a property value with fallback
        private static T SafeGetProperty<T>(Func<T> propertyGetter, T defaultValue)
        {
            try
            {
                return propertyGetter();
            }
            catch
            {
                return defaultValue;
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
