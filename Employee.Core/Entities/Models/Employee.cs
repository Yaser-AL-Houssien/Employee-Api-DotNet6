namespace Employee.Core.Entities.Models
{
    public class Employee:BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public decimal Sallary { get; set; }

    }
}
