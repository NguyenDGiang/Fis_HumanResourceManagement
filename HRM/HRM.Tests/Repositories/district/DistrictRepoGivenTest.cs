using HRM.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Tests.Repositories.district
{
    public partial class DistrictRepoGivenTest:CommonTests
    {
        public void LoadData()
        {
            Given_Status();
            Given_Province();
        }
        public void Given_Status()
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

            DataContext.SaveChanges();
        }
        public void Given_Province()
        {
            ProvinceDAO Province = new ProvinceDAO
            {
                Name = "Province2",
                Code = "Province2",
                Used = false,
                StatusId = 1,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            DataContext.Province.Add(Province);
            DataContext.SaveChanges();
        }
    }
}
