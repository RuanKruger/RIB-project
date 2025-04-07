namespace AcmeSoft.Core.Models
{
    public class Person
    {
        public int PersonId { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }

        // Navigation property
        public virtual ICollection<Employee>? Employees { get; set; }
    }
}