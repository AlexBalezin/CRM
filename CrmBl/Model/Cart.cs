using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmBl.Model
{
    public class Cart
    {
        public int Id { get; set; }

        public Customer Customer { get; set; }

        public Dictionary<Product, int> Products { get; set; } = new Dictionary<Product, int>();

        public decimal Sum { get; set; }

        public void Add(Product product, int count)
        {
            if (Products.TryGetValue(product, out int value))
                Products[product] += count;
            else
                Products.Add(product, count);
        }
    }
}
