using System;
using WeirdBot.DataAccess;
using WeirdBot.Models;

namespace WeirdBot.Utilities
{
    public class NullRecommendationEngine : ComponentRecommendationEngineBase, IComponentRecommendationEngine
    {
        public NullRecommendationEngine(IComponentRepository db) : base(db) { }

        public override Component GetRecommendedComponent(Usage[] usage, decimal highPrice)
        {
            return null;
        }
    }
}