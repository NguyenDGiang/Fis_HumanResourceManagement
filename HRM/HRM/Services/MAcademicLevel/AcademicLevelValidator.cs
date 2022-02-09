using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRM.Entities;
using HRM.Enums;
using HRM.Repositories;
using TrueSight.Common;

namespace HRM.Services.MAcademicLevel
{
    public interface IAcademicLevelValidator : IServiceScoped
    {
        Task<bool> Create(AcademicLevel AcademicLevel);
        Task<bool> Update(AcademicLevel AcademicLevel);
        Task<bool> Delete(AcademicLevel AcademicLevel);
        Task<bool> BulkDelete(List<AcademicLevel> AcademicLevels);
    }
    public class AcademicLevelValidator : IAcademicLevelValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            AcademicLevelInUsed,
            CodeHasSpecialCharacter,
            CodeEmpty,
            CodeExisted,
            NameEmpty,
            PercentageEmpty,
            NameOverLength
        }
        private IUOW UOW;
        public AcademicLevelValidator(IUOW UOW)
        {
            this.UOW = UOW;
        }
        public async Task<bool> ValidateId(AcademicLevel AcademicLevel)
        {
            AcademicLevelFilter AcademicLevelFilter = new AcademicLevelFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = AcademicLevel.Id },
                Selects = AcademicLevelSelect.Id
            };

            int count = await UOW.AcademicLevelRepository.Count(AcademicLevelFilter);
            if (count == 0)
                AcademicLevel.AddError(nameof(AcademicLevelValidator), nameof(AcademicLevel.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        private async Task<bool> ValidateCode(AcademicLevel AcademicLevel)
        {
            if (string.IsNullOrEmpty(AcademicLevel.Code))
            {
                AcademicLevel.AddError(nameof(AcademicLevelValidator), nameof(AcademicLevel.Code), ErrorCode.CodeEmpty);
                return false;
            }
            else
            {
                var Code = AcademicLevel.Code;
                if (AcademicLevel.Code.Contains(" ") || !Code.ChangeToEnglishChar().Equals(AcademicLevel.Code))
                {
                    AcademicLevel.AddError(nameof(AcademicLevelValidator), nameof(AcademicLevel.Code), ErrorCode.CodeHasSpecialCharacter);
                }
                else
                {
                    AcademicLevelFilter AcademicLevelFilter = new AcademicLevelFilter
                    {
                        Skip = 0,
                        Take = 10,
                        Id = new IdFilter { NotEqual = AcademicLevel.Id },
                        Code = new StringFilter { Equal = AcademicLevel.Code },
                        Selects = AcademicLevelSelect.Code
                    };

                    int count = await UOW.AcademicLevelRepository.Count(AcademicLevelFilter);
                    if (count != 0)
                        AcademicLevel.AddError(nameof(AcademicLevelValidator), nameof(AcademicLevel.Code), ErrorCode.CodeExisted);
                }

            }

            return AcademicLevel.IsValidated;
        }

        private async Task<bool> ValidateName(AcademicLevel AcademicLevel)
        {
            if (string.IsNullOrEmpty(AcademicLevel.Name))
            {
                AcademicLevel.AddError(nameof(AcademicLevelValidator), nameof(AcademicLevel.Name), ErrorCode.NameEmpty);
                return false;
            }
            else if (AcademicLevel.Name.Length > 255)
            {
                AcademicLevel.AddError(nameof(AcademicLevelValidator), nameof(AcademicLevel.Name), ErrorCode.NameOverLength);
                return false;
            }
            return true;
        }

        private async Task<bool> ValidateAcademicLevelInUsed(AcademicLevel AcademicLevel)
        {
            EmployeeFilter EmployeeFilter = new EmployeeFilter
            {
                AcademicLevelId = new IdFilter { Equal = AcademicLevel.Id },
                StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id },
            };
            int count = await UOW.EmployeeRepository.Count(EmployeeFilter);
            if (count > 0)
                AcademicLevel.AddError(nameof(AcademicLevelValidator), nameof(AcademicLevel.Id), ErrorCode.AcademicLevelInUsed);

            return AcademicLevel.IsValidated;
        }
        public async Task<bool> BulkDelete(List<AcademicLevel> AcademicLevels)
        {
            foreach (AcademicLevel AcademicLevel in AcademicLevels)
            {
                await Delete(AcademicLevel);
            }
            return AcademicLevels.All(st => st.IsValidated);
        }

        public async Task<bool> Create(AcademicLevel AcademicLevel)
        {
            await ValidateCode(AcademicLevel);
            await ValidateName(AcademicLevel);
            return AcademicLevel.IsValidated;
        }

        public async Task<bool> Delete(AcademicLevel AcademicLevel)
        {
            if (await ValidateId(AcademicLevel))
            {
                await ValidateAcademicLevelInUsed(AcademicLevel);
            }
            return AcademicLevel.IsValidated;
        }

        public async Task<bool> Update(AcademicLevel AcademicLevel)
        {
            if (await ValidateId(AcademicLevel))
            {
                await ValidateCode(AcademicLevel);
                await ValidateName(AcademicLevel);
            }
            return AcademicLevel.IsValidated;
        }
    }
}