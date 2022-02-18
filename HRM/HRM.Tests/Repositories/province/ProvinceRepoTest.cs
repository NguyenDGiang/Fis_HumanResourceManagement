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

namespace HRM.Tests.Repositories.province
{
    [TestFixture]
    public partial class ProvinceRepoTest:CommonTests
    {
        IProvinceRepository repository;
        public ProvinceRepoTest() : base()
        {

        }

        [SetUp]
        public void Setup()
        {
            Init();
            Clean();
            repository = new ProvinceRepository(DataContext);
            LoadData();
        }

        //Create
        [Test]
        public async Task Province_Create_ReturnTrue()
        {
            await When_CreateProvince_Success();
            await Then_CreateProvince_Success();
        }

        //Update
        [Test]
        public async Task Province_Update_ReturnTrue()
        {
            await When_UpdateProvince_Success();
            await Then_UpdateProvince_Success();
        }


        [Test]
        public async Task Province_Delete_ReturnTrue()
        {
            await When_DeleteProvince_Success();
            await Then_DeleteProvince_Success();
        }

        [Test]
        public async Task Province_GetListByName_ReturnTrue()
        {
            await When_GetListByName_Success();
            await Then_GetListByName_Success();
        }
    }
}
