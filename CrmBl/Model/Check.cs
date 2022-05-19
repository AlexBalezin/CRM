using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrmBl.Model
{
    public class Check
    {
        public int Id { get; set; }

        public virtual Customer Customer { get; set; }  

        public DateTime Created { get; set; } = DateTime.Now;

        public decimal Sum { get; set; }

        public override string ToString()
        {
            return $"{Id} от {Created.ToString("dd.MM.yy hh:mm:ss")}";
        }
    }
}
