﻿using TrueSight.Common;
using HRM.Entities;
using HRM.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRM.Services.MProvince
{
    public interface IProvinceValidator : IServiceScoped
    {
        Task<bool> Create(Province Province);
        Task<bool> Update(Province Province);
        Task<bool> Delete(Province Province);
        Task<bool> BulkDelete(List<Province> Provinces);
        //Task<bool> BulkMerge(List<Province> Provinces);
    }
    public class ProvinceValidator : IProvinceValidator
    {
        private IUOW UOW;

        public ProvinceValidator(IUOW UOW)
        {
            this.UOW = UOW;
        }
        public enum ErrorCode
        {
            IdNotExisted,
            ProvinceInUsed,
            CodeEmpty,
            CodeHasSpecialCharacter,
            CodeOverLength,
            NameEmpty,
            CodeExisted,
            NameOverLength
        }

        public async Task<bool> ValidateId(Province Province)
        {
            ProvinceFilter ProvinceFilter = new ProvinceFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = Province.Id },
                Selects = ProvinceSelect.Id
            };

            int count = await UOW.ProvinceRepository.Count(ProvinceFilter);
            if (count == 0)
                Province.AddError(nameof(ProvinceValidator), nameof(Province.Id), ErrorCode.IdNotExisted);
            return Province.IsValidated;
        }

        private async Task<bool> ValidateCode(Province Province)
        {
            if (string.IsNullOrEmpty(Province.Code))
            {
                Province.AddError(nameof(ProvinceValidator), nameof(Province.Code), ErrorCode.CodeEmpty);
            }
            else
            {
                var Code = Province.Code;
                if (Province.Code.Contains(" ") || !Code.ChangeToEnglishChar().Equals(Province.Code))
                {
                    Province.AddError(nameof(ProvinceValidator), nameof(Province.Code), ErrorCode.CodeHasSpecialCharacter);
                }
                else if (Province.Code.Length > 50)
                {
                    Province.AddError(nameof(ProvinceValidator), nameof(Province.Code), ErrorCode.CodeOverLength);
                }
                else
                {
                    ProvinceFilter ProvinceFilter = new ProvinceFilter
                    {
                        Skip = 0,
                        Take = 10,
                        Id = new IdFilter { NotEqual = Province.Id },
                        Code = new StringFilter { Equal = Province.Code },
                        Selects = ProvinceSelect.Code
                    };

                    int count = await UOW.ProvinceRepository.Count(ProvinceFilter);
                    if (count != 0)
                        Province.AddError(nameof(ProvinceValidator), nameof(Province.Code), ErrorCode.CodeExisted);
                }
            }

            return Province.IsValidated;
        }

        private async Task<bool> ValidateName(Province Province)
        {
            if (string.IsNullOrEmpty(Province.Name))
            {
                Province.AddError(nameof(ProvinceValidator), nameof(Province.Name), ErrorCode.NameEmpty);
            }
            else if (Province.Name.Length > 255)
            {
                Province.AddError(nameof(ProvinceValidator), nameof(Province.Name), ErrorCode.NameOverLength);
            }
            return Province.IsValidated;
        }

        public async Task<bool> BulkDelete(List<Province> Provinces)
        {
            foreach (Province Province in Provinces)
            {
                await Delete(Province);
            }
            return Provinces.All(st => st.IsValidated);
        }

        public async Task<bool> Create(Province Province)
        {
            await ValidateCode(Province);
            await ValidateName(Province);
            return Province.IsValidated;
        }

        public async Task<bool> Delete(Province Province)
        {
            if (await ValidateId(Province))
            {
                if (Province.Used)
                    Province.AddError(nameof(ProvinceValidator), nameof(Province.Id), ErrorCode.ProvinceInUsed);
            }
            return Province.IsValidated;
        }

        public async Task<bool> Update(Province Province)
        {
            if (await ValidateId(Province))
            {
                await ValidateCode(Province);
                await ValidateName(Province);
            }
            return Province.IsValidated;
        }
    }
}
