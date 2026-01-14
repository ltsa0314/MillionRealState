using MillionRealState.Domain.Aggregates.Owner;
using MillionRealState.Domain.Aggregates.Owner.Specifications;
using MillionRealState.Domain.SeedWork.Models;
using System.Linq.Expressions;

public class OwnerByFilterPagedSpecification : Specification<OwnerAggregate>
{
    public OwnerByFilterPagedSpecification(OwnerFilter filter)
    {
        var nameSpec = new OwnerByNameSpecification(filter.Name);
        var citySpec = new OwnerByCitySpecification(filter.City);
        var birthdaySpec = new OwnerByBirthdayRangeSpecification(filter.Birthday);

        // Combina todas las especificaciones con AND
        var combinedSpec = nameSpec.And(citySpec).And(birthdaySpec);

        Criteria = combinedSpec.Criteria;
        OrderBy = o => o.Name;
        Take = filter.PageSize;
        Skip = (filter.Page - 1) * filter.PageSize;
    }

    public override Expression<Func<OwnerAggregate, bool>> Criteria { get; }
}