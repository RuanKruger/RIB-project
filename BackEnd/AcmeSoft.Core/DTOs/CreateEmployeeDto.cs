namespace AcmeSoft.Core.DTOs
{
    public class CreateEmployeeDto
    {
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string EmployeeNum { get; set; } = string.Empty;
        public DateTime EmployeeDate { get; set; }
    }
}