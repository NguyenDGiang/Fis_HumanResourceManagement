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

namespace HRM.Tests.Repositories.jobposition
{
    [TestFixture]
    public partial class JobPositionRepoTest : CommonTests
    {
        IJobPositionRepository repository;
        public JobPositionRepoTest() : base()
        {

        }

        [SetUp]
        public void Setup()
        {
            Init();
            Clean();
            repository = new JobPositionRepository(DataContext);
            LoadData();

        }

        

        //Create
        [Test]
        public async Task JobPosition_Create_ReturnTrue()
        {
            await When_CreateJobPosition_Success();
            await Then_CreateJobPosition_Success();
        }

        //Update
        [Test]
        public async Task JobPosition_Update_ReturnTrue()
        {
            await When_UpdateJobPosition_Success();
            await Then_UpdateJobPosition_Success();
            

        }


        [Test]
        public async Task JobPosition_Delete_ReturnTrue()
        {
            await When_DeleteEmployee_Success();
            await Then_DeleteJobPosition_Success();
        }

        [Test]
        public async Task JobPosition_GetListByName_ReturnTrue()
        {
            await When_GetListByName_Success();

            await Then_GetListByName_Success();
        }


    }
}
