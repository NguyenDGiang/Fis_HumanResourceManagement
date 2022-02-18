using HRM.Entities;
using HRM.Helpers;
using HRM.Models;
using HRM.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrueSight.Common;

namespace HRM.Tests.Repositories.district
{
    [TestFixture]
    public partial class DistrictRepoTest:CommonTests
    {
        IDistrictRepository repository;
        public DistrictRepoTest() : base()
        {

        }
        [OneTimeSetUp]
        [SetUp]
        public void Setup()
        {
            Init();
            Clean();
            repository = new DistrictRepository(DataContext);
            LoadData();

        }

        //Create
        [Test]
        public async Task District_Create_ReturnTrue()
        {
            await When_CreateDistrict_Success();
            await Then_CreateDistrict_Success();
        }

        //Update
        [Test]
        public async Task District_Update_ReturnTrue()
        {
            await When_UpdateDistrict_Success();
            await Then_UpdateDistrict_Success();
        }


        [Test]
        public async Task District_Delete_ReturnTrue()
        {
            await When_DeleteDistrict_Success();
            await Then_DeleteDistrict_Success();
        }

        [Test]
        public async Task District_GetListByName_ReturnTrue()
        {
            await When_GetListByName_Success();
            await Then_GetListByName_Success();
        }
    }
}
