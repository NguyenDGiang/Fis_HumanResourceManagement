using TrueSight.Common;
using HRM.Entities;
using HRM.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HRM.Services.MProvince
{
    public interface IProvinceService : IServiceScoped
    {
        Task<int> Count(ProvinceFilter ProvinceFilter);
        Task<List<Province>> List(ProvinceFilter ProvinceFilter);
        Task<Province> Get(long Id);
        Task<Province> Create(Province Province);
        Task<Province> Update(Province Province);
        Task<Province> Delete(Province Province);
        Task<List<Province>> BulkDelete(List<Province> Provinces);
        //Task<List<Province>> BulkMerge(List<Province> Provinces);

        ProvinceFilter ToFilter(ProvinceFilter ProvinceFilter);
    }
    public class ProvinceService : IProvinceService
    {
        private IUOW UOW;
        private IProvinceValidator ProvinceValidator;

        public ProvinceService(IUOW UOW, IProvinceValidator ProvinceValidator)
        {
            this.UOW = UOW;
            this.ProvinceValidator = ProvinceValidator;
        }
        public async Task<List<Province>> BulkDelete(List<Province> Provinces)
        {
            if (!await ProvinceValidator.BulkDelete(Provinces))
                return Provinces;

            try
            {
                await UOW.ProvinceRepository.BulkDelete(Provinces);
                List<long> Ids = Provinces.Select(x => x.Id).ToList();
                Provinces = await UOW.ProvinceRepository.List(Ids);
                return Provinces;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<int> Count(ProvinceFilter ProvinceFilter)
        {
            try
            {
                int result = await UOW.ProvinceRepository.Count(ProvinceFilter);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return 0;
        }

        public async Task<Province> Create(Province Province)
        {
            if (!await ProvinceValidator.Create(Province))
                return Province;

            try
            {
                await UOW.ProvinceRepository.Create(Province);
                Province = await UOW.ProvinceRepository.Get(Province.Id);
                return Province;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<Province> Delete(Province Province)
        {
            if (!await ProvinceValidator.Delete(Province))
                return Province;

            try
            {
                await UOW.ProvinceRepository.Delete(Province);
                var Provinces = await UOW.ProvinceRepository.List(new List<long> { Province.Id });
                Province = Provinces.FirstOrDefault();
                return Province;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<Province> Get(long Id)
        {
            Province Province = await UOW.ProvinceRepository.Get(Id);
            if (Province == null)
                return null;
            return Province;
        }

        public async Task<List<Province>> List(ProvinceFilter ProvinceFilter)
        {
            try
            {
                List<Province> Provinces = await UOW.ProvinceRepository.List(ProvinceFilter);
                return Provinces;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public ProvinceFilter ToFilter(ProvinceFilter ProvinceFilter)
        {
            return ProvinceFilter;
        }

        public async Task<Province> Update(Province Province)
        {
            if (!await ProvinceValidator.Update(Province))
                return Province;
            try
            {
                var oldData = await UOW.ProvinceRepository.Get(Province.Id);
                await UOW.ProvinceRepository.Update(Province);
                Province = await UOW.ProvinceRepository.Get(Province.Id);
                return Province;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }
}
