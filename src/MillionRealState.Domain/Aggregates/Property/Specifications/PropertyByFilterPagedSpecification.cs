using MillionRealState.Domain.Aggregates.Property;
using MillionRealState.Domain.Aggregates.Property.Specifications;
using MillionRealState.Domain.SeedWork.Models;
using System.Linq.Expressions;

public class PropertyByFilterPagedSpecification : Specification<PropertyAggregate>
{
    public PropertyByFilterPagedSpecification(PropertyFilter filter)
    {
        var ownerSpec = new PropertyByOwnerSpecification(filter.IdOwner);
        var citySpec = new PropertyByCitySpecification(filter.City);
        var priceSpec = new PropertyByPriceRangeSpecification(filter.PriceMin, filter.PriceMax);
        var yearSpec = new PropertyByYearRangeSpecification(filter.YearFrom, filter.YearTo);
        var textSpec = new PropertyByTextSpecification(filter.Text);

        var combinedSpec = ownerSpec
            .And(citySpec)
            .And(priceSpec)
            .And(yearSpec)
            .And(textSpec);

        Criteria = combinedSpec.Criteria;
        OrderBy = p => p.Name;
        Take = filter.PageSize;
        Skip = (filter.Page - 1) * filter.PageSize;
    }

    public override Expression<Func<PropertyAggregate, bool>> Criteria { get; }
}