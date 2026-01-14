using MillionRealState.Domain.SeedWork.Models;
using System.Linq.Expressions;

namespace MillionRealState.Domain.Aggregates.Property.Specifications
{
    public class PropertyByPriceRangeSpecification : Specification<PropertyAggregate>
    {
        private readonly decimal? _minPrice;
        private readonly decimal? _maxPrice;

        public PropertyByPriceRangeSpecification(decimal? minPrice, decimal? maxPrice)
        {
            _minPrice = minPrice;
            _maxPrice = maxPrice;
        }

        public override Expression<Func<PropertyAggregate, bool>> Criteria =>
            p => (!_minPrice.HasValue || p.Price >= _minPrice.Value)
              && (!_maxPrice.HasValue || p.Price <= _maxPrice.Value);
    }
}