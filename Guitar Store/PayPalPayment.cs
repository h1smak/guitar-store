using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitar_Store
{
    public class PayPalPayment : IPayment
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public PayPalPayment()
        {
        }

        public bool ProcessPayment(decimal amount)
        {
            Console.WriteLine($"Processing PayPal payment of {amount:C}");
            return true;
        }

        public string GetPaymentDetails()
        {
            return $"PayPal Payment - Email: {Email}";
        }

    }
}
