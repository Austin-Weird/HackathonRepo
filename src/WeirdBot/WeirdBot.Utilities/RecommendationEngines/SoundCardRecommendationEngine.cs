using System;
using WeirdBot.DataAccess;
using WeirdBot.Models;

namespace WeirdBot.Utilities
{
    public class SoundCardRecommendationEngine : ComponentRecommendationEngineBase, IComponentRecommendationEngine
    {
        public SoundCardRecommendationEngine(IComponentRepository db) : base(db) { }

        public override Component GetRecommendedComponent(Usage[] usage, decimal priceCap)
        {
            return GetRecommendedComponentOfType(ComponentType.SoundCard, usage, priceCap);
        }
    }
}