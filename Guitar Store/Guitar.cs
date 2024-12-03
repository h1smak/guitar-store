using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitar_Store
{
    public class Guitar
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BrandName { get; set; }
        public GuitarCategory Category { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int StockQuantity { get; set; }

        public Guitar()
        {
            throw new NotImplementedException();
        }

        public void UpdateStock(int quantity)
        {
            throw new NotImplementedException();
        }        
    }
}
