using HRM.Entities;
using HRM.Helpers;
using HRM.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HRM.Models;
using System.Linq;

namespace HRM.Tests.Repositories.province
{
    public partial class ProvinceRepoWhenTest:CommonTests
    {
        Province Input;
        Province InputCreate;
        public async Task When_CreateProvince_Success()
        {
            Input = new Province()
            {
                Name = "Pro1",
                Code = "P",
                Used = false,
                StatusId = 1,
                CreatedAt = StaticParams.DateTimeNow,
                UpdatedAt = StaticParams.DateTimeNow
            };
            await repository.Create(Input);
        }

        public async Task When_UpdateProvince_Success()
        {
            InputCreate = new Province()
            {
                StatusId = 1,
                Name = "Pro1",
                Code = "1",
                Used = false,
                CreatedAt = StaticParams.DateTimeNow,
                UpdatedAt = StaticParams.DateTimeNow
            };
            await repository.Create(InputCreate);
            var province = DataContext.Province.Where(x => x.Id == InputCreate.Id).FirstOrDefault();
            Input = new Province()
            {
                Id = province.Id,
                StatusId = 1,
                Name = "Province",
                Code = "1",
                Used = false,
                CreatedAt = StaticParams.DateTimeNow,
                UpdatedAt = StaticParams.DateTimeNow
            };

            await repository.Update(Input);
        }

        public async Task When_DeleteProvince_Success()
        {
            InputCreate = new Province
            {
                Name = "Pro1",
                Code = "P",
                Used = false,
                StatusId = 1,
                CreatedAt = StaticParams.DateTimeNow,
                UpdatedAt = StaticParams.DateTimeNow
            };
            await repository.Create(InputCreate);
            await repository.Delete(InputCreate);
        }

        public async Task When_GetListByName_Success()
        {
            InputCreate = new Province
            {
                Name = "Pro1",
                Code = "P",
                Used = false,
                StatusId = 1,
                CreatedAt = StaticParams.DateTimeNow,
                UpdatedAt = StaticParams.DateTimeNow
            };
            // Create Instance
            await repository.Create(InputCreate);
        }
    }
}
