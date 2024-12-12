using Guitar_Store;

class Program
{
    public delegate void SystemEventHandler(string message);

    public static event SystemEventHandler OnUserRegistered;
    public static event SystemEventHandler OnOrderPlaced;

    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.Unicode;
        var users = new List<User>();
        var guitars = SeedGuitars();
        User currentUser = null;

        OnUserRegistered += message => Console.WriteLine($"Подія: {message}");
        OnOrderPlaced += message => Console.WriteLine($"Подія: {message}");

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Меню:");
            Console.WriteLine("1. Зареєструватися в системі");
            Console.WriteLine("2. Подивитися каталог наявних гітар");
            Console.WriteLine("3. Переглянути кошик");
            Console.WriteLine("4. Виконати оплату");
            Console.WriteLine("5. Переглянути історію замовлень");
            Console.WriteLine("0. Вийти");
            Console.Write("Оберіть пункт меню: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    currentUser = Register(users);
                    break;
                case "2":
                    ViewCatalog(guitars, currentUser);
                    break;
                case "3":
                    ViewCart(currentUser?.Cart);
                    break;
                case "4":
                    PerformPayment(currentUser);
                    break;
                case "5":
                    ViewOrderHistory(currentUser);
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Невірний вибір.");
                    break;
            }

            Console.WriteLine("Натисніть будь-яку клавішу для продовження...");
            Console.ReadKey();
        }
    }

    static User Register(List<User> users)
    {
        Predicate<User> userExists = u => users.Any();

        if (userExists.Invoke(null))
        {
            Console.WriteLine("У системі вже є зареєстрований користувач.");
            Console.WriteLine("1. Вийти з поточного акаунта");
            Console.WriteLine("0. Повернутися до головного меню");
            Console.Write("Оберіть дію: ");
            var action = Console.ReadLine();

            if (action == "0") return null;
            if (action == "1")
            {
                Console.Write("Ви хочете видалити акаунт? (y/n): ");
                string confirm = Console.ReadLine();
                if (confirm?.ToLower() == "y")
                {
                    Console.Write("Введіть email акаунта для підтвердження: ");
                    string emailToRemove = Console.ReadLine();

                    var userToRemove = users.FirstOrDefault(u => u.Email == emailToRemove);
                    if (userToRemove != null)
                    {
                        users.Remove(userToRemove);
                        Console.WriteLine("Акаунт успішно видалено.");
                    }
                    else
                    {
                        Console.WriteLine("Акаунт із таким email не знайдено.");
                    }
                }
                return null;
            }

            Console.WriteLine("Невірний вибір.");
            return null;
        }

        string name = null;
        while (name == null)
        {
            try
            {
                Console.Write("Введіть ім'я (мінімум 3 літери): ");
                name = Console.ReadLine();
                var tempUser = new User { Name = name }; 
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
                name = null;
            }
        }

        string email = null;
        while (email == null)
        {
            try
            {
                Console.Write("Введіть email: ");
                email = Console.ReadLine();
                var tempUser = new User { Email = email };
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
                email = null; 
            }
        }

        Console.Write("Введіть пароль: ");
        string password = Console.ReadLine();

        var user = new User
        {
            Name = name,
            Email = email,
            Password = password,
            Cart = new Cart(),
            OrderHistory = new List<Order>()
        };

        users.Add(user);
        Console.WriteLine("Реєстрація успішна.");

        OnUserRegistered?.Invoke($"Користувач {name} зареєстрований.");
        return user;
    }


    static void ViewCatalog(List<Guitar> guitars, User user)
    {
        Console.WriteLine("Каталог гітар:");
        for (int i = 0; i < guitars.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {guitars[i].Name} - {guitars[i].Price:C} ({guitars[i].StockQuantity} в наявності)");
        }

        if (user != null)
        {
            Console.Write("Оберіть номер гітари для додавання в кошик (0 для виходу): ");
            if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= guitars.Count)
            {
                var selectedGuitar = guitars[choice - 1];

                int currentCartQuantity = user.Cart.Items.Count(g => g.Id == selectedGuitar.Id);

                if (currentCartQuantity >= selectedGuitar.StockQuantity)
                {
                    Console.WriteLine("Вибачте, ви вже додали максимальну кількість цього товару.");
                    return;
                }

                if (selectedGuitar.StockQuantity > 0)
                {
                    user.Cart.AddItem(selectedGuitar);
                    user.Cart.OnItemAdded = item => Console.WriteLine($"Гітару додано: {item.Name}");
                    selectedGuitar.UpdateStock(-1);
                    Console.WriteLine("Гітара додана до кошика.");
                }
                else
                {
                    Console.WriteLine("Гітара відсутня в наявності.");
                }
            }
        }
        else
        {
            Console.WriteLine("Ви маєте зареєструватися для додавання в кошик.");
        }
    }


    static void ViewCart(Cart cart)
    {
        if (cart == null || cart.Items.Count == 0)
        {
            Console.WriteLine("Кошик порожній.");
            return;
        }

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Ваш кошик:");
            var groupedItems = cart.Items.GroupBy(i => i.Id)
                                         .Select(g => new { Guitar = g.First(), Quantity = g.Count() })
                                         .ToList();

            for (int i = 0; i < groupedItems.Count; i++)
            {
                var group = groupedItems[i];
                Console.WriteLine($"{i + 1}. {group.Guitar.Name} - {group.Guitar.Price:C} x {group.Quantity} = {group.Guitar.Price * group.Quantity:C}");
            }

            Console.WriteLine($"Загальна сума: {cart.TotalPrice:C}");
            Console.WriteLine("1. Змінити кількість товару");
            Console.WriteLine("2. Видалити товар");
            Console.WriteLine("0. Повернутися до меню");
            Console.Write("Оберіть дію: ");
            var action = Console.ReadLine();

            if (action == "0") break;

            Console.Write("Оберіть номер товару: ");
            if (!int.TryParse(Console.ReadLine(), out int itemIndex) || itemIndex < 1 || itemIndex > groupedItems.Count)
            {
                Console.WriteLine("Невірний вибір.");
                continue;
            }

            var selectedItem = groupedItems[itemIndex - 1].Guitar;

            switch (action)
            {
                case "1":
                    Console.Write("Введіть нову кількість: ");

                    if (int.TryParse(Console.ReadLine(), out int newQuantity) && newQuantity >= 0)
                    {
                        if (newQuantity > selectedItem.StockQuantity)
                        {
                            Console.WriteLine("Вибачте, ви вже додали максимальну кількість цього товару.");
                            return;
                        }

                        cart.UpdateQuantity(selectedItem, newQuantity);
                        selectedItem.UpdateStock(-newQuantity + 1);
                        Console.WriteLine("Кількість оновлено.");
                    }
                    else
                    {
                        Console.WriteLine("Невірна кількість.");
                    }
                    break;

                case "2":
                    cart.UpdateQuantity(selectedItem, 0); 
                    Console.WriteLine("Товар видалено.");
                    break;

                default:
                    Console.WriteLine("Невірний вибір.");
                    break;
            }
        }
    }



    static void PerformPayment(User user)
    {
        Random random = new Random();
        int id = random.Next(10000, 100000);

        if (user == null || user.Cart == null || user.Cart.Items.Count == 0)
        {
            Console.WriteLine("Кошик порожній або користувач не зареєстрований.");
            return;
        }

        Console.WriteLine("Оберіть метод оплати:");
        Console.WriteLine("1. Карта");
        Console.WriteLine("2. PayPal");
        var choice = Console.ReadLine();

        IPayment paymentMethod = choice switch
        {
            "1" => new CardPayment { CardHolderName = "John Doe", CardNumber = "1234 5678 9876 5432", ExpiryDate = DateTime.Now.AddYears(3), CVV = 123 },
            "2" => new PayPalPayment { Email = "example@paypal.com", Password = "password" },
            _ => null
        };

        if (paymentMethod == null || !paymentMethod.ProcessPayment(user.Cart.TotalPrice))
        {
            Console.WriteLine("Оплата не виконана.");
            return;
        }

        var order = new Order
        {
            Id = id,
            Items = new List<Guitar>(user.Cart.Items),
            TotalAmount = user.Cart.TotalPrice,
            User = user,
            PaymentMethod = paymentMethod,
            Status = OrderStatus.NEW
        };

        user.AddOrder(order);
        user.Cart.ClearCart();
        Console.WriteLine("Оплата успішна. Замовлення оформлено.");

        OnOrderPlaced?.Invoke($"Замовлення #{id} на суму {order.TotalAmount:C} оформлено.");
    }

    static void ViewOrderHistory(User user)
    {
        if (user == null || user.OrderHistory.Count == 0)
        {
            Console.WriteLine("Історія замовлень порожня.");
            return;
        }

        Console.WriteLine("Ваші замовлення:");
        foreach (var order in user.OrderHistory)
        {
            Console.WriteLine($"Замовлення #{order.Id} на суму {order.TotalAmount:C}, статус: {order.Status}");
        }
    }

    static List<Guitar> SeedGuitars()
    {
        return new List<Guitar>
    {
        new Guitar { Id = 1, Name = "Fender Stratocaster", BrandName = "Fender", Category = GuitarCategory.ELECTRIC, Price = 1200m, StockQuantity = 5 },
        new Guitar { Id = 2, Name = "Gibson Les Paul", BrandName = "Gibson", Category = GuitarCategory.ELECTRIC, Price = 1500m, StockQuantity = 3 },
        new Guitar { Id = 3, Name = "Yamaha FG800", BrandName = "Yamaha", Category = GuitarCategory.ACOUSTIC, Price = 300m, StockQuantity = 10 }
    };
    }
}