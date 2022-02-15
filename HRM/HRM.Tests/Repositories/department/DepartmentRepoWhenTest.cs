using HRM.Entities;
using HRM.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrueSight.Common;

namespace HRM.Tests.Repositories.department
{
    public partial class DepartmentRepoTest : CommonTests
    {
        DepartmentFilter filter;
        Department Department;
        List<long> Ids;
        List<Department> Departments;
        int count;
        private async Task When_CountDepartment_Success()
        {
            filter = new DepartmentFilter()
            {
                Name = new StringFilter()
                {
                    Contain = "a"
                }
            };
            count = await repository.Count(filter);
        }

        private async Task When_CreateDepartment_Success()
        {
            Department = new Department
            {
                Id = 0,
                Name = "Test create â",
                Code ="CV",
                PhoneNumber = "00012345678",
                VillageId = 1,
                DistrictId = 1,
                ProvinceId = 1,
                StatusId = 1,
                Used = false,
                CreatedAt = StaticParams.DateTimeNow,
                UpdatedAt = StaticParams.DateTimeNow,
            };

            await repository.Create(Department);
        }
        private async Task When_GetDepartment_Success()
        {
            Ids = new List<long>() { 1 };
            long id = Ids.FirstOrDefault();
            Department = await repository.Get(id);
        }
        private async Task When_DeleteDepartment_Success()
        {
            Department = new Department
            {
                Id = 2
            };
            await repository.Delete(Department);
        }
        private async Task When_BulkDeleteDepartment_Success()
        {
            List<long> Ids = new List<long>() { 1, 2, 3 };
            Departments = Ids.Select(id => new Department
            {
                Id = id
            }
            ).ToList();

            await repository.BulkDelete(Departments);
        }

        private async Task When_ListOrderByDepartment_Success()
        {
            filter = new DepartmentFilter
            {
                OrderBy = DepartmentOrder.Name,
                OrderType = OrderType.DESC,
                Take = int.MaxValue,
                Skip = 0,
                Selects = DepartmentSelect.ALL
            };
            Departments = await repository.List(filter);
        }

        private async Task When_ListFilterDepartment_Success()
        {
            filter = new DepartmentFilter
            {
                Name = new StringFilter
                {
                    Contain = "x"
                },
                Take = int.MaxValue,
                Skip = 0,
                Selects = DepartmentSelect.ALL
            };
            Departments = await repository.List(filter);
        }
    }
}
