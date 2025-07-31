using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Pet
{
    public class CreatePetImagesDto
    {
        public string UserId { get; set; } = default!;
        public Guid PetId { get; set; } = default!;
        public List<string> Images { get; set; } = new List<string>();
    }
}
