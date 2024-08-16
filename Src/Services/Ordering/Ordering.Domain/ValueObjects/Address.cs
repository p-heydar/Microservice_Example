﻿using System.Reflection;

namespace Ordering.Domain.ValueObjects;

public record Address
{
    #region Properties

    public string FirstName { get; } = default!;
    public string LastName { get; } = default!;
    public string EmailAddress { get; } = default!;
    public string AddressLine { get; } = default!;
    public string Country { get; } = default!;
    public string State { get; } = default!;
    public string ZipCode { get; } = default!;

    #endregion
    
    #region Ctors

    private Address(string firstName, string lastName, string emailAddress, string addressLine, string country, string state,
        string zipCode)
    {
        this.FirstName = firstName;
        this.LastName = lastName;
        this.EmailAddress = emailAddress;
        this.AddressLine = addressLine;
        this.Country = country;
        this.State = state;
        this.ZipCode = zipCode;
    }

    #endregion

    #region Factories

    public static Address Of(string firstName, string lastName, string emailAddress, string addressLine
        , string country, string state, string zipCode)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(emailAddress);
        ArgumentException.ThrowIfNullOrWhiteSpace(addressLine);

        return new Address(firstName, lastName, emailAddress, addressLine, country, state, zipCode);
    }
    #endregion
}