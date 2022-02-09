using TrueSight.Common;
using HRM.Entities;
using HRM.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace HRM.Services.MDistrict
{
    public interface IDistrictService : IServiceScoped
    {
        Task<int> Count(DistrictFilter DistrictFilter);
        Task<List<District>> List(DistrictFilter DistrictFilter);
        Task<District> Get(long Id);
        Task<District> Create(District District);
        Task<District> Update(District District);
        Task<District> Delete(District District);
        Task<List<District>> BulkDelete(List<District> Districts);
        DistrictFilter ToFilter(DistrictFilter DistrictFilter);
    }
    public class DistrictService : IDistrictService
    {
        private IUOW UOW;
        private IDistrictValidator DistrictValidator;

        public DistrictService(IUOW UOW, IDistrictValidator DistrictValidator)
        {
            this.UOW = UOW;
            this.DistrictValidator = DistrictValidator;
        }
        public async Task<List<District>> BulkDelete(List<District> Districts)
        {
            if (!await DistrictValidator.BulkDelete(Districts))
                return Districts;

            try
            {
                await UOW.DistrictRepository.BulkDelete(Districts);
                List<long> Ids = Districts.Select(x => x.Id).ToList();
                Districts = await UOW.DistrictRepository.List(Ids);
                return Districts;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<int> Count(DistrictFilter DistrictFilter)
        {
            try
            {
                int result = await UOW.DistrictRepository.Count(DistrictFilter);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return 0;
        }

        public async Task<District> Create(District District)
        {
            if (!await DistrictValidator.Create(District))
                return District;

            try
            {
                await UOW.DistrictRepository.Create(District);
                List<District> Districts = await UOW.DistrictRepository.List(new List<long> { District.Id });
                District = Districts.FirstOrDefault();
                return District;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<District> Delete(District District)
        {
            if (!await DistrictValidator.Delete(District))
                return District;

            try
            {
                await UOW.DistrictRepository.Delete(District);
                List<District> Districts = await UOW.DistrictRepository.List(new List<long> { District.Id });
                District = Districts.FirstOrDefault();
                return District;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<District> Get(long Id)
        {
            District District = await UOW.DistrictRepository.Get(Id);
            if (District == null)
                return null;
            return District;
        }

        public async Task<List<District>> List(DistrictFilter DistrictFilter)
        {
            try
            {
                List<District> Districts = await UOW.DistrictRepository.List(DistrictFilter);
                return Districts;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public DistrictFilter ToFilter(DistrictFilter DistrictFilter)
        {
            return DistrictFilter;
        }

        public async Task<District> Update(District District)
        {
            if (!await DistrictValidator.Update(District))
                return District;
            try
            {
                var oldData = await UOW.DistrictRepository.Get(District.Id);
                await UOW.DistrictRepository.Update(District);
                List<District> Districts = await UOW.DistrictRepository.List(new List<long> { District.Id });
                District = Districts.FirstOrDefault();
                return District;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }
}
