using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRM.Entities;
using HRM.Repositories;
using Newtonsoft.Json;
using TrueSight.Common;

namespace HRM.Services.MCandidate
{
    public interface ICandidateService : IServiceScoped
    {
        Task<int> Count(CandidateFilter CandidateFilter);
        Task<List<Candidate>> List(CandidateFilter CandidateFilter);
        Task<Candidate> Get(long Id);
        Task<Candidate> Create(Candidate Candidate);
        Task<List<Candidate>> BulkInsert(List<Candidate> Candidates);
        Task<Candidate> Update(Candidate Candidate);
        Task<Candidate> Delete(Candidate Candidate);
        Task<List<Candidate>> BulkDelete(List<Candidate> Candidates);
        Task<List<Candidate>> BulkMerge(List<Candidate> Candidates);
        CandidateFilter ToFilter(CandidateFilter CandidateFilter);
    }

    public class CandidateService : ICandidateService
    {
        private IUOW UOW;
        public CandidateService(IUOW UOW)
        {
            this.UOW = UOW;
        }
        public async Task<List<Candidate>> BulkDelete(List<Candidate> Candidates)
        {
            try
            {
                await UOW.CandidateRepository.BulkDelete(Candidates);
                List<long> Ids = Candidates.Select(c => c.Id).ToList();
                Candidates = await UOW.CandidateRepository.List(Ids);

                return Candidates;
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex);
                throw;
            }
        }

        public Task<List<Candidate>> BulkInsert(List<Candidate> Candidates)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Candidate>> BulkMerge(List<Candidate> Candidates)
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> Count(CandidateFilter CandidateFilter)
        {
            try
            {
                int result = await UOW.CandidateRepository.Count(CandidateFilter);
                return result;

            }
            catch (System.Exception)
            {


            }
            return 0;
        }

        public async Task<Candidate> Create(Candidate Candidate)
        {
            try
            {
                await UOW.CandidateRepository.Create(Candidate);

                Candidate = await UOW.CandidateRepository.Get(Candidate.Id);
                return Candidate;
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex);
            }
            return null;
        }

        public async Task<Candidate> Delete(Candidate Candidate)
        {
            try
            {
                await UOW.CandidateRepository.Delete(Candidate);

                Candidate = await UOW.CandidateRepository.Get(Candidate.Id);
                return Candidate;
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex);
            }
            return null;

        }

        public async Task<Candidate> Get(long Id)
        {
            Candidate Candidate = await UOW.CandidateRepository.Get(Id);
            if (Candidate == null)
                return null;
            return Candidate;
        }

        public async Task<List<Candidate>> List(CandidateFilter CandidateFilter)
        {
            try
            {
                List<Candidate> Candidates = await UOW.CandidateRepository.List(CandidateFilter);
                return Candidates;
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public CandidateFilter ToFilter(CandidateFilter CandidateFilter)
        {
            return CandidateFilter;
        }

        public async Task<Candidate> Update(Candidate Candidate)
        {

            try
            {
                await UOW.CandidateRepository.Update(Candidate);
                Candidate = await UOW.CandidateRepository.Get(Candidate.Id);
                return Candidate;

            }
            catch (System.Exception ex)
            {

                System.Console.WriteLine(ex);
            }

            return null;
        }


    }
}