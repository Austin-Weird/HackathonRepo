using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;
using WeirdBot.Models;

namespace WeirdBot.DataAccess.DataObjects
{
    public class ProductEntity : TableEntity
    {
        public ProductEntity()
        {

        }

        public ProductEntity(int id, Category cat)
        {
            PartitionKey = LookUpCategoryString(cat);
            RowKey = id.ToString();
        }

        private static string LookUpCategoryString(Category cat)
        {
            switch (cat)
            {
                case Category.Media:
                    return "media";
                case Category.Business:
                    return "business";
                case Category.Developer:
                    return "developer";
                case Category.Gaming:
                    return "gaming";
                case Category.General:
                default:
                    return "general";
            }
        }

        internal static Category LookUpCategoryValue(string partitionKey)
        {
            switch (partitionKey)
            {
                case "media":
                    return Category.Media;
                case "business":
                    return Category.Business;
                case "developer":
                    return Category.Developer;
                case "gaming":
                    return Category.Gaming;
                case "general":
                    return Category.General;
                default:
                    var errorMessage = string.Format("Invalid category type. Could not convert value: {0}", partitionKey);
                    throw new ArgumentException(errorMessage);
            }
        }
    }

    public static class ProductEntityExtensions
    {
        public static Product ToProduct(this ProductEntity entity)
        {
            return new Product
            {
                ID = int.Parse(entity.RowKey),
                Category = ProductEntity.LookUpCategoryValue(entity.PartitionKey)
            };
        }
    }
}
