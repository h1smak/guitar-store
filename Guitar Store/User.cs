using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitar_Store
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public Cart Cart { get; set; }
        public List<Order> OrderHistory { get; set; }

        public User()
        {
            throw new NotImplementedException();
        }

        public void UpdateUserInfo(string newEmail, string newAddress)
        {
            throw new NotImplementedException();
        }

        public void AddOrder(Order order)
        {
            throw new NotImplementedException();
        }        
    }
}
