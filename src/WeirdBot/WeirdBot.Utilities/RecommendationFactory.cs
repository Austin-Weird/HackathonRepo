using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeirdBot.DataAccess;
using WeirdBot.Models;

namespace WeirdBot.Utilities
{
    public class RecommendationFactory
    {
        private IComponentRepository componentDb;
        private RecommendationEngineSupplier recommendationEngineSource;

        public RecommendationFactory(IComponentRepository db, RecommendationEngineSupplier engineSource)
        {
            componentDb = db;
            recommendationEngineSource = engineSource;
        }

        public Recommendation GetRecommendation(Usage[] usage, decimal priceCap)
        {
            var recommended = new Recommendation();

            //if (!IsValid(usage, priceCap))
            //    return recommended;

            for (int i = 0; i < Enum.GetValues(typeof(ComponentType)).Length; i++)
            {
                var componentRecommender = recommendationEngineSource.GetComponentRecommendationEngine(componentDb, (ComponentType)i);
                var recommendedItem = componentRecommender.GetRecommendedComponent(usage, priceCap);
                if (recommendedItem != null)
                    recommended.SetComponent((ComponentType)i, recommendedItem);
            }

            return recommended;
        }

        //protected bool IsValid(Usage[] usage, decimal priceCap)
        //{
        //    if (usage.Contains(Usage.Gaming))
        //        return priceCap > 500M;
        //    if (usage.Contains(Usage.Media) || usage.Contains(Usage.Programming))
        //        return priceCap > 500M;
        //    return priceCap > 200M;
        //}


    }
}
