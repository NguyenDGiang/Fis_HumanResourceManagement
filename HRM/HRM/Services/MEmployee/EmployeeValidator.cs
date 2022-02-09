using HRM.Entities;
using HRM.Repositories;
using System.Threading.Tasks;
using TrueSight.Common;

namespace HRM.Services.MEmployee
{
    public interface IEmployeeValidator:IServiceScoped
    {
        Task<bool> Create(Employee Employee);
        Task<bool> Update(Employee Employee);
        Task<bool> Delete(Employee Employee);
    }
    public class EmployeeValidator : IEmployeeValidator
    {
        private IUOW UOW;
        public EmployeeValidator(IUOW UOW)
        {
            this.UOW = UOW;
        }
        private async Task<bool> ValidateId(Employee Employee)
        {
            EmployeeFilter EmployeeFilter = new EmployeeFilter
            {
                Skip = 0,
                Take = 10,
                Id = new IdFilter { Equal = Employee.Id },
                Selects = EmployeeSelect.Id
            };

            int count = await UOW.EmployeeRepository.Count(EmployeeFilter);
            if (count == 0)
                Employee.AddError(nameof(EmployeeValidator), nameof(Employee.Id), EmployeeMessage.Error.IdNotExisted);
            return count == 1;
        }
        private async Task<bool> ValidateName(Employee Employee)
        {
            var oldData = await UOW.EmployeeRepository.Get(Employee.Id);
            if (oldData != null && oldData.Used)
            {
                if (oldData.FullName != Employee.FullName)
                    Employee.AddError(nameof(EmployeeValidator), nameof(Employee.Id), EmployeeMessage.Error.EmployeeInUsed);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(Employee.FullName))
                {
                    Employee.AddError(nameof(EmployeeValidator), nameof(Employee.FullName), EmployeeMessage.Error.NameEmpty);
                }
                else if (Employee.FullName.Length > 3000)
                {
                    Employee.AddError(nameof(EmployeeValidator), nameof(Employee.FullName), EmployeeMessage.Error.NameOverLength);
                }
            }

            return Employee.IsValidated;
        }
        private async Task<bool> ValidateChucVu(Employee Employee)
        {
            if (Employee.ChucVuId.HasValue)
            {
                ChucVuFilter ChucVuFilter = new ChucVuFilter
                {
                    Skip = 0,
                    Take = 10,
                    Id = new IdFilter { Equal = Employee.ChucVuId },
                    Selects = ChucVuSelect.Id
                };

                int count = await UOW.ChucVuRepository.Count(ChucVuFilter);
                if (count == 0)
                    Employee.AddError(nameof(EmployeeValidator), nameof(Employee.ChucVu),EmployeeMessage.Error.EmployeeNotExisted);
            }

            return Employee.IsValidated;
        }

        private async Task<bool> ValidateAcademicLevel(Employee Employee)
        {
            if (Employee.AcademicLevelId.HasValue)
            {
                AcademicLevelFilter AcademicLevelFilter = new AcademicLevelFilter
                {
                    Skip = 0,
                    Take = 10,
                    Id = new IdFilter { Equal = Employee.AcademicLevelId },
                    Selects = AcademicLevelSelect.Id
                };

                int count = await UOW.AcademicLevelRepository.Count(AcademicLevelFilter);
                if (count == 0)
                    Employee.AddError(nameof(EmployeeValidator), nameof(Employee.AcademicLevel), EmployeeMessage.Error.EmployeeNotExisted);
            }

            return Employee.IsValidated;
        }
        private async Task<bool> ValidateJobPosition(Employee Employee)
        {
            if (Employee.JobPositionId.HasValue)
            {
                JobPositionFilter JobPositionFilter = new JobPositionFilter
                {
                    Skip = 0,
                    Take = 10,
                    Id = new IdFilter { Equal = Employee.JobPositionId },
                    Selects = JobPositionSelect.Id
                };

                int count = await UOW.JobPositionRepository.Count(JobPositionFilter);
                if (count == 0)
                    Employee.AddError(nameof(EmployeeValidator), nameof(Employee.AcademicLevel), EmployeeMessage.Error.EmployeeNotExisted);
            }

            return Employee.IsValidated;
        }

        private async Task<bool> ValidateVillage(Employee Employee)
        {
            if (Employee.VillageId.HasValue)
            {
                VillageFilter VillageFilter = new VillageFilter
                {
                    Skip = 0,
                    Take = 10,
                    Id = new IdFilter { Equal = Employee.VillageId },
                    Selects = VillageSelect.Id
                };

                int count = await UOW.VillageRepository.Count(VillageFilter);
                if (count == 0)
                    Employee.AddError(nameof(EmployeeValidator), nameof(Employee.Village), EmployeeMessage.Error.EmployeeNotExisted);
            }

            return Employee.IsValidated;
        }

        private async Task<bool> ValidateDistrict(Employee Employee)
        {
            if (Employee.DistrictId.HasValue)
            {
                DistrictFilter DistrictFilter = new DistrictFilter
                {
                    Skip = 0,
                    Take = 10,
                    Id = new IdFilter { Equal = Employee.DistrictId },
                    Selects = DistrictSelect.Id
                };

                int count = await UOW.DistrictRepository.Count(DistrictFilter);
                if (count == 0)
                    Employee.AddError(nameof(EmployeeValidator), nameof(Employee.Village), EmployeeMessage.Error.EmployeeNotExisted);
            }

            return Employee.IsValidated;
        }

        private async Task<bool> ValidateProvince(Employee Employee)
        {
            if (Employee.ProvinceId.HasValue)
            {
                ProvinceFilter ProvinceFilter = new ProvinceFilter
                {
                    Skip = 0,
                    Take = 10,
                    Id = new IdFilter { Equal = Employee.ProvinceId },
                    Selects = ProvinceSelect.Id
                };

                int count = await UOW.ProvinceRepository.Count(ProvinceFilter);
                if (count == 0)
                    Employee.AddError(nameof(EmployeeValidator), nameof(Employee.Province), EmployeeMessage.Error.EmployeeNotExisted);
            }

            return Employee.IsValidated;
        }

        public async Task<bool> Create(Employee Employee)
        {
            await ValidateName(Employee);
            await ValidateDistrict(Employee);
            await ValidateAcademicLevel(Employee);
            await ValidateChucVu(Employee);
            await ValidateJobPosition(Employee);
            await ValidateProvince(Employee);
            await ValidateVillage(Employee);
            return Employee.IsValidated;
        }

        public async Task<bool> Delete(Employee Employee)
        {
            if (await ValidateId(Employee))
            {
                if (Employee.Used)
                    Employee.AddError(nameof(EmployeeValidator), nameof(District.Id), EmployeeMessage.Error.EmployeeInUsed);
            }
            return Employee.IsValidated;
        }

        public async Task<bool> Update(Employee Employee)
        {
            if (await ValidateId(Employee))
            {
                await ValidateName(Employee);
                await ValidateDistrict(Employee);
                await ValidateAcademicLevel(Employee);
                await ValidateChucVu(Employee);
                await ValidateJobPosition(Employee);
                await ValidateProvince(Employee);
                await ValidateVillage(Employee);
            }
            return Employee.IsValidated;
        }
    }
}
