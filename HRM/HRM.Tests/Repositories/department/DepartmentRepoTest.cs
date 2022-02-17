using HRM.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Tests.Repositories.department
{
    [TestFixture]
    public partial class DepartmentRepoTest : CommonTests
    {
        IDepartmentRepository repository;
        public DepartmentRepoTest() : base()
        {

        }

        [SetUp]
        public async Task Setup()
        {
            Init();
            repository = new DepartmentRepository(DataContext);
        }

        // [Test]
        // public async Task Department_CountDepartment_Success()
        // {
        //     await LoadData();
        //     await When_CountDepartment_Success();
        //     await Then_CountDepartment_Success();
        // }
        [Test]
        public async Task Department_CreateDepartment_Success()
        {
            await LoadData();
            await When_CreateDepartment_Success();
            await Then_CreateDepartment_Success();
        }
        [Test]
        public async Task Department__GetDepartment_Success()
        {
            await LoadData();
            await When_GetDepartment_Success();
            await Then_GetDepartment_Success();
        }
        [Test]
        public async Task Department_DeleteDepartment_Success()
        {
            await LoadData();
            await When_DeleteDepartment_Success();
            await Then_DeleteDepartment_Success();
        }
        [Test]
        public async Task Department_BulkDeleteDepartment_Success()
        {
            await LoadData();
            await When_BulkDeleteDepartment_Success();
            await Then_BulkDeleteDepartment_Success();
        }
        [Test]
        public async Task Department_ListOrderByDepartment_Success()
        {
            await LoadData();
            await When_ListOrderByDepartment_Success();
            await Then_ListOrderByDepartment_Success();
        }
        [Test]
        public async Task Department_ListFilterDepartment_Success()
        {
            await LoadData();
            await When_ListFilterDepartment_Success();
            await Then_ListFilterDepartment_Success();
        }

        [Test]
        public async Task Department_GetListDepartment_Success()
        {
            await LoadData();
            await When_GetListDepartment_Success();
            await Then_GetListDepartment_Success();
        }


    }
}
