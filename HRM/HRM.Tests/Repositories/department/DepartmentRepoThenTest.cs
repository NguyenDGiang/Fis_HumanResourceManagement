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
using Newtonsoft.Json;

namespace HRM.Tests.Repositories.department
{
    public partial class DepartmentRepoTest : CommonTests
    {
        private async Task Then_CountDepartment_Success()
        {
            IQueryable<DepartmentDAO> query = DataContext.Department.AsNoTracking();
            query = DynamicFilter(query, filter);
            int output = query.Count();
            Assert.AreEqual(count, output);
        }

        private async Task Then_CreateDepartment_Success()
        {
            var Output = await DataContext.Department.Where(x => x.Id == Department.Id).FirstOrDefaultAsync();

            Assert.AreEqual(Department.Code, Output.Code);
            Assert.AreEqual(Department.Name, Output.Name);
            Assert.AreEqual(Department.PhoneNumber, Output.PhoneNumber);
            Assert.AreEqual(Department.VillageId, Output.VillageId);
            Assert.AreEqual(Department.DistrictId, Output.DistrictId);
            Assert.AreEqual(Department.ProvinceId, Output.ProvinceId);
            Assert.AreEqual(Department.StatusId, Output.StatusId);
            Assert.AreEqual(Department.StatusId, Output.StatusId);
            Assert.AreEqual(Department.CreatedAt.ToString("dd-MM-yyyy HH:mm:ss"), Output.CreatedAt.ToString("dd-MM-yyyy HH:mm:ss"));
            Assert.AreEqual(Department.UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss"), Output.UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss"));
        }

        public async Task Then_UpdateDepartment_Success()
        {
            var Output = await DataContext.Department.Where(x => x.Id == Department.Id).FirstOrDefaultAsync();

            Assert.AreEqual(Department.Code, Output.Code);
            Assert.AreEqual(Department.Name, Output.Name);
            Assert.AreEqual(Department.PhoneNumber, Output.PhoneNumber);
            Assert.AreEqual(Department.VillageId, Output.VillageId);
            Assert.AreEqual(Department.DistrictId, Output.DistrictId);
            Assert.AreEqual(Department.ProvinceId, Output.ProvinceId);
            Assert.AreEqual(Department.StatusId, Output.StatusId);
            Assert.AreEqual(Department.StatusId, Output.StatusId);
            Assert.AreEqual(Department.CreatedAt.ToString("dd-MM-yyyy HH:mm:ss"), Output.CreatedAt.ToString("dd-MM-yyyy HH:mm:ss"));
            Assert.AreEqual(Department.UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss"), Output.UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss"));
        }

        public async Task Then_DeleteDepartment_Success()
        {
            var Output = await DataContext.Department.Where(x => x.Id == Department.Id).FirstOrDefaultAsync();
            Assert.IsNotNull(Output);
            Assert.IsNotNull(Output.DeletedAt);
        }

        public async Task Then_ListOrderByDepartment_Success()
        {
            IQueryable<DepartmentDAO> query = DataContext.Department.AsNoTracking();
            query = DynamicOrder(query, filter);

            List<DepartmentDAO> OutputDepartmentDAOs = await query.ToListAsync();

            Assert.AreEqual(OutputDepartmentDAOs.Count(), Departments.Count());
            if (Departments.Count() == OutputDepartmentDAOs.Count())
            {
                int length = Departments.Count();
                for (int i = 0; i < length; i++)
                {
                    Assert.AreEqual(OutputDepartmentDAOs[i].Id, Departments[i].Id);
                }
            }
        }

        public async Task Then_BulkDeleteDepartment_Success()
        {
            List<long> Ids = Departments.Select(x => x.Id).ToList();
            List<DepartmentDAO> Output = await DataContext.Department.Where(x => Ids.Contains(x.Id)).ToListAsync();

            foreach (var item in Output)
            {
                Assert.NotNull(item.DeletedAt);
            }
        }

        public async Task Then_GetDepartment_Success()
        {
            long id = Ids.FirstOrDefault();

            DepartmentDAO Output = await DataContext.Department.AsNoTracking()
            .Where(c => c.Id == id).FirstOrDefaultAsync();

            // Console.WriteLine("Output: " + JsonConvert.SerializeObject(Output));
            // Console.WriteLine("Department: " + JsonConvert.SerializeObject(Department));
            if (Output != null && Department != null)
            {
                Assert.AreEqual(Department.Code, Output.Code);
                Assert.AreEqual(Department.Name, Output.Name);
                Assert.AreEqual(Department.PhoneNumber, Output.PhoneNumber);
                Assert.AreEqual(Department.VillageId, Output.VillageId);
                Assert.AreEqual(Department.DistrictId, Output.DistrictId);
                Assert.AreEqual(Department.ProvinceId, Output.ProvinceId);
                Assert.AreEqual(Department.StatusId, Output.StatusId);
                Assert.AreEqual(Department.StatusId, Output.StatusId);
                Assert.AreEqual(Department.CreatedAt.ToString("dd-MM-yyyy HH:mm:ss"), Output.CreatedAt.ToString("dd-MM-yyyy HH:mm:ss"));
                Assert.AreEqual(Department.UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss"), Output.UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss"));
            }
        }

        public async Task Then_GetListDepartment_Success()
        {
            List<DepartmentDAO> Output = await DataContext.Department.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).ToListAsync();

            Assert.AreEqual(Output.Count(), Departments.Count());

            int length = Output.Count();
            for (int i = 0; i < length; i++)
            {
                if (Output[i] != null && Departments[i] != null)
                {
                    Assert.AreEqual(Departments[i].Code, Output[i].Code);
                    Assert.AreEqual(Departments[i].Name, Output[i].Name);
                    Assert.AreEqual(Departments[i].PhoneNumber, Output[i].PhoneNumber);
                    Assert.AreEqual(Departments[i].VillageId, Output[i].VillageId);
                    Assert.AreEqual(Departments[i].DistrictId, Output[i].DistrictId);
                    Assert.AreEqual(Departments[i].ProvinceId, Output[i].ProvinceId);
                    Assert.AreEqual(Departments[i].StatusId, Output[i].StatusId);
                    Assert.AreEqual(Departments[i].StatusId, Output[i].StatusId);
                    Assert.AreEqual(Departments[i].CreatedAt.ToString("dd-MM-yyyy HH:mm:ss"), Output[i].CreatedAt.ToString("dd-MM-yyyy HH:mm:ss"));
                    Assert.AreEqual(Departments[i].UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss"), Output[i].UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss"));
                }
            }
        }

        public async Task Then_ListFilterDepartment_Success()
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
