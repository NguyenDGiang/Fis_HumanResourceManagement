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
    public interface IProvinceRepository
    {
        Task<int> Count(ProvinceFilter ProvinceFilter);
        Task<List<Province>> List(ProvinceFilter ProvinceFilter);
        Task<List<Province>> List(List<long> Ids);
        Task<Province> Get(long Id);
        Task<bool> Create(Province Province);
        Task<bool> Update(Province Province);
        Task<bool> Delete(Province Province);
        //Task<bool> BulkMerge(List<Province> Provinces);
        Task<bool> BulkDelete(List<Province> Provinces);
        Task<bool> Used(List<long> Ids);
    }
    public class ProvinceRepository : IProvinceRepository
    {
        private DataContext DataContext;
        public ProvinceRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<ProvinceDAO> DynamicFilter(IQueryable<ProvinceDAO> query, ProvinceFilter filter)
        {
            if (filter == null)
                return query.Where(q => false);
            query = query.Where(q => !q.DeletedAt.HasValue);
            query = query.Where(q => q.Id, filter.Id);
            query = query.Where(q => q.Code, filter.Code);
            query = query.Where(q => q.Name, filter.Name);
            query = query.Where(q => q.StatusId, filter.StatusId);
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<ProvinceDAO> OrFilter(IQueryable<ProvinceDAO> query, ProvinceFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<ProvinceDAO> initQuery = query.Where(q => false);
            foreach (ProvinceFilter ProvinceFilter in filter.OrFilter)
            {
                IQueryable<ProvinceDAO> queryable = query;
                queryable = queryable.Where(q => q.Id, ProvinceFilter.Id);
                queryable = queryable.Where(q => q.Name, ProvinceFilter.Name);
                queryable = queryable.Where(q => q.StatusId, ProvinceFilter.StatusId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }

        private IQueryable<ProvinceDAO> DynamicOrder(IQueryable<ProvinceDAO> query, ProvinceFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case ProvinceOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case ProvinceOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case ProvinceOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case ProvinceOrder.Status:
                            query = query.OrderBy(q => q.StatusId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case ProvinceOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case ProvinceOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case ProvinceOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case ProvinceOrder.Status:
                            query = query.OrderByDescending(q => q.StatusId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<Province>> DynamicSelect(IQueryable<ProvinceDAO> query, ProvinceFilter filter)
        {
            List<Province> Provinces = await query.Select(q => new Province()
            {
                Id = filter.Selects.Contains(ProvinceSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(ProvinceSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(ProvinceSelect.Name) ? q.Name : default(string),
                StatusId = filter.Selects.Contains(ProvinceSelect.Status) ? q.StatusId : default(long),
                Status = filter.Selects.Contains(ProvinceSelect.Status) && q.Status != null ? new Status
                {
                    Id = q.Status.Id,
                    Code = q.Status.Code,
                    Name = q.Status.Name,
                } : null,
                Districts = filter.Selects.Contains(ProvinceSelect.Districts) && q.Districts == null ? null :
                q.Districts.Select(p => new District
                {
                    Name = p.Name,
                    Code = p.Code,
                    Villages = p.Villages.Select(w => new Village
                    {
                        Name = w.Name,
                        Code = w.Code,
                    }).ToList(),
                }).ToList(),
                RowId = q.RowId,
                Used = q.Used,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
                DeletedAt = q.DeletedAt,
            }).ToListAsync();
            return Provinces;
        }

        public async Task<bool> BulkDelete(List<Province> Provinces)
        {
            List<long> Ids = Provinces.Select(x => x.Id).ToList();
            await DataContext.Province
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new ProvinceDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        public async Task<int> Count(ProvinceFilter ProvinceFilter)
        {
            IQueryable<ProvinceDAO> Provinces = DataContext.Province;
            Provinces = DynamicFilter(Provinces, ProvinceFilter);
            return await Provinces.CountAsync();
        }

        public async Task<bool> Create(Province Province)
        {

            Province.RowId = Guid.NewGuid();
            ProvinceDAO ProvinceDAO = new ProvinceDAO();
            ProvinceDAO.Id = Province.Id;
            ProvinceDAO.Code = Province.Code;
            ProvinceDAO.Name = Province.Name;
            ProvinceDAO.StatusId = Province.StatusId;
            ProvinceDAO.CreatedAt = StaticParams.DateTimeNow;
            ProvinceDAO.UpdatedAt = StaticParams.DateTimeNow;
            ProvinceDAO.Used = false;
            ProvinceDAO.RowId = Province.RowId;
            DataContext.Province.Add(ProvinceDAO);
            await DataContext.SaveChangesAsync();
            Province.Id = ProvinceDAO.Id;
            await SaveReference(Province);
            return true;
        }
        private async Task SaveReference(Province Province)
        {
        }
        public async Task<bool> Delete(Province Province)
        {
            DateTime Now = StaticParams.DateTimeNow;
            Province.DeletedAt = Now;
            await DataContext.Province.Where(x => x.Id == Province.Id).UpdateFromQueryAsync(x => new ProvinceDAO { DeletedAt = Now });
            // Province.RowId = DataContext.Province.Where(x => x.Id == Province.Id).Select(p => p.RowId).FirstOrDefault();
            Province.RowId = DataContext.Province.Where(x => x.Id == Province.Id).Select(p => p.RowId).FirstOrDefault();
            return true;
        }

        public async Task<Province> Get(long Id)
        {
            Province Province = await DataContext.Province
                .Where(x => x.Id == Id).AsNoTracking()
                .Select(x => new Province()
                {
                    Id = x.Id,
                    Code = x.Code,
                    Name = x.Name,
                    StatusId = x.StatusId,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    DeletedAt = x.DeletedAt,
                    RowId = x.RowId,
                    Used = x.Used,
                    Status = x.Status == null ? null : new Status
                    {
                        Id = x.Status.Id,
                        Code = x.Status.Code,
                        Name = x.Status.Name,
                    },
                }).FirstOrDefaultAsync();

            if (Province == null)
                return null;

            return Province;
        }

        public async Task<List<Province>> List(ProvinceFilter ProvinceFilter)
        {
            if (ProvinceFilter == null) return new List<Province>();
            IQueryable<ProvinceDAO> ProvinceDAOs = DataContext.Province;
            ProvinceDAOs = DynamicFilter(ProvinceDAOs, ProvinceFilter);
            ProvinceDAOs = DynamicOrder(ProvinceDAOs, ProvinceFilter);
            List<Province> Provinces = await DynamicSelect(ProvinceDAOs, ProvinceFilter);
            return Provinces;
        }

        public async Task<List<Province>> List(List<long> Ids)
        {
            IdFilter IdFilter = new IdFilter { In = Ids };
            List<Province> Provinces = await DataContext.Province.AsNoTracking()
            .Where(x => x.Id, IdFilter)
            .Select(x => new Province()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                DeletedAt = x.DeletedAt,
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                StatusId = x.StatusId,
                RowId = x.RowId,
                Used = x.Used,
                Status = x.Status == null ? null : new Status
                {
                    Id = x.Status.Id,
                    Code = x.Status.Code,
                    Name = x.Status.Name,
                },
            }).ToListAsync();
            return Provinces;
        }

        public async Task<bool> Update(Province Province)
        {
            ProvinceDAO ProvinceDAO = DataContext.Province.Where(x => x.Id == Province.Id).FirstOrDefault();
            if (ProvinceDAO == null)
                return false;
            ProvinceDAO.Id = Province.Id;
            ProvinceDAO.Code = Province.Code;
            ProvinceDAO.Name = Province.Name;
            ProvinceDAO.StatusId = Province.StatusId;
            ProvinceDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            Province.RowId = ProvinceDAO.RowId;
            await SaveReference(Province);
            return true;
        }

        public async Task<bool> Used(List<long> Ids)
        {
            await DataContext.Province.Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new ProvinceDAO { Used = true });
            return true;
        }
    }
}
