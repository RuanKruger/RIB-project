namespace AcmeSoft.Core.DTOs
{
    public class UpdateEmployeeDto
    {
        public int EmployeeId { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string EmployeeNum { get; set; } = string.Empty;
        public DateTime EmployeeDate { get; set; }
        public DateTime? Terminated { get; set; }
    }
}