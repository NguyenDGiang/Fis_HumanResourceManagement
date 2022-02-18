using HRM.Entities;
using HRM.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrueSight.Common;

namespace HRM.Tests.Repositories.employee
{
    public partial class EmployeeRepoTest:CommonTests
    {
        public async Task Then_CreateEmployee_Success()
        {
            var Output = DataContext.Employee.Where(x => x.Id == Input.Id).FirstOrDefault();
            Assert.AreEqual(Input.FullName, Output.FullName);
            Assert.AreEqual(Input.Email, Output.Email);
            Assert.AreEqual(Input.PhoneNumber, Output.PhoneNumber);
            Assert.AreEqual(Input.Birthday, Output.Birthday);
            Assert.AreEqual(Input.BeginJobTime, Output.BeginJobTime);
            Assert.AreEqual(Input.ContractStatus, Output.ContractStatus);
            Assert.AreEqual(Input.AcademicLevelId, Output.AcademicLevelId);
            Assert.AreEqual(Input.ChucVuId, Output.ChucVuId);
            Assert.AreEqual(Input.JobPositionId, Output.JobPositionId);
            Assert.AreEqual(Input.StatusId, Output.StatusId);
            Assert.AreEqual(Input.VillageId, Output.VillageId);
            Assert.AreEqual(Input.ProvinceId, Output.ProvinceId);
            Assert.AreEqual(Input.CreatedAt.ToString("dd-MM-yyyy HH:mm:ss"), Output.CreatedAt.ToString("dd-MM-yyyy HH:mm:ss"));
            Assert.AreEqual(Input.UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss"), Output.UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss"));

        }

        public async Task Then_UpdateEmployee_Success()
        {
            var Output = DataContext.Employee.Where(x => x.Id == Input.Id).FirstOrDefault();

            Assert.AreEqual(Input.FullName, Output.FullName);
            Assert.AreEqual(Input.Email, Output.Email);
            Assert.AreEqual(Input.PhoneNumber, Output.PhoneNumber);
            Assert.AreEqual(Input.Birthday, Output.Birthday);
            Assert.AreEqual(Input.BeginJobTime, Output.BeginJobTime);
            Assert.AreEqual(Input.ContractStatus, Output.ContractStatus);
            Assert.AreEqual(Input.AcademicLevelId, Output.AcademicLevelId);
            Assert.AreEqual(Input.ChucVuId, Output.ChucVuId);
            Assert.AreEqual(Input.JobPositionId, Output.JobPositionId);
            Assert.AreEqual(Input.StatusId, Output.StatusId);
            Assert.AreEqual(Input.VillageId, Output.VillageId);
            Assert.AreEqual(Input.ProvinceId, Output.ProvinceId);
            Assert.AreEqual(Input.UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss"), Output.UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss"));

            Assert.AreEqual(1, 1);
        }

        public async Task Then_DeleteEmployee_Success()
        {
            var Output = DataContext.Employee.Where(x => x.Id == InputCreate.Id).FirstOrDefault();
            Assert.AreEqual(InputCreate.DeletedAt.ToString(), Output.DeletedAt.ToString());
        }
        public async Task Then_GetListByName_Success()
        {
            string Name = "afffds";
            EmployeeFilter EmployeeFilter = new EmployeeFilter
            {
                Skip = 0,
                Take = 10,
                FullName = new StringFilter { Equal = Name },
                Selects = EmployeeSelect.FullName
            };

            int count = await repository.Count(EmployeeFilter);

            // Assert
            Assert.AreEqual(1, count);
        }
    }
}
