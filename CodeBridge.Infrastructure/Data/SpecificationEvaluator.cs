using CodeBridge.Domain.Specifications;
using Microsoft.EntityFrameworkCore;

namespace CodeBridge.Infrastructure.Data;

public static class SpecificationEvaluator
{
    public static IQueryable<T> GetQuery<T>(
        IQueryable<T> inputQueryable,
        Specification<T> specification)
        where T : class
    {
        IQueryable<T> queryable = inputQueryable;

        if (specification.Criteria is not null)
        {
            queryable = queryable.Where(specification.Criteria);
        }

        queryable = specification.IncludeExpressions.Aggregate(
            queryable,
            (current, includeExpression) =>
                current.Include(includeExpression));

        if (specification.OrderByExpression is not null)
        {
            queryable = queryable.OrderBy(specification.OrderByExpression);
        }
        else if (specification.OrderByDescendingExpression is not null)
        {
            queryable = queryable.OrderByDescending(specification.OrderByDescendingExpression);
        }

        if (specification.IsPagingEnabled)
        {
            queryable = queryable.Skip(specification.Skip)
                .Take(specification.Take);
        }

        if (specification.AsNoTracking)
        {
            queryable = queryable.AsNoTracking();
        }
        
        return queryable;
    }
}