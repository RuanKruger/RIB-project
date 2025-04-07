namespace AcmeSoft.Core.DTOs
{
    public class PersonDto
    {
        public int PersonId { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
    }
}