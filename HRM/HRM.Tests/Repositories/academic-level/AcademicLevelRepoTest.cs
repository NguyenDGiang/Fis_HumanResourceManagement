using HRM.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Tests.Repositories.academic_level
{
    [TestFixture]
    public partial class AcademicLevelRepoTest : CommonTests
    {
        IAcademicLevelRepository repository;
        public AcademicLevelRepoTest() : base()
        {

        }

        [SetUp]
        public async Task Setup()
        {
            Init();
            repository = new AcademicLevelRepository(DataContext);
        }

        // [Test]
        // public async Task AcademicLevel_CountAcademicLevel_Success()
        // {
        //     await LoadData();
        //     await When_CountAcademicLevel_Success();
        //     await Then_CountAcademicLevel_Success();
        // }
        [Test]
        public async Task AcademicLevel_CreateAcademicLevel_Success()
        {
            await LoadData();
            await When_CreateAcademicLevel_Success();
            await Then_CreateAcademicLevel_Success();
        }
        [Test]
        public async Task AcademicLevel__GetAcademicLevel_Success()
        {
            await LoadData();
            await When_GetAcademicLevel_Success();
            await Then_GetAcademicLevel_Success();
        }
        [Test]
        public async Task AcademicLevel_DeleteAcademicLevel_Success()
        {
            await LoadData();
            await When_DeleteAcademicLevel_Success();
            await Then_DeleteAcademicLevel_Success();
        }
        [Test]
        public async Task AcademicLevel_BulkDeleteAcademicLevel_Success()
        {
            await LoadData();
            await When_BulkDeleteAcademicLevel_Success();
            await Then_BulkDeleteAcademicLevel_Success();
        }
        [Test]
        public async Task AcademicLevel_ListOrderByAcademicLevel_Success()
        {
            await LoadData();
            await When_ListOrderByAcademicLevel_Success();
            await Then_ListOrderByAcademicLevel_Success();
        }
        [Test]
        public async Task AcademicLevel_ListFilterAcademicLevel_Success()
        {
            await LoadData();
            await When_ListFilterAcademicLevel_Success();
            await Then_ListFilterAcademicLevel_Success();
        }


    }
}
