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
    public interface IEmployeeRepository
    {
        Task<int> Count(EmployeeFilter employeeFilter);
        Task<List<Employee>> List(EmployeeFilter employeeFilter);
        Task<List<Employee>> List(List<long> Ids);
        Task<Employee> Get(long Id);
        Task<bool> Create(Employee employee);
        Task<bool> Update(Employee employee);
        Task<bool> BulkInsertNewEmployee(List<Employee> employees);
        Task<bool> BulkDeleteNewEmployee(List<Employee> employees);
        Task<bool> Delete(Employee employee);
        Task<bool> SimpleBulkMerge(List<Employee> employees);
        Task<bool> BulkMerge(List<Employee> employees);
        Task<bool> BulkDelete(List<Employee> employees);
        Task<bool> BulkUpdate(List<Employee> employees);
        Task<bool> Used(List<long> Ids);
    }

    public class EmployeeRepository : IEmployeeRepository
    {
        private DataContext DataContext;
        public EmployeeRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;

        }
        private IQueryable<EmployeeDAO> DynamicFilter(IQueryable<EmployeeDAO> query,EmployeeFilter filter)
        {
            if (filter == null)
                return query.Where(q => false);
            query = query.Where(q => q.Id , filter.Id);
            query = query.Where(q => q.FullName , filter.FullName);
            query = query.Where(q => q.Email , filter.Email);
            query = query.Where(q => q.PhoneNumber, filter.PhoneNumber);
            query = query.Where(q => q.Birthday, filter.Birthday);
            query = query.Where(q => q.BeginJobTime, filter.BeginJobTime);
            query = query.Where(q => q.ContractStatus, filter.ContractStatus);
            query = query.Where(q => q.StatusId, filter.StatusId);
            query = query.Where(q => q.ChucVuId, filter.ChucVuId);
            query = query.Where(q => q.AcademicLevelId, filter.AcademicLevelId);
            query = query.Where(q => q.VillageId, filter.VillageId);
            query = query.Where(q => q.JobPositionId, filter.JobPositionId);
            query = query.Where(q => q.DistrictId, filter.DistrictId);
            query = query.Where(q => q.ProvinceId, filter.ProvinceId);
            query = OrFilter(query, filter);
            return query;
        }
        private IQueryable<EmployeeDAO> OrFilter(IQueryable<EmployeeDAO> query, EmployeeFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<EmployeeDAO> initQuery = query.Where(q => false);
            foreach (EmployeeFilter EmployeeFilter in filter.OrFilter)
            {
                IQueryable<EmployeeDAO> queryable = query;
                queryable = queryable.Where(q => q.Id, filter.Id);
                queryable = queryable.Where(q => q.FullName, filter.FullName);
                queryable = queryable.Where(q => q.Email, filter.Email);
                queryable = queryable.Where(q => q.PhoneNumber, filter.PhoneNumber);
                queryable = queryable.Where(q => q.Birthday, filter.Birthday);
                queryable = queryable.Where(q => q.BeginJobTime, filter.BeginJobTime);
                queryable = queryable.Where(q => q.ContractStatus, filter.ContractStatus);
                queryable = queryable.Where(q => q.StatusId, filter.StatusId);
                queryable = queryable.Where(q => q.ChucVuId, filter.ChucVuId);
                queryable = queryable.Where(q => q.AcademicLevelId, filter.AcademicLevelId);
                queryable = queryable.Where(q => q.VillageId, filter.VillageId);
                queryable = queryable.Where(q => q.JobPositionId, filter.JobPositionId);
                queryable = queryable.Where(q => q.DistrictId, filter.DistrictId);
                queryable = queryable.Where(q => q.ProvinceId, filter.ProvinceId);
                initQuery = initQuery.Union(queryable);
            }
            return query;
        }
        private IQueryable<EmployeeDAO> DynamicOrder(IQueryable<EmployeeDAO> query, EmployeeFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC: switch (filter.OrderBy)
                    {
                        case EmployeeOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case EmployeeOrder.FullName:
                            query = query.OrderBy(q => q.FullName);
                            break;
                        case EmployeeOrder.Email:
                            query = query.OrderBy(q => q.Email);
                            break;
                        case EmployeeOrder.PhoneNumber:
                            query = query.OrderBy(q => q.PhoneNumber);
                            break;
                        case EmployeeOrder.Birthday:
                            query = query.OrderBy(q => q.Birthday);
                            break;
                        case EmployeeOrder.BeginJobTime:
                            query = query.OrderBy(q => q.BeginJobTime);
                            break;
                        case EmployeeOrder.ContractStatus:
                            query = query.OrderBy(q => q.ContractStatus);
                            break;
                        case EmployeeOrder.Status:
                            query = query.OrderBy(q => q.StatusId);
                            break;
                        case EmployeeOrder.ChucVu:
                            query = query.OrderBy(q => q.ChucVu.Name);
                            break;
                        case EmployeeOrder.AcademicLevel:
                            query = query.OrderBy(q => q.AcademicLevel.Name);
                            break;
                        case EmployeeOrder.JobPosition:
                            query = query.OrderBy(q => q.JobPosition.Name);
                            break;
                        case EmployeeOrder.Village:
                            query = query.OrderBy(q => q.Village.Name);
                            break;
                        case EmployeeOrder.Province:
                            query = query.OrderBy(q => q.Province.Name);
                            break;

                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case EmployeeOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case EmployeeOrder.FullName:
                            query = query.OrderBy(q => q.FullName);
                            break;
                        case EmployeeOrder.Email:
                            query = query.OrderBy(q => q.Email);
                            break;
                        case EmployeeOrder.PhoneNumber:
                            query = query.OrderBy(q => q.PhoneNumber);
                            break;
                        case EmployeeOrder.Birthday:
                            query = query.OrderBy(q => q.Birthday);
                            break;
                        case EmployeeOrder.BeginJobTime:
                            query = query.OrderBy(q => q.BeginJobTime);
                            break;
                        case EmployeeOrder.ContractStatus:
                            query = query.OrderBy(q => q.ContractStatus);
                            break;
                        case EmployeeOrder.Status:
                            query = query.OrderBy(q => q.StatusId);
                            break;
                        case EmployeeOrder.ChucVu:
                            query = query.OrderBy(q => q.ChucVu.Name);
                            break;
                        case EmployeeOrder.AcademicLevel:
                            query = query.OrderBy(q => q.AcademicLevel.Name);
                            break;
                        case EmployeeOrder.JobPosition:
                            query = query.OrderBy(q => q.JobPosition.Name);
                            break;
                        case EmployeeOrder.Village:
                            query = query.OrderBy(q => q.Village.Name);
                            break;
                        case EmployeeOrder.Province:
                            query = query.OrderBy(q => q.Province.Name);
                            break;
                    }
                    break;
            }
            query =  query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }
        private async Task<List<Employee>> DynamicSelect(IQueryable<EmployeeDAO> query, EmployeeFilter filter)
        {
            List<Employee> Employees = await query.Select(q => new Employee()
            {
                Id = filter.Selects.Contains(EmployeeSelect.Id)?q.Id:default(long) ,
                FullName = filter.Selects.Contains(EmployeeSelect.FullName)? q.FullName:default(string),
                Email = filter.Selects.Contains(EmployeeSelect.Email)? q.Email:default(string),
                PhoneNumber = filter.Selects.Contains(EmployeeSelect.PhoneNumber)?q.PhoneNumber:default(string),
                Birthday = filter.Selects.Contains(EmployeeSelect.Birthday)?q.Birthday:default(DateTime),
                BeginJobTime = filter.Selects.Contains(EmployeeSelect.BeginJobTime)?q.BeginJobTime:default(DateTime),
                ContractStatus = filter.Selects.Contains(EmployeeSelect.ContractStatus)?q.ContractStatus:default(bool),
                StatusId = filter.Selects.Contains(EmployeeSelect.Status)?q.StatusId:default(long),
                ChucVuId = filter.Selects.Contains(EmployeeSelect.ChucVu)?q.ChucVuId:default(long),
                AcademicLevelId = filter.Selects.Contains(EmployeeSelect.AcademicLevel)?q.AcademicLevelId:default(long),
                JobPositionId = filter.Selects.Contains(EmployeeSelect.JobPosition)?q.JobPositionId:default(long),
                VillageId = filter.Selects.Contains(EmployeeSelect.Village)?q.VillageId:default(long),
                DistrictId = filter.Selects.Contains(EmployeeSelect.Village)?q.VillageId:default(long),
                ProvinceId = filter.Selects.Contains(EmployeeSelect.Province)?q.ProvinceId:default(long),
                AcademicLevel = filter.Selects.Contains(EmployeeSelect.AcademicLevel) && q.AcademicLevel!=null? new AcademicLevel
                {
                    Id = q.AcademicLevel.Id,
                    Name = q.AcademicLevel.Name,
                    Code = q.AcademicLevel.Code,
                    StatusId = q.AcademicLevel.StatusId,
                }:null,
                ChucVu = filter.Selects.Contains(EmployeeSelect.ChucVu) && q.ChucVu!= null ? new ChucVu
                {
                    Id=q.ChucVu.Id,
                    Name=q.ChucVu.Name,
                    Code=q.ChucVu.Code,
                    StatusId=q.ChucVu.StatusId,

                }:null,
                District = filter.Selects.Contains(EmployeeSelect.District) && q.District!= null ? new District
                {
                    Id = q.District.Id,
                    Name = q.District.Name,
                    Code = q.District.Code,
                    StatusId = q.District.StatusId
                }:null,
                Province = filter.Selects.Contains(EmployeeSelect.Province) && q.Province != null ? new Province
                {
                    Id = q.Province.Id,
                    Name = q.Province.Name,
                    Code = q.Province.Code,
                    StatusId = q.Province.StatusId
                } : null,
                JobPosition = filter.Selects.Contains(EmployeeSelect.JobPosition) && q.JobPosition != null ? new JobPosition
                {
                    Id = q.JobPosition.Id,
                    Name = q.JobPosition.Name,
                    Code = q.JobPosition.Code,
                    StatusId = q.JobPosition.StatusId
                }:null
                ,
                Status = filter.Selects.Contains(EmployeeSelect.Status) && q.Status != null ? new Status
                {
                    Id = q.Status.Id,
                    Name = q.Status.Name,
                    Code = q.Status.Code,
                } : null,
                Village = filter.Selects.Contains(EmployeeSelect.Village) && q.Village != null ? new Village
                {
                    Id = q.Village.Id,
                    Name = q.Village.Name,
                    Code = q.Village.Code,
                    StatusId = q.Village.StatusId
                } : null,




            }).ToListAsync();
            return Employees;
        }
        public Task<bool> BulkDelete(List<Employee> employees)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> BulkDeleteNewEmployee(List<Employee> employees)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> BulkInsertNewEmployee(List<Employee> employees)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> BulkMerge(List<Employee> employees)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> BulkUpdate(List<Employee> employees)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> Count(EmployeeFilter EmployeeFilter)
        {
            IQueryable<EmployeeDAO> Employees = DataContext.Employee;
            Employees = DynamicFilter(Employees, EmployeeFilter);
            return Employees.CountAsync();
        }

        public async Task<bool> Create(Employee Employee)
        {

            EmployeeDAO EmployeeDAO = new EmployeeDAO();
            EmployeeDAO.Id = Employee.Id;
            EmployeeDAO.FullName = Employee.FullName;
            EmployeeDAO.Email = Employee.Email;
            EmployeeDAO.PhoneNumber = Employee.PhoneNumber;
            EmployeeDAO.Birthday = Employee.Birthday;
            EmployeeDAO.BeginJobTime = Employee.BeginJobTime;
            EmployeeDAO.ContractStatus = Employee.ContractStatus;
            EmployeeDAO.StatusId = Employee.StatusId;
            EmployeeDAO.ChucVuId = Employee.ChucVuId;
            EmployeeDAO.AcademicLevelId = Employee.AcademicLevelId;
            EmployeeDAO.JobPositionId = Employee.JobPositionId;
            EmployeeDAO.VillageId = Employee.VillageId; 
            EmployeeDAO.DistrictId = Employee.DistrictId;
            EmployeeDAO.ProvinceId = Employee.ProvinceId;
            EmployeeDAO.Used = false;
            EmployeeDAO.CreatedAt = StaticParams.DateTimeNow;
            EmployeeDAO.UpdatedAt = StaticParams.DateTimeNow;
            DataContext.Employee.Add(EmployeeDAO);
            await DataContext.SaveChangesAsync();
            Employee.Id = EmployeeDAO.Id;
            return true;
        }

        public async Task<bool> Delete(Employee Employee)
        {
            await DataContext.Employee.Where(x => x.Id == Employee.Id).UpdateFromQueryAsync(x => new EmployeeDAO { DeletedAt = StaticParams.DateTimeNow });
            return true;
        }

        public async Task<Employee> Get(long Id)
        {
            Employee Employee = await DataContext.Employee.AsNoTracking().Where(x => x.Id ==Id).Select(x => new Employee()
            {
                Id = x.Id,
                FullName = x.FullName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                Birthday = x.Birthday,
                BeginJobTime = x.BeginJobTime,
                ContractStatus = x.ContractStatus,
                StatusId = x.StatusId,
                ChucVuId = x.ChucVuId,
                AcademicLevelId = x.AcademicLevelId,
                JobPositionId = x.JobPositionId,
                VillageId = x.VillageId,
                DistrictId = x.DistrictId,
                ProvinceId = x.ProvinceId,
                Used = x.Used,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                DeletedAt = x.DeletedAt,
                AcademicLevel = x.AcademicLevel == null ? null : new AcademicLevel()
                {
                    Id = x.AcademicLevel.Id,
                    Name = x.AcademicLevel.Name,
                    Code = x.AcademicLevel.Code,
                    StatusId = x.AcademicLevel.StatusId
                },
                ChucVu = x.ChucVu == null ? null : new ChucVu()
                {
                    Id = x.ChucVu.Id,
                    Name = x.ChucVu.Name,
                    Code = x.ChucVu.Code,
                    StatusId = x.ChucVu.StatusId
                },
                District = x.District == null ? null : new District()
                {
                    Id = x.District.Id,
                    Name = x.District.Name,
                    Code = x.District.Code,
                    StatusId = x.District.StatusId
                },
                JobPosition = x.JobPosition == null ? null : new JobPosition()
                {
                    Id = x.JobPosition.Id,
                    Name = x.JobPosition.Name,
                    Code = x.JobPosition.Code,
                    StatusId = x.JobPosition.StatusId
                },
                Province = x.Province == null ? null : new Province()
                {
                    Id = x.Province.Id,
                    Name = x.Province.Name,
                    Code = x.Province.Code,
                    StatusId = x.Province.StatusId
                },
                Village = x.Village == null ? null : new Village()
                {
                    Id = x.Village.Id,
                    Name = x.Village.Name,
                    Code = x.Village.Code,
                    StatusId = x.Village.StatusId
                },
                Status = x.Status == null ? null : new Status()
                {
                    Id = x.Status.Id,
                    Name = x.Status.Name,
                    Code = x.Status.Code
                }

            }).FirstOrDefaultAsync();
            if(Employee == null)
                return null;
            return Employee;
        }

        public async Task<List<Employee>> List(EmployeeFilter filter)
        {
            if (filter == null) return new List<Employee>();
            IQueryable<EmployeeDAO> EmployeeDAOs = DataContext.Employee.AsNoTracking();
            EmployeeDAOs = DynamicFilter(EmployeeDAOs, filter);
            EmployeeDAOs = DynamicOrder(EmployeeDAOs, filter);
            List<Employee> Employees = await DynamicSelect(EmployeeDAOs, filter);
            return Employees;
        }

        public async Task<List<Employee>> List(List<long> Ids)
        {
            List<Employee> Employees = await DataContext.Employee.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).Select(x => new Employee()
            {
                Id = x.Id,
                FullName = x.FullName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                Birthday = x.Birthday,
                BeginJobTime = x.BeginJobTime,
                ContractStatus = x.ContractStatus,
                StatusId = x.StatusId,
                ChucVuId = x.ChucVuId,
                AcademicLevelId = x.AcademicLevelId,
                JobPositionId = x.JobPositionId,
                VillageId = x.VillageId,
                DistrictId = x.DistrictId,
                ProvinceId = x.ProvinceId,
                Used = x.Used,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                DeletedAt = x.DeletedAt,
                AcademicLevel = x.AcademicLevel == null ? null : new AcademicLevel()
                {
                    Id = x.AcademicLevel.Id,
                    Name = x.AcademicLevel.Name,
                    Code = x.AcademicLevel.Code,
                    StatusId = x.AcademicLevel.StatusId
                },
                ChucVu = x.ChucVu == null ? null : new ChucVu()
                {
                    Id = x.ChucVu.Id,
                    Name = x.ChucVu.Name,
                    Code = x.ChucVu.Code,
                    StatusId = x.ChucVu.StatusId
                },
                District = x.District == null ? null : new District()
                {
                    Id = x.District.Id,
                    Name = x.District.Name,
                    Code = x.District.Code,
                    StatusId = x.District.StatusId
                },
                JobPosition = x.JobPosition == null ? null : new JobPosition()
                {
                    Id = x.JobPosition.Id,
                    Name = x.JobPosition.Name,
                    Code = x.JobPosition.Code,
                    StatusId = x.JobPosition.StatusId
                },
                Province = x.Province == null ? null : new Province()
                {
                    Id = x.Province.Id,
                    Name = x.Province.Name,
                    Code = x.Province.Code,
                    StatusId = x.Province.StatusId
                },
                Village = x.Village == null ? null : new Village()
                {
                    Id = x.Village.Id,
                    Name = x.Village.Name,
                    Code = x.Village.Code,
                    StatusId = x.Village.StatusId
                },
                Status = x.Status == null ? null : new Status()
                {
                    Id = x.Status.Id,
                    Name = x.Status.Name,
                    Code = x.Status.Code
                }
            }).ToListAsync();

            return Employees;
        }

        public Task<bool> SimpleBulkMerge(List<Employee> employees)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> Update(Employee Employee)
        {
            EmployeeDAO EmployeeDAO = DataContext.Employee.Where(x => x.Id == Employee.Id).FirstOrDefault();
            if (EmployeeDAO == null)
                return false;
            EmployeeDAO.Id = Employee.Id;
            EmployeeDAO.FullName = Employee.FullName;
            EmployeeDAO.Email = Employee.Email;
            EmployeeDAO.PhoneNumber = Employee.PhoneNumber;
            EmployeeDAO.Birthday = Employee.Birthday;
            EmployeeDAO.BeginJobTime = Employee.BeginJobTime;
            EmployeeDAO.ContractStatus = Employee.ContractStatus;
            EmployeeDAO.StatusId = Employee.StatusId;
            EmployeeDAO.ChucVuId = Employee.ChucVuId;
            EmployeeDAO.AcademicLevelId = Employee.AcademicLevelId;
            EmployeeDAO.JobPositionId = Employee.JobPositionId;
            EmployeeDAO.VillageId = Employee.VillageId;
            EmployeeDAO.DistrictId = Employee.DistrictId;
            EmployeeDAO.ProvinceId = Employee.ProvinceId;
            EmployeeDAO.Used = false;
            EmployeeDAO.UpdatedAt = StaticParams.DateTimeNow;
            await DataContext.SaveChangesAsync();
                
            return true;
        }

        public async Task<bool> Used(List<long> Ids)
        {
            await DataContext.Employee.Where(x => Ids.Contains(x.Id))
            .UpdateFromQueryAsync(x => new EmployeeDAO { Used = true });
            return true;
        }
            
    }
    
}
