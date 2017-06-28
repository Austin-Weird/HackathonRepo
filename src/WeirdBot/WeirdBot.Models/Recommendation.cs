using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeirdBot.Models
{
    public class Recommendation
    {
        public Component HardDiskDrive { get; set; }
        public Component Processor { get; set; }
        public Component RamKit { get; set; }
        public Component SoundCard { get; set; }
        public Component VideoCard { get; set; }

        public void SetComponent(ComponentType type, Component item)
        {
            switch (type)
            {
                case ComponentType.HardDrive:
                    HardDiskDrive = item;
                    break;
                case ComponentType.Processor:
                    Processor = item;
                    break;
                case ComponentType.RAM:
                    RamKit = item;
                    break;
                case ComponentType.SoundCard:
                    SoundCard = item;
                    break;
                case ComponentType.VideoCard:
                    VideoCard = item;
                    break;
                default:
                    break;
            }
        }
    }
}
