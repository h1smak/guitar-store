using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Guitar_Store
{
    public class User
    {
        private string _name;
        private string _email;

        public int Id { get; set; }

        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 3)
                    throw new ArgumentException("Ім'я повинно бути не менше 3 літер.");
                _name = value;
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || !Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                    throw new ArgumentException("Невірний формат email.");
                _email = value;
            }
        }

        public string Password { get; set; }
        public string Address { get; set; } = "Не вказано";
        public Cart Cart { get; set; }
        public List<Order> OrderHistory { get; set; } = new();

        public void UpdateUserInfo(string newEmail, string newAddress)
        {
            Email = newEmail;
            Address = newAddress;
        }

        public void AddOrder(Order order)
        {
            OrderHistory.Add(order);
        }       
    }

}
