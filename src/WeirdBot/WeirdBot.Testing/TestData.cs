using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeirdBot.Models;
using WeirdBot.DataAccess.DataObjects;
using Newtonsoft.Json;

namespace WeirdBot.Testing
{
    public class TestData
    {

        public static IEnumerable<Component> GetFakeComponents()
        {
            return new Component[] {
                new Component {
                    ID = 1,
                    Category = ComponentType.HardDrive,
                    Name = "Decent 1TB Hard Drive",
                    Description ="1TB worth of storage with decent performance",
                    Price = 40.00M,
                    Quality = Quality.Good,
                    VendorUrl= "myvendor.com/products/X123-33.html"
                },
                new Component {
                    ID = 2,
                    Category = ComponentType.HardDrive,
                    Name = "4TB Hard Drive",
                    Description = "4TB storage",
                    Price = 75.00M,
                    Quality = Quality.Better,
                    VendorUrl = "myvendor.com/products/X123-133X.html"
                },
                new Component {
                    ID = 3,
                    Category = ComponentType.HardDrive,
                    Name = "250GB SSD Drive",
                    Description = "Faster tech, smaller drive",
                    Price = 100.00M,
                    Quality = Quality.Best,
                    VendorUrl = "myvendor.com/products/X223-S134X.html"
                },
                new Component {
                    ID = 1,
                    Category = ComponentType.Processor,
                    Name = "AMD Decent Processor",
                    Description = "Not bad for the buck, but not going to run your heavy processing",
                    Price = 211.00M,
                    Quality = Quality.Better,
                    VendorUrl = "myvendor.com/products/P1393-CCX.html"
                },
                new Component {
                    ID = 2,
                    Category = ComponentType.Processor,
                    Name = "Intel Hot Processor",
                    Description = "Drooool...",
                    Price = 309.00M,
                    Quality = Quality.Best,
                    VendorUrl = "myvendor.com/products/P8873-JSH.html"
                },
                new Component {
                    ID = 1,
                    Category = ComponentType.VideoCard,
                    Name = "AMD Good Enough Card",
                    Description = "Meh",
                    Price = 100.00M,
                    Quality = Quality.Good,
                    VendorUrl = "myvendor.com/products/GP99923-FF-234"
                },
                new Component {
                    ID = 2,
                    Category = ComponentType.VideoCard,
                    Name = "NVidia Powerhouse",
                    Description = "Yeah, baby",
                    Price = 338.00M,
                    Quality = Quality.Best,
                    VendorUrl = "myvendor.com/products/GP99923-FF-234"
                }
            };
        }

        public static IEnumerable<ComponentEntity> GetFakeComponentEntities(string partitionKey)
        {
            return GetFakeComponents()
                .Where(c => c.Category == ComponentTypeHelpers.LookUpComponentTypeValue(partitionKey))
                .Select(c => new ComponentEntity(c));
        }
    }
}
