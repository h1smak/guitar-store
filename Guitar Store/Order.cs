using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitar_Store
{
    public class Order : IComparable<Order>, ICloneable
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
            Items = new List<Guitar>();
            OrderDate = DateTime.Now;
        }

        public void UpdateOrderStatus(OrderStatus newStatus)
        {
            Status = newStatus;
        }

        public void AddItemToOrder(Guitar item)
        {
            Items.Add(item);
            TotalAmount += item.Price;
        }

        public decimal CalculateTotalAmount()
        {
            TotalAmount = Items.Sum(i => i.Price);
            return TotalAmount;
        }

        public int CompareTo(Order other)
        {
            return OrderDate.CompareTo(other.OrderDate);
        }

        public object Clone()
        {
            return new Order
            {
                Id = this.Id,
                User = this.User,
                Items = new List<Guitar>(this.Items.Select(item => (Guitar)item.Clone())),
                Status = this.Status,
                TotalAmount = this.TotalAmount,
                OrderDate = this.OrderDate,
                PaymentMethod = this.PaymentMethod
            };
        }
    }

}
