using HRM.Entities;
using HRM.Helpers;
using HRM.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using TrueSight.Common;

namespace HRM.Repositories
{
    public interface IDistrictRepository
    {
        Task<int> Count(DistrictFilter DistrictFilter);
        Task<List<District>> List(DistrictFilter DistrictFilter);
        Task<List<District>> List(List<long> Ids);
        Task<District> Get(long Id);
        Task<bool> Create(District District);
        Task<bool> Update(District District);
        Task<bool> Delete(District District);
        //Task<bool> BulkMerge(List<District> Districts);
        Task<bool> BulkDelete(List<District> Districts);
        Task<bool> Used(List<long> Ids);
    }
    public class DistrictRepository : IDistrictRepository
    {
        private DataContext DataContext;
        public DistrictRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }


        private IQueryable<DistrictDAO> DynamicFilter(IQueryable<DistrictDAO> query, DistrictFilter filter)
        {
            if (filter == null)
                return query.Where(q => false);
            query = query.Where(q => !q.DeletedAt.HasValue);
            query = query.Where(q => q.Id, filter.Id);
            query = query.Where(q => q.Name, filter.Name);
            query = query.Where(q => q.Code, filter.Code);
            query = query.Where(q => q.ProvinceId, filter.ProvinceId);
            query = query.Where(q => q.StatusId, filter.StatusId);
            query = OrFilter(query, filter);
            return query;
        }
        private IQueryable<DistrictDAO> OrFilter(IQueryable<DistrictDAO> query, DistrictFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<DistrictDAO> initQuery = query.Where(q => false);
            foreach (DistrictFilter DistrictFilter in filter.OrFilter)
            {
                IQueryable<DistrictDAO> queryable = query;
                queryable = queryable.Where(q => q.Id, DistrictFilter.Id);
                queryable = queryable.Where(q => q.Code, DistrictFilter.Code);
                queryable = queryable.Where(q => q.Name, DistrictFilter.Name);
                queryable = queryable.Where(q => q.ProvinceId, DistrictFilter.ProvinceId);
                queryable = queryable.Where(q => q.StatusId, DistrictFilter.StatusId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }

        private IQueryable<DistrictDAO> DynamicOrder(IQueryable<DistrictDAO> query, DistrictFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case DistrictOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case DistrictOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case DistrictOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case DistrictOrder.Province:
                            query = query.OrderBy(q => q.Province.Name);
                            break;
                        case DistrictOrder.Status:
                            query = query.OrderBy(q => q.StatusId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case DistrictOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case DistrictOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case DistrictOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case DistrictOrder.Province:
                            query = query.OrderByDescending(q => q.Province.Name);
                            break;
                        case DistrictOrder.Status:
                            query = query.OrderByDescending(q => q.StatusId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<District>> DynamicSelect(IQueryable<DistrictDAO> query, DistrictFilter filter)
        {
            List<District> Districts = await query.Select(q => new District()
            {
                Id = filter.Selects.Contains(DistrictSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(DistrictSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(DistrictSelect.Name) ? q.Name : default(string),
                ProvinceId = filter.Selects.Contains(DistrictSelect.Province) ? q.ProvinceId : default(long),
                StatusId = filter.Selects.Contains(DistrictSelect.Status) ? q.StatusId : default(long),
                Province = filter.Selects.Contains(DistrictSelect.Province) && q.Province != null ? new Province
                {
                    Id = q.Province.Id,
                    Name = q.Province.Name,
                    StatusId = q.Province.StatusId,
                } : null,
                Status = filter.Selects.Contains(DistrictSelect.Status) && q.Status != null ? new Status
                {
                    Id = q.Status.Id,
                    Code = q.Status.Code,
                    Name = q.Status.Name,
                } : null,
                Used = q.Used,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
                DeletedAt = q.DeletedAt,
            }).ToListAsync();
            return Districts;
        }

        public async Task<bool> BulkDelete(List<District> Districts)
        {
            DateTime Now = StaticParams.DateTimeNow;
            Districts.ForEach(d => d.DeletedAt = Now);
            List<long> Ids = Districts.Select(x => x.Id).ToList();
            await DataContext.District
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new DistrictDAO { DeletedAt = Now });
            return true;
        }

        public async Task<int> Count(DistrictFilter DistrictFilter)
        {
            IQueryable<DistrictDAO> Districts = DataContext.District;
            Districts = DynamicFilter(Districts, DistrictFilter);
            return await Districts.CountAsync();
        }

        public async Task<bool> Create(District District)
        {
            DistrictDAO DistrictDAO = new DistrictDAO();
            DistrictDAO.Id = District.Id;
            DistrictDAO.Code = District.Code;
            DistrictDAO.Name = District.Name;
            DistrictDAO.ProvinceId = District.ProvinceId;
            DistrictDAO.StatusId = District.StatusId;
            DistrictDAO.CreatedAt = StaticParams.DateTimeNow;
            DistrictDAO.UpdatedAt = StaticParams.DateTimeNow;
            DistrictDAO.Used = false;

            DataContext.District.Add(DistrictDAO);
            await DataContext.SaveChangesAsync();
            District.Id = DistrictDAO.Id;
            return true;
        }

        public async Task<bool> Delete(District District)
        {
            DateTime Now = StaticParams.DateTimeNow;
            District.DeletedAt = Now;
            await DataContext.District.Where(x => x.Id == District.Id).UpdateFromQueryAsync(x => new DistrictDAO { DeletedAt = Now });
            return true;
        }

        public Task<District> Get(long Id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<District>> List(DistrictFilter DistrictFilter)
        {
            if (DistrictFilter == null) return new List<District>();
            IQueryable<DistrictDAO> DistrictDAOs = DataContext.District;
            DistrictDAOs = DynamicFilter(DistrictDAOs, DistrictFilter);
            DistrictDAOs = DynamicOrder(DistrictDAOs, DistrictFilter);
            List<District> Districts = await DynamicSelect(DistrictDAOs, DistrictFilter);
            return Districts;
        }

        public async Task<List<District>> List(List<long> Ids)
        {
            IdFilter IdFilter = new IdFilter { In = Ids };
            List<District> Districts = await DataContext.District.AsNoTracking()
            .Where(x => x.Id, IdFilter)
            .Select(x => new District()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                DeletedAt = x.DeletedAt,
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                ProvinceId = x.ProvinceId,
                StatusId = x.StatusId,
                Used = x.Used,
                Status = x.Status == null ? null : new Status
                {
                    Id = x.Status.Id,
                    Code = x.Status.Code,
                    Name = x.Status.Name,
                },
            }).ToListAsync();

            return Districts;
        }

        public async Task<bool> Update(District District)
        {
            DistrictDAO DistrictDAO = DataContext.District.Where(x => x.Id == District.Id).FirstOrDefault();
            if (DistrictDAO == null)
                return false;
            DistrictDAO.Id = District.Id;
            DistrictDAO.Code = District.Code;
            DistrictDAO.Name = District.Name;
            DistrictDAO.ProvinceId = District.ProvinceId;
            DistrictDAO.StatusId = District.StatusId;
            DistrictDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Used(List<long> Ids)
        {
            await DataContext.District.Where(x => Ids.Contains(x.Id))
               .UpdateFromQueryAsync(x => new DistrictDAO { Used = true });
            return true;
        }
    }

}
