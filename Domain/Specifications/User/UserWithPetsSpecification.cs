using Domain.Abstractions;
using UserEntity = Domain.Entities.User;

namespace Domain.Specifications.User;

public class UserWithPetsSpecification : Specification<UserEntity>
{
    public UserWithPetsSpecification()
    {
        AddInclude(u => u.Pets);
    }

    public UserWithPetsSpecification WithProviders()
    {
        AddInclude(u => u.Providers);
        return this;
    }

    public UserWithPetsSpecification WithNotifications()
    {
        AddInclude(u => u.Notification);
        return this;
    }
}

