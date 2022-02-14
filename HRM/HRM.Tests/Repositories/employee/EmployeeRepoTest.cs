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

namespace HRM.Tests.Repositories.employee
{
    [TestFixture]
    public partial class EmployeeRepoTest : CommonTests
    {
        IEmployeeRepository repository;
        public EmployeeRepoTest() : base()
        {

        }
        [OneTimeSetUp]
        [SetUp]
        public void Setup()
        {
            Init();
            Clean();
            repository = new EmployeeRepository(DataContext);
            LoadData();

        }

        

        //Create
        [Test]
        public async Task Employee_Create_ReturnTrue()
        {
            await When_CreateEmployee_Success();
            await Then_CreateEmployee_Success();
        }

        //Update
        [Test]
        public async Task Employee_Update_ReturnTrue()
        {
            await When_UpdateEmployee_Success();
            await Then_UpdateEmployee_Success();
            

        }


        [Test]
        public async Task Employee_Delete_ReturnTrue()
        {
            await When_DeleteEmployee_Success();
            await Then_DeleteEmployee_Success();

        }

        [Test]
        public async Task Employee_GetListByName_ReturnTrue()
        {
            await When_GetListByName_Success();

            await Then_GetListByName_Success();
        }


    }
}
