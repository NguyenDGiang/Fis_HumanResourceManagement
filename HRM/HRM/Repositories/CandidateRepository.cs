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

    public interface ICandidateRepository
    {
        Task<int> Count(CandidateFilter CandidateFilter);
        Task<List<Candidate>> List(CandidateFilter CandidateFilter);
        Task<List<Candidate>> List(List<long> Ids);
        Task<Candidate> Get(long Id);
        Task<bool> Create(Candidate Candidate);
        Task<bool> Update(Candidate Candidate);
        Task<bool> Delete(Candidate Candidate);
        Task<bool> BulkDelete(List<Candidate> Candidates);
        // Task<bool> SimpleBulkMerge(List<Candidate> Candidates);
        // Task<List<long>> BulkMerge(List<Candidate> Candidates);
        // Task<bool> BulkUpdate(List<Candidate> Candidates);
        Task<bool> Used(List<long> Ids);
    }
    public class CandidateRepository : ICandidateRepository
    {
        private DataContext DataContext;
        public CandidateRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        public async Task<bool> BulkDelete(List<Candidate> Candidates)
        {
            List<long> Ids = Candidates.Select(x => x.Id).ToList();
            await DataContext.Candidate.Where(x => Ids.Contains(x.Id))
            .UpdateFromQueryAsync(x => new CandidateDAO
            {
                UpdatedAt = StaticParams.DateTimeNow,
                DeletedAt = StaticParams.DateTimeNow
            });

            return true;
        }

        public async Task<int> Count(CandidateFilter filter)
        {
            IQueryable<CandidateDAO> Candidates = DataContext.Candidate;
            Candidates = DynamicFilter(Candidates, filter);
            Candidates = OrFilter(Candidates, filter);
            return await Candidates.CountAsync();
        }

        public async Task<bool> Create(Candidate Candidate)
        {
            CandidateDAO CandidateDAO = new CandidateDAO();
            CandidateDAO.Id = Candidate.Id;
            CandidateDAO.FullName = Candidate.FullName;
            CandidateDAO.Email = Candidate.Email;
            CandidateDAO.PhoneNumber = Candidate.PhoneNumber;
            CandidateDAO.Birthday = Candidate.Birthday;
            CandidateDAO.StatusId = Candidate.StatusId;
            CandidateDAO.Used = false;
            CandidateDAO.VillageId = Candidate.VillageId;
            CandidateDAO.DistrictId = Candidate.DistrictId;
            CandidateDAO.ProvinceId = Candidate.ProvinceId;
            CandidateDAO.CreatedAt = StaticParams.DateTimeNow;
            CandidateDAO.UpdatedAt = StaticParams.DateTimeNow;

            DataContext.Candidate.Add(CandidateDAO);
            await DataContext.SaveChangesAsync();
            Candidate.Id = CandidateDAO.Id;
            await SaveReference(Candidate);

            return true;
        }

        public async Task<bool> Delete(Candidate Candidate)
        {
            await DataContext.Candidate.Where(x => x.Id == Candidate.Id)
             .UpdateFromQueryAsync(x => new CandidateDAO
             {
                 UpdatedAt = StaticParams.DateTimeNow,
                 DeletedAt = StaticParams.DateTimeNow
             });

            return true;
        }

        public async Task<Candidate> Get(long Id)
        {
            Candidate Candidate = await DataContext.Candidate.AsNoTracking()
            .Where(x => x.Id == Id)
            .Select(x => new Candidate
            {
                Id = x.Id,
                FullName = x.FullName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                Birthday = x.Birthday,
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

            return Candidate;
        }

        public async Task<List<Candidate>> List(CandidateFilter filter)
        {
            if (filter == null) return new List<Candidate>();

            IQueryable<CandidateDAO> CandidateDAOs = DataContext.Candidate;
            CandidateDAOs = DynamicFilter(CandidateDAOs, filter);
            CandidateDAOs = DynamicOrder(CandidateDAOs, filter);

            List<Candidate> Candidates = await DynamicSelect(CandidateDAOs, filter);

            return Candidates;
        }

        public async Task<List<Candidate>> List(List<long> Ids)
        {
            IdFilter IdFilter = new IdFilter { In = Ids };

            List<Candidate> Candidates = await DataContext.Candidate.AsNoTracking()
            .Where(x => x.Id, IdFilter).Select(x => new Candidate
            {
                Id = x.Id,
                FullName = x.FullName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                Birthday = x.Birthday,
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

            return Candidates;

        }

        public async Task<bool> Update(Candidate Candidate)
        {
            CandidateDAO CandidateDAO = DataContext.Candidate.Where(x => x.Id == Candidate.Id).FirstOrDefault();

            CandidateDAO.Id = Candidate.Id;
            CandidateDAO.FullName = Candidate.FullName;
            CandidateDAO.Email = Candidate.Email;
            CandidateDAO.PhoneNumber = Candidate.PhoneNumber;
            CandidateDAO.Birthday = Candidate.Birthday;
            CandidateDAO.StatusId = Candidate.StatusId;
            CandidateDAO.VillageId = Candidate.VillageId;
            CandidateDAO.DistrictId = Candidate.DistrictId;
            CandidateDAO.ProvinceId = Candidate.ProvinceId;
            CandidateDAO.UpdatedAt = StaticParams.DateTimeNow;

            await DataContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Used(List<long> Ids)
        {
            await DataContext.Candidate.AsNoTracking()
            .Where(x => Ids.Contains(x.Id))
            .UpdateFromQueryAsync(x => new CandidateDAO { Used = true });

            return true;
        }

        private async Task SaveReference(Candidate Candidate)
        {

        }

        private async Task<List<Candidate>> DynamicSelect(IQueryable<CandidateDAO> query, CandidateFilter filter)
        {
            List<Candidate> Candidates = await query.Select(q => new Candidate()
            {
                Id = filter.Selects.Contains(CandidateSelect.Id) ? q.Id : default(long),
                FullName = filter.Selects.Contains(CandidateSelect.FullName) ? q.FullName : default(string),
                Email = filter.Selects.Contains(CandidateSelect.Email) ? q.Email : default(string),
                PhoneNumber = filter.Selects.Contains(CandidateSelect.PhoneNumber) ? q.PhoneNumber : default(string),
                Birthday = filter.Selects.Contains(CandidateSelect.Birthday) ? q.Birthday : default(DateTime),
                StatusId = filter.Selects.Contains(CandidateSelect.Status) ? q.StatusId : default(long),
                Used = filter.Selects.Contains(CandidateSelect.Used) ? q.Used : default(bool),
                VillageId = filter.Selects.Contains(CandidateSelect.Village) ? q.VillageId : default(long),
                DistrictId = filter.Selects.Contains(CandidateSelect.District) ? q.DistrictId : default(long),
                ProvinceId = filter.Selects.Contains(CandidateSelect.Province) ? q.ProvinceId : default(long),
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
                DeletedAt = q.DeletedAt,
                District = filter.Selects.Contains(CandidateSelect.District) && q.District != null ? new District
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
                Province = filter.Selects.Contains(CandidateSelect.Province) && q.Province != null ? new Province
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
                Village = filter.Selects.Contains(CandidateSelect.Village) && q.Village != null ? new Village
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
                Status = filter.Selects.Contains(CandidateSelect.Status) && q.Status != null ? new Status
                {
                    Id = q.Status.Id,
                    Code = q.Status.Code,
                    Name = q.Status.Name,
                } : null,
                // InterviewResults

            }).ToListAsync();

            return Candidates;

        }

        private IQueryable<CandidateDAO> DynamicOrder(IQueryable<CandidateDAO> query, CandidateFilter filter)
        {
            switch (filter.OrderType)
            {
                case OrderType.ASC:
                    switch (filter.OrderBy)
                    {
                        case CandidateOrder.Id:
                            query = query.OrderBy(q => q.Id);
                            break;
                        case CandidateOrder.FullName:
                            query = query.OrderBy(q => q.FullName);
                            break;
                        case CandidateOrder.Email:
                            query = query.OrderBy(q => q.Email);
                            break;
                        case CandidateOrder.Status:
                            query = query.OrderBy(q => q.StatusId);
                            break;
                        case CandidateOrder.Used:
                            query = query.OrderBy(q => q.Used);
                            break;
                        case CandidateOrder.PhoneNumber:
                            query = query.OrderBy(q => q.PhoneNumber);
                            break;
                        case CandidateOrder.Birthday:
                            query = query.OrderBy(q => q.Birthday);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (filter.OrderBy)
                    {
                        case CandidateOrder.Id:
                            query = query.OrderByDescending(q => q.Id);
                            break;
                        case CandidateOrder.FullName:
                            query = query.OrderByDescending(q => q.FullName);
                            break;
                        case CandidateOrder.Email:
                            query = query.OrderByDescending(q => q.Email);
                            break;
                        case CandidateOrder.Status:
                            query = query.OrderByDescending(q => q.StatusId);
                            break;
                        case CandidateOrder.Used:
                            query = query.OrderByDescending(q => q.Used);
                            break;
                        case CandidateOrder.PhoneNumber:
                            query = query.OrderByDescending(q => q.PhoneNumber);
                            break;
                        case CandidateOrder.Birthday:
                            query = query.OrderByDescending(q => q.Birthday);
                            break;
                    }
                    break;
            }
            query = query.Skip(filter.Skip).Take(filter.Take);
            return query;
        }

        private IQueryable<CandidateDAO> OrFilter(IQueryable<CandidateDAO> query, CandidateFilter filter)
        {
            if (filter.OrFilter == null || filter.OrFilter.Count == 0)
                return query;
            IQueryable<CandidateDAO> initQuery = query.Where(q => false);

            foreach (CandidateFilter CandidateFilter in filter.OrFilter)
            {
                IQueryable<CandidateDAO> queryable = query;

                queryable = queryable.Where(q => q.Id, filter.Id);
                queryable = queryable.Where(q => q.FullName, filter.FullName);
                queryable = queryable.Where(q => q.Email, filter.Email);
                queryable = queryable.Where(q => q.PhoneNumber, filter.PhoneNumber);
                queryable = queryable.Where(q => q.Birthday, filter.Birthday);
                queryable = queryable.Where(q => q.StatusId, filter.StatusId);



                initQuery = initQuery.Union(queryable);
            }
            return initQuery;
        }

        private IQueryable<CandidateDAO> DynamicFilter(IQueryable<CandidateDAO> query, CandidateFilter filter)
        {
            if (filter == null)
                return query.Where(q => false);
            query = query.Where(q => !q.DeletedAt.HasValue);
            query = query.Where(q => q.Id, filter.Id);
            query = query.Where(q => q.FullName, filter.FullName);
            query = query.Where(q => q.Email, filter.Email);
            query = query.Where(q => q.PhoneNumber, filter.PhoneNumber);
            query = query.Where(q => q.Birthday, filter.Birthday);
            query = query.Where(q => q.StatusId, filter.StatusId);
            query = OrFilter(query, filter);

            return query;
        }

    }
}