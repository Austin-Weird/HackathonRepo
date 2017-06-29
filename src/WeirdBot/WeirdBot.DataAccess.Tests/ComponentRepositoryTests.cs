using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.WindowsAzure.Storage.Table;
using WeirdBot.DataAccess.DataObjects;
using System.Collections.Generic;
using WeirdBot.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.Azure;
using System.Linq;
using WeirdBot.Testing;

namespace WeirdBot.DataAccess.Tests
{
    [TestClass]
    public class ComponentRepositoryTests
    {
        /*
         * Note: Integration indicates that the test is running locally against the Azure Storage Emulator.
         */

        CloudTableClient tableClient;
        CloudTable table;
        IEnumerable<Component> fakeComponents;

        [TestInitialize]
        public void SetUp()
        {
            fakeComponents = TestData.GetFakeComponents();
            var storageAccount = CloudStorageAccount
                .Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            tableClient = storageAccount.CreateCloudTableClient();
            table = tableClient.GetTableReference("components");
            if (table.Exists())
                table.DeleteIfExists();  //Previous TearDown() failed.
            table.CreateIfNotExists();
            InitializeComponentsTable();
        }

        private void InitializeComponentsTable()
        {
            foreach (var key in fakeComponents.Select(x => x.Category).Distinct())
            {
                var batchOperation = new TableBatchOperation();
                foreach (var component in fakeComponents.Where(x => x.Category == key))
                {
                    var dataObject = new ComponentEntity(component);
                    batchOperation.Insert(dataObject);
                }

                table.ExecuteBatch(batchOperation);
            }
        }

        [TestCleanup]
        public void TearDown()
        {
            fakeComponents = null;
            tableClient = null;
            table.DeleteIfExists();
            table = null;
        }

        [TestMethod]
        public void Integration_GetAllProducts_ShouldReturnListOfProductObjects()
        {
            var sut = new ComponentRepository(tableClient);
            var results = sut.GetAllComponents();

            Assert.IsNotNull(results);
            Assert.AreEqual(fakeComponents.Count(), results.Count());
            Assert.IsInstanceOfType(results, typeof(List<Component>));
        }

        [TestMethod]
        public void Integration_GetComponentByPriceAndPowerRank_ShouldReturnSingleComponent()
        {
            var usage = new Usage[] { };
            var rank = Quality.Best;
            var componentType = ComponentType.HardDrive;
            var target = 200.00M;

            var sut = new ComponentRepository(tableClient);
            var result = sut.GetComponentByPriceAndQuality(componentType, rank, target);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Component));
            Assert.IsTrue(result.Quality >= rank);
            Assert.IsTrue(result.Price <= target);
        }

    }
}
