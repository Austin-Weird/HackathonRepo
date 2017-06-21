using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage.Table;
using WeirdBot.DataAccess.DataObjects;
using WeirdBot.Models;

namespace WeirdBot.DataAccess
{
    public class ProductRepository
    {
        private CloudTableClient dbTableClient;
        private CloudTable productTable;

        public ProductRepository(CloudTableClient source)
        {
            this.dbTableClient = source;
        }

        public List<Component> GetAllProducts()
        {
            var results = new List<Component>();
            var query = new TableQuery<ProductEntity>();

            foreach (var productElement in productTable.ExecuteQuery(query))
            {
                results.Add(productElement.ToProduct());
            }
            return results;
        }
    }
}