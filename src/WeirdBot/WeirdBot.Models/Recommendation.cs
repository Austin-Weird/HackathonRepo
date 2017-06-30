using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeirdBot.Models
{
    public class Recommendation
    {
        private decimal _total = 0.00M;

        private Component _hdd;
        public Component HardDiskDrive
        {
            get { return _hdd; }
            set
            {
                SetComponentProperty(value, ComponentType.HardDrive, ref _hdd);
            }
        }
        private Component _cpu;
        public Component Processor
        {
            get { return _cpu; }
            set
            {
                SetComponentProperty(value, ComponentType.Processor, ref _cpu);
            }
        }
        private Component _ram;
        public Component RamKit
        {
            get { return _ram; }
            set
            {
                SetComponentProperty(value, ComponentType.RAM, ref _ram);
            }
        }
        private Component _vc;
        public Component VideoCard
        {
            get { return _vc; }
            set
            {
                SetComponentProperty(value, ComponentType.VideoCard, ref _vc);
            }
        }

        private void SetComponentProperty(Component value, ComponentType type, ref Component backingVariable)
        {
            if (value != null && value.Category != type)
                throw new ArgumentException("Component is not of correct type.");
            _total += value != null ? value.Price : 0;
            backingVariable = value;
            if (value == null)
                ResetTotal();
        }

        public decimal TotalPrice
        {
            get
            {
                return _total;
            }
        }

        private void ResetTotal()
        {
            _total = 0M;
            _total += HardDiskDrive != null ? HardDiskDrive.Price : 0;
            _total += RamKit != null ? RamKit.Price : 0;
            _total += Processor != null ? Processor.Price : 0;
            _total += VideoCard != null ? VideoCard.Price : 0;
        }

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
                 case ComponentType.VideoCard:
                    VideoCard = item;
                    break;
                default:
                    break;
            }
        }
    }
}
