using MillionRealState.Domain.SeedWork.Models;

namespace MillionRealState.Domain.Aggregates.Owner
{
    public class OwnerAggregate : AggregateRoot
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public AddressValueObject Address { get; private set; }
        public string? Photo { get; private set; }
        public DateTime Birthday { get; private set; }

        private OwnerAggregate() { } // EF Core

        public OwnerAggregate(Guid id, string name, AddressValueObject address, string? photo, DateTime birthday)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.");

            if (address is null)
                throw new ArgumentNullException(nameof(address));

            if (birthday > DateTime.UtcNow)
                throw new ArgumentException("Birthday cannot be a future date.");

            Id = id;
            Name = name;
            Address = address ?? throw new ArgumentNullException(nameof(address));
            Photo = photo;
            Birthday = birthday;
        }

        public void UpdateAddress(AddressValueObject newAddress)
        {
            if (newAddress is null) throw new ArgumentNullException(nameof(newAddress));
            Address = newAddress;
        }

        public void UpdatePhoto(string photoUrl)
        {
            Photo = photoUrl;
        }
    }
}
