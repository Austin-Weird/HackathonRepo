using System;
using WeirdBot.DataAccess;
using WeirdBot.Models;

namespace WeirdBot.Utilities
{
    internal class HardDriveRecommendationEngine : IComponentRecommendationEngine
    {
        ComponentRepository _db;

        public HardDriveRecommendationEngine(ComponentRepository db)
        {
            _db = db;
        }

        public Component GetRecommendedComponent(Usage[] usage, decimal lowPrice, decimal highPrice)
        {
            throw new NotImplementedException();
        }
    }
}