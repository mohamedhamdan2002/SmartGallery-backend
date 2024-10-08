﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.Specifications;

public static class SpecificationEvaluator
{
    private static bool hasSelector = false;
    public static IQueryable<TResult> GetQuery<TEntity, TResult>(this IQueryable<TEntity> inputQuery, SpecificationWithResultType<TEntity, TResult> specification) where TEntity : class, IEntity
    {
        hasSelector = true;
        var query = inputQuery.GetQuery<TEntity>(specification);
        return query.Select(specification.Selector);
    }
    public static IQueryable<TEntity> GetQuery<TEntity>(this IQueryable<TEntity> inputQuery, Specification<TEntity> specification) where TEntity : class, IEntity
    {
        var query = inputQuery;

        if (specification is null)
            return query;

        if (specification.Predicate is not null)
        {
            query = query.Where(specification.Predicate);
        }

        query = specification?.Includes.Aggregate(query,
            (currentQuery, newIncludeExpression) => currentQuery.Include(newIncludeExpression));


        if (specification!.OrderBy is not null)
        {
            query = query.OrderBy(specification.OrderBy);
        }

        if (specification.OrderByDescending is not null)
        {
            query = query.OrderByDescending(specification.OrderByDescending);
        }

        if (specification!.IsPaginationEnabled)
        {
            query = query!.Skip(specification.Skip).Take(specification.Take);
        }


        if (specification.AsNoTracking && !hasSelector)
        {
            query = query!.AsNoTracking();
        }
        return query!;
    }
}
