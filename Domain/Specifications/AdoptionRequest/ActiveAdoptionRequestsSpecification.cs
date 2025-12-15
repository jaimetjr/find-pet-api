using Domain.Abstractions;
using AdoptionRequestEntity = Domain.Entities.AdoptionRequest;
using Domain.Enums;
using System.Linq.Expressions;

namespace Domain.Specifications.AdoptionRequest;

public class ActiveAdoptionRequestsSpecification : Specification<AdoptionRequestEntity>
{
    public ActiveAdoptionRequestsSpecification()
    {
        Criteria = ar => ar.Active;
    }

    public ActiveAdoptionRequestsSpecification WithPet()
    {
        AddInclude(ar => ar.Pet);
        return this;
    }

    public ActiveAdoptionRequestsSpecification WithAdopter()
    {
        AddInclude(ar => ar.Adopter);
        return this;
    }

    public ActiveAdoptionRequestsSpecification WithOwner()
    {
        AddInclude(ar => ar.Owner);
        return this;
    }

    public ActiveAdoptionRequestsSpecification ByOwner(string ownerClerkId)
    {
        var existingCriteria = Criteria;
        Expression<Func<AdoptionRequestEntity, bool>> ownerCriteria = ar => ar.OwnerClerkId == ownerClerkId;
        
        if (existingCriteria != null)
        {
            // Combine existing criteria with owner criteria using AND
            var parameter = Expression.Parameter(typeof(AdoptionRequestEntity));
            var existingBody = ReplaceParameter(existingCriteria.Body, existingCriteria.Parameters[0], parameter);
            var ownerBody = ReplaceParameter(ownerCriteria.Body, ownerCriteria.Parameters[0], parameter);
            var combined = Expression.AndAlso(existingBody, ownerBody);
            Criteria = Expression.Lambda<Func<AdoptionRequestEntity, bool>>(combined, parameter);
        }
        else
        {
            Criteria = ar => ar.Active && ar.OwnerClerkId == ownerClerkId;
        }
        return this;
    }

    public ActiveAdoptionRequestsSpecification ByAdopter(string adopterClerkId)
    {
        var existingCriteria = Criteria;
        Expression<Func<AdoptionRequestEntity, bool>> adopterCriteria = ar => ar.AdopterClerkId == adopterClerkId;
        
        if (existingCriteria != null)
        {
            // Combine existing criteria with adopter criteria using AND
            var parameter = Expression.Parameter(typeof(AdoptionRequestEntity));
            var existingBody = ReplaceParameter(existingCriteria.Body, existingCriteria.Parameters[0], parameter);
            var adopterBody = ReplaceParameter(adopterCriteria.Body, adopterCriteria.Parameters[0], parameter);
            var combined = Expression.AndAlso(existingBody, adopterBody);
            Criteria = Expression.Lambda<Func<AdoptionRequestEntity, bool>>(combined, parameter);
        }
        else
        {
            Criteria = ar => ar.Active && ar.AdopterClerkId == adopterClerkId;
        }
        return this;
    }

    public ActiveAdoptionRequestsSpecification WithStatuses(params AdoptionRequestStatus[] statuses)
    {
        if (statuses.Length > 0)
        {
            var existingCriteria = Criteria;
            Expression<Func<AdoptionRequestEntity, bool>> statusCriteria = ar => statuses.Contains(ar.Status);
            
            if (existingCriteria != null)
            {
                // Combine existing criteria with status criteria using AND
                var parameter = Expression.Parameter(typeof(AdoptionRequestEntity));
                var existingBody = ReplaceParameter(existingCriteria.Body, existingCriteria.Parameters[0], parameter);
                var statusBody = ReplaceParameter(statusCriteria.Body, statusCriteria.Parameters[0], parameter);
                var combined = Expression.AndAlso(existingBody, statusBody);
                Criteria = Expression.Lambda<Func<AdoptionRequestEntity, bool>>(combined, parameter);
            }
            else
            {
                Criteria = ar => ar.Active && statuses.Contains(ar.Status);
            }
        }
        return this;
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

    public ActiveAdoptionRequestsSpecification OrderedByCreatedDateDescending()
    {
        AddOrderByDescending(ar => ar.CreatedAt);
        return this;
    }
}

