using HRM.Entities;
using HRM.Helpers;
using HRM.Models;
using HRM.Repositories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Tests.Repositories.chuc_vu
{
    [TestFixture]
    public partial class ChucVuRepoTest : CommonTests
    {
        IChucVuRepository repository;
        public ChucVuRepoTest() : base()
        {
            
        }

        [SetUp]
        public  void Setup()
        {
            Init();
            repository = new ChucVuRepository(DataContext);
        }

        //Create
        [Test]
        public async Task ChucVu_Create_ReturnTrue()
        {
            await LoadData();
            await When_CreateChucVu_Success();
            await Then_CreateChucVu_Success();
        }

        //Update
        [Test]
        public async Task ChucVu_Update_Success()
        {
            await LoadData();
            await When_UpdateChucVu_Success();
            await Then_UpdateChucVu_Success();
        }

        //Delete
        [Test]
        public async Task ChucVu_Delete_Success()
        {
            await LoadData();
            await When_DeleteChucVu_Success();
            await Then_DeleteChucVu_Success();
        }

        //List Order By Name + Skip and Take
        [Test]
        public async Task ChucVu_ListOrderByName_Success()
        {
            await LoadData();
            await When_ListOrderByNameChucVu_Success();
            await Then_ListOrderByChucVu_Success();
        }

        [Test]
        public async Task ChucVu_ListOrderByCode_Success()
        {
            await LoadData();
            await When_ListOrderByCodeChucVu_Success();
            await Then_ListOrderByChucVu_Success();
        }

        [Test]
        public async Task ChucVu_BulkDelete_Success()
        {
            await LoadData();
            await When_BulkDeleteChucVu_Success();
            await Then_BulkDeleteChucVu_Success();
        }

        [Test]
        public async Task ChucVu_Get_Success()
        {
            await LoadData();
            await When_GetChucVu_Success();
            await Then_GetChucVu_Success();
        }

        [Test]
        public async Task ChucVu_GetList_Success()
        {
            await LoadData();
            await When_GetListChucVu_Success();
            await Then_GetListChucVu_Success();
        }


    }
}
