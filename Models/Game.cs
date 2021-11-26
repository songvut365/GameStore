using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Models
{
    public class Game
    {
        public int Id { get; set; }
        public String Main_Image { get; set; }
        public String Image1 { get; set; }
        public String Image2 { get; set; }
        public String Image3 { get; set; }
        public String Developer { get; set; }
        public String Name { get; set; }
        public String Type { get; set; }
        public String Detail { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
    }
}
