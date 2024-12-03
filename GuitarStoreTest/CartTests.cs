using Guitar_Store;

namespace GuitarStoreTest
{
    [TestClass]
    public class CartTests
    {
        [TestMethod]
        public void AddItem_ShouldIncreaseItemsCount()
        {
            var cart = new Cart { Items = new List<Guitar>() };
            var guitar = new Guitar { Id = 1, Name = "Fender Stratocaster", Price = 1200 };

            cart.AddItem(guitar);

            Assert.AreEqual(1, cart.Items.Count);
            Assert.AreEqual(guitar, cart.Items[0]);
        }

        [TestMethod]
        public void RemoveItem_ShouldDecreaseItemsCount()
        {
            var guitar = new Guitar { Id = 1, Name = "Fender Stratocaster", Price = 1200 };
            var cart = new Cart { Items = new List<Guitar> { guitar } };

            cart.RemoveItem(guitar);

            Assert.AreEqual(0, cart.Items.Count);
        }

        [TestMethod]
        public void ClearCart_ShouldEmptyTheCart()
        {
            var cart = new Cart
            {
                Items = new List<Guitar>
            {
                new Guitar { Id = 1, Name = "Gibson Les Paul", Price = 2500 },
                new Guitar { Id = 2, Name = "Ibanez RG", Price = 900 }
            }
            };

            cart.ClearCart();

            Assert.AreEqual(0, cart.Items.Count);
        }
    }
}