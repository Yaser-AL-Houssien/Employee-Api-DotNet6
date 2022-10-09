using Microsoft.AspNetCore.Identity;
using Employee.Core.Entities.Models;
using Employee.Core.Interfaces;
using Employee.EF;
using Employee.EF.Repositories;
using AutoMapper;

namespace Employee.Services.UOW;
public class UnitOfWork : IUnitOfWork
{
    //The only place can access database 
    private readonly ApplicationDbContext _db;

    public UnitOfWork(ApplicationDbContext db,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        ITokenHandler jwt, IMapper mapper)
    {
        _db = db;

        //Initialize App Repositories
        Employee = new BaseRepository<Employee.Core.Entities.Models.Employee>(_db.Set<Employee.Core.Entities.Models.Employee>());

        Auth = new AuthRepository(userManager, roleManager, jwt, mapper);
    }

    public IBaseRepository<Employee.Core.Entities.Models.Employee> Employee { get; }

    public IAuthRepository Auth { get; private set; }

    public void Save() => _db.SaveChanges();
}
