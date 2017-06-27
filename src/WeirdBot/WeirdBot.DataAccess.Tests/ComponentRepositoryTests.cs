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

namespace WeirdBot.DataAccess.Tests
{
    [TestClass]
    public class ComponentRepositoryTests
    {
        CloudTableClient tableClient;
        CloudTable table;

        [TestInitialize]
        public void SetUp()
        {
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
            tableClient = null;
            table.DeleteIfExists();
            table = null;
        }

        [TestMethod]
        public void Integration_GetAllProducts_ShouldReturnListOfProductObjects()
        {
            var sut = new ComponentRepository(tableClient);
            var results = sut.GetAllProducts();

            Assert.IsNotNull(results);
            Assert.AreEqual(fakeComponents.Count(), results.Count());
            Assert.IsInstanceOfType(results, typeof(List<Component>));
        }


        IEnumerable<Component> fakeComponents = new Component[] {
                new Component {
                    ID = 1,
                    Category = ComponentType.HardDrive,
                    Name = "Decent 1TB Hard Drive",
                    Description ="1TB worth of storage with decent performance",
                    Price = 40.00M,
                    VendorUrl= "myvendor.com/products/X123-33.html"
                },
                new Component {
                    ID = 2,
                    Category = ComponentType.HardDrive,
                    Name = "4TB Hard Drive",
                    Description = "4TB storage",
                    Price = 75.00M,
                    VendorUrl = "myvendor.com/products/X123-133X.html"
                },
                new Component {
                    ID = 3,
                    Category = ComponentType.HardDrive,
                    Name = "250GB SSD Drive",
                    Description = "Faster tech, smaller drive",
                    Price = 100.00M,
                    VendorUrl = "myvendor.com/products/X223-S134X.html"
                },
                new Component {
                    ID = 1,
                    Category = ComponentType.Processor,
                    Name = "AMD Decent Processor",
                    Description = "Not bad for the buck, but not going to run your heavy processing",
                    Price = 211.00M,
                    VendorUrl = "myvendor.com/products/P1393-CCX.html"
                },
                new Component {
                    ID = 2,
                    Category = ComponentType.Processor,
                    Name = "Intel Hot Processor",
                    Description = "Drooool...",
                    Price = 309.00M,
                    VendorUrl = "myvendor.com/products/P8873-JSH.html"
                },
                new Component {
                    ID = 1,
                    Category = ComponentType.VideoCard,
                    Name = "AMD Good Enough Card",
                    Description = "Meh",
                    Price = 100.00M,
                    VendorUrl = "myvendor.com/products/GP99923-FF-234"
                },
                new Component {
                    ID = 2,
                    Category = ComponentType.VideoCard,
                    Name = "NVidia Powerhouse",
                    Description = "Yeah, baby",
                    Price = 338.00M,
                    VendorUrl = "myvendor.com/products/GP99923-FF-234"
                }
            };
    }
}
