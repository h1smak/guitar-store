using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guitar_Store
{
    public class Cart
    {
        public int Id { get; set; }
        public User User { get; set; }
        public List<Guitar> Items { get; set; }
        public decimal TotalPrice { get; set; }

        public Cart()
        {
            Items = new List<Guitar>();
            TotalPrice = 0m;
        }

        public Action<Guitar> OnItemAdded { get; set; }
        public Action<Guitar> OnItemRemoved { get; set; }
        public Func<Guitar, bool> ItemDiscountCriteria { get; set; }

        public void AddItem(Guitar item)
        {
            Items.Add(item);
            TotalPrice += item.Price;

            OnItemAdded?.Invoke(item);
        }

        public void RemoveItem(Guitar item)
        {
            if (Items.Remove(item))
            {
                TotalPrice -= item.Price;

                OnItemRemoved?.Invoke(item);
            }
        }

        public void ClearCart()
        {
            Items.Clear();
            TotalPrice = 0m;
        }

        public void UpdateQuantity(Guitar item, int newQuantity)
        {
            if (newQuantity < 0) throw new ArgumentException("Кількість не може бути від'ємною.");

            int currentQuantity = Items.Count(i => i.Id == item.Id);
            int difference = newQuantity - currentQuantity;

            if (difference > 0)
            {
                for (int i = 0; i < difference; i++)
                {
                    AddItem(item);
                }
            }
            else if (difference < 0)
            {
                for (int i = 0; i < Math.Abs(difference); i++)
                {
                    RemoveItem(item);
                }
            }
        }

        public List<Guitar> FindItems(Predicate<Guitar> criteria)
        {
            return Items.FindAll(criteria);
        }

        public void ApplyDiscount(Func<Guitar, decimal> discountCalculator)
        {
            foreach (var item in Items)
            {
                if (ItemDiscountCriteria?.Invoke(item) == true)
                {
                    TotalPrice -= discountCalculator(item);
                }
            }
        }
    }

}
