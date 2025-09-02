namespace MillionRealState.Application.Features.Owner.Dtos
{
    public sealed record OwnerDto(int IdOwner, string Name, string Address, string Photo);
    public sealed record CreateOwnerDto(string Name, string Address, string Photo);
    public sealed record UpdateOwnerDto(string Name, string Address, string Photo);

}
