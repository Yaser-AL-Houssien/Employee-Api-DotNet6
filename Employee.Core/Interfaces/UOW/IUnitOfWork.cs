
namespace Employee.Core.Interfaces;
public interface IUnitOfWork
{
    //Register App Repositories

    IBaseRepository<Employee.Core.Entities.Models.Employee> Employee { get; }
    IAuthRepository Auth { get;  }

    //Global Methods
    void Save();
}
