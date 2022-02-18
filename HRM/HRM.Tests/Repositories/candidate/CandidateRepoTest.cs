using HRM.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Tests.Repositories.candidate
{
    [TestFixture]
    public partial class CandidateRepoTest : CommonTests
    {
        ICandidateRepository repository;
        public CandidateRepoTest() : base()
        {

        }

        [SetUp]
        public async Task Setup()
        {
            Init();
            repository = new CandidateRepository(DataContext);
        }

        [Test]
        public async Task Candidate_CountCandidate_Success()
        {
            await LoadData();
            await When_CountCandidate_Success();
            await Then_CountCandidate_Success();
        }
        [Test]
        public async Task Candidate_CreateCandidate_Success()
        {
            await LoadData();
            await When_CreateCandidate_Success();
            await Then_CreateCandidate_Success();
        }
        [Test]
        public async Task Candidate__GetCandidate_Success()
        {
            await LoadData();
            await When_GetCandidate_Success();
            await Then_GetCandidate_Success();
        }
        [Test]
        public async Task Candidate_DeleteCandidate_Success()
        {
            await LoadData();
            await When_DeleteCandidate_Success();
            await Then_DeleteCandidate_Success();
        }
        [Test]
        public async Task Candidate_BulkDeleteCandidate_Success()
        {
            await LoadData();
            await When_BulkDeleteCandidate_Success();
            await Then_BulkDeleteCandidate_Success();
        }
        [Test]
        public async Task Candidate_ListOrderByCandidate_Success()
        {
            await LoadData();
            await When_ListOrderByCandidate_Success();
            await Then_ListOrderByCandidate_Success();
        }
        [Test]
        public async Task Candidate_ListFilterCandidate_Success()
        {
            await LoadData();
            await When_ListFilterCandidate_Success();
            await Then_ListFilterCandidate_Success();
        }


    }
}
