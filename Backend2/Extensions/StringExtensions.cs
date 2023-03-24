using System.Net.Mail;

namespace Fictichos.Constructora.Utilities;

public static class StringExtensions
{
    public static bool IsEmailFormatted(this string props)
    {
        try
        {
            MailAddress m = new(props);

            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }
}