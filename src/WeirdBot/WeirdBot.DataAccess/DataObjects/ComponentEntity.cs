using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;
using WeirdBot.Models;
using Newtonsoft.Json;

namespace WeirdBot.DataAccess.DataObjects
{
    public class ComponentEntity : TableEntity
    {
        public string Component { get; set; }

        public ComponentEntity() { }

        public ComponentEntity(int id, ComponentType cat)
        {
            PartitionKey = ComponentTypeHelpers.LookUpCategoryString(cat);
            RowKey = id.ToString();
        }

        public ComponentEntity(Component component) : this(component.ID, component.Category)
        {
            Component = JsonConvert.SerializeObject(component);
        }

    }

    public static class ProductEntityExtensions
    {
        public static Component ToComponent(this ComponentEntity entity)
        {
            var component = JsonConvert.DeserializeObject<Component>(entity.Component);
            return new Component
            {
                ID = int.Parse(entity.RowKey),
                Category = ComponentTypeHelpers.LookUpCategoryValue(entity.PartitionKey),
                Name = component.Name,
                Description = component.Description,
                Price = component.Price,
                Quality = component.Quality,
                VendorUrl = component.VendorUrl
            };
        }
    }
}
