using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitar_Store
{
    public class Cart
    {
        public int Id { get; set; }
        public User User { get; set; }
        public List<Guitar> Items { get; set; }
        public decimal TotalPrice { get; set; }

        public Cart()
        {
            throw new NotImplementedException();
        }

        public void AddItem(Guitar item)
        {
            throw new NotImplementedException();
        }

        public void RemoveItem(Guitar item)
        {
            throw new NotImplementedException();
        }

        public void ClearCart()
        {
            throw new NotImplementedException();
        }
    }
}
