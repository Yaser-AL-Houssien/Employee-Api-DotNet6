namespace Employee.Core.DTOs.Employee
{
    public class EmployeeCreationDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public decimal Sallary { get; set; }
    }
}
