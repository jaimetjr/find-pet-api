using Application.DTOs.User;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Pet
{
    public class PetDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid BreedId { get; set; }
        public PetBreedDto Breed { get; set; }
        public Guid TypeId { get; set; }
        public PetTypeDto Type { get; set; }
        public int Age { get; set; }
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
        public UserDto User { get; set; } = default!;
        public List<PetImagesDto> PetImages { get; set; } = new List<PetImagesDto>();
    }
}
