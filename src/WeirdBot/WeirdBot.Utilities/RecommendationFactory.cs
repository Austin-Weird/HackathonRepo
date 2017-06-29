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
        private ComponentRepository componentDb;
        private RecommendationEngineSupplier recommendationEngineSource;
        public RecommendationFactory(ComponentRepository db, RecommendationEngineSupplier engineSource)
        {
            componentDb = db;
            recommendationEngineSource = engineSource;
        }

        public Recommendation GetRecommendation(Usage[] usage, decimal highPrice)
        {
            var recommended = new Recommendation();

            for (int i = 0; i < Enum.GetValues(typeof(ComponentType)).Length; i++)
            {
                var componentRecommender = recommendationEngineSource.GetComponentRecommendationEngine(componentDb, (ComponentType)i);
                var recommendedItem = componentRecommender.GetRecommendedComponent(usage, highPrice);
                if (recommendedItem != null)
                    recommended.SetComponent((ComponentType)i, recommendedItem);
            }

            return recommended;
        }


    }
}
