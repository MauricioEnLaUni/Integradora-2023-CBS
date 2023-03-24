using Fictichos.Constructora.Utilities;

namespace Fictichos.Constructora.Dto;

public class RolesUpdateResultDto : Enumeration<RolesUpdateResultDto>
{
    public static readonly RolesUpdateResultDto RolesSuccess
        = new(1, "RolesSuccess");
    public static readonly RolesUpdateResultDto RolesNotFound
        = new(2, "RolesNotFound");

    private RolesUpdateResultDto(int value, string name)
        : base(value, name) { }
}