using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Winspector.ViewModels;

namespace Winspector.Tests.ViewModels
{
    [TestClass]
    public class RelayCommandTests
    {
        [TestMethod]
        public void RelayCommand_Execute_CallsAction()
        {
            // Arrange
            bool wasExecuted = false;
            Action execute = () => wasExecuted = true;
            var command = new RelayCommand(execute);

            // Act
            command.Execute(null);

            // Assert
            Assert.IsTrue(wasExecuted, "Execute action was not called.");
        }

        [TestMethod]
        public void RelayCommand_CanExecute_ReturnsTrue_WhenNoCanExecuteProvided()
        {
            // Arrange
            var command = new RelayCommand(() => { });

            // Act
            bool canExecute = command.CanExecute(null);

            // Assert
            Assert.IsTrue(canExecute, "CanExecute should return true when no CanExecute delegate is provided.");
        }

        [TestMethod]
        public void RelayCommand_CanExecute_ReturnsFalse_WhenCanExecuteIsFalse()
        {
            // Arrange
            var command = new RelayCommand(() => { }, () => false);

            // Act
            bool canExecute = command.CanExecute(null);

            // Assert
            Assert.IsFalse(canExecute, "CanExecute should return false as per the provided delegate.");
        }

        [TestMethod]
        public void RelayCommand_CanExecute_ReturnsTrue_WhenCanExecuteIsTrue()
        {
            // Arrange
            var command = new RelayCommand(() => { }, () => true);

            // Act
            bool canExecute = command.CanExecute(null);

            // Assert
            Assert.IsTrue(canExecute, "CanExecute should return true as per the provided delegate.");
        }
    }
}
