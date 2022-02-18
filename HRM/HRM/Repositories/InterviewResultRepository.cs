using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRM.Entities;
using HRM.Helpers;
using HRM.Models;
using Microsoft.EntityFrameworkCore;
using TrueSight.Common;
namespace HRM.Repositories
{
    public interface IInterviewResultRepository
    {
        Task<int> Count(InterviewResultFilter filter);
        Task<List<InterviewResult>> List(InterviewResultFilter filter);
        Task<List<InterviewResult>> List(List<long> Ids);
        Task<InterviewResult> Get(long Id);
        Task<bool> Create(InterviewResult InterviewResult);
        Task<bool> Update(InterviewResult InterviewResult);
        Task<bool> Delete(InterviewResult InterviewResult);
        Task<bool> Used(List<long> Ids);
    }
    public class InterviewResultRepository : IInterviewResultRepository
    {
        private DataContext DataContext;
        public InterviewResultRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;

        }
        private IQueryable<InterviewResultDAO> DynamicFilter(IQueryable<InterviewResultDAO> query, InterviewResultFilter filter)
        {
            if (filter == null)
                return query.Where(q => false);
            query = query.Where(q => q.Id, filter.Id);
            query = query.Where(q => q.CandidateId, filter.CandidateId);
            query = query.Where(q => q.InterviewTime, filter.InterviewTime);
            query = query.Where(q => q.PassInterview, filter.PassInterview);
            query = query.Where(q => q.BeginJobTime, filter.BeginJobTime);
            query = query.Where(q => q.TrialTime, filter.TrialTime);
            query = query.Where(q => q.StatusId, filter.StatusId);
            query = OrFilter(query, filter);
            return query;
        }
        private IQueryable<InterviewResultDAO> OrFilter(IQueryable<InterviewResultDAO> query, InterviewResultFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<InterviewResultDAO> initQuery = query.Where(q => false);
            foreach (InterviewResultFilter InterviewResultFilter in filter.OrFilter)
            {
                IQueryable<InterviewResultDAO> queryable = query;
                queryable = queryable.Where(q => q.Id, filter.Id);
                queryable = queryable.Where(q => q.CandidateId, InterviewResultFilter.CandidateId);
                queryable = queryable.Where(q => q.InterviewTime, InterviewResultFilter.InterviewTime);
                queryable = queryable.Where(q => q.PassInterview, InterviewResultFilter.PassInterview);
                queryable = queryable.Where(q => q.BeginJobTime, InterviewResultFilter.BeginJobTime);
                queryable = queryable.Where(q => q.TrialTime, InterviewResultFilter.TrialTime);
                queryable = queryable.Where(q => q.StatusId, InterviewResultFilter.StatusId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }
        private IQueryable<InterviewResultDAO> DynamicOrder(IQueryable<InterviewResultDAO> query, InterviewResultFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case InterviewResultOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case InterviewResultOrder.CandidateId:
                            query = query.OrderBy(q => q.CandidateId);
                            break;
                        case InterviewResultOrder.InterviewTime:
                            query = query.OrderBy(q => q.InterviewTime);
                            break;
                        case InterviewResultOrder.PassInterview:
                            query = query.OrderBy(q => q.PassInterview);
                            break;
                        case InterviewResultOrder.BeginJobTime:
                            query = query.OrderBy(q => q.BeginJobTime);
                            break;
                        case InterviewResultOrder.TrialTime:
                            query = query.OrderBy(q => q.TrialTime);
                            break;
                        case InterviewResultOrder.Status:
                            query = query.OrderBy(q => q.Status.Id);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case InterviewResultOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case InterviewResultOrder.CandidateId:
                            query = query.OrderBy(q => q.CandidateId);
                            break;
                        case InterviewResultOrder.InterviewTime:
                            query = query.OrderBy(q => q.InterviewTime);
                            break;
                        case InterviewResultOrder.PassInterview:
                            query = query.OrderBy(q => q.PassInterview);
                            break;
                        case InterviewResultOrder.BeginJobTime:
                            query = query.OrderBy(q => q.BeginJobTime);
                            break;
                        case InterviewResultOrder.TrialTime:
                            query = query.OrderBy(q => q.TrialTime);
                            break;
                        case InterviewResultOrder.Status:
                            query = query.OrderBy(q => q.Status.Id);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }
        private async Task<List<InterviewResult>> DynamicSelect(IQueryable<InterviewResultDAO> query, InterviewResultFilter filter)
        {
            List<InterviewResult> InterviewResults = await query.Select(q => new InterviewResult()
            {
                Id = filter.Selects.Contains(InterviewResultSelect.Id) ? q.Id : default(long),
                CandidateId = filter.Selects.Contains(InterviewResultSelect.CandidateId) ? q.CandidateId : default(long),
                InterviewTime = filter.Selects.Contains(InterviewResultSelect.InterviewTime) ? q.InterviewTime : default(DateTime),
                PassInterview = filter.Selects.Contains(InterviewResultSelect.PassInterview) ? q.PassInterview : default(bool),
                BeginJobTime = filter.Selects.Contains(InterviewResultSelect.BeginJobTime) ? q.BeginJobTime : default(DateTime),
                TrialTime = filter.Selects.Contains(InterviewResultSelect.TrialTime) ? q.TrialTime : default(DateTime),
                StatusId = filter.Selects.Contains(InterviewResultSelect.Status) ? q.StatusId : default(long),
                Status = filter.Selects.Contains(EmployeeSelect.Status) && q.Status != null ? new Status
                {
                    Id = q.Status.Id,
                    Name = q.Status.Name,
                    Code = q.Status.Code,
                } : null
            }).ToListAsync();
            return InterviewResults;
        }
        public async Task<int> Count(InterviewResultFilter filter)
        {
            IQueryable<InterviewResultDAO> InterviewResults = DataContext.InterviewResult;
            InterviewResults = DynamicFilter(InterviewResults, filter);
            return await InterviewResults.CountAsync();
        }

        public async Task<List<InterviewResult>> List(InterviewResultFilter filter)
        {
            if(filter == null)
                return new List<InterviewResult>();
            IQueryable<InterviewResultDAO> InterviewResultDAOs = DataContext.InterviewResult.AsTracking();
            InterviewResultDAOs = DynamicFilter(InterviewResultDAOs, filter);
            InterviewResultDAOs = DynamicOrder(InterviewResultDAOs, filter);
            List<InterviewResult> InterviewResults = await DynamicSelect(InterviewResultDAOs, filter);
            return InterviewResults;
        }

        public async Task<List<InterviewResult>> List(List<long> Ids)
        {
            List<InterviewResult> InterviewResults = await DataContext.InterviewResult.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new InterviewResult()
            {
                Id = x.Id,
                CandidateId= x.CandidateId,
                InterviewTime = x.InterviewTime,
                PassInterview = x.PassInterview,
                BeginJobTime = x.BeginJobTime,
                TrialTime = x.TrialTime,
                StatusId = x.StatusId,
                Used = x.Used,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                DeletedAt = x.DeletedAt,
                Status = x.Status == null ? null : new Status()
                {
                    Id = x.Status.Id,
                    Name = x.Status.Name,
                    Code = x.Status.Code
                },
                Candidate = x.Candidate == null ? null : new Candidate()
                {
                    Id = x.Candidate.Id,
                    FullName = x.Candidate.FullName,
                    Email = x.Candidate.Email,
                    PhoneNumber = x.Candidate.PhoneNumber,
                    Birthday = x.Candidate.Birthday,
                    StatusId = x.Candidate.StatusId,
                    VillageId = x.Candidate.VillageId,
                    DistrictId = x.Candidate.DistrictId,
                    ProvinceId = x.Candidate.ProvinceId

                }
            }).ToListAsync();
            var x = InterviewResults;
            return InterviewResults;
        }

        public async Task<InterviewResult> Get(long Id)
        {
            InterviewResult InterviewResult = await DataContext.InterviewResult.AsNoTracking().Where(x => x.Id == Id).Select(x => new InterviewResult()
            {
                Id = x.Id,
                CandidateId = x.CandidateId,
                InterviewTime = x.InterviewTime,
                PassInterview = x.PassInterview,
                BeginJobTime = x.BeginJobTime,
                TrialTime = x.TrialTime,
                StatusId = x.StatusId,
                Used = x.Used,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                DeletedAt = x.DeletedAt,
                Status = x.Status == null ? null : new Status()
                {
                    Id = x.Status.Id,
                    Name = x.Status.Name,
                    Code = x.Status.Code
                },
                Candidate = x.Candidate == null ? null : new Candidate()
                {
                    Id = x.Candidate.Id,
                    FullName = x.Candidate.FullName,
                    Email = x.Candidate.Email,
                    PhoneNumber = x.Candidate.PhoneNumber,
                    Birthday = x.Candidate.Birthday,
                    StatusId = x.Candidate.StatusId,
                    VillageId = x.Candidate.VillageId,
                    DistrictId = x.Candidate.DistrictId,
                    ProvinceId = x.Candidate.ProvinceId

                }

            }).FirstOrDefaultAsync();
            if (InterviewResult == null)
                return null;
            return InterviewResult;
        }

        public async Task<bool> Create(InterviewResult InterviewResult)
        {
            InterviewResultDAO InterviewResultDAO = new InterviewResultDAO();
            InterviewResultDAO.Id = InterviewResult.Id;
            InterviewResultDAO.CandidateId = InterviewResult.CandidateId;
            InterviewResultDAO.InterviewTime = InterviewResult.InterviewTime;
            InterviewResultDAO.PassInterview = InterviewResult.PassInterview;
            InterviewResultDAO.BeginJobTime = InterviewResult.BeginJobTime;
            InterviewResultDAO.TrialTime = InterviewResult.TrialTime;
            InterviewResultDAO.StatusId = InterviewResult.StatusId;
            InterviewResultDAO.Used = false;
            InterviewResultDAO.CreatedAt = StaticParams.DateTimeNow;
            InterviewResultDAO.UpdatedAt = StaticParams.DateTimeNow;

            DataContext.InterviewResult.Add(InterviewResultDAO);
            await DataContext.SaveChangesAsync();
            InterviewResult.Id = InterviewResultDAO.Id;
            return true;
        }

        public async Task<bool> Update(InterviewResult InterviewResult)
        {
            InterviewResultDAO InterviewResultDAO = DataContext.InterviewResult.Where(x => x.Id == InterviewResult.Id).FirstOrDefault();
            if (InterviewResultDAO == null)
                return false;
            InterviewResultDAO.Id = InterviewResult.Id;
            InterviewResultDAO.CandidateId = InterviewResult.CandidateId;
            InterviewResultDAO.InterviewTime = InterviewResult.InterviewTime;
            InterviewResultDAO.PassInterview = InterviewResult.PassInterview;
            InterviewResultDAO.BeginJobTime = InterviewResult.BeginJobTime;
            InterviewResultDAO.TrialTime = InterviewResult.TrialTime;
            InterviewResultDAO.StatusId = InterviewResult.StatusId;
            InterviewResultDAO.Used = false;
            InterviewResultDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(InterviewResult InterviewResult)
        {
            await DataContext.InterviewResult.Where(x => x.Id == InterviewResult.Id).UpdateFromQueryAsync(x => new InterviewResultDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        public async Task<bool> Used(List<long> Ids)
        {
            await DataContext.InterviewResult.Where(x => Ids.Contains(x.Id))
            .UpdateFromQueryAsync(x => new InterviewResultDAO { Used = true });
            return true;
        }
    }
}
