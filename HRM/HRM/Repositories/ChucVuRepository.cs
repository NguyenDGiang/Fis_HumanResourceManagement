using System.Collections.Generic;
using System.Threading.Tasks;
using HRM.Entities;
using HRM.Models;
using Microsoft.EntityFrameworkCore;
using TrueSight.Common;
using System.Linq;
using HRM.Helpers;

namespace HRM.Repositories
{
    public interface IChucVuRepository
    {
        Task<int> Count(ChucVuFilter ChucVuFilter);
        Task<List<ChucVu>> List(ChucVuFilter ChucVuFilter);
        Task<List<ChucVu>> List(List<long> Ids);
        Task<ChucVu> Get(long Id);
        Task<bool> Create(ChucVu ChucVu);
        Task<bool> Update(ChucVu ChucVu);
        Task<bool> Delete(ChucVu ChucVu);
        Task<bool> BulkDelete(List<ChucVu> ChucVus);
        // Task<bool> SimpleBulkMerge(List<ChucVu> ChucVus);
        // Task<List<long>> BulkMerge(List<ChucVu> ChucVus);
        Task<bool> BulkUpdate(List<ChucVu> ChucVus);
        Task<bool> Used(List<long> Ids);
    }
    public class ChucVuRepository : IChucVuRepository
    {
        private DataContext DataContext;
        public ChucVuRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        public async Task<bool> BulkDelete(List<ChucVu> ChucVus)
        {
            List<long> Ids = ChucVus.Select(x => x.Id).ToList();
            await DataContext.ChucVu
            .Where(x => Ids.Contains(x.Id))
            .UpdateFromQueryAsync(x => new ChucVuDAO { DeletedAt = StaticParams.DateTimeNow, UpdatedAt = StaticParams.DateTimeNow });

            return true;
        }

        // public async Task<List<long>> BulkMerge(List<ChucVu> ChucVus)
        // {
        //     List<ChucVuDAO> ChucVuDAOs = new List<ChucVuDAO>();

        //     foreach (ChucVu ChucVu in ChucVus)
        //     {
        //         ChucVuDAO ChucVuDAO = new ChucVuDAO();
        //         ChucVuDAO.Id = ChucVu.Id;
        //         ChucVuDAO.Name = ChucVu.Name;
        //         ChucVuDAO.Code = ChucVu.Code;
        //         ChucVuDAO.StatusId = ChucVu.StatusId;
        //         ChucVuDAO.Used = ChucVu.Used;
        //         ChucVuDAO.CreatedAt = StaticParams.DateTimeNow;
        //         ChucVuDAO.UpdatedAt = StaticParams.DateTimeNow;

        //         ChucVuDAOs.Add(ChucVuDAO);
        //     }
        //     await DataContext.BulkMergeAsync(ChucVuDAOs);



        // }

        public Task<bool> BulkUpdate(List<ChucVu> ChucVus)
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> Count(ChucVuFilter filter)
        {
            IQueryable<ChucVuDAO> ChucVus = DataContext.ChucVu.AsNoTracking();
            ChucVus = DynamicFilter(ChucVus, filter);
            return await ChucVus.CountAsync();
        }

        public async Task<bool> Create(ChucVu ChucVu)
        {
            ChucVuDAO ChucVuDAO = new ChucVuDAO();
            ChucVuDAO.Id = ChucVu.Id;
            ChucVuDAO.Name = ChucVu.Name;
            ChucVuDAO.Code = ChucVu.Code;
            ChucVuDAO.StatusId = ChucVu.StatusId;
            ChucVuDAO.Used = ChucVu.Used;
            ChucVuDAO.CreatedAt = StaticParams.DateTimeNow;
            ChucVuDAO.UpdatedAt = StaticParams.DateTimeNow;

            DataContext.ChucVu.Add(ChucVuDAO);
            await DataContext.SaveChangesAsync();
            ChucVu.Id = ChucVuDAO.Id;
            await SaveReference(ChucVu);

            return true;
        }

        public async Task<bool> Delete(ChucVu ChucVu)
        {
            // ChucVuDAO ChucVuDAO = await DataContext.ChucVu.Where(x => x.Id == ChucVu.Id).FirstOrDefaultAsync();
            await DataContext.ChucVu
            .Where(x => x.Id == ChucVu.Id)
            .UpdateFromQueryAsync(x => new ChucVuDAO
            {
                UpdatedAt = StaticParams.DateTimeNow,
                DeletedAt = StaticParams.DateTimeNow
            });

            return true;
        }

        public async Task<ChucVu> Get(long Id)
        {
            ChucVu ChucVu = await DataContext.ChucVu.AsNoTracking()
            .Where(c => c.Id == Id).Select(c => new ChucVu()
            {
                Id = c.Id,
                Name = c.Name,
                Code = c.Code,
                Used = c.Used,
                StatusId = c.StatusId,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt,
                DeletedAt = c.DeletedAt,

                Status = c.Status == null ? null : new Status
                {
                    Id = c.Status.Id,
                    Code = c.Status.Code,
                    Name = c.Status.Name
                }
            }
            ).FirstOrDefaultAsync();

            if (ChucVu == null)
            {
                return null;
            }

            return ChucVu;
        }

