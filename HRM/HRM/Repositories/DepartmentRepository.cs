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
    public interface IDepartmentRepository
    {
        Task<int> Count(DepartmentFilter filter);
        Task<List<Department>> List(DepartmentFilter filter);
        Task<List<Department>> List(List<long> Ids);
        Task<Department> Get(long Id);
        Task<bool> Create(Department Department);
        Task<bool> Update(Department Department);
        Task<bool> Delete(Department Department);
        Task<bool> BulkDelete(List<Department> Departments);
        // Task<bool> SimpleBulkMerge(List<Department> Departments);
        // Task<List<long>> BulkMerge(List<Department> Departments);
        // Task<bool> BulkUpdate(List<Department> Departments);
        Task<bool> Used(List<long> Ids);
    }

    public class DepartmentRepository : IDepartmentRepository
    {
        private DataContext DataContext;
        public DepartmentRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }
        public async Task<bool> BulkDelete(List<Department> Departments)
        {
            List<long> Ids = Departments.Select(x => x.Id).ToList();
            await DataContext.Department.Where(x => Ids.Contains(x.Id))
            .UpdateFromQueryAsync(x => new DepartmentDAO
            {
                UpdatedAt = StaticParams.DateTimeNow,
                DeletedAt = StaticParams.DateTimeNow
            });

            return true;
        }

        public async Task<int> Count(DepartmentFilter filter)
        {
            IQueryable<DepartmentDAO> Departments = DataContext.Department;
            Departments = DynamicFilter(Departments, filter);
            Departments = OrFilter(Departments, filter);
            return await Departments.CountAsync();
        }

        public async Task<bool> Create(Department Department)
        {
            DepartmentDAO DepartmentDAO = new DepartmentDAO();

            DepartmentDAO.Id = Department.Id;
            DepartmentDAO.Name = Department.Name;
            DepartmentDAO.Code = Department.Code;
            DepartmentDAO.PhoneNumber = Department.PhoneNumber;
            DepartmentDAO.StatusId = Department.StatusId;
            DepartmentDAO.Used = Department.Used;
            DepartmentDAO.VillageId = Department.VillageId;
            DepartmentDAO.DistrictId = Department.DistrictId;
            DepartmentDAO.ProvinceId = Department.ProvinceId;
            DepartmentDAO.CreatedAt = StaticParams.DateTimeNow;
            DepartmentDAO.UpdatedAt = StaticParams.DateTimeNow;

            DataContext.Department.Add(DepartmentDAO);

            await DataContext.SaveChangesAsync();
            await SaveReference(Department);

            Department.Id = DepartmentDAO.Id;

            return true;

        }

        public async Task<bool> Delete(Department Department)
        {
            await DataContext.Department.Where(x => x.Id == Department.Id)
            .UpdateFromQueryAsync(x => new DepartmentDAO
            {
                UpdatedAt = StaticParams.DateTimeNow,
                DeletedAt = StaticParams.DateTimeNow
            });
            return true;
        }

        public async Task<Department> Get(long Id)
        {
            Department Department = await DataContext.Department.Where(x => x.Id == Id).Select(x => new Department
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                PhoneNumber = x.PhoneNumber,
                StatusId = x.StatusId,
                Used = x.Used,
                VillageId = x.VillageId,
                DistrictId = x.DistrictId,
                ProvinceId = x.ProvinceId,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                DeletedAt = x.DeletedAt,
                District = x.District == null ? null : new District
                {
                    Id = x.District.Id,
                    Name = x.District.Name,
                    Code = x.District.Code,
                    ProvinceId = x.District.ProvinceId,
                    StatusId = x.District.StatusId,
                    Used = x.District.Used,
                },
                Province = x.Province == null ? null : new Province
                {
                    Id = x.Province.Id,
                    Name = x.Province.Name,
                    Code = x.Province.Code,
                    StatusId = x.Province.StatusId,
                    Used = x.Province.Used,
                },
                Village = x.Village == null ? null : new Village
                {
                    Id = x.Village.Id,
                    Name = x.Village.Name,
                    Code = x.Village.Code,
                    DistrictId = x.Village.DistrictId,
                    StatusId = x.Village.StatusId,
                    Used = x.Village.Used,
                },
                Status = x.Status == null ? null : new Status
                {
                    Id = x.Status.Id,
                    Code = x.Status.Code,
                    Name = x.Status.Name,
                }
            }).FirstOrDefaultAsync();

            return Department;
        }

        public async Task<List<Department>> List(DepartmentFilter filter)
        {
            IQueryable<DepartmentDAO> DepartmentDAOs = DataContext.Department;
            DepartmentDAOs = DynamicFilter(DepartmentDAOs, filter);
            DepartmentDAOs = DynamicOrder(DepartmentDAOs, filter);
            List<Department> Departments = await DynamicSelect(DepartmentDAOs, filter);

            return Departments;
        }

        public async Task<List<Department>> List(List<long> Ids)
        {
            List<Department> Departments = await DataContext.Department.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new Department
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                PhoneNumber = x.PhoneNumber,
                StatusId = x.StatusId,
                Used = x.Used,
                VillageId = x.VillageId,
                DistrictId = x.DistrictId,
                ProvinceId = x.ProvinceId,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                DeletedAt = x.DeletedAt,
                District = x.District == null ? null : new District
                {
                    Id = x.District.Id,
                    Name = x.District.Name,
                    Code = x.District.Code,
                    ProvinceId = x.District.ProvinceId,
                    StatusId = x.District.StatusId,
                    Used = x.District.Used,
                },
                Province = x.Province == null ? null : new Province
                {
                    Id = x.Province.Id,
                    Name = x.Province.Name,
                    Code = x.Province.Code,
                    StatusId = x.Province.StatusId,
                    Used = x.Province.Used,
                },
                Village = x.Village == null ? null : new Village
                {
                    Id = x.Village.Id,
                    Name = x.Village.Name,
                    Code = x.Village.Code,
                    DistrictId = x.Village.DistrictId,
                    StatusId = x.Village.StatusId,
                    Used = x.Village.Used,
                },
                Status = x.Status == null ? null : new Status
                {
                    Id = x.Status.Id,
                    Code = x.Status.Code,
                    Name = x.Status.Name,
                }
            }).ToListAsync();

            return Departments;
        }

        public async Task<bool> Update(Department Department)
        {
            DepartmentDAO DepartmentDAO = DataContext.Department.Where(x => x.Id == Department.Id).FirstOrDefault();

            DepartmentDAO.Id = Department.Id;
            DepartmentDAO.Name = Department.Name;
            DepartmentDAO.Code = Department.Code;
            DepartmentDAO.PhoneNumber = Department.PhoneNumber;
            DepartmentDAO.StatusId = Department.StatusId;
            DepartmentDAO.VillageId = Department.VillageId;
            DepartmentDAO.DistrictId = Department.DistrictId;
            DepartmentDAO.ProvinceId = Department.ProvinceId;
            DepartmentDAO.UpdatedAt = StaticParams.DateTimeNow;

            await DataContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Used(List<long> Ids)
        {
            await DataContext.Department.Where(x => Ids.Contains(x.Id)).UpdateFromQueryAsync(x => new DepartmentDAO { Used = true });

            return true;
        }

        private async Task SaveReference(Department Department)
        {

        }

        private async Task<List<Department>> DynamicSelect(IQueryable<DepartmentDAO> query, DepartmentFilter filter)
        {
            List<Department> Departments = await query.Select(q => new Department()
            {
                Id = filter.Selects.Contains(DepartmentSelect.Id) ? q.Id : default(long),
                Name = filter.Selects.Contains(DepartmentSelect.Name) ? q.Name : default(string),
                Code = filter.Selects.Contains(DepartmentSelect.Code) ? q.Code : default(string),
                PhoneNumber = filter.Selects.Contains(DepartmentSelect.PhoneNumber) ? q.PhoneNumber : default(string),
                StatusId = filter.Selects.Contains(DepartmentSelect.Status) ? q.StatusId : default(long),
                Used = filter.Selects.Contains(DepartmentSelect.Used) ? q.Used : default(bool),
                VillageId = filter.Selects.Contains(DepartmentSelect.Village) ? q.VillageId : default(long),
                DistrictId = filter.Selects.Contains(DepartmentSelect.District) ? q.DistrictId : default(long),
                ProvinceId = filter.Selects.Contains(DepartmentSelect.Province) ? q.ProvinceId : default(long),
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
                DeletedAt = q.DeletedAt,
                District = filter.Selects.Contains(DepartmentSelect.District) && q.District != null ? new District
                {
                    Id = q.District.Id,
                    Name = q.District.Name,
                    Code = q.District.Code,
                    ProvinceId = q.District.ProvinceId,
                    StatusId = q.District.StatusId,
                    Used = q.District.Used,
                    CreatedAt = q.District.CreatedAt,
                    UpdatedAt = q.District.UpdatedAt,
                    DeletedAt = q.District.DeletedAt,
                } : null,
                Province = filter.Selects.Contains(DepartmentSelect.Province) && q.Province != null ? new Province
                {
                    Id = q.Province.Id,
                    Name = q.Province.Name,
                    Code = q.Province.Code,
                    StatusId = q.Province.StatusId,
                    Used = q.Province.Used,
                    CreatedAt = q.Province.CreatedAt,
                    UpdatedAt = q.Province.UpdatedAt,
                    DeletedAt = q.Province.DeletedAt,
                } : null,
                Village = filter.Selects.Contains(DepartmentSelect.Village) && q.Village != null ? new Village
                {
                    Id = q.Village.Id,
                    Name = q.Village.Name,
                    Code = q.Village.Code,
                    DistrictId = q.Village.DistrictId,
                    StatusId = q.Village.StatusId,
                    Used = q.Village.Used,
                    CreatedAt = q.Village.CreatedAt,
                    UpdatedAt = q.Village.UpdatedAt,
                    DeletedAt = q.Village.DeletedAt,
                } : null,
                Status = filter.Selects.Contains(DepartmentSelect.Status) && q.Status != null ? new Status
                {
                    Id = q.Status.Id,
                    Code = q.Status.Code,
                    Name = q.Status.Name,
                } : null,

            }).ToListAsync();

            return Departments;
        }

        private IQueryable<DepartmentDAO> DynamicOrder(IQueryable<DepartmentDAO> query, DepartmentFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case DepartmentOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case DepartmentOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        case DepartmentOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case DepartmentOrder.Status:
                            query = query.OrderBy(q => q.StatusId);
                            break;
                        case DepartmentOrder.Used:
                            query = query.OrderBy(q => q.Used);
                            break;
                        case DepartmentOrder.PhoneNumber:
                            query = query.OrderBy(q => q.PhoneNumber);
                            break;

                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case DepartmentOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case DepartmentOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
                            break;
                        case DepartmentOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case DepartmentOrder.Status:
                            query = query.OrderByDescending(q => q.StatusId);
                            break;
                        case DepartmentOrder.Used:
                            query = query.OrderByDescending(q => q.Used);
                            break;
                        case DepartmentOrder.PhoneNumber:
                            query = query.OrderByDescending(q => q.PhoneNumber);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private IQueryable<DepartmentDAO> OrFilter(IQueryable<DepartmentDAO> query, DepartmentFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<DepartmentDAO> initQuery = query.Where(q => false);

            foreach (DepartmentFilter DepartmentFilter in filter.OrFilter)
            {
                IQueryable<DepartmentDAO> queryable = query;

                queryable = queryable.Where(q => q.Id, filter.Id);
                queryable = queryable.Where(q => q.Name, filter.Name);
                queryable = queryable.Where(q => q.Code, filter.Code);
                queryable = queryable.Where(q => q.PhoneNumber, filter.PhoneNumber);
                queryable = queryable.Where(q => q.CreatedAt, filter.CreatedAt);
                queryable = queryable.Where(q => q.StatusId, filter.StatusId);

                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }

        private IQueryable<DepartmentDAO> DynamicFilter(IQueryable<DepartmentDAO> query, DepartmentFilter filter)
        {
            if (filter == null)
                return query.Where(q => false);
            query = query.Where(q => q.Id, filter.Id);
            query = query.Where(q => q.Name, filter.Name);
            query = query.Where(q => q.Code, filter.Code);
            query = query.Where(q => q.PhoneNumber, filter.PhoneNumber);
            query = query.Where(q => q.CreatedAt, filter.CreatedAt);
            query = query.Where(q => q.StatusId, filter.StatusId);

            return query;
        }
    }
}