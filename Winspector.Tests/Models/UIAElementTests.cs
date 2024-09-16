using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows.Automation;
using Winspector.Models;

namespace Winspector.Tests.Models
{
    [TestClass]
    public class UIAElementTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void UIAElement_Constructor_NullAutomationElement_ThrowsException()
        {
            // Act
            var uiElement = new UIAElement(null);
        }

        [TestMethod]
        public void UIAElement_Constructor_ValidElement_SetsProperties()
        {
            // Arrange
            var rootElement = AutomationElement.RootElement;

            // Act
            var uiElement = new UIAElement(rootElement);

            Assert.IsNotNull(uiElement.Name, "Name property should be set.");
            Assert.IsNotNull(uiElement.ClassName, "ClassName property should be set.");
            Assert.IsNotNull(uiElement.AutomationId, "AutomationId property should be set.");
            Assert.IsNotNull(uiElement.ControlType, "ControlType property should be set.");
        }

        [TestMethod]
        public void UIAElement_SafeGetProperty_ReturnsDefaultValueOnException()
        {
            // Arrange
            Func<string> getProperty = () => throw new InvalidOperationException();
            var defaultValue = "N/A";

            // Act
            var result = UIAElement.SafeGetProperty(getProperty, defaultValue);

            // Assert
            Assert.AreEqual(defaultValue, result, "SafeGetProperty should return the default value when an exception is thrown.");
        }

        [TestMethod]
        public void UIAElement_SafeGetProperty_ReturnsActualValueIfNoException()
        {
            // Arrange
            Func<string> getProperty = () => "TestValue";
            var defaultValue = "N/A";

            // Act
            var result = UIAElement.SafeGetProperty(getProperty, defaultValue);

            // Assert
            Assert.AreEqual("TestValue", result, "SafeGetProperty should return the actual value if no exception is thrown.");
        }
    }
}
