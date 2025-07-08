using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public sealed class PetImages : Entity
    {
        public string UserId { get; private set; }
        public Guid PetId { get; private set; }
        public string ImageUrl { get; private set; }

        public Pet Pet { get; private set; }

        public PetImages() { }

        public PetImages(string userId, string imageUrl, Guid petId)
        {
            UserId = userId;
            ImageUrl = imageUrl;
            PetId = petId;
        }
    }
}
