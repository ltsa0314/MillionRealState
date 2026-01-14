using MillionRealState.Domain.SeedWork.Models;
using System;
using System.Linq.Expressions;

namespace MillionRealState.Domain.Aggregates.Property.Specifications
{
    public class PropertyByOwnerSpecification : Specification<PropertyAggregate>
    {
        private readonly Guid? _idOwner;

        public PropertyByOwnerSpecification(Guid? idOwner)
        {
            _idOwner = idOwner;
        }

        public override Expression<Func<PropertyAggregate, bool>> Criteria =>
            !_idOwner.HasValue ? p => true : p => p.IdOwner == _idOwner.Value;
    }
}