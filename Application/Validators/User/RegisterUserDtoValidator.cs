using Application.DTOs.User;
using FluentValidation;
using Application.Helpers;

namespace Application.Validators.User
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(ValidationMessagesHelper.GetMessage("NameRequired"))
                .MaximumLength(100).WithMessage(ValidationMessagesHelper.GetMessage("FieldLength100"))
                .Matches(@"^[a-zA-ZÀ-ÿ\s]+$").WithMessage(ValidationMessagesHelper.GetMessage("NameRequired"));

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(ValidationMessagesHelper.GetMessage("EmailRequired"))
                .EmailAddress().WithMessage(ValidationMessagesHelper.GetMessage("InvalidEmail"))
                .MaximumLength(255).WithMessage(ValidationMessagesHelper.GetMessage("FieldLength200"));

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage(ValidationMessagesHelper.GetMessage("PhoneRequired"))
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage(ValidationMessagesHelper.GetMessage("PhoneFormat"));

            RuleFor(x => x.ClerkId)
                .NotEmpty().WithMessage(ValidationMessagesHelper.GetMessage("ClerkIdRequired"));

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage(ValidationMessagesHelper.GetMessage("AddressRequired"))
                .MaximumLength(200).WithMessage(ValidationMessagesHelper.GetMessage("FieldLength200"));

            RuleFor(x => x.Neighborhood)
                .NotEmpty().WithMessage(ValidationMessagesHelper.GetMessage("NeighborhoodRequired"))
                .MaximumLength(100).WithMessage(ValidationMessagesHelper.GetMessage("FieldLength100"));

            RuleFor(x => x.CEP)
                .NotEmpty().WithMessage(ValidationMessagesHelper.GetMessage("CEPRequired"))
                .Matches(@"^\d{5}-?\d{3}$").WithMessage(ValidationMessagesHelper.GetMessage("CEPFormat"));

            RuleFor(x => x.State)
                .NotEmpty().WithMessage(ValidationMessagesHelper.GetMessage("StateRequired"))
                .MaximumLength(100).WithMessage(ValidationMessagesHelper.GetMessage("FieldLength100"));

            RuleFor(x => x.City)
                .NotEmpty().WithMessage(ValidationMessagesHelper.GetMessage("CityRequired"))
                .MaximumLength(100).WithMessage(ValidationMessagesHelper.GetMessage("FieldLength100"));

            RuleFor(x => x.Number)
                .NotEmpty().WithMessage(ValidationMessagesHelper.GetMessage("NumberRequired"))
                .MaximumLength(10).WithMessage(ValidationMessagesHelper.GetMessage("FieldLength10"));

            RuleFor(x => x.Provider)
                .NotEmpty().WithMessage(ValidationMessagesHelper.GetMessage("ProviderRequired"));
        }
    }
} 