using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_access
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal CostPrice { get; set; }
        public string Producer { get; set; }
        public int Quantity { get; set; }
        public string Type { get; set; }

        public override string ToString()
        {
            return $"{Name}, {Price}$ - {Quantity} items";
        }
    }
}
