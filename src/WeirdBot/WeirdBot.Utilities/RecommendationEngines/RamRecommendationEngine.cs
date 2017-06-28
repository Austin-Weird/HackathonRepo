using System;
using WeirdBot.DataAccess;
using WeirdBot.Models;

namespace WeirdBot.Utilities
{
    public class RamRecommendationEngine : ComponentRecommendationEngineBase, IComponentRecommendationEngine
    {
        public RamRecommendationEngine(IComponentRepository db) : base(db) { }

        public override Component GetRecommendedComponent(Usage[] usage, decimal priceCap)
        {
            return GetRecommendedComponentOfType(ComponentType.RAM, usage, priceCap);
        }
    }
}