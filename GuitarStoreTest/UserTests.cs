using Guitar_Store;

namespace GuitarStoreTest
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void UpdateUserInfo_ShouldUpdateEmailAndAddress()
        {
            var user = new User { Email = "old@example.com", Address = "Old Address" };

            user.UpdateUserInfo("new@example.com", "New Address");

            Assert.AreEqual("new@example.com", user.Email);
            Assert.AreEqual("New Address", user.Address);
        }

        [TestMethod]
        public void AddOrder_ShouldAddOrderToOrderHistory()
        {
            var user = new User { OrderHistory = new List<Order>() };
            var order = new Order { Id = 1, TotalAmount = 2000 };

            user.AddOrder(order);

            Assert.AreEqual(1, user.OrderHistory.Count);
            Assert.AreEqual(order, user.OrderHistory[0]);
        }
    }
}
