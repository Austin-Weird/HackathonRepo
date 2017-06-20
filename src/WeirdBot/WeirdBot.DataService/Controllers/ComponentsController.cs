using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;
using WeirdBot.Models;

namespace WeirdBot.DataAccess.Controllers
{
    public class ComponentsController : ApiController
    {
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

        // GET api/components
        [SwaggerOperation("GetAll")]
        public IEnumerable<Component> Get()
        {
            return fakeComponents;
        }

        // GET api/category/3/components
        [Route("api/category/{type}/components")]
        public IEnumerable<Component> GetByCategory(ComponentType type)
        {
            return fakeComponents.Where(p => p.Category == type);
        }

    }
}
