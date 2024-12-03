using Guitar_Store;

namespace GuitarStoreTest
{
    [TestClass]
    public class OrderTests
    {
        [TestMethod]
        public void UpdateOrderStatus_ShouldChangeStatus()
        {
            var order = new Order { Status = OrderStatus.NEW };

            order.UpdateOrderStatus(OrderStatus.SHIPPED);

            Assert.AreEqual(OrderStatus.SHIPPED, order.Status);
        }

        [TestMethod]
        public void AddItemToOrder_ShouldIncreaseItemsCount()
        {
            var order = new Order { Items = new List<Guitar>() };
            var guitar = new Guitar { Id = 1, Name = "Fender Telecaster", Price = 1000 };

            order.AddItemToOrder(guitar);

            Assert.AreEqual(1, order.Items.Count);
            Assert.AreEqual(guitar, order.Items[0]);
        }

        [TestMethod]
        public void CalculateTotalAmount_ShouldReturnCorrectSum()
        {
            var order = new Order
            {
                Items = new List<Guitar>
            {
                new Guitar { Id = 1, Price = 1500 },
                new Guitar { Id = 2, Price = 1000 }
            }
            };

            var total = order.CalculateTotalAmount();

            Assert.AreEqual(2500, total);
        }
    }

}