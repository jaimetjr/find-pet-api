using Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

internal static class SpecificationEvaluator
{
    public static IQueryable<T> GetQuery<T>(IQueryable<T> inputQuery, ISpecification<T>? spec)
        where T : class
    {
        var query = inputQuery;

        if (spec is null)
            return query;

        // Apply AsNoTracking if specified
        if (spec.AsNoTracking)
            query = query.AsNoTracking();

        // Apply SplitQuery if specified
        if (spec.IsSplitQuery)
            query = query.AsSplitQuery();

        // Apply criteria (where clause)
        if (spec.Criteria is not null)
            query = query.Where(spec.Criteria);

        // Apply includes
        foreach (var include in spec.Includes)
            query = query.Include(include);

        // Apply ordering
        if (spec.OrderByExpressions.Count > 0)
        {
            IOrderedQueryable<T>? orderedQuery = null;
            for (int i = 0; i < spec.OrderByExpressions.Count; i++)
            {
                if (i == 0)
                    orderedQuery = query.OrderBy(spec.OrderByExpressions[i]);
                else
                    orderedQuery = orderedQuery!.ThenBy(spec.OrderByExpressions[i]);
            }
            if (orderedQuery != null)
                query = orderedQuery;
        }

        if (spec.OrderByDescendingExpressions.Count > 0)
        {
            IOrderedQueryable<T>? orderedQuery = null;
            if (spec.OrderByExpressions.Count > 0)
            {
                // If we already have OrderBy, use ThenByDescending
                orderedQuery = query as IOrderedQueryable<T>;
                for (int i = 0; i < spec.OrderByDescendingExpressions.Count; i++)
                {
                    orderedQuery = orderedQuery!.ThenByDescending(spec.OrderByDescendingExpressions[i]);
                }
            }
            else
            {
                // Start with OrderByDescending
                for (int i = 0; i < spec.OrderByDescendingExpressions.Count; i++)
                {
                    if (i == 0)
                        orderedQuery = query.OrderByDescending(spec.OrderByDescendingExpressions[i]);
                    else
                        orderedQuery = orderedQuery!.ThenByDescending(spec.OrderByDescendingExpressions[i]);
                }
            }
            if (orderedQuery != null)
                query = orderedQuery;
        }

        // Apply pagination
        if (spec.Skip.HasValue)
            query = query.Skip(spec.Skip.Value);

        if (spec.Take.HasValue)
            query = query.Take(spec.Take.Value);

        return query;
    }
}
