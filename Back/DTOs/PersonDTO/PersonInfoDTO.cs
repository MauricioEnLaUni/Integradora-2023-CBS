namespace Fictichos.Constructora.DTOs
{
    public record PersonInfoDTO
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public ContactInfoDTO Contacts { get; set; }
        public EmployeeInfoDTO Employed { get; set; }
    }
}