using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRM.Entities;
using HRM.Repositories;
using TrueSight.Common;

namespace HRM.Services.MAcademicLevel
{
    public interface IAcademicLevelService : IServiceScoped
    {
        Task<int> Count(AcademicLevelFilter AcademicLevelFilter);
        Task<List<AcademicLevel>> List(AcademicLevelFilter AcademicLevelFilter);
        Task<AcademicLevel> Get(long Id);
        Task<AcademicLevel> Create(AcademicLevel AcademicLevel);
        Task<List<AcademicLevel>> BulkInsert(List<AcademicLevel> AcademicLevels);
        Task<AcademicLevel> Update(AcademicLevel AcademicLevel);
        Task<AcademicLevel> Delete(AcademicLevel AcademicLevel);
        Task<List<AcademicLevel>> BulkDelete(List<AcademicLevel> AcademicLevels);
        Task<List<AcademicLevel>> BulkMerge(List<AcademicLevel> AcademicLevels);
        AcademicLevelFilter ToFilter(AcademicLevelFilter AcademicLevelFilter);
    }
    public class AcademicLevelService : IAcademicLevelService
    {
        private IUOW UOW;
        private IAcademicLevelValidator AcademicLevelValidator;
        public AcademicLevelService(IUOW UOW, IAcademicLevelValidator AcademicLevelValidator)
        {
            this.UOW = UOW;
            this.AcademicLevelValidator = AcademicLevelValidator;
        }
        public async Task<List<AcademicLevel>> BulkDelete(List<AcademicLevel> AcademicLevels)
        {
            if (!await AcademicLevelValidator.BulkDelete(AcademicLevels))
                return AcademicLevels;
            try
            {
                await UOW.AcademicLevelRepository.BulkDelete(AcademicLevels);
                List<long> Ids = AcademicLevels.Select(x => x.Id).ToList();
                AcademicLevels = await UOW.AcademicLevelRepository.List(Ids);

                return AcademicLevels;
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex);
                throw;
            }
        }

        public Task<List<AcademicLevel>> BulkInsert(List<AcademicLevel> AcademicLevels)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<AcademicLevel>> BulkMerge(List<AcademicLevel> AcademicLevels)
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> Count(AcademicLevelFilter AcademicLevelFilter)
        {
            try
            {
                int result = await UOW.AcademicLevelRepository.Count(AcademicLevelFilter);
                return result;
            }
            catch (System.Exception ex)
            {

                System.Console.WriteLine(ex);
            }
            return 0;

        }

        public async Task<AcademicLevel> Create(AcademicLevel AcademicLevel)
        {
            if (!await AcademicLevelValidator.Create(AcademicLevel))
                return AcademicLevel;
            try
            {
                await UOW.AcademicLevelRepository.Create(AcademicLevel);
                AcademicLevel = await UOW.AcademicLevelRepository.Get(AcademicLevel.Id);
                return AcademicLevel;
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex);
                throw;
            }
        }

        public async Task<AcademicLevel> Delete(AcademicLevel AcademicLevel)
        {
            if (!await AcademicLevelValidator.Delete(AcademicLevel))
                return AcademicLevel;
            try
            {
                await UOW.AcademicLevelRepository.Delete(AcademicLevel);
                var AcademicLevels = await UOW.AcademicLevelRepository.List(new List<long> { AcademicLevel.Id });
                AcademicLevel = AcademicLevels.FirstOrDefault();

                return AcademicLevel;
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex);

                throw;
            }
        }

        public async Task<AcademicLevel> Get(long Id)
        {
            try
            {
                AcademicLevel AcademicLevel = await UOW.AcademicLevelRepository.Get(Id);
                return AcademicLevel;
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine("Get\t: " + ex);
                throw;
            }
        }

        public async Task<List<AcademicLevel>> List(AcademicLevelFilter AcademicLevelFilter)
        {
            try
            {
                List<AcademicLevel> AcademicLevels = await UOW.AcademicLevelRepository.List(AcademicLevelFilter);
                return AcademicLevels;
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex);

                throw;
            }
        }

        public AcademicLevelFilter ToFilter(AcademicLevelFilter AcademicLevelFilter)
        {
            return AcademicLevelFilter;
        }

        public async Task<AcademicLevel> Update(AcademicLevel AcademicLevel)
        {
            if (!await AcademicLevelValidator.Update(AcademicLevel))
                return AcademicLevel;

            try
            {
                await UOW.AcademicLevelRepository.Update(AcademicLevel);
                AcademicLevel = await UOW.AcademicLevelRepository.Get(AcademicLevel.Id);
                return AcademicLevel;
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex);

                throw;
            }
        }
    }
}