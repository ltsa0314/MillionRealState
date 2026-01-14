using MillionRealState.Domain.SeedWork.Models;
using System.Linq.Expressions;

namespace MillionRealState.Domain.Aggregates.Owner.Specifications
{
    public class OwnerByBirthdayRangeSpecification : Specification<OwnerAggregate>
    {
        private readonly BirthdayRange? _range;

        public OwnerByBirthdayRangeSpecification(BirthdayRange? range)
        {
            _range = range;
        }

        public override Expression<Func<OwnerAggregate, bool>> Criteria =>
            _range == null ? o => true :
            o => (!_range.From.HasValue || o.Birthday >= _range.From.Value)
              && (!_range.To.HasValue || o.Birthday <= _range.To.Value);
    }
}
