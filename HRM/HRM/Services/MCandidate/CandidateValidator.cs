using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRM.Entities;
using HRM.Enums;
using HRM.Repositories;
using TrueSight.Common;

namespace HRM.Services.MCandidate
{
    public interface ICandidateValidator : IServiceScoped
    {
        Task<bool> Create(Candidate Candidate);
        Task<bool> Update(Candidate Candidate);
        Task<bool> Delete(Candidate Candidate);
        Task<bool> BulkDelete(List<Candidate> Candidates);
    }
    public class CandidateValidator : ICandidateValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            CandidateInUsed,
            CodeHasSpecialCharacter,
            CodeEmpty,
            CodeExisted,
            NameEmpty,
            PercentageEmpty,
            NameOverLength
        }
        private IUOW UOW;
        public CandidateValidator(IUOW UOW)
        {
            this.UOW = UOW;
        }
        public async Task<bool> ValidateId(Candidate Candidate)
        {
            CandidateFilter CandidateFilter = new CandidateFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = Candidate.Id },
                Selects = CandidateSelect.Id
            };

            int count = await UOW.CandidateRepository.Count(CandidateFilter);
            System.Console.WriteLine("count : " + count);
            if (count == 0)
                Candidate.AddError(nameof(CandidateValidator), nameof(Candidate.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        private async Task<bool> ValidateName(Candidate Candidate)
        {
            if (string.IsNullOrEmpty(Candidate.FullName))
            {
                Candidate.AddError(nameof(CandidateValidator), nameof(Candidate.FullName), ErrorCode.NameEmpty);
                return false;
            }
            else if (Candidate.FullName.Length > 255)
            {
                Candidate.AddError(nameof(CandidateValidator), nameof(Candidate.FullName), ErrorCode.NameOverLength);
                return false;
            }
            return true;
        }

        public async Task<bool> BulkDelete(List<Candidate> Candidates)
        {
            foreach (Candidate Candidate in Candidates)
            {
                await Delete(Candidate);
            }
            return Candidates.All(st => st.IsValidated);
        }

        public async Task<bool> Create(Candidate Candidate)
        {
            await ValidateName(Candidate);
            return Candidate.IsValidated;
        }

        public async Task<bool> Delete(Candidate Candidate)
        {
            if (await ValidateId(Candidate))
            {
                // await ValidateCandidateInUsed(Candidate);
            }
            return Candidate.IsValidated;
        }

        public async Task<bool> Update(Candidate Candidate)
        {
            if (await ValidateId(Candidate))
            {
                await ValidateName(Candidate);
            }
            return Candidate.IsValidated;
        }
    }
}