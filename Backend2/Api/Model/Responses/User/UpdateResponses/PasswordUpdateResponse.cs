using Fictichos.Constructora.Utilities;

namespace Fictichos.Constructora.Dto;

public class PasswordUpdateResultDto : Enumeration<PasswordUpdateResultDto>
{
    public static readonly PasswordUpdateResultDto PasswordSuccess
        = new(0, "PasswordSuccess");
    public static readonly PasswordUpdateResultDto PasswordTooShort
        = new(1, "PasswordTooShort");
    public static readonly PasswordUpdateResultDto PasswordTooLong
        = new(2, "PasswordTooLong");
    public static readonly PasswordUpdateResultDto PasswordRegexFailure
        = new(3, "PasswordRegexFailure");

    private PasswordUpdateResultDto(int value, string name)
        : base(value, name) { }
}