using Domain.Abstractions;
using UserEntity = Domain.Entities.User;

namespace Domain.Specifications.User;

public class UserByEmailSpecification : Specification<UserEntity>
{
    public UserByEmailSpecification(string email)
    {
        Criteria = u => u.Email == email;
    }

    public UserByEmailSpecification WithProviders()
    {
        AddInclude(u => u.Providers);
        return this;
    }

    public UserByEmailSpecification WithPets()
    {
        AddInclude(u => u.Pets);
        return this;
    }
}

