using HRM.Models;
using HRM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TrueSight.Common;
using Microsoft.EntityFrameworkCore;

namespace HRM.Tests.Repositories.candidate
{
    public partial class CandidateRepoTest : CommonTests
    {
        private async Task Then_CountCandidate_Success()
        {
            IQueryable<CandidateDAO> query = DataContext.Candidate;
            query = DynamicFilter(query, filter);
            int output = query.Count();
            Assert.AreEqual(count, output);
        }

        private async Task Then_CreateCandidate_Success()
        {
            var Output = await DataContext.Candidate.Where(x => x.Id == Candidate.Id).FirstOrDefaultAsync();

            Assert.AreEqual(Candidate.FullName, Output.FullName);
            Assert.AreEqual(Candidate.Birthday.ToString("dd-MM-yyyy HH:mm:ss"), Output.Birthday.ToString("dd-MM-yyyy HH:mm:ss"));
            Assert.AreEqual(Candidate.StatusId, Output.StatusId);
            Assert.AreEqual(Candidate.PhoneNumber, Output.PhoneNumber);
            Assert.AreEqual(Candidate.StatusId, Output.StatusId);
            Assert.AreEqual(Candidate.ProvinceId, Output.ProvinceId);
            Assert.AreEqual(Candidate.DistrictId, Output.DistrictId);
            Assert.AreEqual(Candidate.VillageId, Output.VillageId);
            Assert.AreEqual(Candidate.CreatedAt.ToString("dd-MM-yyyy HH:mm:ss"), Output.CreatedAt.ToString("dd-MM-yyyy HH:mm:ss"));
            Assert.AreEqual(Candidate.UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss"), Output.UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss"));
        }

