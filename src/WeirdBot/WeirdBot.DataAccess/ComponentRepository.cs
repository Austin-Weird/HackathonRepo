using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage.Table;
using WeirdBot.DataAccess.DataObjects;
using WeirdBot.Models;

namespace WeirdBot.DataAccess
{
    public class ComponentRepository
    {
        private CloudTableClient dbTableClient;
        private CloudTable componentTable;

        public ComponentRepository() { }
        public ComponentRepository(CloudTableClient source)
        {
            this.dbTableClient = source;
            componentTable = dbTableClient.GetTableReference("components");
            componentTable.CreateIfNotExists();
        }

        public List<Component> GetAllProducts()
        {
            var results = new List<Component>();
            var query = new TableQuery<ComponentEntity>();

            foreach (var productElement in componentTable.ExecuteQuery(query))
            {
                results.Add(productElement.ToComponent());
            }
            return results;
        }
    }
}