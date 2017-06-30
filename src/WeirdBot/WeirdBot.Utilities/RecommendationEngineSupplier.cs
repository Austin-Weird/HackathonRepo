using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeirdBot.DataAccess;
using WeirdBot.Models;

namespace WeirdBot.Utilities
{
    public class RecommendationEngineSupplier
    {
        public virtual IComponentRecommendationEngine GetComponentRecommendationEngine(IComponentRepository componentDb, ComponentType type)
        {
            switch (type)
            {
                case ComponentType.HardDrive:
                    return new HardDriveRecommendationEngine(componentDb);
                case ComponentType.Processor:
                    return new ProcessorRecommendationEngine(componentDb);
                case ComponentType.RAM:
                    return new RamRecommendationEngine(componentDb);
                case ComponentType.VideoCard:
                    return new VideoCardRecommendationEngine(componentDb);
                default:
                    return new NullRecommendationEngine(componentDb);
            }
        }
    }
}
