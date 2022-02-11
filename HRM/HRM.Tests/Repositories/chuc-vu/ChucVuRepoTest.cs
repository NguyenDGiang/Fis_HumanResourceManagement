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
            //Business Group

            

            Console.WriteLine("setup ChucVuRepoTest " + DataContext.GetHashCode());
            //DataContext.Entry(StatusDAO_INACTIVE).State = EntityState.Detached;
            //DataContext.Entry(StatusDAO_ACTIVE).State = EntityState.Detached;

        }

        public override void InitData()
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

        //Create
        [Test]
        public async Task ChucVu_Create_ReturnTrue()
        {
            ChucVu Input = new ChucVu
            {
                StatusId = 1,
                Code = "C_" + DateTimeOffset.Now.ToUnixTimeSeconds(),
                Name = "Chức " + DateTimeOffset.Now.ToUnixTimeSeconds(),
                CreatedAt = StaticParams.DateTimeNow,
                UpdatedAt = StaticParams.DateTimeNow,
                Used = false
            };
            await repository.Create(Input);

            var Output = DataContext.ChucVu.Where(x => x.Id == Input.Id).FirstOrDefault();
            Console.WriteLine("input: " + Input.CreatedAt.ToString("dd-MM-yyyy HH:mm:ss"));
            Console.WriteLine("output: " + Output.CreatedAt.ToString("dd-MM-yyyy HH:mm:ss"));

            Assert.AreEqual(Input.Code, Output.Code);
            Assert.AreEqual(Input.Name, Output.Name);
            Assert.AreEqual(Input.StatusId, Output.StatusId);
            Assert.AreEqual(Input.Used, Output.Used);
            Assert.AreEqual(Input.CreatedAt.ToString("dd-MM-yyyy HH:mm:ss"), Output.CreatedAt.ToString("dd-MM-yyyy HH:mm:ss"));
            Assert.AreEqual(Input.UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss"), Output.UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss"));
        }

        //Update
        [Test]
        public async Task ChucVu_Update_ReturnTrue()
        {
            ChucVuDAO ChucVuDAO = new ChucVuDAO
            {
                StatusId = 1,
                Code = "C_" + DateTimeOffset.Now.ToUnixTimeSeconds(),
                Name = "Chức " + DateTimeOffset.Now.ToUnixTimeSeconds(),
                CreatedAt = StaticParams.DateTimeNow,
                UpdatedAt = StaticParams.DateTimeNow,
                Used = false
            };
            System.Console.WriteLine(JsonConvert.SerializeObject(ChucVuDAO));
            DataContext.ChucVu.Add(ChucVuDAO);
            await DataContext.SaveChangesAsync();


            ChucVu ChucVu = new ChucVu()
            {
                Id = ChucVuDAO.Id,
                Code = ChucVuDAO.Code,
                Name = "update Chức_" + DateTimeOffset.Now.ToUnixTimeSeconds(),
                StatusId = ChucVuDAO.StatusId,
                UpdatedAt = StaticParams.DateTimeNow,
                Used = false
            };

            await repository.Update(ChucVu);
            await DataContext.SaveChangesAsync();

            var Output = DataContext.ChucVu.Where(x => x.Id == ChucVu.Id).FirstOrDefault();

            Assert.AreEqual(ChucVu.Code, Output.Code);
            Assert.AreEqual(ChucVu.Name, Output.Name);
            Assert.AreEqual(ChucVu.StatusId, Output.StatusId);
            Assert.AreEqual(ChucVu.UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss"), Output.UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss"));

            Assert.AreEqual(1, 1);

        }

        //Delete

        //List Order By Name + Skip and Take

        //List Order By Type + Skip and Take

        //Bulk Insert 

    }
}
