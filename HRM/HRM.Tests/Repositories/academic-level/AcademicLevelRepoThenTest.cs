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

namespace HRM.Tests.Repositories.academic_level
{
    public partial class AcademicLevelRepoTest : CommonTests
    {
        private async Task Then_CountAcademicLevel_Success()
        {
            IQueryable<AcademicLevelDAO> query = DataContext.AcademicLevel.AsNoTracking();
            query = DynamicFilter(query, filter);
            int output = query.Count();
            Assert.AreEqual(count, output);
        }

        private async Task Then_CreateAcademicLevel_Success()
        {
            var Output = await DataContext.AcademicLevel.Where(x => x.Id == AcademicLevel.Id).FirstOrDefaultAsync();

            Assert.AreEqual(AcademicLevel.Code, Output.Code);
            Assert.AreEqual(AcademicLevel.Name, Output.Name);
            Assert.AreEqual(AcademicLevel.StatusId, Output.StatusId);
            Assert.AreEqual(AcademicLevel.StatusId, Output.StatusId);
            Assert.AreEqual(AcademicLevel.CreatedAt.ToString("dd-MM-yyyy HH:mm:ss"), Output.CreatedAt.ToString("dd-MM-yyyy HH:mm:ss"));
            Assert.AreEqual(AcademicLevel.UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss"), Output.UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss"));
        }

        public async Task Then_UpdateAcademicLevel_Success()
        {
            var Output = await DataContext.AcademicLevel.Where(x => x.Id == AcademicLevel.Id).FirstOrDefaultAsync();

            Assert.AreEqual(AcademicLevel.Code, Output.Code);
            Assert.AreEqual(AcademicLevel.Name, Output.Name);
            Assert.AreEqual(AcademicLevel.StatusId, Output.StatusId);
            Assert.AreEqual(AcademicLevel.StatusId, Output.StatusId);
            Assert.AreEqual(AcademicLevel.CreatedAt.ToString("dd-MM-yyyy HH:mm:ss"), Output.CreatedAt.ToString("dd-MM-yyyy HH:mm:ss"));
            Assert.AreEqual(AcademicLevel.UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss"), Output.UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss"));
        }

        public async Task Then_DeleteAcademicLevel_Success()
        {
            var Output = await DataContext.AcademicLevel.Where(x => x.Id == AcademicLevel.Id).FirstOrDefaultAsync();
            Assert.IsNotNull(Output);
            Assert.IsNotNull(Output.DeletedAt);
        }

        public async Task Then_ListOrderByAcademicLevel_Success()
        {
            IQueryable<AcademicLevelDAO> query = DataContext.AcademicLevel.AsNoTracking();
            query = DynamicOrder(query, filter);

            List<AcademicLevelDAO> OutputAcademicLevelDAOs = await query.ToListAsync();

            Assert.AreEqual(OutputAcademicLevelDAOs.Count(), AcademicLevels.Count());
            if (AcademicLevels.Count() == OutputAcademicLevelDAOs.Count())
            {
                int length = AcademicLevels.Count();
                for (int i = 0; i < length; i++)
                {
                    Assert.AreEqual(OutputAcademicLevelDAOs[i].Id, AcademicLevels[i].Id);
                }
            }
        }

        public async Task Then_BulkDeleteAcademicLevel_Success()
        {
            List<long> Ids = AcademicLevels.Select(x => x.Id).ToList();
            List<AcademicLevelDAO> Output = await DataContext.AcademicLevel.Where(x => Ids.Contains(x.Id)).ToListAsync();

            foreach (var item in Output)
            {
                Assert.NotNull(item.DeletedAt);
            }
        }

        public async Task Then_GetAcademicLevel_Success()
        {
            long id = Ids.FirstOrDefault();

            AcademicLevelDAO Output = await DataContext.AcademicLevel.AsNoTracking()
            .Where(c => c.Id == id).FirstOrDefaultAsync();

            // Console.WriteLine("Output: " + JsonConvert.SerializeObject(Output));
            // Console.WriteLine("AcademicLevel: " + JsonConvert.SerializeObject(AcademicLevel)); 
            if (Output != null && AcademicLevel != null)
            {
                Assert.AreEqual(AcademicLevel.Code, Output.Code);
                Assert.AreEqual(AcademicLevel.Name, Output.Name);
                Assert.AreEqual(AcademicLevel.StatusId, Output.StatusId);
                Assert.AreEqual(AcademicLevel.Used, Output.Used);
                Assert.AreEqual(AcademicLevel.CreatedAt.ToString("dd-MM-yyyy HH:mm:ss"), Output.CreatedAt.ToString("dd-MM-yyyy HH:mm:ss"));
                Assert.AreEqual(AcademicLevel.UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss"), Output.UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss"));
            }
        }

        public async Task Then_GetListAcademicLevel_Success()
        {
            List<AcademicLevelDAO> Output = await DataContext.AcademicLevel.AsNoTracking()
            .Where(x => Ids.Contains(x.Id)).ToListAsync();

            Assert.AreEqual(Output.Count(), AcademicLevels.Count());

            int length = Output.Count();
            for (int i = 0; i < length; i++)
            {
                if (Output[i] != null && AcademicLevels[i] != null)
                {
                    Assert.AreEqual(AcademicLevels[i].Id, Output[i].Id);
                    Assert.AreEqual(AcademicLevels[i].Code, Output[i].Code);
                    Assert.AreEqual(AcademicLevels[i].Name, Output[i].Name);
                    Assert.AreEqual(AcademicLevels[i].StatusId, Output[i].StatusId);
                    Assert.AreEqual(AcademicLevels[i].Used, Output[i].Used);
                    Assert.AreEqual(AcademicLevels[i].CreatedAt.ToString("dd-MM-yyyy HH:mm:ss"), Output[i].CreatedAt.ToString("dd-MM-yyyy HH:mm:ss"));
                    Assert.AreEqual(AcademicLevels[i].UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss"), Output[i].UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss"));
                }
            }
        }

        public async Task Then_ListFilterAcademicLevel_Success()
        {

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
