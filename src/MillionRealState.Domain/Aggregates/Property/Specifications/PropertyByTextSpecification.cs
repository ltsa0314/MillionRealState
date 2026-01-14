using MillionRealState.Domain.SeedWork.Models;
using System;
using System.Linq.Expressions;

namespace MillionRealState.Domain.Aggregates.Property.Specifications
{
    public class PropertyByTextSpecification : Specification<PropertyAggregate>
    {
        private readonly string? _text;

        public PropertyByTextSpecification(string? text)
        {
            _text = text;
        }

        public override Expression<Func<PropertyAggregate, bool>> Criteria =>
            string.IsNullOrWhiteSpace(_text)
                ? p => true
                : p => p.Name.Contains(_text)
                    || p.Address.City.Contains(_text);
    }
}