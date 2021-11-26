using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int Game_Id { get; set; }
        public int Game_Amount { get; set; }
        public decimal Price_Total { get; set; }
        public String Name { get; set; }
        public String LastName { get; set; }
        public int Phone { get; set; }
        public String Email { get; set; }
    }
}
