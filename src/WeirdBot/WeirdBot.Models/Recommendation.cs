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
                //if (value.Category != ComponentType.HardDrive)
                //    throw new ArgumentException("Component is not of correct type.");
                //_total += value.Price;
                //_hdd = value;
                //if (value == null)
                //    ResetTotal();
            }
        }
        private Component _cpu;
        public Component Processor
        {
            get { return _cpu; }
            set
            {
                SetComponentProperty(value, ComponentType.Processor, ref _cpu);
                //if (value.Category != ComponentType.Processor)
                //    throw new ArgumentException("Component is not of correct type.");
                //_total += value.Price;
                //_cpu = value;
                //if (value == null)
                //    ResetTotal();
            }
        }
        private Component _ram;
        public Component RamKit
        {
            get { return _ram; }
            set
            {
                SetComponentProperty(value, ComponentType.RAM, ref _ram);
                //if (value.Category != ComponentType.RAM)
                //    throw new ArgumentException("Component is not of correct type.");
                //_total += value.Price;
                //_ram = value;
                //if (value == null)
                //    ResetTotal();
            }
        }
        private Component _sc;
        public Component SoundCard
        {
            get { return _sc; }
            set
            {
                SetComponentProperty(value, ComponentType.SoundCard, ref _sc);
                //if (value.Category != ComponentType.SoundCard)
                //    throw new ArgumentException("Component is not of correct type.");
                //_total += value.Price;
                //_sc = value;
                //if (value == null)
                //    ResetTotal();
            }
        }
        private Component _vc;
        public Component VideoCard
        {
            get { return _vc; }
            set
            {
                SetComponentProperty(value, ComponentType.VideoCard, ref _vc);
                //if (value.Category != ComponentType.VideoCard)
                //    throw new ArgumentException("Component is not of correct type.");
                //_total += value.Price;
                //_vc = value;
                //if (value == null)
                //    ResetTotal();
            }
        }

        private void SetComponentProperty(Component value, ComponentType type, ref Component backingVariable)
        {
            if (value.Category != type)
                throw new ArgumentException("Component is not of correct type.");
            _total += value.Price;
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
            _total += SoundCard != null ? SoundCard.Price : 0;
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
