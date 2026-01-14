using MillionRealState.Domain.SeedWork.Contracts;
using MillionRealState.Domain.SeedWork.Extensions;
using System.Linq.Expressions;

namespace MillionRealState.Domain.SeedWork.Models
{
    public abstract class Specification<T> : IReadSpecification<T>
    {
        public abstract Expression<Func<T, bool>> Criteria { get; }
        public List<Expression<Func<T, object>>> Includes { get; } = new();
        public Expression<Func<T, object>>? OrderBy { get; protected set; }
        public Expression<Func<T, object>>? OrderByDescending { get; protected set; }
        public int? Take { get; protected set; }
        public int? Skip { get; protected set; }
        public bool IsPagingEnabled => Take.HasValue && Skip.HasValue;

        public Specification<T> And(Specification<T> other)
        {
            return new AndSpecification<T>(this, other);
        }

        public Specification<T> Or(Specification<T> other)
        {
            return new OrSpecification<T>(this, other);
        }
    }

    public class AndSpecification<T> : Specification<T>
    {
        private readonly Specification<T> _left;
        private readonly Specification<T> _right;

        public AndSpecification(Specification<T> left, Specification<T> right)
        {
            _left = left;
            _right = right;
        }

        public override Expression<Func<T, bool>> Criteria =>
            _left.Criteria.And(_right.Criteria);
    }

    public class OrSpecification<T> : Specification<T>
    {
        private readonly Specification<T> _left;
        private readonly Specification<T> _right;

        public OrSpecification(Specification<T> left, Specification<T> right)
        {
            _left = left;
            _right = right;
        }

        public override Expression<Func<T, bool>> Criteria =>
            _left.Criteria.Or(_right.Criteria);
    }
}
