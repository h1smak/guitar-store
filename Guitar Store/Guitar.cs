using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitar_Store
{
    public class Guitar : ICloneable
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
        }

        public void UpdateStock(int quantity)
        {
            if (quantity < 0 && StockQuantity + quantity < 0)
                throw new InvalidOperationException("Not enough stock.");
            StockQuantity += quantity;
        }

        public object Clone()
        {
            return new Guitar
            {
                Id = this.Id,
                Name = this.Name,
                BrandName = this.BrandName,
                Category = this.Category,
                Price = this.Price,
                Description = this.Description,
                StockQuantity = this.StockQuantity
            };
        }
    }

}
