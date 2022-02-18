using HRM.Entities;
using HRM.Services.MEmployee;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrueSight.Common;

namespace HRM.Rpc.employee
{
    public class EmployeeController : RpcController
    {
        private IEmployeeService EmployeeService;
        public EmployeeController(IEmployeeService EmployeeService)
        {
            this.EmployeeService = EmployeeService;
        }
        [Route(EmployeeRoute.Count), HttpPost]
        public async Task<ActionResult<int>> Count([FromBody] Employee_EmployeeFilterDTO Employee_EmployeeFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            EmployeeFilter EmployeeFilter = ConvertFilterDTOToFilterEntity(Employee_EmployeeFilterDTO);
            EmployeeFilter = EmployeeService.ToFilter(EmployeeFilter);
            int count = await EmployeeService.Count(EmployeeFilter);
            return count;
        }
        [Route(EmployeeRoute.Get), HttpPost]

        public async Task<ActionResult<Employee_EmployeeDTO>> Get([FromBody] Employee_EmployeeDTO Employee_EmployeeDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            Employee Employee = await EmployeeService.Get(Employee_EmployeeDTO.Id);
            return new Employee_EmployeeDTO(Employee);
        }

        [Route(EmployeeRoute.List), HttpPost]
        public async Task<ActionResult<List<Employee_EmployeeDTO>>> List([FromBody] Employee_EmployeeFilterDTO Employee_EmployeeFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            EmployeeFilter EmployeeFilter = ConvertFilterDTOToFilterEntity(Employee_EmployeeFilterDTO);
            EmployeeFilter = EmployeeService.ToFilter(EmployeeFilter);
            List<Employee> Employees = await EmployeeService.List(EmployeeFilter);
            List<Employee_EmployeeDTO> Employee_EmployeeDTOs = Employees
                .Select(c => new Employee_EmployeeDTO(c)).ToList();
            return Employee_EmployeeDTOs;
        }

        [Route(EmployeeRoute.Create), HttpPost]
        public async Task<ActionResult<Employee_EmployeeDTO>> Create([FromBody] Employee_EmployeeDTO Employee_EmployeeDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            Employee Employee = ConvertDTOToEntity(Employee_EmployeeDTO);
            Employee = await EmployeeService.Create(Employee);
            Employee_EmployeeDTO = new Employee_EmployeeDTO(Employee);
            if (Employee.IsValidated)
                return Employee_EmployeeDTO;
            else
                return BadRequest(Employee_EmployeeDTO); 
        }

        [Route(EmployeeRoute.Update), HttpPost]
        public async Task<ActionResult<Employee_EmployeeDTO>> Update([FromBody] Employee_EmployeeDTO Employee_EmployeeDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            Employee Employee = ConvertDTOToEntity(Employee_EmployeeDTO);
            Employee = await EmployeeService.Update(Employee);
            Employee_EmployeeDTO = new Employee_EmployeeDTO(Employee);

            return Employee_EmployeeDTO;
        }

        [Route(EmployeeRoute.Delete), HttpPost]

        public async Task<ActionResult<Employee_EmployeeDTO>> Delete([FromBody] Employee_EmployeeDTO Employee_EmployeeDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            Employee Employee = ConvertDTOToEntity(Employee_EmployeeDTO);
            Employee = await EmployeeService.Delete(Employee);
            Employee_EmployeeDTO = new Employee_EmployeeDTO(Employee);

            if (Employee.IsValidated)

                return Employee_EmployeeDTO;
            else
                return BadRequest(Employee_EmployeeDTO);

        }

