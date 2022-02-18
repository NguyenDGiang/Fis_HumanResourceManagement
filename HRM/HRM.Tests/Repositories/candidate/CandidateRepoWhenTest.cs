using HRM.Entities;
using HRM.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrueSight.Common;

namespace HRM.Tests.Repositories.candidate
{
    public partial class CandidateRepoTest : CommonTests
    {
        CandidateFilter filter;
        Candidate Candidate;
        List<long> Ids;
        List<Candidate> Candidates;
        int count;
        private async Task When_CountCandidate_Success()
        {
            filter = new CandidateFilter()
            {
                FullName = new StringFilter()
                {
                    Contain = "a"
                }
            };
            count = await repository.Count(filter);
        }

        private async Task When_CreateCandidate_Success()
        {
            Candidate = new Candidate
            {
                Id = 0,
                FullName = "Test create â",
                Email = "email@mail.com",
                PhoneNumber = "0326598754",
                Birthday = StaticParams.DateTimeNow,
                StatusId = 1,
                Used = false,
                VillageId = 1,
                DistrictId = 1,
                ProvinceId = 1,
                CreatedAt = StaticParams.DateTimeNow,
                UpdatedAt = StaticParams.DateTimeNow,
            };

            await repository.Create(Candidate);
        }
        private async Task When_GetCandidate_Success()
        {
            Ids = new List<long>() { 2 };
            long id = Ids.FirstOrDefault();
            Candidate = await repository.Get(id);
        }
        private async Task When_DeleteCandidate_Success()
        {
            Candidate = new Candidate
            {
                Id = 2
            };
            await repository.Delete(Candidate);
        }
        private async Task When_BulkDeleteCandidate_Success()
        {
            List<long> Ids = new List<long>() { 1, 2, 3, 4, 5, 6 };
            Candidates = Ids.Select(id => new Candidate
            {
                Id = id
            }
            ).ToList();

            await repository.BulkDelete(Candidates);
        }

        private async Task When_ListOrderByCandidate_Success()
        {
            filter = new CandidateFilter
            {
                OrderBy = CandidateOrder.FullName,
                OrderType = OrderType.DESC,
                Take = int.MaxValue,
                Skip = 0,
                Selects = CandidateSelect.ALL
            };
            Candidates = await repository.List(filter);
        }

        private async Task When_ListFilterCandidate_Success()
        {
            filter = new CandidateFilter
            {
                FullName = new StringFilter
                {
                    Contain = "x"
                },
                Take = int.MaxValue,
                Skip = 0,
                Selects = CandidateSelect.ALL
            };
            Candidates = await repository.List(filter);
        }
    }
}
