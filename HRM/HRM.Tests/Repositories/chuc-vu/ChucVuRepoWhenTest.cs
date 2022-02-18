using HRM.Entities;
using HRM.Helpers;
using HRM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrueSight.Common;

namespace HRM.Tests.Repositories.chuc_vu
{
    public partial class ChucVuRepoTest : CommonTests
    {
        ChucVu ChucVu;
        ChucVuFilter ChucVuFilter;
        List<ChucVu> ChucVus;
        List<long> Ids;
        public async Task When_CreateChucVu_Success()
        {
            string stringRd = RandomString(4);
             ChucVu = new ChucVu
            {
                StatusId = 1,
                Code = "C_" + DateTimeOffset.Now.ToUnixTimeSeconds() + stringRd,
                Name = "Chức " + DateTimeOffset.Now.ToUnixTimeSeconds() + stringRd,
                CreatedAt = StaticParams.DateTimeNow,
                UpdatedAt = StaticParams.DateTimeNow,
                Used = false
            };

            await repository.Create(ChucVu);
   

        }

        public async Task When_UpdateChucVu_Success()
        {
            string stringRd = "_"+RandomString(4);
            ChucVu = new ChucVu
            {
                Id = 2,
                Code = "C_" + DateTimeOffset.Now.ToUnixTimeSeconds() + stringRd,
                Name = "update Chức " + DateTimeOffset.Now.ToUnixTimeSeconds() + stringRd,
                CreatedAt = StaticParams.DateTimeNow,
                UpdatedAt = StaticParams.DateTimeNow,
                Used = false,
                StatusId = 1
            };

            await repository.Update(ChucVu);
  

        }

        public async Task When_DeleteChucVu_Success()
        {
            ChucVu = new ChucVu
            {
                Id = 2
            };
            await repository.Delete(ChucVu);
        }


        public async Task When_BulkDeleteChucVu_Success()
        {
            List<long> Ids = new List<long>(){1,2,3,4,5,6};
            ChucVus = Ids.Select( id => new ChucVu
                {
                    Id = id
                }
            ).ToList();

            await repository.BulkDelete(ChucVus);
        }

        public async Task When_ListOrderByNameChucVu_Success()
        {
            ChucVuFilter = new ChucVuFilter
            {
                OrderBy = ChucVuOrder.Name,
                OrderType = OrderType.DESC,
                Take = 10,
                Skip = 0,
                Selects =  ChucVuSelect.ALL
            };
            ChucVus = await repository.List(ChucVuFilter);
        }
        public async Task When_ListOrderByCodeChucVu_Success()
        {
            ChucVuFilter = new ChucVuFilter
            {
                OrderBy = ChucVuOrder.Code,
                OrderType = OrderType.DESC,
                Take = 10,
                Skip = 0,
                Selects = ChucVuSelect.ALL
            };
            ChucVus = await repository.List(ChucVuFilter);
        }

        public async Task When_GetChucVu_Success()
        {
            Ids = new List<long>() { 10 };

            long id = Ids.FirstOrDefault();
            ChucVu = await repository.Get(id);
        }

        public async Task When_GetListChucVu_Success()
        {
            Ids = new List<long>() { 1,2,3,4,5,6,7,8,9,15,50};

            ChucVus = await repository.List(Ids);
        }
    }
}
