using System;
using WeirdBot.DataAccess;
using WeirdBot.Models;

namespace WeirdBot.Utilities
{
    public class VideoCardRecommendationEngine : ComponentRecommendationEngineBase, IComponentRecommendationEngine
    {
        public VideoCardRecommendationEngine(IComponentRepository db) : base(db) { }

        public override Component GetRecommendedComponent(Usage[] usage, decimal priceCap)
        {
            return GetRecommendedComponentOfType(ComponentType.VideoCard, usage, priceCap);
        }
    }
}