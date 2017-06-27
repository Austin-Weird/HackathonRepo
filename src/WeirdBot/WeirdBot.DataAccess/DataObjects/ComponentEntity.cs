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
            PartitionKey = LookUpCategoryString(cat);
            RowKey = id.ToString();
        }

        public ComponentEntity(Component component) : this(component.ID, component.Category)
        {
            Component = JsonConvert.SerializeObject(component);
        }

        private static string LookUpCategoryString(ComponentType cat)
        {
            switch (cat)
            {
                case ComponentType.HardDrive:
                    return "harddrive";
                case ComponentType.Processor:
                    return "processor";
                case ComponentType.RAM:
                    return "ram";
                case ComponentType.SoundCard:
                    return "soundcard";
                case ComponentType.VideoCard:
                    return "videocard";
                default:
                    return "unknown";
            }
        }

        internal static ComponentType LookUpCategoryValue(string partitionKey)
        {
            switch (partitionKey)
            {
                case "harddrive":
                    return ComponentType.HardDrive;
                case "processor":
                    return ComponentType.Processor;
                case "ram":
                    return ComponentType.RAM;
                case "soundcard":
                    return ComponentType.SoundCard;
                case "videocard":
                    return ComponentType.VideoCard;
                default:
                    var errorMessage = string.Format("Invalid category type. Could not convert value: {0}", partitionKey);
                    throw new ArgumentException(errorMessage);
            }
        }
    }

    public static class ProductEntityExtensions
    {
        public static Component ToComponent(this ComponentEntity entity)
        {
            return new Component
            {
                ID = int.Parse(entity.RowKey),
                Category = ComponentEntity.LookUpCategoryValue(entity.PartitionKey)
            };
        }
    }
}
