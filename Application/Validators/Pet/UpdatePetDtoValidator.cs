using Application.DTOs.Pet;
using FluentValidation;
using Application.Helpers;

namespace Application.Validators.Pet
{
    public class UpdatePetDtoValidator : AbstractValidator<UpdatePetDto>
    {
        public UpdatePetDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(ValidationMessagesHelper.GetMessage("PetNameRequired"))
                .MaximumLength(100).WithMessage(ValidationMessagesHelper.GetMessage("PetNameLength"))
                .Matches(@"^[a-zA-ZÀ-ÿ\s]+$").WithMessage(ValidationMessagesHelper.GetMessage("PetNameFormat"));

            RuleFor(x => x.Age)
                .GreaterThan(0).WithMessage(ValidationMessagesHelper.GetMessage("AgeRequired"))
                .LessThanOrEqualTo(30).WithMessage(ValidationMessagesHelper.GetMessage("AgeRange"));

            RuleFor(x => x.Bio)
                .NotEmpty().WithMessage(ValidationMessagesHelper.GetMessage("BioRequired"))
                .MaximumLength(500).WithMessage(ValidationMessagesHelper.GetMessage("FieldLength500"));

            RuleFor(x => x.History)
                .NotEmpty().WithMessage(ValidationMessagesHelper.GetMessage("HistoryRequired"))
                .MaximumLength(1000).WithMessage(ValidationMessagesHelper.GetMessage("FieldLength1000"));

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage(ValidationMessagesHelper.GetMessage("AddressRequired"))
                .MaximumLength(200).WithMessage(ValidationMessagesHelper.GetMessage("FieldLength200"));

            RuleFor(x => x.Neighborhood)
                .NotEmpty().WithMessage(ValidationMessagesHelper.GetMessage("NeighborhoodRequired"))
                .MaximumLength(100).WithMessage(ValidationMessagesHelper.GetMessage("FieldLength100"));

            RuleFor(x => x.City)
                .NotEmpty().WithMessage(ValidationMessagesHelper.GetMessage("CityRequired"))
                .MaximumLength(100).WithMessage(ValidationMessagesHelper.GetMessage("FieldLength100"));

            RuleFor(x => x.State)
                .NotEmpty().WithMessage(ValidationMessagesHelper.GetMessage("StateRequired"))
                .MaximumLength(100).WithMessage(ValidationMessagesHelper.GetMessage("FieldLength100"));

            RuleFor(x => x.CEP)
                .NotEmpty().WithMessage(ValidationMessagesHelper.GetMessage("CEPRequired"))
                .Matches(@"^\d{5}-?\d{3}$").WithMessage(ValidationMessagesHelper.GetMessage("CEPFormat"));

            RuleFor(x => x.Number)
                .NotEmpty().WithMessage(ValidationMessagesHelper.GetMessage("NumberRequired"))
                .MaximumLength(10).WithMessage(ValidationMessagesHelper.GetMessage("FieldLength10"));

            RuleFor(x => x.Type)
                .NotNull().WithMessage(ValidationMessagesHelper.GetMessage("PetTypeRequired"));

            RuleFor(x => x.Type.Id)
                .NotEmpty().WithMessage(ValidationMessagesHelper.GetMessage("PetTypeRequired"));

            RuleFor(x => x.Breed)
                .NotNull().WithMessage(ValidationMessagesHelper.GetMessage("PetBreedRequired"));

            RuleFor(x => x.Breed.Id)
                .NotEmpty().WithMessage(ValidationMessagesHelper.GetMessage("PetBreedRequired"));

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage(ValidationMessagesHelper.GetMessage("UserIdRequired"));
        }
    }
} 