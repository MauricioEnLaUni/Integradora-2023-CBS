using Fictichos.Constructora.Utilities;

namespace Fictichos.Constructora.Dto;

public class ActiveUpdateResultDto : Enumeration<ActiveUpdateResultDto>
{
    public static readonly ActiveUpdateResultDto ActiveSetOff
        = new(1, "ActiveSetOff");
    public static readonly ActiveUpdateResultDto ActiveSetOn
        = new(2, "ActiveSetOn");

    private ActiveUpdateResultDto(int value, string name)
        : base(value, name) { }
}