using System.Runtime.Versioning;
using Ordering.Domain.Abstractions;
using Ordering.Domain.Enums;
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
        return new(id, customerId, orderName, shippingAddress, billingAddress, payment, OrderStatus.Pending);
    }
    
    #endregion
    

    #endregion
}