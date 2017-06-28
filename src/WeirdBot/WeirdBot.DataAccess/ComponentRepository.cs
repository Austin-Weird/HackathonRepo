using System;
using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage.Table;
using WeirdBot.DataAccess.DataObjects;
using WeirdBot.Models;
using System.Linq;

namespace WeirdBot.DataAccess
{
    public class ComponentRepository : IComponentRepository
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

        public Component GetComponentByPriceAndPowerRank(ComponentType componentType, Quality powerRank, decimal priceTarget)
        {
            var partitionKeyFilter = TableQuery.GenerateFilterCondition("PartitionKey",
                        QueryComparisons.Equal,
                        ComponentTypeHelpers.LookUpCategoryString(componentType));
            var query = new TableQuery<ComponentEntity>().Where(partitionKeyFilter);
            var components = componentTable.ExecuteQuery(query).Select(x => x.ToComponent());
            return components.FirstOrDefault(c => c.Price <= priceTarget && c.Quality >= powerRank);
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