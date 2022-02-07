using HRM.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrueSight.Common;

namespace HRM.Services.MEmployee
{
    public interface IEmployeeService : IServiceScoped
    {
        Task<int> Count(EmployeeFilter AppUserFilter);
        Task<List<Employee>> List(EmployeeFilter EmployeeFilter);
        Task<Employee> Get(long Id);
    }

    public class EmployeeService : IEmployeeService
    {
        public Task<int> Count(EmployeeFilter AppUserFilter)
        {
            throw new System.NotImplementedException();
        }

        public Task<Employee> Get(long Id)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Employee>> List(EmployeeFilter EmployeeFilter)
        {
            throw new System.NotImplementedException();
        }
    }
}
