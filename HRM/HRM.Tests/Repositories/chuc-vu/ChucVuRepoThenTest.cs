using HRM.Entities;
using HRM.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrueSight.Common;

namespace HRM.Tests.Repositories.chuc_vu
{
    public partial class ChucVuRepoTest : CommonTests
    {
        public async Task Then_CreateChucVu_Success()
        {
            var Output = DataContext.ChucVu.Where(x => x.Id == ChucVu.Id).FirstOrDefault();

            Assert.AreEqual(ChucVu.Code, Output.Code);
            Assert.AreEqual(ChucVu.Name, Output.Name);
            Assert.AreEqual(ChucVu.StatusId, Output.StatusId);
            Assert.AreEqual(ChucVu.Used, Output.Used);
            Assert.AreEqual(ChucVu.CreatedAt.ToString("dd-MM-yyyy HH:mm:ss"), Output.CreatedAt.ToString("dd-MM-yyyy HH:mm:ss"));
            Assert.AreEqual(ChucVu.UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss"), Output.UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss"));
        }

        public async Task Then_UpdateChucVu_Success()
        {
            //System.Console.WriteLine(JsonConvert.SerializeObject(ChucVu));
            var Output = DataContext.ChucVu.Where(x => x.Id == ChucVu.Id).FirstOrDefault();

            Assert.AreEqual(ChucVu.Code, Output.Code);
            Assert.AreEqual(ChucVu.Name, Output.Name);
            Assert.AreEqual(ChucVu.StatusId, Output.StatusId);
            Assert.AreEqual(ChucVu.CreatedAt.ToString("dd-MM-yyyy HH:mm:ss"), Output.CreatedAt.ToString("dd-MM-yyyy HH:mm:ss"));
            Assert.AreEqual(ChucVu.UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss"), Output.UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss"));
        }

        public async Task Then_DeleteChucVu_Success()
        {
            var Output = await DataContext.ChucVu.Where(x => x.Id == ChucVu.Id).FirstOrDefaultAsync();

            Assert.IsNotNull(Output);
            Assert.IsNotNull(Output.DeletedAt);
        }


        public async Task Then_ListOrderByChucVu_Success()
        {
            IQueryable<ChucVuDAO> query = DataContext.ChucVu.AsNoTracking();
            query = DynamicOrder(query, ChucVuFilter);


            List<ChucVu> OutputChucVus = await query.Select(q => new ChucVu()
            {
                Id = ChucVuFilter.Selects.Contains(ChucVuSelect.Id) ? q.Id : default(long),
                Code = ChucVuFilter.Selects.Contains(ChucVuSelect.Code) ? q.Code : default(string),
                Name = ChucVuFilter.Selects.Contains(ChucVuSelect.Name) ? q.Name : default(string),
                Used = ChucVuFilter.Selects.Contains(ChucVuSelect.Used) ? q.Used : default(bool),
                StatusId = ChucVuFilter.Selects.Contains(ChucVuSelect.Status) ? q.StatusId : default(long),

                Status = ChucVuFilter.Selects.Contains(ChucVuSelect.Status) && q.Status != null ? new Status
                {
                    Id = q.Status.Id,
                    Name = q.Status.Name,
                    Code = q.Status.Code
                } : null
            }).ToListAsync();

            Assert.AreEqual(OutputChucVus.Count(), ChucVus.Count());
            if (ChucVus.Count() == OutputChucVus.Count())
            {
                int length = ChucVus.Count();
                //Console.WriteLine("length: " + length);
                for (int i = 0; i < length; i++)
                {
                    //System.Console.WriteLine($"OutputChucVu {i}: " + JsonConvert.SerializeObject(OutputChucVus[i]));
                    //System.Console.WriteLine($"ChucVu {i}: " + JsonConvert.SerializeObject(ChucVus[i]));
                    Assert.IsTrue(OutputChucVus[i].Equals(ChucVus[i]));
                }

            }
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

        public async Task Then_BulkDeleteChucVu_Success()
        {
            List<long> Ids = ChucVus.Select(x => x.Id).ToList();
            List<ChucVuDAO> Output = await DataContext.ChucVu.Where(x => Ids.Contains(x.Id)).ToListAsync();

            foreach (var item in Output)
            {
                Assert.NotNull(item.DeletedAt);
            }
        }
        public async Task Then_GetChucVu_Success()
        {
            long id = Ids.FirstOrDefault();

            ChucVu Output = await DataContext.ChucVu.AsNoTracking()
            .Where(c => c.Id == id).Select(c => new ChucVu()
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

            if (Output != null && ChucVu != null)
            {
                Assert.AreEqual(ChucVu.Code, Output.Code);
                Assert.AreEqual(ChucVu.Name, Output.Name);
                Assert.AreEqual(ChucVu.StatusId, Output.StatusId);
                Assert.AreEqual(ChucVu.UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss"), Output.UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss"));
            }


        }

        public async Task Then_GetListChucVu_Success()
        {
            List<ChucVu> Output = await DataContext.ChucVu.AsNoTracking()
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

            Assert.AreEqual(Output.Count(), ChucVus.Count);

            int length = Output.Count();
            for (int i = 0; i < length; i++)
            {
                if (Output[i] != null && ChucVus[i] != null)
                {
                    Assert.IsTrue(Output[i].Equals(ChucVus[i]));
                }
            }
        }


    }
}
