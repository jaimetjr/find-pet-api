using FluentValidation.Results;

namespace Application.Interfaces.Services
{
    public interface IValidationService
    {
        Task<ValidationResult> ValidateAsync<T>(T model);
        bool IsValid(ValidationResult result);
        List<string> GetErrors(ValidationResult result);
    }
} 