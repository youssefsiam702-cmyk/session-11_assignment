using System;
using System.Collections.Generic;

namespace Assignment_08
{
    internal class Program
    {
        static decimal CalculateTotal(Order order)
        {
            return order.Price * order.Quantity;
        }

        static decimal CalculateTotalWithDiscount(Order order)
        {
            decimal total = order.Price * order.Quantity;
            decimal discount = total * 0.10m;
            return total - discount;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("===== Section 01: Library Engine =====\n");

            List<Book> bList = new List<Book>
            {
                new Book(
                    "978-0132350884",
                    "Clean Code",
                    new[] { "Robert C. Martin" },
                    new DateTime(2008, 8, 1),
                    45.50m),

                new Book(
                    "978-1617294945",
                    "C# in a Nutshell",
                    new[] { "Joseph Albahari", "Ben Albahari" },
                    new DateTime(2022, 11, 10),
                    52.75m),

                new Book(
                    "978-0134494166",
                    "The C# Player's Guide",
                    new[] { "RB Whitaker" },
                    new DateTime(2016, 1, 15),
                    39.99m)
            };

            Console.WriteLine("Book Titles - User-Defined Delegate:");
            BookFunction titleFunction = BookFunctions.GetTitle;
            LibraryEngine.ProcessBooks(bList, titleFunction);

            Console.WriteLine("\nBook Authors - Built-in Func<>:");
            Func<Book, string> authorFunction = BookFunctions.GetAuthors;
            LibraryEngine.ProcessBooks(bList, authorFunction);

            Console.WriteLine("\nBook ISBNs - Anonymous Method:");
            BookFunction isbnFunction = delegate (Book B)
            {
                return B.ISBN;
            };
            LibraryEngine.ProcessBooks(bList, isbnFunction);

            Console.WriteLine("\nPublication Dates - Lambda Expression:");
            Func<Book, string> publicationFunction =
                B => B.PublicationDate.ToString("yyyy-MM-dd");
            LibraryEngine.ProcessBooks(bList, publicationFunction);

            Console.WriteLine("\nComplete Book Information:");
            Console.WriteLine(bList[0]);

            Console.WriteLine("\n===== Section 02: Order Processing System =====\n");

            Order order = new Order(101, "Omar Hassan", 250m, 3);

            PriceCalculator normalPrice = CalculateTotal;
            PriceCalculator discountPrice = CalculateTotalWithDiscount;

            decimal totalPrice = OrderService.CalculateOrderPrice(order, normalPrice);
            decimal discountedPrice = OrderService.CalculateOrderPrice(order, discountPrice);

            Console.WriteLine($"Normal total: {totalPrice:0.00}");
            Console.WriteLine($"Discounted total: {discountedPrice:0.00}");

            Func<Order, decimal> normalCalculation =
                x => x.Price * x.Quantity;

            Func<Order, decimal> twentyPercentDiscount =
                x => (x.Price * x.Quantity) * 0.80m;

            Console.WriteLine($"Func normal calculation: {OrderService.CalculateOrderPrice(order, normalCalculation):0.00}");
            Console.WriteLine($"Func 20% discount: {OrderService.CalculateOrderPrice(order, twentyPercentDiscount):0.00}");

            Predicate<Order> validQuantity =
                x => x.Quantity > 0;

            Predicate<Order> validPrice =
                x => x.Price > 0;

            Predicate<Order> validCustomer =
                x => !string.IsNullOrWhiteSpace(x.CustomerName);

            Console.WriteLine($"Valid quantity: {OrderService.ValidateOrder(order, validQuantity)}");
            Console.WriteLine($"Valid price: {OrderService.ValidateOrder(order, validPrice)}");
            Console.WriteLine($"Valid customer name: {OrderService.ValidateOrder(order, validCustomer)}");

            Action<Order> printOrder =
                x => Console.WriteLine($"Order {x.Id} processed for {x.CustomerName}.");

            Action<Order> sendConfirmation =
                x => Console.WriteLine($"Confirmation sent to {x.CustomerName}.");

            Action<Order> writeAudit =
                x => Console.WriteLine($"Audit: Order {x.Id} was processed successfully.");

            Console.WriteLine("\nProcessing actions:");
            OrderService.ProcessOrder(order, printOrder);
            OrderService.ProcessOrder(order, sendConfirmation);
            OrderService.ProcessOrder(order, writeAudit);

            OrderService orderService = new OrderService();

            Action<Order> handler1 =
                x => Console.WriteLine($"Handler 1: Order {x.Id} completed.");

            Action<Order> handler2 =
                x => Console.WriteLine($"Handler 2: Notification sent for order {x.Id}.");

            Action<Order> handler3 =
                x => Console.WriteLine($"Handler 3: Audit created for order {x.Id}.");

            orderService.OrderProcessed += handler1;
            orderService.OrderProcessed += handler2;
            orderService.OrderProcessed += handler3;

            Console.WriteLine("\nEvent with three subscribers:");
            orderService.ProcessOrder(order);

            orderService.OrderProcessed -= handler1;

            Console.WriteLine("\nEvent after unsubscribing Handler 1:");
            orderService.ProcessOrder(order);

            Console.WriteLine("\n===== Questions =====");
            Console.WriteLine("Q1: PriceCalculator is a custom delegate, while Func<Order, decimal> is a built-in generic delegate.");
            Console.WriteLine("Q2: Action<Order> performs an operation without returning a value; Func<Order, decimal> returns a decimal result.");
            Console.WriteLine("Q3: Predicate<T> returns bool because it represents a condition or validation rule.");
            Console.WriteLine("Q4: A delegate can reference and invoke methods, while an event controls how outside code subscribes to notifications.");
            Console.WriteLine("Q5: External code cannot normally invoke another class's event because only the declaring class can raise it.");
            Console.WriteLine("Q6: All subscribed handlers are called in sequence when the event is raised.");
            Console.WriteLine("Q7: += subscribes HandleOrderProcessed to the OrderProcessed event.");
            Console.WriteLine("Q8: event Action<Order> restricts direct invocation and exposes subscription behavior, unlike a plain Action<Order> field/property.");

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
