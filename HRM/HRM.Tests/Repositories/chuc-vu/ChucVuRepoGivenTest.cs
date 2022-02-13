using HRM.Helpers;
using HRM.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Tests.Repositories.chuc_vu
{
    public partial class ChucVuRepoTest : CommonTests
    {
        private List<ChucVuDAO> ChucVuDAOs { get; set; }
        private List<StatusDAO> StatusDAOs { get; set; }

        public async Task LoadData()
        {
            await Given_Status();
            await Given_ChucVu();
        }
       public async Task Given_Status()
        {

            StatusDAO StatusDAO_INACTIVE = new StatusDAO
            {
                Id = 0,
                Code = "INACTIVE",
                Name = "Dừng hoạt động",
            };
            DataContext.Status.Add(StatusDAO_INACTIVE);
            StatusDAO StatusDAO_ACTIVE = new StatusDAO
            {
                Id = 0,
                Code = "ACTIVE",
                Name = "Hoạt động",
            };
            DataContext.Status.Add(StatusDAO_ACTIVE);

            await DataContext.SaveChangesAsync(); 
        }

        public async Task Given_ChucVu()
        {
            ChucVuDAOs = new List<ChucVuDAO>();
            for (int i = 0; i < 20; i++)
            {
                string stringRd = RandomString(4);
                ChucVuDAO ChucVuDAO = new ChucVuDAO
                {
                    StatusId = 1,
                    Code = "C_" + DateTimeOffset.Now.ToUnixTimeSeconds() + stringRd,
                    Name = "Chức " + DateTimeOffset.Now.ToUnixTimeSeconds() + stringRd,
                    CreatedAt = StaticParams.DateTimeNow,
                    UpdatedAt = StaticParams.DateTimeNow,
                    Used = false
                };
                ChucVuDAOs.Add(ChucVuDAO);
            }
            await DataContext.ChucVu.BulkMergeAsync(ChucVuDAOs);
        }

        public string RandomString(int length)
        {
            //length = length < 0 ? length * -1 : length;
            var str = "";

            do
            {
                str += Guid.NewGuid().ToString().Replace("-", "");
            }

            while (length > str.Length);

            return str.Substring(0, length);
        }
    }
}