        public async Task Then_UpdateCandidate_Success()
        {
            var Output = DataContext.Candidate.Where(x => x.Id == Candidate.Id).FirstOrDefault();

            Assert.AreEqual(Candidate.FullName, Output.FullName);
            Assert.AreEqual(Candidate.Birthday.ToString("dd-MM-yyyy HH:mm:ss"), Output.Birthday.ToString("dd-MM-yyyy HH:mm:ss"));
            Assert.AreEqual(Candidate.StatusId, Output.StatusId);
            Assert.AreEqual(Candidate.PhoneNumber, Output.PhoneNumber);
            Assert.AreEqual(Candidate.StatusId, Output.StatusId);
            Assert.AreEqual(Candidate.ProvinceId, Output.ProvinceId);
            Assert.AreEqual(Candidate.DistrictId, Output.DistrictId);
            Assert.AreEqual(Candidate.VillageId, Output.VillageId);
            Assert.AreEqual(Candidate.CreatedAt.ToString("dd-MM-yyyy HH:mm:ss"), Output.CreatedAt.ToString("dd-MM-yyyy HH:mm:ss"));
            Assert.AreEqual(Candidate.UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss"), Output.UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss"));
        }

        public async Task Then_DeleteCandidate_Success()
        {
            var Output = await DataContext.Candidate.Where(x => x.Id == Candidate.Id).FirstOrDefaultAsync();
            Assert.IsNotNull(Output);
            Assert.IsNotNull(Output.DeletedAt);
        }

        public async Task Then_ListOrderByCandidate_Success()
        {
            IQueryable<CandidateDAO> query = DataContext.Candidate.AsNoTracking();
            query = DynamicOrder(query, filter);

            List<CandidateDAO> OutputCandidateDAOs = await query.ToListAsync();

            Assert.AreEqual(OutputCandidateDAOs.Count(), Candidates.Count());
            if (Candidates.Count() == OutputCandidateDAOs.Count())
            {
                int length = Candidates.Count();
                for (int i = 0; i < length; i++)
                {
                    Assert.AreEqual(OutputCandidateDAOs[i].Id, Candidates[i].Id);
                }
            }
        }

        public async Task Then_BulkDeleteCandidate_Success()
        {
            List<long> Ids = Candidates.Select(x => x.Id).ToList();
            List<CandidateDAO> Output = await DataContext.Candidate.Where(x => Ids.Contains(x.Id)).ToListAsync();

            foreach (var item in Output)
            {
                Assert.NotNull(item.DeletedAt);
            }
        }

        public async Task Then_GetCandidate_Success()
        {
            long id = Ids.FirstOrDefault();

            CandidateDAO Output = await DataContext.Candidate.AsNoTracking()
            .Where(c => c.Id == id).FirstOrDefaultAsync();

            if (Output != null && Candidate != null)
            {
                Assert.AreEqual(Candidate.FullName, Output.FullName);
                Assert.AreEqual(Candidate.Birthday.ToString("dd-MM-yyyy HH:mm:ss"), Output.Birthday.ToString("dd-MM-yyyy HH:mm:ss"));
                Assert.AreEqual(Candidate.StatusId, Output.StatusId);
                Assert.AreEqual(Candidate.PhoneNumber, Output.PhoneNumber);
                Assert.AreEqual(Candidate.StatusId, Output.StatusId);
                Assert.AreEqual(Candidate.ProvinceId, Output.ProvinceId);
                Assert.AreEqual(Candidate.DistrictId, Output.DistrictId);
                Assert.AreEqual(Candidate.VillageId, Output.VillageId);
                Assert.AreEqual(Candidate.CreatedAt.ToString("dd-MM-yyyy HH:mm:ss"), Output.CreatedAt.ToString("dd-MM-yyyy HH:mm:ss"));
                Assert.AreEqual(Candidate.UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss"), Output.UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss"));
            }
        }

        public async Task Then_GetListCandidate_Success()
        {
            List<CandidateDAO> Output = await DataContext.Candidate.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).ToListAsync();

            Assert.AreEqual(Output.Count(), Candidates.Count());

            int length = Output.Count();
            for (int i = 0; i < length; i++)
            {
                if (Output[i] != null && Candidates[i] != null)
                {
                    Assert.AreEqual(Candidates[i].Id, Output[i].Id);
                    Assert.AreEqual(Candidates[i].FullName, Output[i].FullName);
                    Assert.AreEqual(Candidates[i].Birthday.ToString("dd-MM-yyyy HH:mm:ss"), Output[i].Birthday.ToString("dd-MM-yyyy HH:mm:ss"));
                    Assert.AreEqual(Candidates[i].StatusId, Output[i].StatusId);
                    Assert.AreEqual(Candidates[i].PhoneNumber, Output[i].PhoneNumber);
                    Assert.AreEqual(Candidates[i].StatusId, Output[i].StatusId);
                    Assert.AreEqual(Candidates[i].ProvinceId, Output[i].ProvinceId);
                    Assert.AreEqual(Candidates[i].DistrictId, Output[i].DistrictId);
                    Assert.AreEqual(Candidates[i].VillageId, Output[i].VillageId);
                    Assert.AreEqual(Candidates[i].CreatedAt.ToString("dd-MM-yyyy HH:mm:ss"), Output[i].CreatedAt.ToString("dd-MM-yyyy HH:mm:ss"));
                    Assert.AreEqual(Candidates[i].UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss"), Output[i].UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss"));
                }
            }
        }

        public async Task Then_ListFilterCandidate_Success()
        {

        }

        private IQueryable<CandidateDAO> DynamicOrder(IQueryable<CandidateDAO> query, CandidateFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case CandidateOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case CandidateOrder.FullName:
                            query = query.OrderBy(q => q.FullName);
                            break;
                        case CandidateOrder.Email:
                            query = query.OrderBy(q => q.Email);
                            break;
                        case CandidateOrder.Status:
                            query = query.OrderBy(q => q.StatusId);
                            break;
                        case CandidateOrder.Used:
                            query = query.OrderBy(q => q.Used);
                            break;
                        case CandidateOrder.PhoneNumber:
                            query = query.OrderBy(q => q.PhoneNumber);
                            break;
                        case CandidateOrder.Birthday:
                            query = query.OrderBy(q => q.Birthday);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case CandidateOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case CandidateOrder.FullName:
                            query = query.OrderByDescending(q => q.FullName);
                            break;
                        case CandidateOrder.Email:
                            query = query.OrderByDescending(q => q.Email);
                            break;
                        case CandidateOrder.Status:
                            query = query.OrderByDescending(q => q.StatusId);
                            break;
                        case CandidateOrder.Used:
                            query = query.OrderByDescending(q => q.Used);
                            break;
                        case CandidateOrder.PhoneNumber:
                            query = query.OrderByDescending(q => q.PhoneNumber);
                            break;
                        case CandidateOrder.Birthday:
                            query = query.OrderByDescending(q => q.Birthday);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private IQueryable<CandidateDAO> OrFilter(IQueryable<CandidateDAO> query, CandidateFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<CandidateDAO> initQuery = query.Where(q => false);

            foreach (CandidateFilter CandidateFilter in filter.OrFilter)
            {
                IQueryable<CandidateDAO> queryable = query;

                queryable = queryable.Where(q => q.Id, filter.Id);
                queryable = queryable.Where(q => q.FullName, filter.FullName);
                queryable = queryable.Where(q => q.Email, filter.Email);
                queryable = queryable.Where(q => q.PhoneNumber, filter.PhoneNumber);
                queryable = queryable.Where(q => q.Birthday, filter.Birthday);
                queryable = queryable.Where(q => q.StatusId, filter.StatusId);



                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }

        private IQueryable<CandidateDAO> DynamicFilter(IQueryable<CandidateDAO> query, CandidateFilter filter)
        {
            if (filter == null)
                return query.Where(q => false);
            query = query.Where(q => !q.DeletedAt.HasValue);
            query = query.Where(q => q.Id, filter.Id);
            query = query.Where(q => q.FullName, filter.FullName);
            query = query.Where(q => q.Email, filter.Email);
            query = query.Where(q => q.PhoneNumber, filter.PhoneNumber);
            query = query.Where(q => q.Birthday, filter.Birthday);
            query = query.Where(q => q.StatusId, filter.StatusId);
            query = OrFilter(query, filter);

            return query;
        }
    }
}
