namespace Employee.Core.DTOs.Employee
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public decimal Sallary { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
