using System;
using WeirdBot.DataAccess;
using WeirdBot.Models;

namespace WeirdBot.Utilities
{
    internal class RamRecommendationEngine : IComponentRecommendationEngine
    {
        private ComponentRepository _db;

        public RamRecommendationEngine(ComponentRepository componentDb)
        {
            _db = componentDb;
        }

        public Component GetRecommendedComponent(Usage[] usage, decimal lowPrice, decimal highPrice)
        {
            throw new NotImplementedException();
        }
    }
}