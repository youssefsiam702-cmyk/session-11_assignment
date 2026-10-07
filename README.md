# Assignment 08 - Delegates and Events

This console application demonstrates the required C# delegate and event concepts from Assignment 08.

## Section 01 - Library Engine

The first section implements the Book and BookFunctions examples and demonstrates:

- A user-defined delegate named `BookFunction`.
- The built-in `Func<Book, string>` delegate.
- An anonymous method for reading the ISBN.
- A lambda expression for reading the publication date.
- A parameterized `ProcessBooks` method.

## Section 02 - Order Processing System

The second section implements a small order-processing example using:

- `PriceCalculator` user-defined delegate.
- `Func<Order, decimal>` for pricing strategies.
- `Predicate<Order>` for validation rules.
- `Action<Order>` for processing actions.
- `OrderProcessed` event using `Action<Order>`.
- Lambda expressions.
- Multiple event subscribers and event unsubscription.

## Runtime Flow

The program demonstrates the following sequence:

1. Create an order.
2. Validate the order with different rules.
3. Calculate the order price using multiple strategies.
4. Execute different processing actions.
5. Raise the `OrderProcessed` event.
6. Execute multiple subscribed handlers.
7. Unsubscribe one handler and demonstrate the changed event behavior.

## Answers to Assignment Questions

### Q1
`PriceCalculator` is a custom delegate created specifically for the application. `Func<Order, decimal>` is a reusable built-in generic delegate that represents a method receiving an Order and returning a decimal.

### Q2
`Action<Order>` represents an operation that accepts an Order and returns nothing. `Func<Order, decimal>` accepts an Order and returns a decimal value.

### Q3
`Predicate<T>` is designed to represent a condition or test and therefore returns a Boolean value.

### Q4
A delegate can be used directly to reference and invoke methods. An event is a controlled notification mechanism that allows other objects to subscribe and unsubscribe without allowing them to raise the event themselves.

### Q5
Only the class that declares an event can normally raise it. External code is limited to subscribing and unsubscribing.

### Q6
When several handlers subscribe to an event, raising the event invokes the subscribed handlers one after another.

### Q7
`orderService.OrderProcessed += HandleOrderProcessed;` adds `HandleOrderProcessed` to the event's invocation list.

### Q8
`Action<Order>` is a delegate type. `event Action<Order>` exposes that delegate as an event, restricting external code from directly invoking it while still allowing subscription and unsubscription.

## Bonus Challenge

The application demonstrates runtime pricing strategies with `Func<Order, decimal>`, including normal pricing and discounted pricing. The pricing logic is supplied by the caller rather than being hard-coded inside the service.
