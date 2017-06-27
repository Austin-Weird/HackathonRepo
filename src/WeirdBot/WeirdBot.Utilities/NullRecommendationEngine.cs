using System;
using WeirdBot.DataAccess;
using WeirdBot.Models;

namespace WeirdBot.Utilities
{
    internal class NullRecommendationEngine : IComponentRecommendationEngine
    {
        private ComponentRepository _db;

        public NullRecommendationEngine(ComponentRepository componentDb)
        {
            _db = componentDb;
        }

        public Component GetRecommendedComponent(Usage[] usage, decimal lowPrice, decimal highPrice)
        {
            throw new NotImplementedException();
        }
    }
}