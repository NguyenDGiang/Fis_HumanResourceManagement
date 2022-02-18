using HRM.Entities;
using HRM.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrueSight.Common;

namespace HRM.Tests.Repositories.district
{
    public partial class DistrictRepoThenTest:CommonTests
    {
        public async Task Then_CreateDistrict_Success()
        {
            var Output = DataContext.District.Where(x => x.Id == Input.Id).FirstOrDefault();
            Assert.AreEqual(Input.Name, Output.Name);
            Assert.AreEqual(Input.Code, Output.Code);
            Assert.AreEqual(Input.StatusId, Output.StatusId);
            Assert.AreEqual(Input.ProvinceId, Output.ProvinceId);
            Assert.AreEqual(Input.CreatedAt.ToString("dd-MM-yyyy HH:mm:ss"), Output.CreatedAt.ToString("dd-MM-yyyy HH:mm:ss"));
            Assert.AreEqual(Input.UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss"), Output.UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss"));

        }

        public async Task Then_UpdateDistrict_Success()
        {
            var Output = DataContext.District.Where(x => x.Id == Input.Id).FirstOrDefault();

            Assert.AreEqual(Input.Name, Output.Name);
            Assert.AreEqual(Input.Code, Output.Code);
            Assert.AreEqual(Input.StatusId, Output.StatusId);
            Assert.AreEqual(Input.ProvinceId, Output.ProvinceId);
            Assert.AreEqual(Input.UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss"), Output.UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss"));

            Assert.AreEqual(1, 1);
        }

        public async Task Then_DeleteDistrict_Success()
        {
            var Output = DataContext.District.Where(x => x.Id == InputCreate.Id).FirstOrDefault();
            Assert.AreEqual(InputCreate.DeletedAt.ToString(), Output.DeletedAt.ToString());
        }
        public async Task Then_GetListByName_Success()
        {
            string Name = "abcde";
            DistrictFilter DistrictFilter = new DistrictFilter
            {
                Skip = 0,
                Take = 10,
                Name = new StringFilter { Equal = Name },
                Selects = DistrictSelect.Name
            };

            int count = await repository.Count(DistrictFilter);

            // Assert
            Assert.AreEqual(1, count);
        }
    }
}
