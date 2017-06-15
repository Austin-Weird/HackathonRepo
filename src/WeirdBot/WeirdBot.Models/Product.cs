using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeirdBot.Models
{
    public class Product
    {
        public int ID { get; set; }
        public Category Category { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Dictionary<string, string> Specs { get; set; }
    }
}
