using System.Linq.Expressions;
using CodeBridge.Domain.Models;

namespace CodeBridge.Domain.Specifications;

public abstract class Specification<T> where T : class
{
    protected Specification(Expression<Func<T, bool>> criteria) =>
        Criteria = criteria;

    protected Specification() {}
    
    public Expression<Func<T, bool>>? Criteria { get; }

    public List<Expression<Func<T, object>>> IncludeExpressions { get; } = new();

    public Expression<Func<T, object>>? OrderByExpression { get; private set; }

    public Expression<Func<T, object>>? OrderByDescendingExpression { get; private set; }

    public int Take { get; private set; }

    public int Skip { get; private set; }

    public bool AsNoTracking { get; private set; } = false;

    public bool IsPagingEnabled { get; private set; } = false;
    
    public void AddInclude(Expression<Func<T, object>> includeExpressions) =>
        IncludeExpressions.Add(includeExpressions);

    public void AddOrderBy(
        Expression<Func<T, object>> orderByExpression) =>
        OrderByExpression = orderByExpression;
    
    public void AddOrderByDescending(
        Expression<Func<T, object>> orderByDescendingExpression) =>
        OrderByDescendingExpression = orderByDescendingExpression;

    public virtual void ApplyPaging(PagingParameters parameters)
    {
        Skip = (parameters.PageNumber - 1) * parameters.PageSize;
        Take = parameters.PageSize;
        IsPagingEnabled = true;
    }

    public virtual void AddAsNoTracking() => AsNoTracking = true;
}