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
    public interface IAcademicLevelRepository
    {
        Task<int> Count(AcademicLevelFilter AcademicLevelFilter);
        Task<List<AcademicLevel>> List(AcademicLevelFilter AcademicLevelFilter);
        Task<List<AcademicLevel>> List(List<long> Ids);
        Task<AcademicLevel> Get(long Id);
        Task<bool> Create(AcademicLevel AcademicLevel);
        Task<bool> Update(AcademicLevel AcademicLevel);
        Task<bool> Delete(AcademicLevel AcademicLevel);
        Task<bool> BulkDelete(List<AcademicLevel> AcademicLevels);
        // Task<bool> SimpleBulkMerge(List<AcademicLevel> AcademicLevels);
        // Task<List<long>> BulkMerge(List<AcademicLevel> AcademicLevels);
        // Task<bool> BulkUpdate(List<AcademicLevel> AcademicLevels);
        Task<bool> Used(List<long> Ids);
    }
    public class AcademicLevelRepository : IAcademicLevelRepository
    {
        private DataContext DataContext;
        public AcademicLevelRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }
        public async Task<bool> BulkDelete(List<AcademicLevel> AcademicLevels)
        {
            List<long> Ids = AcademicLevels.Select(x => x.Id).ToList();

            await DataContext.AcademicLevel.Where(x => Ids.Contains(x.Id))
            .UpdateFromQueryAsync(x => new AcademicLevelDAO() { UpdatedAt = StaticParams.DateTimeNow, DeletedAt = StaticParams.DateTimeNow });

            return true;
        }

        public async Task<int> Count(AcademicLevelFilter filter)
        {
            IQueryable<AcademicLevelDAO> AcademicLevels = DataContext.AcademicLevel;
            AcademicLevels = DynamicFilter(AcademicLevels, filter);

            return await AcademicLevels.CountAsync();
        }

        public async Task<bool> Create(AcademicLevel AcademicLevel)
        {
            AcademicLevelDAO AcademicLevelDAO = new AcademicLevelDAO()
            {
                Id = AcademicLevel.Id,
                Name = AcademicLevel.Name,
                Code = AcademicLevel.Code,
                StatusId = AcademicLevel.StatusId,
                Used = false,
                CreatedAt = StaticParams.DateTimeNow,
                UpdatedAt = StaticParams.DateTimeNow
            };

            DataContext.AcademicLevel.Add(AcademicLevelDAO);
            await DataContext.SaveChangesAsync();
            AcademicLevel.Id = AcademicLevelDAO.Id;
            await SaveReference(AcademicLevel);

            return true;
        }

        public async Task<bool> Delete(AcademicLevel AcademicLevel)
        {
            await DataContext.AcademicLevel.Where(x => x.Id == AcademicLevel.Id)
            .UpdateFromQueryAsync(x => new AcademicLevelDAO
            {
                UpdatedAt = StaticParams.DateTimeNow,
                DeletedAt = StaticParams.DateTimeNow
            });

            return true;
        }

        public async Task<AcademicLevel> Get(long Id)
        {
            AcademicLevel AcademicLevel = await DataContext.AcademicLevel.AsNoTracking()
            .Where(x => x.Id == Id).Select(x => new AcademicLevel()
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                StatusId = x.StatusId,
                Used = x.Used,

                Status = x.Status == null ? null : new Status
                {
                    Id = x.Status.Id,
                    Name = x.Status.Name,
                    Code = x.Status.Code,
                }
            }
            ).FirstOrDefaultAsync();

            if (AcademicLevel == null)
            {
                return null;
            }

            return AcademicLevel;
        }

        public async Task<List<AcademicLevel>> List(AcademicLevelFilter filter)
        {
            if (filter == null) return new List<AcademicLevel>();

            IQueryable<AcademicLevelDAO> AcademicLevelDAOs = DataContext.AcademicLevel.AsNoTracking();
            AcademicLevelDAOs = DynamicFilter(AcademicLevelDAOs, filter);
            AcademicLevelDAOs = DynamicOrder(AcademicLevelDAOs, filter);
            List<AcademicLevel> AcademicLevels = await DynamicSelect(AcademicLevelDAOs, filter);

            return AcademicLevels;
        }

        public async Task<List<AcademicLevel>> List(List<long> Ids)
        {
            List<AcademicLevel> AcademicLevels = await DataContext.AcademicLevel.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new AcademicLevel()
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                StatusId = x.StatusId,
                Used = x.Used,

                Status = x.Status == null ? null : new Status
                {
                    Id = x.Status.Id,
                    Name = x.Status.Name,
                    Code = x.Status.Code,
                }
            }).ToListAsync();

            return AcademicLevels;
        }

        public async Task<bool> Update(AcademicLevel AcademicLevel)
        {
            AcademicLevelDAO AcademicLevelDAO = DataContext.AcademicLevel.Where(x => x.Id == AcademicLevel.Id).FirstOrDefault();
            if (AcademicLevelDAO == null)
                return false;
            AcademicLevelDAO.Id = AcademicLevel.Id;
            AcademicLevelDAO.Name = AcademicLevel.Name;
            AcademicLevelDAO.Code = AcademicLevel.Code;
            AcademicLevelDAO.StatusId = AcademicLevel.StatusId;
            AcademicLevel.UpdatedAt = StaticParams.DateTimeNow;

            await DataContext.SaveChangesAsync();
            await SaveReference(AcademicLevel);

            return true;
        }

        private async Task SaveReference(AcademicLevel AcademicLevel)
        {

        }

        public async Task<bool> Used(List<long> Ids)
        {
            await DataContext.AcademicLevel.Where(x => Ids.Contains(x.Id))
               .UpdateFromQueryAsync(x => new AcademicLevelDAO { Used = true });
            return true;
        }

        private async Task<List<AcademicLevel>> DynamicSelect(IQueryable<AcademicLevelDAO> query, AcademicLevelFilter filter)
        {
            List<AcademicLevel> AcademicLevels = await query.Select(q => new AcademicLevel()
            {
                Id = filter.Selects.Contains(AcademicLevelSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(AcademicLevelSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(AcademicLevelSelect.Name) ? q.Name : default(string),

                StatusId = filter.Selects.Contains(AcademicLevelSelect.Status) ? q.StatusId : default(long),
                Used = filter.Selects.Contains(AcademicLevelSelect.Used) ? q.Used : default(bool),

                Status = filter.Selects.Contains(AcademicLevelSelect.Status) && q.Status != null ? new Status
                {
                    Id = q.Status.Id,
                    Code = q.Status.Code,
                    Name = q.Status.Name,
                } : null,
            }).ToListAsync();

            return AcademicLevels;
        }

        private IQueryable<AcademicLevelDAO> DynamicOrder(IQueryable<AcademicLevelDAO> query, AcademicLevelFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case AcademicLevelOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case AcademicLevelOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case AcademicLevelOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case AcademicLevelOrder.Status:
                            query = query.OrderBy(q => q.StatusId);
                            break;
                        case AcademicLevelOrder.Used:
                            query = query.OrderBy(q => q.Used);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case AcademicLevelOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case AcademicLevelOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case AcademicLevelOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case AcademicLevelOrder.Status:
                            query = query.OrderByDescending(q => q.StatusId);
                            break;
                        case AcademicLevelOrder.Used:
                            query = query.OrderByDescending(q => q.Used);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private IQueryable<AcademicLevelDAO> OrFilter(IQueryable<AcademicLevelDAO> query, AcademicLevelFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<AcademicLevelDAO> initQuery = query.Where(q => false);
            foreach (AcademicLevelFilter AcademicLevelFilter in filter.OrFilter)
            {
                IQueryable<AcademicLevelDAO> queryable = query;
                queryable = queryable.Where(q => q.Id, filter.Id);
                queryable = queryable.Where(q => q.Code, filter.Code);
                queryable = queryable.Where(q => q.Name, filter.Name);
                queryable = queryable.Where(q => q.StatusId, filter.StatusId);

                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }

        private IQueryable<AcademicLevelDAO> DynamicFilter(IQueryable<AcademicLevelDAO> query, AcademicLevelFilter filter)
        {
            if (filter == null)
            {
                return query.Where(q => false);
            }

            query = query.Where(q => !q.DeletedAt.HasValue);
            query = query.Where(q => q.Id, filter.Id);

            if (filter.Code != null && filter.Code.HasValue)
            {
                query = query.Where(q => q.Code, filter.Code);
            }
            if (filter.Name != null && filter.Name.HasValue)
            {
                query = query.Where(q => q.Name, filter.Name);
            }

            query = query.Where(q => q.StatusId, filter.StatusId);

            query = OrFilter(query, filter);
            return query;

        }


    }
}