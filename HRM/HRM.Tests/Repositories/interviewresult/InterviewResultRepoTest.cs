using HRM.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Tests.Repositories.interviewresult
{
    [TestFixture]
    public partial class InterviewResultRepoTest:CommonTests
    {
        IInterviewResultRepository repository;
        public InterviewResultRepoTest() : base()
        {

        }

        [SetUp]
        public void Setup()
        {
            Init();
            Clean();
            repository = new InterviewResultRepository(DataContext);
            LoadData();

        }



        //Create
        [Test]
        public async Task InterviewResult_Create_ReturnTrue()
        {
            await When_CreateInterviewResult_Success();
            await Then_CreateInterviewResult_Success();
            
        }

        //Update
        [Test]
        public async Task InterviewResult_Update_ReturnTrue()
        {
            await When_UpdateInterviewResult_Success();
            await Then_UpdateInterviewResult_Success();


        }


        [Test]
        public async Task InterviewResult_Delete_ReturnTrue()
        {
            await When_DeleteEmployee_Success();
            await Then_DeleteInterviewResult_Success();
        }

    }
}
