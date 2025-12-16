using FluentValidation;
using FluentValidation.Results;
using MediatR;
using OpsForge.Domain.Exceptions;

namespace OpsForge.Application.Pipelines;

public sealed class ValidatorBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
{
    private readonly IValidator<TRequest>[] _validators;

    public ValidatorBehavior(IValidator<TRequest>[] validators)
    {
        this._validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request,
        RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        List<ValidationFailure> failures = this._validators
            .Select(v => v.Validate(request))
            .SelectMany(result => result.Errors)
            .Where(error => error != null)
            .ToList();

        if (failures.Any())
        {
            throw new OrderingDomainException(
                $"Command validation Error for type {typeof(TRequest).Name}",
                    new ValidationException("Validation exception", failures));
        }

        TResponse response = await next();
        return response;
    }
}
