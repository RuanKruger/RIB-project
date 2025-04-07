namespace AcmeSoft.Core.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public int PersonId { get; set; }  // Foreign key property
        public string EmployeeNum { get; set; } = string.Empty;
        public DateTime EmployeeDate { get; set; }
        public DateTime? Terminated { get; set; }

        // Navigation property
        public virtual Person? Person { get; set; }
    }
}