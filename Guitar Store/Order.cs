using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitar_Store
{
    public class Order
    {
        public int Id { get; set; }
        public User User { get; set; }
        public List<Guitar> Items { get; set; }
        public OrderStatus Status { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public IPayment PaymentMethod { get; set; }

        public Order()
        {
            throw new NotImplementedException();
        }

        public void UpdateOrderStatus(OrderStatus newStatus)
        {
            throw new NotImplementedException();
        }

        public void AddItemToOrder(Guitar item)
        {
            throw new NotImplementedException();
        }

        public decimal CalculateTotalAmount()
        {
            throw new NotImplementedException();
        }
    }
}
