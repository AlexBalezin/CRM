using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmBl.Model
{
    public class Sell
    {
        public int Id { get; set; }

        public Check Check { get; set; }

        public Product Product { get; set; }

        public int Count { get; set; }
    }
}
