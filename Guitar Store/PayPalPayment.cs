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

        public bool ProcessPayment(decimal amount)
        {
            throw new NotImplementedException();
        }

        public string GetPaymentDetails()
        {
            throw new NotImplementedException();
        }

        public PayPalPayment()
        {
            throw new NotImplementedException();
        }
    }
}
