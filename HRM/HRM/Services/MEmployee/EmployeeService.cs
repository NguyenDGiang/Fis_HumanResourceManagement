using HRM.Entities;
using HRM.Repositories;
using System;
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
        public EmployeeService(IUOW UOW,IEmployeeValidator EmployeeValidator)
        {
            this.UOW = UOW;
            this.EmployeeValidator = EmployeeValidator;
        }
        public async Task<int> Count(EmployeeFilter EmployeeFilter)
        {
            try
            {
                 int result = await UOW.EmployeeRepository.Count(EmployeeFilter);
                 return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("IEmployeeService:" + ex.Message);
            }
            return 0;
            
        }

        public async Task<Employee> Create(Employee Employee)
        {
            if (!await EmployeeValidator.Create(Employee))
                return Employee;
            try
            {

                await UOW.EmployeeRepository.Create(Employee);
                List<Employee> Employees = await UOW.EmployeeRepository.List(new List<long> { Employee.Id });
                Employee = Employees.FirstOrDefault();
                return Employee;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("IEmployeeService: " + ex.Message);
            }
            return null;
        }

        public async Task<Employee> Delete(Employee Employee)
        {
            if (!await EmployeeValidator.Delete(Employee))
                return Employee;
            await UOW.EmployeeRepository.Delete(Employee);
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
            try
            {
                var oldData = await UOW.EmployeeRepository.Get(Employee.Id);
                await UOW.EmployeeRepository.Update(Employee);
                List<Employee> Employees = await UOW.EmployeeRepository.List(new List<long> { Employee.Id });
                Employee = Employees.FirstOrDefault();
                return Employee;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("IEmployeeService" + ex.Message);
            }
            return null;

        }
    }
}
