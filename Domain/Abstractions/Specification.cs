using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Domain.Abstractions;

public abstract class Specification<T> : ISpecification<T>
{
    public Expression<Func<T, bool>>? Criteria { get; protected set; }

    public List<Expression<Func<T, object>>> IncludesInternal { get; } = [];
    public IReadOnlyList<Expression<Func<T, object>>> Includes => IncludesInternal;
    
    public List<Expression<Func<T, object>>> OrderByExpressionsInternal { get; } = [];
    public IReadOnlyList<Expression<Func<T, object>>> OrderByExpressions => OrderByExpressionsInternal;
    
    public List<Expression<Func<T, object>>> OrderByDescendingExpressionsInternal { get; } = [];
    public IReadOnlyList<Expression<Func<T, object>>> OrderByDescendingExpressions => OrderByDescendingExpressionsInternal;
    
    public int? Skip { get; protected set; }
    public int? Take { get; protected set; }
    public bool AsNoTracking { get; protected set; }
    public bool IsSplitQuery { get; protected set; }

    protected void AddInclude(Expression<Func<T, object>> includeExpression) => IncludesInternal.Add(includeExpression);
    
    protected void AddOrderBy(Expression<Func<T, object>> orderByExpression) => OrderByExpressionsInternal.Add(orderByExpression);
    
    protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression) => OrderByDescendingExpressionsInternal.Add(orderByDescendingExpression);
    
    protected void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
    }
    
    protected void ApplyNoTracking() => AsNoTracking = true;
    
    protected void ApplySplitQuery() => IsSplitQuery = true;

    // Composition methods
    public Specification<T> And(ISpecification<T> other)
    {
        var combined = new AndSpecification<T>(this, other);
        return combined;
    }

    public Specification<T> Or(ISpecification<T> other)
    {
        var combined = new OrSpecification<T>(this, other);
        return combined;
    }

    public Specification<T> Not()
    {
        return new NotSpecification<T>(this);
    }
}

// Composition specifications
internal class AndSpecification<T> : Specification<T>
{
    public AndSpecification(ISpecification<T> left, ISpecification<T> right)
    {
        if (left.Criteria != null && right.Criteria != null)
        {
            var parameter = Expression.Parameter(typeof(T));
            var leftBody = ReplaceParameter(left.Criteria.Body, left.Criteria.Parameters[0], parameter);
            var rightBody = ReplaceParameter(right.Criteria.Body, right.Criteria.Parameters[0], parameter);
            var combined = Expression.AndAlso(leftBody, rightBody);
            Criteria = Expression.Lambda<Func<T, bool>>(combined, parameter);
        }
        else if (left.Criteria != null)
        {
            Criteria = left.Criteria;
        }
        else if (right.Criteria != null)
        {
            Criteria = right.Criteria;
        }

        IncludesInternal.AddRange(left.Includes);
        IncludesInternal.AddRange(right.Includes);
        OrderByExpressionsInternal.AddRange(left.OrderByExpressions);
        OrderByExpressionsInternal.AddRange(right.OrderByExpressions);
        OrderByDescendingExpressionsInternal.AddRange(left.OrderByDescendingExpressions);
        OrderByDescendingExpressionsInternal.AddRange(right.OrderByDescendingExpressions);
    }

    private static Expression ReplaceParameter(Expression expression, ParameterExpression oldParam, ParameterExpression newParam)
    {
        return new ParameterReplacer(oldParam, newParam).Visit(expression);
    }

    private class ParameterReplacer : ExpressionVisitor
    {
        private readonly ParameterExpression _oldParam;
        private readonly ParameterExpression _newParam;

        public ParameterReplacer(ParameterExpression oldParam, ParameterExpression newParam)
        {
            _oldParam = oldParam;
            _newParam = newParam;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return node == _oldParam ? _newParam : base.VisitParameter(node);
        }
    }
}

internal class OrSpecification<T> : Specification<T>
{
    public OrSpecification(ISpecification<T> left, ISpecification<T> right)
    {
        if (left.Criteria != null && right.Criteria != null)
        {
            var parameter = Expression.Parameter(typeof(T));
            var leftBody = ReplaceParameter(left.Criteria.Body, left.Criteria.Parameters[0], parameter);
            var rightBody = ReplaceParameter(right.Criteria.Body, right.Criteria.Parameters[0], parameter);
            var combined = Expression.OrElse(leftBody, rightBody);
            Criteria = Expression.Lambda<Func<T, bool>>(combined, parameter);
        }
        else if (left.Criteria != null)
        {
            Criteria = left.Criteria;
        }
        else if (right.Criteria != null)
        {
            Criteria = right.Criteria;
        }

        IncludesInternal.AddRange(left.Includes);
        IncludesInternal.AddRange(right.Includes);
        OrderByExpressionsInternal.AddRange(left.OrderByExpressions);
        OrderByExpressionsInternal.AddRange(right.OrderByExpressions);
        OrderByDescendingExpressionsInternal.AddRange(left.OrderByDescendingExpressions);
        OrderByDescendingExpressionsInternal.AddRange(right.OrderByDescendingExpressions);
    }

    private static Expression ReplaceParameter(Expression expression, ParameterExpression oldParam, ParameterExpression newParam)
    {
        return new ParameterReplacer(oldParam, newParam).Visit(expression);
    }

    private class ParameterReplacer : ExpressionVisitor
    {
        private readonly ParameterExpression _oldParam;
        private readonly ParameterExpression _newParam;

        public ParameterReplacer(ParameterExpression oldParam, ParameterExpression newParam)
        {
            _oldParam = oldParam;
            _newParam = newParam;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return node == _oldParam ? _newParam : base.VisitParameter(node);
        }
    }
}

internal class NotSpecification<T> : Specification<T>
{
    public NotSpecification(ISpecification<T> specification)
    {
        if (specification.Criteria != null)
        {
            Criteria = Expression.Lambda<Func<T, bool>>(
                Expression.Not(specification.Criteria.Body),
                specification.Criteria.Parameters);
        }

        IncludesInternal.AddRange(specification.Includes);
        OrderByExpressionsInternal.AddRange(specification.OrderByExpressions);
        OrderByDescendingExpressionsInternal.AddRange(specification.OrderByDescendingExpressions);
    }
}
