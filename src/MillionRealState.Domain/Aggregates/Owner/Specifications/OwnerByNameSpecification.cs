using MillionRealState.Domain.SeedWork.Models;
using System.Linq.Expressions;

namespace MillionRealState.Domain.Aggregates.Owner.Specifications
{
    public class OwnerByNameSpecification : Specification<OwnerAggregate>
    {
        private readonly string? _name;

        public OwnerByNameSpecification(string? name)
        {
            _name = name;
        }

        public override Expression<Func<OwnerAggregate, bool>> Criteria =>
            string.IsNullOrWhiteSpace(_name) ? o => true : o => o.Name.Contains(_name);
    }
}
