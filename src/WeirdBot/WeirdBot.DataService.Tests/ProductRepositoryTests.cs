using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.WindowsAzure.Storage.Table;
using WeirdBot.DataAccess.DataObjects;
using System.Collections.Generic;
using WeirdBot.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.Azure;

namespace WeirdBot.DataAccess.Tests
{
    [TestClass]
    public class ProductRepositoryTests
    {
        CloudTableClient tableClient;

        [TestInitialize]
        public void SetUp()
        {
            var storageAccount = CloudStorageAccount
                .Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            tableClient = storageAccount.CreateCloudTableClient();
        }

        [TestCleanup]
        public void TearDown()
        {
            tableClient = null;
        }

        [TestMethod]
        public void Integration_GetAllProducts_ShouldReturnListOfProductObjects()
        {
            var sut = new ProductRepository(tableClient);
            var results = sut.GetAllProducts();

            Assert.IsNotNull(results);
            Assert.IsInstanceOfType(results, typeof(List<Product>));
        }
    }
}
