using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BuildingBlocks.Behaviors;

public class ValidationPipeLineBehavior<TRequest, TResponse>
    :IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Results
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    public ValidationPipeLineBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }
    public async Task<TResponse> Handle(TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
      if (!_validators.Any())
        {
            return await next();
        }
        Error[]errors = _validators
            .Select(validator => validator.Validate(request))
            .SelectMany(validationResult => validationResult.Errors)
            .Where(validationFailure => validationFailure is not null)
            .Select(failure => new Error(failure.ErrorMessage, failure.PropertyName))
            .Distinct()
            .ToArray();
        if (errors.Any())
        { 
          return CreateValidationResult<TResponse>(errors);
        }
        return await next();
    }
    private static TResult CreateValidationResult<TResult>(Error[] errors)
        where TResult : Results
    {
       if(typeof(TResult) == typeof(Results))
        {
            return (ValidationResult.WithErrors(errors) as TResult)!;
        }
       object ValidationResult = typeof(ValidationResult<>)
            .GetGenericTypeDefinition()
            .MakeGenericType(typeof(TResult).GetGenericArguments[0])
            .GetMethod(nameof(ValidationResult.WithErrors))
            .Invoke(null, new object?[] { errors });
        return (TResult)validationResult;
    }

