using Application.Interfaces.Services;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Services
{
    public class ValidationService : IValidationService
    {
        private readonly IServiceProvider _serviceProvider;

        public ValidationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<ValidationResult> ValidateAsync<T>(T model)
        {
            var validator = _serviceProvider.GetService<IValidator<T>>();
            if (validator == null)
            {
                // If no validator is found, return a successful result
                return new ValidationResult();
            }

            return await validator.ValidateAsync(model);
        }

        public bool IsValid(ValidationResult result)
        {
            return result.IsValid;
        }

        public List<string> GetErrors(ValidationResult result)
        {
            return result.Errors.Select(e => e.ErrorMessage).ToList();
        }
    }
} 