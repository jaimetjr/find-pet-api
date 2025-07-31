using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Pet
{
    public class PetImagesDto
    {
        public Guid Id { get; set; } = default!;
        public string ImageUrl { get; set; } = default!;
    }
}
