namespace Ordering.Domain.ValueObjects;

public sealed class Address
{
    public string AddressLine { get; } = default!;
    public string Country { get; } = default!;
    public string State { get; } = default!;
    public string ZipCode { get; } = default!;

    private Address(CreateAddressDto addressDto)
    {
        AddressLine = addressDto.addressLine;
        Country = addressDto.country;
        State = addressDto.state;
        ZipCode = addressDto.zipCode;
    }

    public static Address Of(CreateAddressDto addressDto)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(addressDto.addressLine);
        ArgumentNullException.ThrowIfNullOrEmpty(addressDto.country);
        ArgumentNullException.ThrowIfNullOrEmpty(addressDto.state);
        ArgumentNullException.ThrowIfNullOrEmpty(addressDto.zipCode);

        return new Address(addressDto);
    }
}

public sealed record CreateAddressDto(string addressLine, string country, string state, string zipCode);