using HRM.Entities;
using HRM.Helpers;
using HRM.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HRM.Models;
using System.Linq;

namespace HRM.Tests.Repositories.employee
{
    public partial class EmployeeRepoTest:CommonTests
    {
        Employee Input;
        Employee InputCreate;
        public async Task When_CreateEmployee_Success()
        {
            Input = new Employee()
            {
                StatusId = 1,
                FullName = "afffds",
                Email = "Nguyenginag@gmail.com",
                PhoneNumber = "0123123",
                Birthday = new DateTime(2003, 09, 09),
                BeginJobTime = new DateTime(2022, 09, 09),
                ContractStatus = false,
                AcademicLevelId = 1,
                ChucVuId = 1,
                JobPositionId = 1,
                VillageId = 1,
                DistrictId = 1,
                ProvinceId = 1,
                Used = false,
                CreatedAt = StaticParams.DateTimeNow,
                UpdatedAt = StaticParams.DateTimeNow
            };
            await repository.Create(Input);
        }

        public async Task When_UpdateEmployee_Success()
        {
            InputCreate = new Employee()
            {
                StatusId = 1,
                FullName = "afffddfdfs",
                Email = "Nguyenginag@gmail.com",
                PhoneNumber = "0123123",
                Birthday = new DateTime(2003, 09, 09),
                BeginJobTime = new DateTime(2022, 09, 09),
                ContractStatus = false,
                AcademicLevelId = 1,
                ChucVuId = 1,
                JobPositionId = 1,
                VillageId = 1,
                DistrictId = 1,
                ProvinceId = 1,
                Used = false,
                CreatedAt = StaticParams.DateTimeNow,
                UpdatedAt = StaticParams.DateTimeNow
            };
            await repository.Create(InputCreate);
            var employee = DataContext.Employee.Where(x => x.Id == InputCreate.Id).FirstOrDefault();
            Input = new Employee()
            {
                Id = employee.Id,
                StatusId = employee.StatusId,
                FullName = "afs",
                Email = "Nguyeng@gmail.com",
                PhoneNumber = "0123123",
                Birthday = employee.Birthday,
                BeginJobTime = employee.BeginJobTime,
                ContractStatus = false,
                AcademicLevelId = employee.AcademicLevelId,
                ChucVuId = employee.ChucVuId,
                JobPositionId = employee.JobPositionId,
                VillageId = employee.VillageId,
                DistrictId = employee.DistrictId,
                ProvinceId = employee.ProvinceId,
                Used = false,
                CreatedAt = StaticParams.DateTimeNow,
                UpdatedAt = StaticParams.DateTimeNow
            };

            await repository.Update(Input);
        }

        public async Task When_DeleteEmployee_Success()
        {
            InputCreate = new Employee
            {
                StatusId = 1,
                FullName = "afffds",
                Email = "Nguyenginag@gmail.com",
                PhoneNumber = "0123123",
                Birthday = new DateTime(2003, 09, 09),
                BeginJobTime = new DateTime(2022, 09, 09),
                ContractStatus = false,
                AcademicLevelId = 1,
                ChucVuId = 1,
                JobPositionId = 1,
                VillageId = 1,
                DistrictId = 1,
                ProvinceId = 1,
                Used = false,
                CreatedAt = StaticParams.DateTimeNow,
                UpdatedAt = StaticParams.DateTimeNow
            };
            await repository.Create(InputCreate);
            await repository.Delete(InputCreate);
        }

        public async Task When_GetListByName_Success()
        {
            InputCreate = new Employee
            {
                StatusId = 1,
                FullName = "afffds",
                Email = "Nguyenginag@gmail.com",
                PhoneNumber = "0123123",
                Birthday = new DateTime(2003, 09, 09),
                BeginJobTime = new DateTime(2022, 09, 09),
                ContractStatus = false,
                AcademicLevelId = 1,
                ChucVuId = 1,
                JobPositionId = 1,
                VillageId = 1,
                DistrictId = 1,
                ProvinceId = 1,
                Used = false,
                CreatedAt = StaticParams.DateTimeNow,
                UpdatedAt = StaticParams.DateTimeNow
            };
            // Create Instance
            await repository.Create(InputCreate);
        }
    }
}
