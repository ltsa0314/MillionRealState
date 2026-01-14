using MillionRealState.Domain.SeedWork.Models;
using System;
using System.Linq.Expressions;

namespace MillionRealState.Domain.Aggregates.Property.Specifications
{
    public class PropertyByYearRangeSpecification : Specification<PropertyAggregate>
    {
        private readonly int? _yearFrom;
        private readonly int? _yearTo;

        public PropertyByYearRangeSpecification(int? yearFrom, int? yearTo)
        {
            _yearFrom = yearFrom;
            _yearTo = yearTo;
        }

        public override Expression<Func<PropertyAggregate, bool>> Criteria =>
            p => (!_yearFrom.HasValue || p.Year >= _yearFrom.Value)
              && (!_yearTo.HasValue || p.Year <= _yearTo.Value);
    }
}