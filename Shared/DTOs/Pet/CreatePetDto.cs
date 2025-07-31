using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Pet
{
    public class CreatePetDto
    {
        public string UserId { get; set; } = default!;
        public string Name { get; set; } = default!;
        public PetTypeDto Type { get; set; } = default!;
        public PetBreedDto Breed { get; set; } = default!;
        public PetGender Gender { get; set; } = default!;
        public int Age { get; set; } = default!;
        public PetSize Size { get; set; } = default!;
        public string Bio { get; set; } = default!;
        public string History { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string Neighborhood { get; set; } = default!;
        public string CEP { get; set; } = default!;
        public string State { get; set; } = default!;
        public string City { get; set; } = default!;
        public string Number { get; set; } = default!;
        public string? Complement { get; set; } = default!;
        //public List<IFormFile> PetImages { get; set; } = default!;
    }
}
