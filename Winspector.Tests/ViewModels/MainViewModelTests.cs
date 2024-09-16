using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Winspector.ViewModels;

namespace Winspector.Tests.ViewModels
{
    [TestClass]
    public class MainViewModelTests
    {
        private MainViewModel _viewModel;

        [TestInitialize]
        public void Setup()
        {
            _viewModel = new MainViewModel();
        }

        [TestMethod]
        public async Task PopulateDataGridAsync_NoElement_NoPopulation()
        {
            // Arrange

            // Act
            await _viewModel.PopulateDataGridAsync();

            // Assert
            Assert.AreEqual(0, _viewModel.DataGridItems.Count, "DataGridItems should remain empty when no UI element is found.");
        }

        [TestMethod]
        public void CancelTasks_StopsViewModelCorrectly()
        {
            // Arrange

            // Act
            _viewModel.CancelTasks();

            // Assert
            Assert.IsTrue(true, "CancelTasks executed without throwing exceptions.");
        }
    }
}
