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
    public interface IVillageRepository
    {
        Task<int> Count(VillageFilter VillageFilter);
        Task<List<Village>> List(VillageFilter VillageFilter);
        Task<List<Village>> List(List<long> Ids);
        Task<Village> Get(long Id);
        Task<bool> Create(Village Village);
        Task<bool> Update(Village Village);
        Task<bool> Delete(Village Village);
        //Task<bool> BulkMerge(List<Village> Villages);
        Task<bool> BulkDelete(List<Village> Villages);
        Task<bool> Used(List<long> Ids);
    }
    public class VillageRepository : IVillageRepository
    {
        private DataContext DataContext;
        public VillageRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        private IQueryable<VillageDAO> DynamicFilter(IQueryable<VillageDAO> query, VillageFilter filter)
        {
            if (filter == null)
                return query.Where(q => false);
            query = query.Where(q => !q.DeletedAt.HasValue);
            query = query.Where(q => q.Id, filter.Id);
            query = query.Where(q => q.Code, filter.Code);
            query = query.Where(q => q.Name, filter.Name);
            query = query.Where(q => q.DistrictId, filter.DistrictId);
            query = query.Where(q => q.StatusId, filter.StatusId);
            query = OrFilter(query, filter);
            return query;
        }

        private IQueryable<VillageDAO> OrFilter(IQueryable<VillageDAO> query, VillageFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<VillageDAO> initQuery = query.Where(q => false);
            foreach (VillageFilter VillageFilter in filter.OrFilter)
            {
                IQueryable<VillageDAO> queryable = query;
                queryable = queryable.Where(q => q.Id, VillageFilter.Id);
                queryable = queryable.Where(q => q.Code, VillageFilter.Code);
                queryable = queryable.Where(q => q.Name, VillageFilter.Name);
                queryable = queryable.Where(q => q.DistrictId, VillageFilter.DistrictId);
                queryable = queryable.Where(q => q.StatusId, VillageFilter.StatusId);
                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }

        private IQueryable<VillageDAO> DynamicOrder(IQueryable<VillageDAO> query, VillageFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case VillageOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case VillageOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case VillageOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case VillageOrder.District:
                            query = query.OrderBy(q => q.District.Name);
                            break;
                        case VillageOrder.Status:
                            query = query.OrderBy(q => q.StatusId);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case VillageOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case VillageOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case VillageOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case VillageOrder.District:
                            query = query.OrderByDescending(q => q.District.Name);
                            break;
                        case VillageOrder.Status:
                            query = query.OrderByDescending(q => q.StatusId);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private async Task<List<Village>> DynamicSelect(IQueryable<VillageDAO> query, VillageFilter filter)
        {
            List<Village> Wards = await query.Select(q => new Village()
            {
                Id = filter.Selects.Contains(VillageSelect.Id) ? q.Id : default(long),
                Code = filter.Selects.Contains(VillageSelect.Code) ? q.Code : default(string),
                Name = filter.Selects.Contains(VillageSelect.Name) ? q.Name : default(string),
                DistrictId = filter.Selects.Contains(VillageSelect.District) ? q.DistrictId : default(long),
                StatusId = filter.Selects.Contains(VillageSelect.Status) ? q.StatusId : default(long),
                District = filter.Selects.Contains(VillageSelect.District) && q.District != null ? new District
                {
                    Id = q.District.Id,
                    Name = q.District.Name,
                    ProvinceId = q.District.ProvinceId,
                    StatusId = q.District.StatusId,
                } : null,
                Status = filter.Selects.Contains(VillageSelect.Status) && q.Status != null ? new Status
                {
                    Id = q.Status.Id,
                    Code = q.Status.Code,
                    Name = q.Status.Name,
                } : null,
                // RowId = q.RowId,
                RowId = q.RowId,
                Used = q.Used,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
                DeletedAt = q.DeletedAt,
            }).ToListAsync();
            return Wards;
        }

        public async Task<bool> BulkDelete(List<Village> Villages)
        {
            List<long> Ids = Villages.Select(x => x.Id).ToList();
            await DataContext.Village
                .Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new VillageDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        public async Task<int> Count(VillageFilter VillageFilter)
        {
            IQueryable<VillageDAO> Villages = DataContext.Village;
            Villages = DynamicFilter(Villages, VillageFilter);
            return await Villages.CountAsync();
        }

        public async Task<bool> Create(Village Village)
        {
            // Village.RowId = Guid.NewGuid();
            Village.RowId = Guid.NewGuid();
            VillageDAO VillageDAO = new VillageDAO();
            VillageDAO.Id = Village.Id;
            VillageDAO.Code = Village.Code;
            VillageDAO.Name = Village.Name;
            VillageDAO.DistrictId = Village.DistrictId;
            VillageDAO.StatusId = Village.StatusId;
            VillageDAO.CreatedAt = StaticParams.DateTimeNow;
            VillageDAO.UpdatedAt = StaticParams.DateTimeNow;
            VillageDAO.Used = false;
            VillageDAO.RowId = Village.RowId;
            DataContext.Village.Add(VillageDAO);
            await DataContext.SaveChangesAsync();
            Village.Id = VillageDAO.Id;
            await SaveReference(Village);
            return true;
        }

        public async Task<bool> Delete(Village Village)
        {
            DateTime Now = StaticParams.DateTimeNow;
            Village.DeletedAt = Now;
            await DataContext.Village.Where(x => x.Id == Village.Id).UpdateFromQueryAsync(x => new VillageDAO { DeletedAt = Now });
            // Village.RowId = DataContext.Village.Where(x => x.Id == Village.Id).Select(w => w.RowId).FirstOrDefault();
            Village.RowId = DataContext.Village.Where(x => x.Id == Village.Id).Select(w => w.RowId).FirstOrDefault();
            return true;
        }

        public async Task<Village> Get(long Id)
        {
            Village Village = await DataContext.Village
               .Where(x => x.Id == Id).AsNoTracking()
               .Select(x => new Village()
               {
                   Id = x.Id,
                   Code = x.Code,
                   Name = x.Name,
                   DistrictId = x.DistrictId,
                   StatusId = x.StatusId,
                   CreatedAt = x.CreatedAt,
                   UpdatedAt = x.UpdatedAt,
                   DeletedAt = x.DeletedAt,
                   RowId = x.RowId,
                   Used = x.Used,
                   District = x.District == null ? null : new District
                   {
                       Id = x.District.Id,
                       Name = x.District.Name,
                       ProvinceId = x.District.ProvinceId,
                       StatusId = x.District.StatusId,
                   },
                   Status = x.Status == null ? null : new Status
                   {
                       Id = x.Status.Id,
                       Code = x.Status.Code,
                       Name = x.Status.Name,
                   },
               }).FirstOrDefaultAsync();

            if (Village == null)
                return null;

            return Village;
        }

        public async Task<List<Village>> List(VillageFilter VillageFilter)
        {
            if (VillageFilter == null) return new List<Village>();
            IQueryable<VillageDAO> VillageDAOs = DataContext.Village;
            VillageDAOs = DynamicFilter(VillageDAOs, VillageFilter);
            VillageDAOs = DynamicOrder(VillageDAOs, VillageFilter);
            List<Village> Villages = await DynamicSelect(VillageDAOs, VillageFilter);
            return Villages;
        }

        public async Task<List<Village>> List(List<long> Ids)
        {
            IdFilter IdFilter = new IdFilter { In = Ids };
            List<Village> Village = await DataContext.Village.AsNoTracking()
            .Where(x => x.Id, IdFilter)
            .Select(x => new Village()
            {
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                DeletedAt = x.DeletedAt,
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                DistrictId = x.DistrictId,
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

            return Village;
        }
        private async Task SaveReference(Village Village)
        {
        }
        public async Task<bool> Update(Village Village)
        {
            VillageDAO VillageDAO = DataContext.Village.Where(x => x.Id == Village.Id).FirstOrDefault();
            if (VillageDAO == null)
                return false;
            VillageDAO.Id = Village.Id;
            VillageDAO.Code = Village.Code;
            VillageDAO.Name = Village.Name;
            VillageDAO.DistrictId = Village.DistrictId;
            VillageDAO.StatusId = Village.StatusId;
            VillageDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
            Village.RowId = VillageDAO.RowId;
            await SaveReference(Village);
            return true;
        }

        public async Task<bool> Used(List<long> Ids)
        {
            await DataContext.Village.Where(x => Ids.Contains(x.Id))
                .UpdateFromQueryAsync(x => new VillageDAO { Used = true });
            return true;
        }
    }
}
