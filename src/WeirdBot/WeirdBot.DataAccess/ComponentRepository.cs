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

        public Component GetComponentByPriceAndQuality(ComponentType componentType, Quality quality, decimal priceTarget)
        {
            var partitionKeyFilter = TableQuery.GenerateFilterCondition("PartitionKey",
                        QueryComparisons.Equal,
                        ComponentTypeHelpers.LookUpComponentTypeString(componentType));
            var query = new TableQuery<ComponentEntity>().Where(partitionKeyFilter);
            var components = componentTable.ExecuteQuery(query)
                .Select(x => x.ToComponent())
                .OrderByDescending(c => c.Quality)
                .ThenByDescending(c => c.Price);

            return SelectFirstMatchingComponent(quality, priceTarget, components);
        }

        private static Component SelectFirstMatchingComponent(Quality quality, decimal priceTarget, IEnumerable<Component> components)
        {
            return components
                .Where(c => c.Quality <= quality)
                .FirstOrDefault(c => c.Price <= priceTarget);
        }

        public List<Component> GetAllComponents()
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