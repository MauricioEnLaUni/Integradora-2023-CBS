using Fictichos.Constructora.Models;

namespace Fictichos.Constructora.DTOs
{
    public class ScheduleInfoDTO
    {
        public string Name { get; set; }
        public List<TimeSpan> Hours { get; set; }
        public AddressInfoDTO? Location { get; set; }
    }
}