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
                case Usage.Programming:
                    return new BusinessPriceProfile();
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
            Add(ComponentType.HardDrive, 0.186M);
            Add(ComponentType.Processor, 0.2162M);
            Add(ComponentType.RAM, 0.135M);
            Add(ComponentType.VideoCard, 0.349M);
        }
    }

    internal class GeneralPriceProfile : Dictionary<ComponentType, decimal>
    {
        public GeneralPriceProfile()
        {
            Add(ComponentType.HardDrive, 0.180M);
            Add(ComponentType.Processor, 0.174M);
            Add(ComponentType.RAM, 0.135M);
            Add(ComponentType.VideoCard, 0.280M);
        }
    }

    internal class BusinessPriceProfile : Dictionary<ComponentType, decimal>
    {
        public BusinessPriceProfile()
        {
            Add(ComponentType.HardDrive, 0.329M);
            Add(ComponentType.Processor, 0.204M);
            Add(ComponentType.RAM, 0.135M);
            Add(ComponentType.VideoCard, 0.3537M);
        }
    }

    internal class MediaPriceProfile : Dictionary<ComponentType, decimal>
    {
        public MediaPriceProfile()
        {
            Add(ComponentType.HardDrive, 0.359M);
            Add(ComponentType.Processor, 0.1980M);
            Add(ComponentType.RAM, 0.135M);
            Add(ComponentType.VideoCard, 0.259M);
        }
    }
}