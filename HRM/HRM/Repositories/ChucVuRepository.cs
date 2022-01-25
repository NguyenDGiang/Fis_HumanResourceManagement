using System.Collections.Generic;
using System.Threading.Tasks;
using HRM.Entities;
using HRM.Models;
using Microsoft.EntityFrameworkCore;
using TrueSight.Common;
using System.Linq;

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
        Task<bool> BulkInsertNewChucVu(List<ChucVu> ChucVus);
        Task<bool> BulkDeleteNewChucVu(List<ChucVu> ChucVus);
        Task<bool> SimpleBulkMerge(List<ChucVu> ChucVus);
        Task<bool> BulkMerge(List<ChucVu> ChucVus);
        Task<bool> BulkUpdate(List<ChucVu> ChucVus);
        Task<bool> BulkDelete(List<ChucVu> Products);
        Task<bool> Used(List<long> Ids);
    }
    public class ChucVuRepository : IChucVuRepository
    {
        private DataContext DataContext;
        public ChucVuRepository(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        public Task<bool> BulkDelete(List<ChucVu> Products)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> BulkDeleteNewChucVu(List<ChucVu> ChucVus)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> BulkInsertNewChucVu(List<ChucVu> ChucVus)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> BulkMerge(List<ChucVu> ChucVus)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> BulkUpdate(List<ChucVu> ChucVus)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> Count(ChucVuFilter ChucVuFilter)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Create(ChucVu ChucVu)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Delete(ChucVu ChucVu)
        {
            throw new System.NotImplementedException();
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

        public Task<List<ChucVu>> List(ChucVuFilter ChucVuFilter)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<ChucVu>> List(List<long> Ids)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> SimpleBulkMerge(List<ChucVu> ChucVus)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Update(ChucVu ChucVu)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Used(List<long> Ids)
        {
            throw new System.NotImplementedException();
        }
    }
}
