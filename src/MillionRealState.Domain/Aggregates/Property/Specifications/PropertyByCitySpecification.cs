using MillionRealState.Domain.SeedWork.Models;
using System.Linq.Expressions;

namespace MillionRealState.Domain.Aggregates.Property.Specifications
{
    public class PropertyByCitySpecification : Specification<PropertyAggregate>
    {
        private readonly string? _city;

        public PropertyByCitySpecification(string? city)
        {
            _city = city;
        }

        public override Expression<Func<PropertyAggregate, bool>> Criteria =>
            string.IsNullOrWhiteSpace(_city) ? p => true : p => p.Address.City.Contains(_city);
    }
}