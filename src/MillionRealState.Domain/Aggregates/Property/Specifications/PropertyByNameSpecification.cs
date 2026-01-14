using MillionRealState.Domain.SeedWork.Models;
using System.Linq.Expressions;

namespace MillionRealState.Domain.Aggregates.Property.Specifications
{
    public class PropertyByNameSpecification : Specification<PropertyAggregate>
    {
        private readonly string? _name;

        public PropertyByNameSpecification(string? name)
        {
            _name = name;
        }

        public override Expression<Func<PropertyAggregate, bool>> Criteria =>
            string.IsNullOrWhiteSpace(_name) ? p => true : p => p.Name.Contains(_name);
    }
}