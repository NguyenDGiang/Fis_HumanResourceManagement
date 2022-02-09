using TrueSight.Common;
using HRM.Entities;
using HRM.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRM.Services.MVillage
{
    public interface IVillageValidator : IServiceScoped
    {
        Task<bool> Create(Village Village);
        Task<bool> Update(Village Village);
        Task<bool> Delete(Village Village);
        Task<bool> BulkDelete(List<Village> Villages);
    }
    public class VillageValidator : IVillageValidator
    {
        private IUOW UOW;
        public VillageValidator(IUOW UOW)
        {
            this.UOW = UOW;
        }

        public enum ErrorCode
        {
            IdNotExisted,
            VillageInUsed,
            CodeEmpty,
            CodeHasSpecialCharacter,
            CodeOverLength,
            NameEmpty,
            CodeExisted,
            NameOverLength
        }

        public async Task<bool> ValidateId(Village Village)
        {
            VillageFilter VillageFilter = new VillageFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = Village.Id },
                Selects = VillageSelect.Id
            };

            int count = await UOW.VillageRepository.Count(VillageFilter);
            if (count == 0)
                Village.AddError(nameof(VillageValidator), nameof(Village.Id), ErrorCode.IdNotExisted);
            return Village.IsValidated;
        }
        private async Task<bool> ValidateCode(Village Village)
        {
            if (string.IsNullOrEmpty(Village.Code))
            {
                Village.AddError(nameof(VillageValidator), nameof(Village.Code), ErrorCode.CodeEmpty);
            }
            else
            {
                var Code = Village.Code;
                if (Village.Code.Contains(" ") || !Code.ChangeToEnglishChar().Equals(Village.Code))
                {
                    Village.AddError(nameof(VillageValidator), nameof(Village.Code), ErrorCode.CodeHasSpecialCharacter);
                }
                else if (Village.Code.Length > 50)
                {
                    Village.AddError(nameof(VillageValidator), nameof(Village.Code), ErrorCode.CodeOverLength);
                }
                else
                {
                    VillageFilter WardFilter = new VillageFilter
                    {
                        Skip = 0,
                        Take = 10,
                        Id = new IdFilter { NotEqual = Village.Id },
                        Code = new StringFilter { Equal = Village.Code },
                        Selects = VillageSelect.Code
                    };

                    int count = await UOW.VillageRepository.Count(WardFilter);
                    if (count != 0)
                        Village.AddError(nameof(VillageValidator), nameof(Village.Code), ErrorCode.CodeExisted);
                }
            }
            return Village.IsValidated;
        }

        private async Task<bool> ValidateName(Village Village)
        {
            if (string.IsNullOrEmpty(Village.Name))
            {
                Village.AddError(nameof(VillageValidator), nameof(Village.Name), ErrorCode.NameEmpty);
            }
            else if (Village.Name.Length > 255)
            {
                Village.AddError(nameof(VillageValidator), nameof(Village.Name), ErrorCode.NameOverLength);
            }
            return Village.IsValidated;
        }

        public async Task<bool> BulkDelete(List<Village> Villages)
        {
            foreach (Village Village in Villages)
            {
                await Delete(Village);
            }
            return Villages.All(st => st.IsValidated);
        }

        public async Task<bool> Create(Village Village)
        {
            await ValidateCode(Village);
            await ValidateName(Village);
            return Village.IsValidated;
        }

        public async Task<bool> Delete(Village Village)
        {
            if (await ValidateId(Village))
            {
                if (Village.Used)
                    Village.AddError(nameof(VillageValidator), nameof(Village.Id), ErrorCode.VillageInUsed);
            }
            return Village.IsValidated;
        }

        public async Task<bool> Update(Village Village)
        {
            if (await ValidateId(Village))
            {
                await ValidateCode(Village);
                await ValidateName(Village);
            }
            return Village.IsValidated;
        }
    }
}
