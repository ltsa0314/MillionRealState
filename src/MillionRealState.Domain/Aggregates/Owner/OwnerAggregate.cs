using MillionRealState.Domain.SeedWork.Models;

namespace MillionRealState.Domain.Aggregates.Owner
{
    public class OwnerAggregate : AggregateRoot
    {
        public Guid IdOwner { get; set; }
        public string Name { get; set; }
        public AddressValueObject Address { get; set; }
        public string? Photo { get; set; }
        public DateTime Birthday { get; set; }
        private OwnerAggregate() { }
        // EF Core

        public OwnerAggregate(Guid idOwner, string name, AddressValueObject address, string? photo, DateTime birthday)
        {
            IdOwner = idOwner;
            Name = name;
            Address = address;
            Photo = photo;
            Birthday = birthday;
        }

        public void Update(string name, AddressValueObject address, string? photo, DateTime birthday)
        {
            Name = name;
            Address = address;
            Photo = photo;
            Birthday = birthday;
        }

        public void UpdateAddress(AddressValueObject newAddress)
        {
            Address = newAddress;
        }

        public void UpdatePhoto(string photoUrl)
        {
            Photo = photoUrl;
        }
    }
}
