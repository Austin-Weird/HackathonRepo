using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeirdBot.Models;

namespace WeirdBot.Utilities
{
    public class UsageQualityRank
    {
        private static Quality GetRankOf(Usage item, ComponentType type)
        {
            switch (item)
            {
                case Usage.Gaming:
                    return new GamingQualityProfile()[type];
                case Usage.Media:
                    return new MediaQualityProfile()[type];
                case Usage.Programming:
                    return new ProgrammingQualityProfile()[type];
                case Usage.Business:
                    return new BusinessQualityProfile()[type];
                case Usage.General:
                default:
                    return new GeneralQualityProfile()[type];
            }
        }

        public static Quality GetHighetstRankOf(Usage[] usage, ComponentType type)
        {
            Quality highestRank = Quality.Good;
            foreach (var item in usage)
            {
                var thisItemRank = GetRankOf(item, type);
                highestRank = (int)thisItemRank > (int)highestRank ? thisItemRank : highestRank;
            }
            return highestRank;
        }
    }

    internal class GeneralQualityProfile : Dictionary<ComponentType, Quality>
    {
        public GeneralQualityProfile()
        {
            Add(ComponentType.HardDrive, Quality.Good);
            Add(ComponentType.Processor, Quality.Good);
            Add(ComponentType.RAM, Quality.Good);
            Add(ComponentType.VideoCard, Quality.Good);
        }
    }

    internal class BusinessQualityProfile : Dictionary<ComponentType, Quality>
    {
        public BusinessQualityProfile()
        {
            Add(ComponentType.HardDrive, Quality.Better);
            Add(ComponentType.Processor, Quality.Good);
            Add(ComponentType.RAM, Quality.Good);
            Add(ComponentType.VideoCard, Quality.Good);
        }
    }

    internal class ProgrammingQualityProfile : Dictionary<ComponentType, Quality>
    {
        public ProgrammingQualityProfile()
        {
            Add(ComponentType.HardDrive, Quality.Better);
            Add(ComponentType.Processor, Quality.Best);
            Add(ComponentType.RAM, Quality.Best);
            Add(ComponentType.VideoCard, Quality.Good);
        }
    }

    internal class MediaQualityProfile : Dictionary<ComponentType, Quality>
    {
        public MediaQualityProfile()
        {
            Add(ComponentType.HardDrive, Quality.Better);
            Add(ComponentType.Processor, Quality.Best);
            Add(ComponentType.RAM, Quality.Better);
            Add(ComponentType.VideoCard, Quality.Best);
        }
    }

    internal class GamingQualityProfile : Dictionary<ComponentType, Quality>
    {
        public GamingQualityProfile()
        {
            Add(ComponentType.HardDrive, Quality.Best);
            Add(ComponentType.Processor, Quality.Best);
            Add(ComponentType.RAM, Quality.Best);
            Add(ComponentType.VideoCard, Quality.Best);
        }
    }


}
