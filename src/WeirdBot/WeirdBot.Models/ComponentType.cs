using System;

namespace WeirdBot.Models
{
    public enum ComponentType
    {
        HardDrive,
        Processor,
        RAM,
        VideoCard
    }

    public static class ComponentTypeHelpers
    {
        public static string LookUpComponentTypeString(ComponentType cat)
        {
            switch (cat)
            {
                case ComponentType.HardDrive:
                    return "harddrive";
                case ComponentType.Processor:
                    return "processor";
                case ComponentType.RAM:
                    return "ram";
                case ComponentType.VideoCard:
                    return "videocard";
                default:
                    return "unknown";
            }
        }

        public static ComponentType LookUpComponentTypeValue(string partitionKey)
        {
            switch (partitionKey)
            {
                case "harddrive":
                    return ComponentType.HardDrive;
                case "processor":
                    return ComponentType.Processor;
                case "ram":
                    return ComponentType.RAM;
                case "videocard":
                    return ComponentType.VideoCard;
                default:
                    var errorMessage = string.Format("Invalid category type. Could not convert value: {0}", partitionKey);
                    throw new ArgumentException(errorMessage);
            }
        }
    }
}
