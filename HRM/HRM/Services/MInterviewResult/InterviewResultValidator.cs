using HRM.Entities;
using HRM.Repositories;
using System.Threading.Tasks;
using TrueSight.Common;

namespace HRM.Services.MInterviewResult
{
    public interface IInterviewResultValidator : IServiceScoped
    {
        Task<bool> Create(InterviewResult InterviewResult);
        Task<bool> Update(InterviewResult InterviewResult);
        Task<bool> Delete(InterviewResult InterviewResult);
    }
    public class InterviewResultValidator:IInterviewResultValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            InterviewResultInUsed,
            CandidateNotExisted
        }
        private IUOW UOW;

        public InterviewResultValidator(IUOW UOW)
        {
            this.UOW = UOW;
        }

         private async Task<bool> ValidateId(InterviewResult InterviewResult)
        {
            InterviewResultFilter InterviewResultFilter = new InterviewResultFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = InterviewResult.Id },
                Selects = InterviewResultSelect.Id
            };

            int count = await UOW.InterviewResultRepository.Count(InterviewResultFilter);
            if (count == 0)
                InterviewResult.AddError(nameof(InterviewResultValidator), nameof(InterviewResult.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }
        private async Task<bool> ValidateCandidate(InterviewResult InterviewResult)
        {
            if (InterviewResult.CandidateId.HasValue)
            {
                CandidateFilter CandidateFilter = new CandidateFilter
                {
                    Skip = 0,
                    Take = 10,
                    Id = new IdFilter { Equal = InterviewResult.CandidateId },
                    Selects = CandidateSelect.Id
                };

                int count = await UOW.CandidateRepository.Count(CandidateFilter);
                if (count == 0)
                    InterviewResult.AddError(nameof(InterviewResultValidator), nameof(InterviewResult.Candidate), ErrorCode.CandidateNotExisted);
            }

            return InterviewResult.IsValidated;
        }

        public async Task<bool> Create(InterviewResult InterviewResult)
        {
            await ValidateCandidate(InterviewResult);
            return InterviewResult.IsValidated;
        }

        public async Task<bool> Delete(InterviewResult InterviewResult)
        {
            if (await ValidateId(InterviewResult))
            {
                if (InterviewResult.Used)
                    InterviewResult.AddError(nameof(InterviewResultValidator), nameof(District.Id), ErrorCode.InterviewResultInUsed);
            }
            return InterviewResult.IsValidated;
        }

        public async Task<bool> Update(InterviewResult InterviewResult)
        {
            await ValidateId(InterviewResult);
            await ValidateCandidate(InterviewResult);
            return InterviewResult.IsValidated;
        }

    }
}
