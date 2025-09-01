namespace MillionRealState.Domain.Aggregates.Owner
{
    public class AddressValueObject : IEquatable<AddressValueObject>
    {
        public string Street { get; }
        public string City { get; }
        public string State { get; }
        public string ZipCode { get; }
        public string Country { get; }

        private AddressValueObject() { } // EF Core

        public AddressValueObject(string street, string city, string state, string zipCode, string country)
        {
            if (string.IsNullOrWhiteSpace(street)) throw new ArgumentException("Street cannot be empty");
            if (string.IsNullOrWhiteSpace(city)) throw new ArgumentException("City cannot be empty");

            Street = street;
            City = city;
            State = state;
            ZipCode = zipCode;
            Country = country;
        }

        public bool Equals(AddressValueObject? other)
        {
            if (other is null) return false;
            return Street == other.Street &&
                   City == other.City &&
                   State == other.State &&
                   ZipCode == other.ZipCode &&
                   Country == other.Country;
        }

        public override bool Equals(object? obj) => Equals(obj as AddressValueObject);
        public override int GetHashCode() => HashCode.Combine(Street, City, State, ZipCode, Country);
    }
}
