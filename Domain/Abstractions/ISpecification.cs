using System.Linq.Expressions;

namespace Domain.Abstractions;

public interface ISpecification<T>
{
    Expression<Func<T, bool>>? Criteria { get; }
    IReadOnlyList<Expression<Func<T, object>>> Includes { get; }
    IReadOnlyList<Expression<Func<T, object>>> OrderByExpressions { get; }
    IReadOnlyList<Expression<Func<T, object>>> OrderByDescendingExpressions { get; }
    int? Skip { get; }
    int? Take { get; }
    bool AsNoTracking { get; }
    bool IsSplitQuery { get; }
}
