using HRM.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRM.Tests.Repositories.interviewresult
{
    public partial class InterviewResultRepoTest:CommonTests
    {
        public void LoadData()
        {
            Given_Status();
            Given_Address();
            Given_Candidate();
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
        public void Given_Candidate()
        {
            CandidateDAO Candidate = new CandidateDAO
            {
                FullName = "Candidate1",
                Email= "ChucVu1@gamil.com",
                StatusId = 1,
                Used = false,
                Birthday = new DateTime(2000,09,09),
                PhoneNumber = "34567",
                VillageId = 1,
                DistrictId = 1,
                ProvinceId = 1, 
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now

            };
            DataContext.Candidate.Add(Candidate);
            DataContext.SaveChanges();
        }
    }
}
