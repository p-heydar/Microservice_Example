namespace Ordering.Domain.ValueObjects;

public sealed class Payment
{
    public string CardName { get; } = default!;
    public string CardNumber { get; } = default!;
    public string Expiration { get; } = default!;
    public string CVV { get; } = default!;
    public int PaymentMethod { get; } = default!;

    private Payment(CreatePaymentValueObjectDto createPaymentValueObjectDto)
    {
        CardName = createPaymentValueObjectDto.cardName;
        CardNumber = createPaymentValueObjectDto.cardNumber;
        Expiration = createPaymentValueObjectDto.expiration;
        CVV = createPaymentValueObjectDto.cvv;
        PaymentMethod = createPaymentValueObjectDto.paymentMethod;
    }

    public static Payment Of(CreatePaymentValueObjectDto createPaymentValueObjectDto)
    {
        ArgumentException.ThrowIfNullOrEmpty(createPaymentValueObjectDto.cardName);
        ArgumentException.ThrowIfNullOrEmpty(createPaymentValueObjectDto.cardNumber);
        ArgumentException.ThrowIfNullOrEmpty(createPaymentValueObjectDto.expiration);
        ArgumentException.ThrowIfNullOrEmpty(createPaymentValueObjectDto.cvv);

        return new Payment(createPaymentValueObjectDto);
    }
}

public sealed record CreatePaymentValueObjectDto(string cardName, string cardNumber, string expiration, string cvv, int paymentMethod);