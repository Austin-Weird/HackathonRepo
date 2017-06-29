﻿using System;
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
            if (!IsValid(usage, priceCap))
                return null;

            var priceTarget = GetPriceTarget(type, usage, priceCap);
            var quality = GetComponentQuality(usage);

            return _db.GetComponentByPriceAndQuality(type, quality, priceTarget);
        }

        protected bool IsValid(Usage[] usage, decimal priceCap)
        {
            if (usage.Contains(Usage.Gaming))
                return priceCap > 600M;
            if (usage.Contains(Usage.Media) || usage.Contains(Usage.Programming))
                return priceCap > 500M;
            return priceCap > 200M;
        }


        protected Quality GetComponentQuality(Usage[] usage)
        {
            return UsageQualityRank.GetHighetstRankOf(usage);
        }

        protected decimal GetPriceTarget(ComponentType type, Usage[] usage, decimal highPrice)
        {
            return UsageProfiles.GetPricePercentage(type, usage) * highPrice;
        }


    }
}
