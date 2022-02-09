using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRM.Entities;
using HRM.Enums;
using HRM.Repositories;
using TrueSight.Common;

namespace HRM.Services.MChucVu
{
    public interface IChucVuValidator : IServiceScoped
    {
        Task<bool> Create(ChucVu ChucVu);
        Task<bool> Update(ChucVu ChucVu);
        Task<bool> Delete(ChucVu ChucVu);
        Task<bool> BulkDelete(List<ChucVu> ChucVus);
    }
    public class ChucVuValidator : IChucVuValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            ChucVuInUsed,
            CodeHasSpecialCharacter,
            CodeEmpty,
            CodeExisted,
            NameEmpty,
            PercentageEmpty,
            NameOverLength
        }
        private IUOW UOW;
        public ChucVuValidator(IUOW UOW)
        {
            this.UOW = UOW;
        }
        public async Task<bool> ValidateId(ChucVu ChucVu)
        {
            ChucVuFilter ChucVuFilter = new ChucVuFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = ChucVu.Id },
                Selects = ChucVuSelect.Id
            };

            int count = await UOW.ChucVuRepository.Count(ChucVuFilter);
            if (count == 0)
                ChucVu.AddError(nameof(ChucVuValidator), nameof(ChucVu.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        private async Task<bool> ValidateCode(ChucVu ChucVu)
        {
            if (string.IsNullOrEmpty(ChucVu.Code))
            {
                ChucVu.AddError(nameof(ChucVuValidator), nameof(ChucVu.Code), ErrorCode.CodeEmpty);
                return false;
            }
            else
            {
                var Code = ChucVu.Code;
                if (ChucVu.Code.Contains(" ") || !Code.ChangeToEnglishChar().Equals(ChucVu.Code))
                {
                    ChucVu.AddError(nameof(ChucVuValidator), nameof(ChucVu.Code), ErrorCode.CodeHasSpecialCharacter);
                }
                else
                {
                    ChucVuFilter ChucVuFilter = new ChucVuFilter
                    {
                        Skip = 0,
                        Take = 10,
                        Id = new IdFilter { NotEqual = ChucVu.Id },
                        Code = new StringFilter { Equal = ChucVu.Code },
                        Selects = ChucVuSelect.Code
                    };

                    int count = await UOW.ChucVuRepository.Count(ChucVuFilter);
                    if (count != 0)
                        ChucVu.AddError(nameof(ChucVuValidator), nameof(ChucVu.Code), ErrorCode.CodeExisted);
                }

            }

            return ChucVu.IsValidated;
        }

        private async Task<bool> ValidateName(ChucVu ChucVu)
        {
            if (string.IsNullOrEmpty(ChucVu.Name))
            {
                ChucVu.AddError(nameof(ChucVuValidator), nameof(ChucVu.Name), ErrorCode.NameEmpty);
                return false;
            }
            else if (ChucVu.Name.Length > 255)
            {
                ChucVu.AddError(nameof(ChucVuValidator), nameof(ChucVu.Name), ErrorCode.NameOverLength);
                return false;
            }
            return true;
        }

        private async Task<bool> ValidateChucVuInUsed(ChucVu ChucVu)
        {
            EmployeeFilter EmployeeFilter = new EmployeeFilter
            {
                ChucVuId = new IdFilter { Equal = ChucVu.Id },
                StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id },
            };
            int count = await UOW.EmployeeRepository.Count(EmployeeFilter);
            if (count > 0)
                ChucVu.AddError(nameof(ChucVuValidator), nameof(ChucVu.Id), ErrorCode.ChucVuInUsed);

            return ChucVu.IsValidated;
        }
        public async Task<bool> BulkDelete(List<ChucVu> ChucVus)
        {
            foreach (ChucVu ChucVu in ChucVus)
            {
                await Delete(ChucVu);
            }
            return ChucVus.All(st => st.IsValidated);
        }

        public async Task<bool> Create(ChucVu ChucVu)
        {
            await ValidateCode(ChucVu);
            await ValidateName(ChucVu);
            return ChucVu.IsValidated;
        }

        public async Task<bool> Delete(ChucVu ChucVu)
        {
            if (await ValidateId(ChucVu))
            {
                await ValidateChucVuInUsed(ChucVu);
            }
            return ChucVu.IsValidated;
        }

        public async Task<bool> Update(ChucVu ChucVu)
        {
            if (await ValidateId(ChucVu))
            {
                await ValidateCode(ChucVu);
                await ValidateName(ChucVu);
            }
            return ChucVu.IsValidated;
        }
    }
}