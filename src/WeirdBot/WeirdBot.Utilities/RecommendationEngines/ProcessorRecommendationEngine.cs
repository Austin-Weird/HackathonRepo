using System;
using WeirdBot.DataAccess;
using WeirdBot.Models;

namespace WeirdBot.Utilities
{
    public class ProcessorRecommendationEngine : ComponentRecommendationEngineBase, IComponentRecommendationEngine
    {
        public ProcessorRecommendationEngine(IComponentRepository db) : base(db) { }

        public override Component GetRecommendedComponent(Usage[] usage, decimal priceCap)
        {
            return GetRecommendedComponentOfType(ComponentType.Processor, usage, priceCap);
        }
    }
}