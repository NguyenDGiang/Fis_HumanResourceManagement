using HRM.Entities;
using HRM.Helpers;
using HRM.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HRM.Models;
using System.Linq;

namespace HRM.Tests.Repositories.district
{
    public partial class DistrictRepoWhenTest
    {
        District Input;
        District InputCreate;
        public async Task When_CreateDistrict_Success()
        {
            Input = new District()
            {
                StatusId = 1,
                Name = "abcde",
                ProvinceId = 1,
                Used = false,
                CreatedAt = StaticParams.DateTimeNow,
                UpdatedAt = StaticParams.DateTimeNow
            };
            await repository.Create(Input);
        }

        public async Task When_UpdateDistrict_Success()
        {
            InputCreate = new District()
            {
                StatusId = 1,
                ProvinceId = 1,
                Used = false,
                CreatedAt = StaticParams.DateTimeNow,
                UpdatedAt = StaticParams.DateTimeNow
            };
            await repository.Create(InputCreate);
            var district = DataContext.District.Where(x => x.Id == InputCreate.Id).FirstOrDefault();
            Input = new District()
            {
                Id = district.Id,
                StatusId = district.StatusId,
                Name = "adss",
                ProvinceId = district.ProvinceId,
                Used = false,
                CreatedAt = StaticParams.DateTimeNow,
                UpdatedAt = StaticParams.DateTimeNow
            };

            await repository.Update(Input);
        }

        public async Task When_DeleteDistrict_Success()
        {
            InputCreate = new District
            {
                StatusId = 1,
                Name = "asdfsdf",
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
            InputCreate = new District
            {
                StatusId = 1,
                Name = "afffds",
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
