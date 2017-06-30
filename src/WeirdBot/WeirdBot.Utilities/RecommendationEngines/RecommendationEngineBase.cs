using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeirdBot.DataAccess;
using WeirdBot.Models;

namespace WeirdBot.Utilities
{
    public class ComponentRecommendationEngineBase : IComponentRecommendationEngine
    {
        protected IComponentRepository _db;

        public ComponentRecommendationEngineBase(IComponentRepository db)
        {
            _db = db;
        }

        public virtual Component GetRecommendedComponent(Usage[] usage, decimal highPrice) { return null; }

        protected Component GetRecommendedComponentOfType(ComponentType type, Usage[] usage, decimal priceCap)
        {
            var priceTarget = GetPriceTarget(type, usage, priceCap);
            var quality = GetComponentQuality(type, usage);

            return _db.GetComponentByPriceAndQuality(type, quality, priceTarget);
        }

        private Quality GetComponentQuality(ComponentType type, Usage[] usage)
        {
            return UsageQualityRank.GetHighetstRankOf(usage, type);
        }

        protected decimal GetPriceTarget(ComponentType type, Usage[] usage, decimal highPrice)
        {
            return UsageProfiles.GetPricePercentage(type, usage) * highPrice;
        }


    }
}