        private Employee ConvertDTOToEntity(Employee_EmployeeDTO Employee_EmployeeDTO)
        {
            Employee Employee = new Employee();
            Employee.Id = Employee_EmployeeDTO.Id;
            Employee.FullName = Employee_EmployeeDTO.FullName;
            Employee.Email = Employee_EmployeeDTO.Email;
            Employee.ContractStatus = Employee_EmployeeDTO.ContractStatus;
            Employee.Birthday = Employee_EmployeeDTO.Birthday;
            Employee.BeginJobTime = Employee_EmployeeDTO.BeginJobTime;
            Employee.AcademicLevelId = Employee_EmployeeDTO.AcademicLevelId;
            Employee.ChucVuId = Employee_EmployeeDTO.ChucVuId;
            Employee.DistrictId = Employee_EmployeeDTO.DistrictId;
            Employee.ProvinceId = Employee_EmployeeDTO.ProvinceId;
            Employee.VillageId = Employee_EmployeeDTO.VillageId;
            Employee.PhoneNumber = Employee_EmployeeDTO.PhoneNumber;
            Employee.JobPositionId = Employee_EmployeeDTO.JobPositionId;

            Employee.Status = Employee_EmployeeDTO.Status == null ? null : new Status
            {
                Id = Employee_EmployeeDTO.Status.Id,
                Code = Employee_EmployeeDTO.Status.Code,
                Name = Employee_EmployeeDTO.Status.Name,
            };
            Employee.JobPosition = Employee_EmployeeDTO.JobPosition == null ? null : new JobPosition
            {
                Id = Employee_EmployeeDTO.JobPosition.Id,
                Code = Employee_EmployeeDTO.JobPosition.Code,
                Name = Employee_EmployeeDTO.JobPosition.Name,
                StatusId = Employee_EmployeeDTO.JobPosition.StatusId
            };
            Employee.ChucVu = Employee_EmployeeDTO.ChucVu == null ? null : new ChucVu
            {
                Id = Employee_EmployeeDTO.ChucVu.Id,
                Code = Employee_EmployeeDTO.ChucVu.Code,
                Name = Employee_EmployeeDTO.ChucVu.Name,
                StatusId = Employee_EmployeeDTO.ChucVu.StatusId
            };
            Employee.AcademicLevel = Employee_EmployeeDTO.AcademicLevel == null ? null : new AcademicLevel
            {
                Id = Employee_EmployeeDTO.AcademicLevel.Id,
                Code = Employee_EmployeeDTO.AcademicLevel.Code,
                Name = Employee_EmployeeDTO.AcademicLevel.Name,
                StatusId = Employee_EmployeeDTO.AcademicLevel.StatusId
            };

            Employee.District = Employee_EmployeeDTO.District == null ? null : new District
            {
                Id = Employee_EmployeeDTO.District.Id,
                Code = Employee_EmployeeDTO.District.Code,
                Name = Employee_EmployeeDTO.District .Name,
                StatusId = Employee_EmployeeDTO.District .StatusId
            };
            Employee.Province = Employee_EmployeeDTO.Province == null ? null : new Province
            {
                Id = Employee_EmployeeDTO.Province.Id,
                Code = Employee_EmployeeDTO.Province.Code,
                Name = Employee_EmployeeDTO.Province.Name,
                StatusId = Employee_EmployeeDTO.Province.StatusId
            };
            Employee.Village = Employee_EmployeeDTO.Province == null ? null : new Village
            {
                Id = Employee_EmployeeDTO.Village.Id,
                Code = Employee_EmployeeDTO.Village.Code,
                Name = Employee_EmployeeDTO.Village.Name,
                StatusId = Employee_EmployeeDTO.Village.StatusId
            };


            return Employee;
        }
        private EmployeeFilter ConvertFilterDTOToFilterEntity(Employee_EmployeeFilterDTO Employee_EmployeeFilterDTO)
        {
            EmployeeFilter EmployeeFilter = new EmployeeFilter();
            EmployeeFilter.Selects = EmployeeSelect.ALL;
            EmployeeFilter.Skip = Employee_EmployeeFilterDTO.Skip;
            EmployeeFilter.Take = Employee_EmployeeFilterDTO.Take;
            EmployeeFilter.OrderBy = Employee_EmployeeFilterDTO.OrderBy;
            EmployeeFilter.OrderType = Employee_EmployeeFilterDTO.OrderType;

            EmployeeFilter.Id = Employee_EmployeeFilterDTO.Id;
            EmployeeFilter.FullName = Employee_EmployeeFilterDTO.FullName;
            EmployeeFilter.Email = Employee_EmployeeFilterDTO.Email;    
            EmployeeFilter.ContractStatus = Employee_EmployeeFilterDTO.ContractStatus;
            EmployeeFilter.Birthday = Employee_EmployeeFilterDTO.Birthday;
            EmployeeFilter.BeginJobTime = Employee_EmployeeFilterDTO.BeginJobTime;
            EmployeeFilter.AcademicLevelId = Employee_EmployeeFilterDTO.AcademicLevelId;
            EmployeeFilter.ChucVuId = Employee_EmployeeFilterDTO.ChucVuId;
            EmployeeFilter.DistrictId = Employee_EmployeeFilterDTO.DistrictId;
            EmployeeFilter.ProvinceId = Employee_EmployeeFilterDTO.ProvinceId;
            EmployeeFilter.VillageId = Employee_EmployeeFilterDTO.VillageId;
            EmployeeFilter.PhoneNumber = Employee_EmployeeFilterDTO.PhoneNumber;    
            EmployeeFilter.JobPositionId = Employee_EmployeeFilterDTO.JobPositionId;

            EmployeeFilter.Search = Employee_EmployeeFilterDTO.Search;

            return EmployeeFilter;
        }
    }
}