        public async Task<List<ChucVu>> List(ChucVuFilter filter)
        {
            if (filter == null) return new List<ChucVu>();
            IQueryable<ChucVuDAO> ChucVuDAOs = DataContext.ChucVu.AsNoTracking();
            ChucVuDAOs = DynamicFilter(ChucVuDAOs, filter);
            ChucVuDAOs = DynamicOrder(ChucVuDAOs, filter);
            List<ChucVu> ChucVus = await DynamicSelect(ChucVuDAOs, filter);
            return ChucVus;
        }

        public async Task<List<ChucVu>> List(List<long> Ids)
        {
            List<ChucVu> ChucVus = await DataContext.ChucVu.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new ChucVu()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                DeletedAt = x.DeletedAt,
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                StatusId = x.StatusId,
                Used = x.Used,
                Status = x.Status == null ? null : new Status
                {
                    Id = x.Status.Id,
                    Code = x.Status.Code,
                    Name = x.Status.Name,
                },
            }).ToListAsync();

            return ChucVus;
        }

        public Task<bool> SimpleBulkMerge(List<ChucVu> ChucVus)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> Update(ChucVu ChucVu)
        {
            ChucVuDAO ChucVuDAO = DataContext.ChucVu.Where(x => x.Id == ChucVu.Id).FirstOrDefault();
            if (ChucVuDAO == null)
                return false;

            ChucVuDAO.Id = ChucVu.Id;
            ChucVuDAO.Code = ChucVu.Code;
            ChucVuDAO.Name = ChucVu.Name;
            ChucVuDAO.StatusId = ChucVu.StatusId;
            ChucVuDAO.UpdatedAt = StaticParams.DateTimeNow;

            await DataContext.SaveChangesAsync();
            await SaveReference(ChucVu);

            return true;
        }

        public async Task<bool> Used(List<long> Ids)
        {
            await DataContext.ChucVu.Where(x => Ids.Contains(x.Id))
               .UpdateFromQueryAsync(x => new ChucVuDAO { Used = true });
            return true;
        }

        private async Task SaveReference(ChucVu ChucVu)
        {
        }

        private IQueryable<ChucVuDAO> DynamicFilter(IQueryable<ChucVuDAO> query, ChucVuFilter filter)
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

            // if (!string.IsNullOrWhiteSpace(filter.Search))
            // {
            //     List<string> Tokens = filter.Search.Split(" ").Select(x => x.ToLower()).ToList();

            //     foreach (string Token in Tokens)
            //     {
            //         var queryForCode = query;
            //         var queryForName = query;

            //         if (string.IsNullOrWhiteSpace(Token))
            //             continue;

            //         queryForCode = query.Where(x => x.Code.ToLower().Contains(Token));

            //     }
            // }

            query = OrFilter(query, filter);
            return query;
        }

        private async Task<List<ChucVu>> DynamicSelect(IQueryable<ChucVuDAO> query, ChucVuFilter filter)
        {
            List<ChucVu> ChucVus = await query.Select(q => new ChucVu()
            {
                Id = filter.Selects.Contains(ChucVuSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(ChucVuSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(ChucVuSelect.Name) ? q.Name : default(string),
                Used = filter.Selects.Contains(ChucVuSelect.Used) ? q.Used : default(bool),
                StatusId = filter.Selects.Contains(ChucVuSelect.Status) ? q.StatusId : default(long),

                Status = filter.Selects.Contains(ChucVuSelect.Status) && q.Status != null ? new Status
                {
                    Id = q.Status.Id,
                    Name = q.Status.Name,
                    Code = q.Status.Code
                } : null
            }).ToListAsync();

            return ChucVus;
        }

        IQueryable<ChucVuDAO> DynamicOrder(IQueryable<ChucVuDAO> query, ChucVuFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case ChucVuOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case ChucVuOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case ChucVuOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case ChucVuOrder.Status:
                            query = query.OrderBy(q => q.StatusId);
                            break;
                        case ChucVuOrder.Used:
                            query = query.OrderBy(q => q.Used);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case ChucVuOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case ChucVuOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case ChucVuOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case ChucVuOrder.Status:
                            query = query.OrderByDescending(q => q.StatusId);
                            break;
                        case ChucVuOrder.Used:
                            query = query.OrderByDescending(q => q.Used);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private IQueryable<ChucVuDAO> OrFilter(IQueryable<ChucVuDAO> query, ChucVuFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<ChucVuDAO> initQuery = query.Where(q => false);
            foreach (ChucVuFilter CategoryFilter in filter.OrFilter)
            {
                IQueryable<ChucVuDAO> queryable = query;
                queryable = queryable.Where(q => q.Id, filter.Id);
                queryable = queryable.Where(q => q.Code, filter.Code);
                queryable = queryable.Where(q => q.Name, filter.Name);
                queryable = queryable.Where(q => q.StatusId, filter.StatusId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }

    }
}
