using Domain.Abstractions;
using UserEntity = Domain.Entities.User;

namespace Domain.Specifications.User;

public class UserWithExpoTokenSpecification : Specification<UserEntity>
{
    public UserWithExpoTokenSpecification(string excludeClerkId)
    {
        Criteria = u => u.ClerkId != excludeClerkId 
                     && u.Notifications 
                     && !string.IsNullOrEmpty(u.ExpoPushToken);
    }
}

