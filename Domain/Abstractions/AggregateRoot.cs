using Domain.Entities;

namespace Domain.Abstractions;

public abstract class AggregateRoot : Entity
{
    // Aggregate root marker - inherits from Entity
    // Aggregate roots are the entry points for accessing entities within the aggregate boundary
}

