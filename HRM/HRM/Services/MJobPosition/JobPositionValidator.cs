using HRM.Entities;
using HRM.Repositories;
using System.Threading.Tasks;
using TrueSight.Common;

namespace HRM.Services.MJobPosition
{
    public interface IJobPositionValidator
    {
        Task<bool> Create(JobPosition JobPositiont);
        Task<bool> Update(JobPosition JobPosition);
        Task<bool> Delete(JobPosition JobPosition);
    }
    public class JobPositionValidator:IJobPositionValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            JobPositionInUsed,
            CodeEmpty,
            CodeExisted,
            CodeHasSpecialCharacter,
            NameEmpty,
            NameOverLength

        }
        private IUOW UOW;
        public JobPositionValidator(IUOW UOW)
        {
            this.UOW = UOW;
        }
        private async Task<bool> ValidateId(JobPosition JobPosition)
        {
            JobPositionFilter JobPositionFilter = new JobPositionFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = JobPosition.Id },
                Selects = JobPositionSelect.Id
            };

            int count = await UOW.JobPositionRepository.Count(JobPositionFilter);
            if (count == 0)
                JobPosition.AddError(nameof(JobPositionValidator), nameof(JobPosition.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        private async Task<bool> ValidateCode(JobPosition JobPosition)
        {
            if (string.IsNullOrWhiteSpace(JobPosition.Code))
            {
                JobPosition.AddError(nameof(JobPositionValidator), nameof(JobPosition.Code), ErrorCode.CodeEmpty);
            }
            else
            {
                var Code = JobPosition.Code;
                if (JobPosition.Code.Contains(" ") || !Code.ChangeToEnglishChar().Equals(JobPosition.Code))
                {
                    JobPosition.AddError(nameof(JobPositionValidator), nameof(JobPosition.Code), ErrorCode.CodeHasSpecialCharacter);
                }

                JobPositionFilter JobPositionFilter = new JobPositionFilter
                {
                    Skip = 0,
                    Take = 10,
                    Id = new IdFilter { NotEqual = JobPosition.Id },
                    Code = new StringFilter { Equal = JobPosition.Code },
                    Selects = JobPositionSelect.Code
                };

                int count = await UOW.JobPositionRepository.Count(JobPositionFilter);
                if (count != 0)
                    JobPosition.AddError(nameof(JobPositionValidator), nameof(JobPosition.Code), ErrorCode.CodeExisted);
            }

            return JobPosition.IsValidated;
        }

        private async Task<bool> ValidateName(JobPosition JobPosition)
        {
            var oldData = await UOW.JobPositionRepository.Get(JobPosition.Id);
            if (oldData != null && oldData.Used)
            {
                if (oldData.Name != JobPosition.Name)
                    JobPosition.AddError(nameof(JobPositionValidator), nameof(JobPosition.Id), ErrorCode.JobPositionInUsed);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(JobPosition.Name))
                {
                    JobPosition.AddError(nameof(JobPositionValidator), nameof(JobPosition.Name), ErrorCode.NameEmpty);
                }
                else if (JobPosition.Name.Length > 3000)
                {
                    JobPosition.AddError(nameof(JobPositionValidator), nameof(JobPosition.Name), ErrorCode.NameOverLength);
                }
            }

            return JobPosition.IsValidated;
        }

        public async Task<bool> Create(JobPosition JobPosition)
        {
            await ValidateCode(JobPosition);
            await ValidateName(JobPosition);
            return JobPosition.IsValidated;
        }

        public async Task<bool> Delete(JobPosition JobPosition)
        {
            if (await ValidateId(JobPosition))
            {
                if ((bool)JobPosition.Used)
                    JobPosition.AddError(nameof(JobPositionValidator), nameof(JobPosition.Id), ErrorCode.JobPositionInUsed);
            }
            return JobPosition.IsValidated;
        }

        public async Task<bool> Update(JobPosition JobPosition)
        {
            await ValidateId(JobPosition);
            await ValidateCode(JobPosition);
            await ValidateName(JobPosition);
            return JobPosition.IsValidated;
        }
    }
}
