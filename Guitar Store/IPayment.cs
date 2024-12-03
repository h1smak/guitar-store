using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitar_Store
{
    public interface IPayment
    {
        bool ProcessPayment(decimal amount);
        string GetPaymentDetails();
    }
}
