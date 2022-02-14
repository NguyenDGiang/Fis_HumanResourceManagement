using HRM.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Tests.Repositories.employee
{
    public partial class EmployeeRepoTest:CommonTests
    {
        public void LoadData()
        {
            Given_Status();
            Given_ChucVu();
            Given_AcademicLevel();
            Given_Address();
            Given_JobPosition();
          
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
        public void Given_ChucVu()
        {
            ChucVuDAO ChucVu = new ChucVuDAO
            {
                Name = "ChucVu1",
                Code = "ChucVu1",
                StatusId = 1,
                Used = false,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now

            };
            DataContext.ChucVu.Add(ChucVu);
            DataContext.SaveChanges();
        }
        public void Given_JobPosition()
        {
            JobPositionDAO JobPosition = new JobPositionDAO
            {
                Name = "Job1",
                Code = "J",
                Used = false,
                StatusId = 1,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now

            };

            DataContext.JobPosition.Add(JobPosition);
            DataContext.SaveChanges();
        }
        public void Given_AcademicLevel()
        {
            AcademicLevelDAO AcademicLevel = new AcademicLevelDAO
            {
                Name = "Academic1",
                Code = "A",
                StatusId = 1,
                Used = false,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            DataContext.AcademicLevel.Add(AcademicLevel);
            DataContext.SaveChanges();
        }
        public void Given_Address()
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
            DistrictDAO District = new DistrictDAO
            {
                Name = "District1",
                Code = "District1",
                Used = false,
                StatusId = 1,
                ProvinceId = 1,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            DataContext.District.Add(District);
            DataContext.SaveChanges();
            VillageDAO Village = new VillageDAO
            {
                Name = "Village1",
                Code = "Village1",
                Used = false,
                StatusId = 1,
                DistrictId = 1,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            DataContext.Village.Add(Village);
            DataContext.SaveChanges();
        }
    }
}
