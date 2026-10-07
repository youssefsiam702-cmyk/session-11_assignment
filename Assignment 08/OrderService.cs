using System;

namespace Assignment_08
{
    public delegate decimal PriceCalculator(Order order);

    public class OrderService
    {
        public event Action<Order>? OrderProcessed;

        public static decimal CalculateOrderPrice(Order order, PriceCalculator calculator)
        {
            return calculator(order);
        }

        public static decimal CalculateOrderPrice(Order order, Func<Order, decimal> calculator)
        {
            return calculator(order);
        }

        public static bool ValidateOrder(Order order, Predicate<Order> validationRule)
        {
            return validationRule(order);
        }

        public static void ProcessOrder(Order order, Action<Order> action)
        {
            action(order);
        }

        public void ProcessOrder(Order order)
        {
            Console.WriteLine($"Processing order {order.Id}...");
            OrderProcessed?.Invoke(order);
        }
    }
}
