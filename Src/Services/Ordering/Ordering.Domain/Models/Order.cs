using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using Ordering.Domain.Abstractions;
using Ordering.Domain.Enums;
using Ordering.Domain.Events;
using Ordering.Domain.ValueObjects;

namespace Ordering.Domain.Models;

public sealed class Order : Aggregate<OrderId>
{
    #region Properties
    public List<OrderItem> OrderItems { get; } = default!;
    
    public CustomerId CustomerId { get; private set; } = default!;
    
    public OrderName OrderName { get; private set; } = default!;
    
    public Address ShippingAddress { get; private set; } = default!;
    public Address BillingAddress { get; private set; } = default!;
    
    public Payment Payment { get; private set; } = default!;
    
    public OrderStatus Status { get; private set; } = OrderStatus.Pending;

    public decimal TotalPrice
    {
        get => OrderItems.Sum(x => x.Price * x.Quantity);
        private set { }
    }
    #endregion

    #region Ctors

    // EF
    private Order()
    {
    }

    private Order(OrderId id,CustomerId customerId, OrderName orderName,
        Address shippingAddress, Address billingAddress, Payment payment, OrderStatus orderStatus)
    {
        this.Id = id;
        this.CustomerId = customerId;
        this.OrderName = orderName;
        this.ShippingAddress = shippingAddress;
        this.BillingAddress = billingAddress;
        this.Payment = payment;
        this.Status = orderStatus;
    }

    #region Factories

    public static Order Create(OrderId id, CustomerId customerId, OrderName orderName,
        Address shippingAddress, Address billingAddress, Payment payment, OrderStatus status)
    {
        Order order = new(id, customerId, orderName, shippingAddress, billingAddress, payment, OrderStatus.Pending);
        order.AddDomainEvents(new OrderCreatedEvent(order));
        
        return order;
    }
    
  
    
    #endregion
    

    #endregion

    #region Functions
    
    public void Update(CustomerId customerId, OrderName orderName,
        Address shippingAddress, Address billingAddress, Payment payment, OrderStatus status)
    {
        this.SetCustomer(customerId);
        this.SetOrderName(orderName);
        this.SetShippingAddress(shippingAddress);
        this.SetBillingAddress(billingAddress);
        this.SetPayment(payment);
        this.SetStatus(status);

        this.AddDomainEvents(new OrderUpdatedEvent(this));
    }

    public void AddItem(ProductId productId, int quantity, decimal price)
    {
        OrderItem newOrderItem = OrderItem.Create(OrderItemId.Of(Guid.NewGuid()), this.Id, productId, quantity, price);
                                            
                                            this.OrderItems.Add(newOrderItem);
                                            this.AddDomainEvents(new OrderItemCreatedEvent(newOrderItem));
                                        }
                                        
                                        public void SetCustomer(CustomerId newCustomerId)
    {
        this.CustomerId = newCustomerId;
    }

    public void SetOrderName(OrderName orderName)
    {
        this.OrderName = orderName;
    }

    public void SetShippingAddress(Address newShippingAddress)
    {
        this.ShippingAddress = newShippingAddress;
    }

    public void SetBillingAddress(Address newBillingAddress)
    {
        this.BillingAddress = newBillingAddress;
    }

    public void SetPayment(Payment newPayment)
    {
        this.Payment = newPayment;
    }

    public void SetStatus(OrderStatus newStatus)
    {
        this.Status = newStatus;
    }

    #endregion
}