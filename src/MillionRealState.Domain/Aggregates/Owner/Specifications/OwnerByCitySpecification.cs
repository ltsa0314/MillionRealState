using MillionRealState.Domain.SeedWork.Models;
using System.Linq.Expressions;

namespace MillionRealState.Domain.Aggregates.Owner.Specifications
{
    public class OwnerByCitySpecification : Specification<OwnerAggregate>
    {
        private readonly string? _city;

        public OwnerByCitySpecification(string? city)
        {
            _city = city;
        }

        public override Expression<Func<OwnerAggregate, bool>> Criteria =>
            string.IsNullOrWhiteSpace(_city) ? o => true : o => o.Address.City.Contains(_city);
    }
}
