using Microsoft.VisualStudio.TestTools.UnitTesting;
using Winspector.Models;

namespace Winspector.Tests.Models
{
    [TestClass]
    public class DataGridItemTests
    {
        [TestMethod]
        public void ElementName_PropertyChanged_IsTriggered()
        {
            // Arrange
            var item = new DataGridItem();
            bool eventTriggered = false;
            item.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(DataGridItem.ElementName))
                    eventTriggered = true;
            };

            // Act
            item.ElementName = "TestName";

            // Assert
            Assert.IsTrue(eventTriggered, "PropertyChanged event was not triggered for ElementName.");
            Assert.AreEqual("TestName", item.ElementName);
        }

        [TestMethod]
        public void ElementValue_PropertyChanged_IsTriggered()
        {
            // Arrange
            var item = new DataGridItem();
            bool eventTriggered = false;
            item.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(DataGridItem.ElementValue))
                    eventTriggered = true;
            };

            // Act
            item.ElementValue = "TestValue";

            // Assert
            Assert.IsTrue(eventTriggered, "PropertyChanged event was not triggered for ElementValue.");
            Assert.AreEqual("TestValue", item.ElementValue);
        }

        [TestMethod]
        public void ElementName_NoChange_NoPropertyChanged()
        {
            // Arrange
            var item = new DataGridItem { ElementName = "InitialName" };
            bool eventTriggered = false;
            item.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(DataGridItem.ElementName))
                    eventTriggered = true;
            };

            // Act
            item.ElementName = "InitialName"; // Setting to the same value

            // Assert
            Assert.IsFalse(eventTriggered, "PropertyChanged event should not be triggered when setting the same value.");
        }
    }
}
