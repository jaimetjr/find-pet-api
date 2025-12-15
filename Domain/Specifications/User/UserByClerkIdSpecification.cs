using Domain.Abstractions;
using UserEntity = Domain.Entities.User;

namespace Domain.Specifications.User;

public class UserByClerkIdSpecification : Specification<UserEntity>
{
    public UserByClerkIdSpecification(string clerkId)
    {
        Criteria = u => u.ClerkId == clerkId;
    }

    public UserByClerkIdSpecification WithProviders()
    {
        AddInclude(u => u.Providers);
        return this;
    }

    public UserByClerkIdSpecification WithPets()
    {
        AddInclude(u => u.Pets);
        return this;
    }

    public UserByClerkIdSpecification WithNotifications()
    {
        AddInclude(u => u.Notification);
        return this;
    }
}

