global using Employee.Core.Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Employee.EF;
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
    {
    }
    public DbSet<Employee.Core.Entities.Models.Employee> Employees { get; set; }

}
