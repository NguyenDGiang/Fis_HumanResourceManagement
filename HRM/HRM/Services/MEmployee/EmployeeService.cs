using HRM.Entities;
using HRM.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrueSight.Common;

namespace HRM.Services.MEmployee
{
    public interface IEmployeeService : IServiceScoped
    {
        Task<int> Count(EmployeeFilter EmployeeFilter);
        Task<List<Employee>> List(EmployeeFilter EmployeeFilter);
        Task<Employee> Get(long Id);
        Task<Employee> Create(Employee Employee);
        Task<Employee> Update(Employee Employee);
        Task<Employee> Delete(Employee Employee);
        EmployeeFilter ToFilter(EmployeeFilter EmployeeFilter);
    }

    public class EmployeeService : IEmployeeService
    {
        private IUOW UOW;
        private IEmployeeValidator EmployeeValidator;
        public EmployeeService(IUOW UOW, IEmployeeValidator EmployeeValidator)
        {
            this.UOW = UOW;
            this.EmployeeValidator = EmployeeValidator;
        }
        public async Task<int> Count(EmployeeFilter EmployeeFilter)
        {
            int result = await UOW.EmployeeRepository.Count(EmployeeFilter);
            return result;
        }

        public async Task<Employee> Create(Employee Employee)
        {
            if (!await EmployeeValidator.Create(Employee))
                return Employee;
            await UOW.EmployeeRepository.Create(Employee);
            List<Employee> Employees = await UOW.EmployeeRepository.List(new List<long> { Employee.Id });
            Employee = Employees.FirstOrDefault(); 
            return Employee;
        }

        public async Task<Employee> Delete(Employee Employee)
        {
            if (!await EmployeeValidator.Delete(Employee))
                return Employee;
            List<Employee> Employees = await UOW.EmployeeRepository.List(new List<long> { Employee.Id });
            Employee = Employees.FirstOrDefault();
            return Employee;
        }

        public async Task<Employee> Get(long Id)
        {
            Employee Employee = await UOW.EmployeeRepository.Get(Id);
            if (Employee == null)
                return null;
            return Employee;
        }

        public async Task<List<Employee>> List(EmployeeFilter EmployeeFilter)
        {
            List<Employee> Employees = await UOW.EmployeeRepository.List(EmployeeFilter);
            return Employees;
        }
        public EmployeeFilter ToFilter(EmployeeFilter filter)
        {

            return filter;
        }

        public async Task<Employee> Update(Employee Employee)
        {
            if (!await EmployeeValidator.Update(Employee))
                return Employee;
            var oldData = await UOW.EmployeeRepository.Get(Employee.Id);
            await UOW.EmployeeRepository.Update(Employee);
            List<Employee> Employees = await UOW.EmployeeRepository.List(new List<long> { Employee.Id });
            Employee = Employees.FirstOrDefault();
            return Employee;

        }
    }
}
