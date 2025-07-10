using Application.DTOs.User;
using FluentValidation;
using Application.Helpers;

namespace Application.Validators.User
{
    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator()
        {
            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage(ValidationMessagesHelper.GetMessage("PhoneRequired"))
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage(ValidationMessagesHelper.GetMessage("PhoneFormat"));

            RuleFor(x => x.Bio)
                .MaximumLength(500).WithMessage(ValidationMessagesHelper.GetMessage("FieldLength500"));

            RuleFor(x => x.CPF)
                .NotEmpty().WithMessage(ValidationMessagesHelper.GetMessage("CPFRequired"))
                .Matches(@"^\d{3}\.?\d{3}\.?\d{3}-?\d{2}$").WithMessage(ValidationMessagesHelper.GetMessage("CPFFormat"));

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

            RuleFor(x => x.Complement)
                .MaximumLength(50).WithMessage(ValidationMessagesHelper.GetMessage("FieldLength50"));

            RuleFor(x => x.BirthDate)
                .LessThan(DateTime.Now).WithMessage(ValidationMessagesHelper.GetMessage("BirthDateInvalid"))
                .GreaterThan(DateTime.Now.AddYears(-120)).WithMessage(ValidationMessagesHelper.GetMessage("BirthDateRange"));
        }
    }
} 