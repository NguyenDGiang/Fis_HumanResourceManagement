using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRM.Entities;
using HRM.Enums;
using HRM.Repositories;
using TrueSight.Common;

namespace HRM.Services.MDepartment
{
    public interface IDepartmentValidator : IServiceScoped
    {
        Task<bool> Create(Department Department);
        Task<bool> Update(Department Department);
        Task<bool> Delete(Department Department);
        Task<bool> BulkDelete(List<Department> Departments);
    }
    public class DepartmentValidator : IDepartmentValidator
    {
        public enum ErrorCode
        {
            IdNotExisted,
            DepartmentInUsed,
            CodeHasSpecialCharacter,
            CodeEmpty,
            CodeExisted,
            NameEmpty,
            PercentageEmpty,
            NameOverLength
        }
        private IUOW UOW;
        public DepartmentValidator(IUOW UOW)
        {
            this.UOW = UOW;
        }
        public async Task<bool> ValidateId(Department Department)
        {
            DepartmentFilter DepartmentFilter = new DepartmentFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = Department.Id },
                Selects = DepartmentSelect.Id
            };

            int count = await UOW.DepartmentRepository.Count(DepartmentFilter);
            System.Console.WriteLine("count : " + count);
            if (count == 0)
                Department.AddError(nameof(DepartmentValidator), nameof(Department.Id), ErrorCode.IdNotExisted);
            return count == 1;
        }

        private async Task<bool> ValidateName(Department Department)
        {
            if (string.IsNullOrEmpty(Department.Name))
            {
                Department.AddError(nameof(DepartmentValidator), nameof(Department.Name), ErrorCode.NameEmpty);
                return false;
            }
            else if (Department.Name.Length > 255)
            {
                Department.AddError(nameof(DepartmentValidator), nameof(Department.Name), ErrorCode.NameOverLength);
                return false;
            }
            return true;
        }

        private async Task<bool> ValidateCode(Department Department)
        {
            if (string.IsNullOrEmpty(Department.Code))
            {
                Department.AddError(nameof(DepartmentValidator), nameof(Department.Code), ErrorCode.CodeEmpty);
                return false;
            }
            else
            {
                var Code = Department.Code;
                if (Department.Code.Contains(" ") || !Code.ChangeToEnglishChar().Equals(Department.Code))
                {
                    Department.AddError(nameof(DepartmentValidator), nameof(Department.Code), ErrorCode.CodeHasSpecialCharacter);
                }
                else
                {
                    DepartmentFilter DepartmentFilter = new DepartmentFilter
                    {
                        Skip = 0,
                        Take = 10,
                        Id = new IdFilter { NotEqual = Department.Id },
                        Code = new StringFilter { Equal = Department.Code },
                        Selects = DepartmentSelect.Code
                    };

                    int count = await UOW.DepartmentRepository.Count(DepartmentFilter);
                    if (count != 0)
                        Department.AddError(nameof(DepartmentValidator), nameof(Department.Code), ErrorCode.CodeExisted);
                }

            }

            return Department.IsValidated;
        }

        public async Task<bool> BulkDelete(List<Department> Departments)
        {
            foreach (Department Department in Departments)
            {
                await Delete(Department);
            }
            return Departments.All(st => st.IsValidated);
        }

        public async Task<bool> Create(Department Department)
        {
            await ValidateName(Department);
            await ValidateCode(Department);
            return Department.IsValidated;
        }

        public async Task<bool> Delete(Department Department)
        {
            if (await ValidateId(Department))
            {
                // await ValidateDepartmentInUsed(Department);
            }
            return Department.IsValidated;
        }

        public async Task<bool> Update(Department Department)
        {
            if (await ValidateId(Department))
            {
                await ValidateName(Department);
                await ValidateCode(Department);
            }
            return Department.IsValidated;
        }
    }
}