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
    public interface IJobPositionRepository
    {
        Task<int> Count(JobPositionFilter filter);
        Task<List<JobPosition>> List(JobPositionFilter filter);
        Task<List<JobPosition>> List(List<long> Ids);
        Task<JobPosition> Get(long Id);
        Task<bool> Create(JobPosition JobPosition);
        Task<bool> Update(JobPosition JobPosition);
        Task<bool> Delete(JobPosition JobPosition);
        Task<bool> Used(List<long> Ids);
    }
    public class JobPositionRepository : IJobPositionRepository
    {
        private DataContext DataContext;
        public JobPositionRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;

        }
        private IQueryable<JobPositionDAO> DynamicFilter(IQueryable<JobPositionDAO> query, JobPositionFilter filter)
        {
            if (filter == null)
                return query.Where(q => false);
            query = query.Where(q => q.Id, filter.Id);
            query = query.Where(q => q.Name, filter.Name);
            query = query.Where(q => q.Code, filter.Code);
            query = query.Where(q => q.StatusId, filter.StatusId);
            query = OrFilter(query, filter);
            return query;
        }
        private IQueryable<JobPositionDAO> OrFilter(IQueryable<JobPositionDAO> query, JobPositionFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<JobPositionDAO> initQuery = query.Where(q => false);
            foreach (JobPositionFilter JobPositionFilter in filter.OrFilter)
            {
                IQueryable<JobPositionDAO> queryable = query;
                query = query.Where(q => q.Id, filter.Id);
                query = query.Where(q => q.Name, filter.Name);
                query = query.Where(q => q.Code, filter.Code);
                query = query.Where(q => q.StatusId, filter.StatusId);
                initQuery = initQuery.Union(queryable);
            }
            return query;
        }
        private IQueryable<JobPositionDAO> DynamicOrder(IQueryable<JobPositionDAO> query, JobPositionFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case JobPositionOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case JobPositionOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case JobPositionOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case JobPositionOrder.Status:
                            query = query.OrderBy(q => q.Status.Id);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case JobPositionOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case JobPositionOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case JobPositionOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case JobPositionOrder.Status:
                            query = query.OrderBy(q => q.Status.Id);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }
        private async Task<List<JobPosition>> DynamicSelect(IQueryable<JobPositionDAO> query, JobPositionFilter filter)
        {
            List<JobPosition> JobPositions = await query.Select(q => new JobPosition()
            {
                Id = filter.Selects.Contains(JobPositionSelect.Id) ? q.Id : default(long),
                Name = filter.Selects.Contains(JobPositionSelect.Name) ? q.Name : default(string),
                Code = filter.Selects.Contains(JobPositionSelect.Code) ? q.Code : default(string),
                StatusId = filter.Selects.Contains(JobPositionSelect.Status) ? q.StatusId : default(long),
                Status = filter.Selects.Contains(EmployeeSelect.Status) && q.Status != null ? new Status
                {
                    Id = q.Status.Id,
                    Name = q.Status.Name,
                    Code = q.Status.Code,
                } : null
            }).ToListAsync();
            return JobPositions;
        }
        public async Task<int> Count(JobPositionFilter filter)
        {
            IQueryable<JobPositionDAO> JobPositions = DataContext.JobPosition;
            JobPositions = DynamicFilter(JobPositions, filter);
            return await JobPositions.CountAsync();
        }

        public async Task<bool> Create(JobPosition JobPosition)
        {
            JobPositionDAO JobPositionDAO = new JobPositionDAO();
            JobPositionDAO.Id = JobPosition.Id;
            JobPositionDAO.Name = JobPosition.Name;
            JobPositionDAO.Code = JobPosition.Code;
            JobPositionDAO.StatusId = JobPosition.StatusId;
            JobPositionDAO.Used = false;
            JobPositionDAO.CreatedAt = StaticParams.DateTimeNow;
            JobPositionDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.JobPosition.Add(JobPositionDAO);
            await DataContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(JobPosition JobPosition)
        {
            await DataContext.JobPosition.Where(x => x.Id == JobPosition.Id).UpdateFromQueryAsync(x => new JobPositionDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        public async Task<JobPosition> Get(long Id)
        {
            JobPosition JobPosition = await DataContext.JobPosition.AsNoTracking().Where(x => x.Id == Id).Select(x => new JobPosition()
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
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
                }

            }).FirstOrDefaultAsync();
            if (JobPosition == null)
                return null;
            return JobPosition;
        }

        public async Task<List<JobPosition>> List(JobPositionFilter filter)
        {
            if (filter == null)
                return new List<JobPosition>();
            IQueryable<JobPositionDAO> JobPositionDAOs = DataContext.JobPosition.AsTracking();
            JobPositionDAOs = DynamicFilter(JobPositionDAOs, filter);
            JobPositionDAOs = DynamicOrder(JobPositionDAOs, filter);
            List<JobPosition> JobPositions = await DynamicSelect(JobPositionDAOs, filter);
            return JobPositions;
        }

        public async Task<List<JobPosition>> List(List<long> Ids)
        {
            List<JobPosition> JobPositions = await DataContext.JobPosition.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new JobPosition()
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
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
                }
            }).ToListAsync();

            return JobPositions;
        }

        public async Task<bool> Update(JobPosition JobPosition)
        {
            JobPositionDAO JobPositionDAO = DataContext.JobPosition.Where(x => x.Id == JobPosition.Id).FirstOrDefault();
            if (JobPositionDAO == null)
                return false;
            JobPositionDAO.Id = JobPosition.Id;
            JobPositionDAO.Name = JobPosition.Name;
            JobPositionDAO.Code = JobPosition.Code;
            JobPositionDAO.StatusId = JobPosition.StatusId;
            JobPositionDAO.Used = false;
            JobPositionDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Used(List<long> Ids)
        {
            await DataContext.JobPosition.Where(x => Ids.Contains(x.Id))
           .UpdateFromQueryAsync(x => new JobPositionDAO { Used = true });
            return true;
        }
    }
}
