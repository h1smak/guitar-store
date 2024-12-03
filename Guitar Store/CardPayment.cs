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

        public bool ProcessPayment(decimal amount)
        {
            throw new NotImplementedException();
        }

        public string GetPaymentDetails()
        {
            throw new NotImplementedException();
        }

        public CardPayment()
        {
            throw new NotImplementedException();
        }
    }
}
