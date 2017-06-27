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

        public RecommendationFactory(ComponentRepository db)
        {
            componentDb = db;
        }

        public Recommendation GetRecommendation(Usage[] usage, decimal lowPrice, decimal highPrice)
        {
            var recommended = new Recommendation();

            for (int i = 0; i < Enum.GetValues(typeof(ComponentType)).Length; i++)
            {
                var componentRecommender = GetComponentRecommendationEngine(i);
                var recommendedItem = componentRecommender.GetRecommendedComponent(usage, lowPrice, highPrice);
            }

            return recommended;
        }

        private IComponentRecommendationEngine GetComponentRecommendationEngine(int i)
        {
            switch ((ComponentType)i)
            {
                case ComponentType.HardDrive:
                    return new HardDriveRecommendationEngine(componentDb);
                case ComponentType.Processor:
                    return new ProcessorRecommendationEngine(componentDb);
                case ComponentType.RAM:
                    return new RamRecommendationEngine(componentDb);
                case ComponentType.VideoCard:
                    return new VideoCardRecommendationEngine(componentDb);
                case ComponentType.SoundCard:
                    return new SoundCardRecommendationEngine(componentDb);
                default:
                    return new NullRecommendationEngine(componentDb);
            }
        }
    }
}
