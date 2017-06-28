using System;
using System.Linq;
using WeirdBot.DataAccess;
using WeirdBot.Models;

namespace WeirdBot.Utilities
{
    public class HardDriveRecommendationEngine : ComponentRecommendationEngineBase, IComponentRecommendationEngine
    {
        public HardDriveRecommendationEngine(IComponentRepository db): base(db) { }

        public override Component GetRecommendedComponent(Usage[] usage, decimal priceCap)
        {
            return GetRecommendedComponentOfType(ComponentType.HardDrive, usage, priceCap);
        }

    }
}