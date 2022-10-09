using Microsoft.AspNetCore.Identity;
using Employee.Core.Constants;
using Employee.Core.Interfaces;

namespace Employee.EF.Seeding;
public class DbInitializer : IDbInitializer
{
    private ApplicationDbContext _db;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITokenHandler _jwt;


    public DbInitializer(
        ApplicationDbContext db,
        RoleManager<IdentityRole> roleManager,
        UserManager<ApplicationUser> userManager, ITokenHandler jwt)
    {
        _db = db;
        _roleManager = roleManager;
        _userManager = userManager;
        _jwt = jwt;
    }

    public void Initialize()
    {
        //check migrations if they are not applied, no need to updata-database command again
        try
        {
            if (_db.Database.GetPendingMigrations().Count() > 0)
            {
                _db.Database.Migrate();
            }
        }
        catch (Exception)
        {
        }

        //create roles if they are not created
        if (!_roleManager.RoleExistsAsync(SD.Role_Admin).GetAwaiter().GetResult())
        {
            _seedRoles();
            //if roles are not created, then we will create admin user as well
            _createAdmin();
        }
        _createEmployees();
        return;
    }

    //----------------Helper Methods------------------------------------
    private void _seedRoles()
    {
        _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
        _roleManager.CreateAsync(new IdentityRole(SD.Role_User)).GetAwaiter().GetResult();
    }

    private void _createAdmin()
    {
        _userManager.CreateAsync(new ApplicationUser
        {
            //temprory magic data
            UserName = "yaser",
            Email = "admin@test.com",
            FirstName = "yaser",
            LastName = "yaser",
        }, "Admin@123").GetAwaiter().GetResult();

        ApplicationUser user = _db.Users.FirstOrDefault(u => u.Email == "admin@test.com");

        _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();

        _jwt.CreateJwtToken(user);
    }
    private void _createEmployees()
    {
        if (_db.Employees.Count() <= 0)
        {
            _db.Employees.AddRangeAsync(
            new Core.Entities.Models.Employee { Name = "Ahmad", Email ="ahmad@gmail.com",Phone="+971 568 513 711",Address="Dubai, Al Rigga",Sallary=Convert.ToDecimal(6000.0), CreatedAt = DateTime.UtcNow },
            new Core.Entities.Models.Employee { Name = "Hasan", Email ="hasan@gmail.com",Phone="+971 568 513 712",Address="Dubai, Al Dera",Sallary=Convert.ToDecimal(6500.0), CreatedAt = DateTime.UtcNow },
            new Core.Entities.Models.Employee { Name = "Omar", Email ="omar@gmail.com",Phone="+971 568 513 714",Address="Dubai, Abou Hail",Sallary=Convert.ToDecimal(5000), CreatedAt = DateTime.UtcNow },
            new Core.Entities.Models.Employee { Name = "Fahad", Email ="fahad@gmail.com",Phone="+971 568 513 715",Address="Dubai, Al Kaouze",Sallary=Convert.ToDecimal(7750), CreatedAt = DateTime.UtcNow }
           

            );
            _db.SaveChanges();

        }

    }
}
