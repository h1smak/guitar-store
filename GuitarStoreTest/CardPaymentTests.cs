using Guitar_Store;

namespace GuitarStoreTest
{
    [TestClass]
    public class CardPaymentTests
    {
        [TestMethod]
        public void ProcessPayment_ShouldReturnTrueForValidAmount()
        {
            var payment = new CardPayment
            {
                CardNumber = "1234567812345678",
                ExpiryDate = DateTime.Now.AddYears(2),
                CVV = 123,
                CardHolderName = "John Doe"
            };

            var result = payment.ProcessPayment(500);

            Assert.IsTrue(result);
        }
    }
}