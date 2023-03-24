using Fictichos.Constructora.Utilities;

namespace Fictichos.Constructora.Dto;

public class EmailUpdateResultDto : Enumeration<EmailUpdateResultDto>
{
    public static readonly EmailUpdateResultDto EmailAdded
        = new(0, "EmailAdded");
    public static readonly EmailUpdateResultDto EmailRemoved
        = new(1, "EmailRemoved");
    public static readonly EmailUpdateResultDto EmailUpdated
        = new(2, "EmailUpdated");
    public static readonly EmailUpdateResultDto EmailNotFound
        = new(3, "EmailNotFound");
    public static readonly EmailUpdateResultDto EmailBadFormat
        = new(4, "EmailBadFormat");

    private EmailUpdateResultDto(int value, string name)
        : base(value, name) { }
}