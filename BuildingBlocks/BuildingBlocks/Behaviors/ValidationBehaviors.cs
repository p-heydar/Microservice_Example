﻿using BuildingBlocks.CQRS.Command;

using FluentValidation;

using MediatR;

using System.Net.Http.Headers;
using System.Windows.Input;

namespace BuildingBlocks.Behaviors;

public sealed class ValidationBehaviors<TRequest, TResponse>
    (IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommand<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        var validationResults =
            await Task.WhenAll(validators
                .Select(x =>
                    x.ValidateAsync(context, cancellationToken)));

        var failures =
            validationResults.Where(failures => failures.Errors.Any())
            .SelectMany(error => error.Errors)
            .ToList();

        if (failures.Any())
        {
            throw new ValidationException(failures);
        }

        return await next();
    }
}
