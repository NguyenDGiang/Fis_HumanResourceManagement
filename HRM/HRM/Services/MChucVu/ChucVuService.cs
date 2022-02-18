using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRM.Entities;
using HRM.Repositories;
using TrueSight.Common;

namespace HRM.Services.MChucVu
{
    public interface IChucVuService : IServiceScoped
    {
        Task<int> Count(ChucVuFilter ChucVuFilter);
        Task<List<ChucVu>> List(ChucVuFilter ChucVuFilter);
        Task<ChucVu> Get(long Id);
        Task<ChucVu> Create(ChucVu ChucVu);
        Task<List<ChucVu>> BulkInsert(List<ChucVu> ChucVus);
        Task<ChucVu> Update(ChucVu ChucVu);
        Task<ChucVu> Delete(ChucVu ChucVu);
        Task<List<ChucVu>> BulkDelete(List<ChucVu> ChucVus);
        Task<List<ChucVu>> BulkMerge(List<ChucVu> ChucVus);
        ChucVuFilter ToFilter(ChucVuFilter ChucVuFilter);

    }
    public class ChucVuService : IChucVuService
    {
        private IUOW UOW;
        private IChucVuValidator ChucVuValidator;
        public ChucVuService(IUOW UOW, IChucVuValidator ChucVuValidator)
        {
            this.UOW = UOW;
            this.ChucVuValidator = ChucVuValidator;
        }

        public async Task<List<ChucVu>> BulkDelete(List<ChucVu> ChucVus)
        {
            if (!await ChucVuValidator.BulkDelete(ChucVus))
                return ChucVus;
            try
            {
                await UOW.ChucVuRepository.BulkDelete(ChucVus);

                List<long> Ids = ChucVus.Select(x => x.Id).ToList();
                ChucVus = await UOW.ChucVuRepository.List(Ids);

                return ChucVus;
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex);
                // throw;
            }
            return null;

        }

        public Task<List<ChucVu>> BulkInsert(List<ChucVu> ChucVus)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<ChucVu>> BulkMerge(List<ChucVu> ChucVus)
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> Count(ChucVuFilter ChucVuFilter)
        {
            try
            {
                int result = await UOW.ChucVuRepository.Count(ChucVuFilter);
                return result;
            }
            catch (System.Exception ex)
            {

                System.Console.WriteLine("ChucVuService -> Count: " + ex);
            }
            return 0;
        }

        public async Task<ChucVu> Create(ChucVu ChucVu)
        {
            if (!await ChucVuValidator.Create(ChucVu))
                return ChucVu;
            try
            {
                await UOW.ChucVuRepository.Create(ChucVu);
                ChucVu = await UOW.ChucVuRepository.Get(ChucVu.Id);

                return ChucVu;
            }
            catch (System.Exception ex)
            {

                System.Console.WriteLine("ChucVuService -> Create: " + ex);
            }
            return null;
        }

        public async Task<ChucVu> Delete(ChucVu ChucVu)
        {
            if (!await ChucVuValidator.Delete(ChucVu))
                return ChucVu;
            try
            {
                await UOW.ChucVuRepository.Delete(ChucVu);
                var ChucVus = await UOW.ChucVuRepository.List(new List<long> { ChucVu.Id });
                ChucVu = ChucVus.FirstOrDefault();

                return ChucVu;
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex);
                // throw;
            }
            return null;

        }

        public async Task<ChucVu> Get(long Id)
        {
            ChucVu ChucVu = await UOW.ChucVuRepository.Get(Id);
            if (ChucVu == null)
            {
                return null;
            }
            return ChucVu;
        }

        public async Task<List<ChucVu>> List(ChucVuFilter ChucVuFilter)
        {
            try
            {
                List<ChucVu> ChucVus = await UOW.ChucVuRepository.List(ChucVuFilter);
                return ChucVus;

            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex);
                // throw;
            }
            return null;

        }

        public ChucVuFilter ToFilter(ChucVuFilter ChucVuFilter)
        {
            return ChucVuFilter;
        }

        public async Task<ChucVu> Update(ChucVu ChucVu)
        {
            if (!await ChucVuValidator.Update(ChucVu))
                return ChucVu;
            try
            {
                await UOW.ChucVuRepository.Update(ChucVu);
                ChucVu = await UOW.ChucVuRepository.Get(ChucVu.Id);

                return ChucVu;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);
            }
            return null;
        }
    }
}