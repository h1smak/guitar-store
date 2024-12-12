using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitar_Store
{
    public class CardPayment : IPayment
    {
        public string CardNumber { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string CardHolderName { get; set; }
        public int CVV { get; set; }

        public CardPayment()
        {
        }

        public bool ProcessPayment(decimal amount)
        {
            Console.WriteLine($"Processing card payment of {amount:C}");
            return true;
        }

        public string GetPaymentDetails()
        {
            return $"Card Payment - Card Holder: {CardHolderName}, Expiry: {ExpiryDate:MM/yy}";
        }

    }
}
