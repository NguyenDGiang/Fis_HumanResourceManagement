using TrueSight.Common;
using HRM.Entities;
using HRM.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HRM.Services.MVillage
{
    public interface IVillageService : IServiceScoped
    {
        Task<int> Count(VillageFilter VillageFilter);
        Task<List<Village>> List(VillageFilter VillageFilter);
        Task<Village> Get(long Id);
        Task<Village> Create(Village Village);
        Task<Village> Update(Village Village);
        Task<Village> Delete(Village Village);
        Task<List<Village>> BulkDelete(List<Village> Villages);
        VillageFilter ToFilter(VillageFilter VillageFilter);
    }
    public class VillageService : IVillageService
    {
        private IUOW UOW;
        private IVillageValidator VillageValidator;
        public VillageService(IUOW UOW, IVillageValidator VillageValidator)
        {
            this.UOW = UOW;
            this.VillageValidator = VillageValidator;
        }
        public async Task<List<Village>> BulkDelete(List<Village> Villages)
        {
            if (!await VillageValidator.BulkDelete(Villages))
                return Villages;

            try
            {

                await UOW.VillageRepository.BulkDelete(Villages);

                List<long> Ids = Villages.Select(x => x.Id).ToList();
                Villages = await UOW.VillageRepository.List(Ids);
                return Villages;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<int> Count(VillageFilter VillageFilter)
        {
            try
            {
                int result = await UOW.VillageRepository.Count(VillageFilter);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return 0;
        }

        public async Task<Village> Create(Village Village)
        {
            if (!await VillageValidator.Create(Village))
                return Village;

            try
            {
                await UOW.VillageRepository.Create(Village);
                List<Village> Villages = await UOW.VillageRepository.List(new List<long> { Village.Id });
                Village = Villages.FirstOrDefault();
                return Village;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<Village> Delete(Village Village)
        {
            if (!await VillageValidator.Delete(Village))
                return Village;

            try
            {
                await UOW.VillageRepository.Delete(Village);
                List<Village> Villages = await UOW.VillageRepository.List(new List<long> { Village.Id });
                Village = Villages.FirstOrDefault();
                return Village;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<Village> Get(long Id)
        {
            Village Village = await UOW.VillageRepository.Get(Id);
            if (Village == null)
                return null;
            return Village;
        }

        public async Task<List<Village>> List(VillageFilter VillageFilter)
        {
            try
            {
                List<Village> Villages = await UOW.VillageRepository.List(VillageFilter);
                return Villages;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public VillageFilter ToFilter(VillageFilter VillageFilter)
        {
            return VillageFilter;
        }

        public async Task<Village> Update(Village Village)
        {
            if (!await VillageValidator.Update(Village))
                return Village;
            try
            {
                var oldData = await UOW.VillageRepository.Get(Village.Id);
                await UOW.VillageRepository.Update(Village);
                List<Village> Villages = await UOW.VillageRepository.List(new List<long> { Village.Id });
                Village = Villages.FirstOrDefault();
                return Village;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }
}
