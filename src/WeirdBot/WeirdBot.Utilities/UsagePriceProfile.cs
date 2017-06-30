using System;
using System.Collections.Generic;
using WeirdBot.Models;

namespace WeirdBot.Utilities
{
    public class UsageProfiles
    {
        public static decimal GetPricePercentage(ComponentType type, Usage[] usage)
        {
            decimal percentage = decimal.MinValue;
            foreach (var item in usage)
            {
                Dictionary<ComponentType, decimal> profile = GetPriceProfile(item);
                percentage = profile[type] > percentage ? profile[type] : percentage;
            }
            return percentage;
        }

        private static Dictionary<ComponentType, decimal> GetPriceProfile(Usage item)
        {
            switch (item)
            {
                case Usage.Gaming:
                    return new GamingPriceProfile();
                case Usage.Media:
                    return new MediaPriceProfile();
                case Usage.Business:
                    return new BusinessPriceProfile();
                case Usage.Programming:
                    return new ProgrammingPriceProfile();
                case Usage.General:
                default:
                    return new GeneralPriceProfile();
            }
        }
    }

    internal class GamingPriceProfile : Dictionary<ComponentType, decimal>
    {
        public GamingPriceProfile()
        {
            Add(ComponentType.HardDrive, 0.149M);
            Add(ComponentType.Processor, 0.164M);
            Add(ComponentType.RAM, 0.135M);
            Add(ComponentType.VideoCard, 0.349M);
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

    internal class GeneralPriceProfile : Dictionary<ComponentType, decimal>
    {
        public GeneralPriceProfile()
        {
            Add(ComponentType.HardDrive, 0.149M);
            Add(ComponentType.Processor, 0.164M);
            Add(ComponentType.RAM, 0.135M);
            Add(ComponentType.VideoCard, 0.150M);
        }
    }

    internal class BusinessPriceProfile : Dictionary<ComponentType, decimal>
    {
        public BusinessPriceProfile()
        {
            Add(ComponentType.HardDrive, 0.329M);
            Add(ComponentType.Processor, 0.204M);
            Add(ComponentType.RAM, 0.135M);
            Add(ComponentType.VideoCard, 0.109M);
        }
    }

    internal class MediaPriceProfile : Dictionary<ComponentType, decimal>
    {
        public MediaPriceProfile()
        {
            Add(ComponentType.HardDrive, 0.359M);
            Add(ComponentType.Processor, 0.164M);
            Add(ComponentType.RAM, 0.135M);
            Add(ComponentType.VideoCard, 0.259M);
        }
    }

    internal class ProgrammingPriceProfile : Dictionary<ComponentType, decimal>
    {
        public ProgrammingPriceProfile()
        {
            Add(ComponentType.HardDrive, 0.149M);
            Add(ComponentType.Processor, 0.164M);
            Add(ComponentType.RAM, 0.185M);
            Add(ComponentType.VideoCard, 0.129M);
        }
    }
}