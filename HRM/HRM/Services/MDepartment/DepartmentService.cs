using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRM.Entities;
using HRM.Repositories;
using TrueSight.Common;

namespace HRM.Services.MDepartment
{
    public interface IDepartmentService : IServiceScoped
    {
        Task<int> Count(DepartmentFilter DepartmentFilter);
        Task<List<Department>> List(DepartmentFilter DepartmentFilter);
        Task<Department> Get(long Id);
        Task<Department> Create(Department Department);
        Task<List<Department>> BulkInsert(List<Department> Departments);
        Task<Department> Update(Department Department);
        Task<Department> Delete(Department Department);
        Task<List<Department>> BulkDelete(List<Department> Departments);
        Task<List<Department>> BulkMerge(List<Department> Departments);
        DepartmentFilter ToFilter(DepartmentFilter DepartmentFilter);
    }
    public class DepartmentService : IDepartmentService
    {
        private IUOW UOW;
        public DepartmentService(IUOW UOW)
        {
            this.UOW = UOW;
        }
        public async Task<List<Department>> BulkDelete(List<Department> Departments)
        {
            try
            {
                await UOW.DepartmentRepository.BulkDelete(Departments);
                List<long> Ids = Departments.Select(x => x.Id).ToList();
                Departments = await UOW.DepartmentRepository.List(Ids);

                return Departments;

            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex);
                throw;
            }
        }

        public Task<List<Department>> BulkInsert(List<Department> Departments)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Department>> BulkMerge(List<Department> Departments)
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> Count(DepartmentFilter DepartmentFilter)
        {
            try
            {
                int result = await UOW.DepartmentRepository.Count(DepartmentFilter);
                return result;
            }
            catch (System.Exception ex)
            {

                System.Console.WriteLine(ex);
            }
            return 0;
        }

        public async Task<Department> Create(Department Department)
        {
            try
            {
                await UOW.DepartmentRepository.Create(Department);
                Department = await UOW.DepartmentRepository.Get(Department.Id);
                return Department;
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public async Task<Department> Delete(Department Department)
        {
            try
            {
                await UOW.DepartmentRepository.Delete(Department);
                var Departments = await UOW.DepartmentRepository.List(new List<long> { Department.Id });
                Department = Departments.FirstOrDefault();
                return Department;
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public async Task<Department> Get(long Id)
        {
            Department Department = await UOW.DepartmentRepository.Get(Id);
            return Department;
        }

        public async Task<List<Department>> List(DepartmentFilter DepartmentFilter)
        {
            List<Department> Departments = await UOW.DepartmentRepository.List(DepartmentFilter);
            return Departments;
        }

        public DepartmentFilter ToFilter(DepartmentFilter DepartmentFilter)
        {
            return DepartmentFilter;
        }

        public async Task<Department> Update(Department Department)
        {
            try
            {
                await UOW.DepartmentRepository.Update(Department);
                Department = await UOW.DepartmentRepository.Get(Department.Id);

                return Department;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}