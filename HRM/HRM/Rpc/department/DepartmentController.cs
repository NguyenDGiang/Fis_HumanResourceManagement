using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRM.Entities;
using HRM.Services.MDepartment;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TrueSight.Common;

namespace HRM.Rpc.department
{
    public class DepartmentController : RpcController
    {
        private IDepartmentService DepartmentService;
        public DepartmentController(IDepartmentService DepartmentService)
        {
            this.DepartmentService = DepartmentService;
        }
        [Route(DepartmentRoute.Count), HttpPost]
        public async Task<ActionResult<int>> Count([FromBody] Department_DepartmentFilterDTO Department_DepartmentFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            DepartmentFilter DepartmentFilter = ConvertFilterDTOToFilterEntity(Department_DepartmentFilterDTO);
            DepartmentFilter = DepartmentService.ToFilter(DepartmentFilter);

            int result = await DepartmentService.Count(DepartmentFilter);

            return result;
        }

        [Route(DepartmentRoute.Create), HttpPost]
        public async Task<ActionResult<Department_DepartmentDTO>> Create([FromBody] Department_DepartmentDTO Department_DepartmentDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            Department Department = ConvertDTOToEntity(Department_DepartmentDTO);
            Department = await DepartmentService.Create(Department);
            Department_DepartmentDTO = new Department_DepartmentDTO(Department);

            if (Department.IsValidated)
                return Department_DepartmentDTO;
            else
                return BadRequest(Department_DepartmentDTO);

                // return Department_DepartmentDTO;
        }

        [Route(DepartmentRoute.Get), HttpPost]
        public async Task<ActionResult<Department_DepartmentDTO>> Get([FromBody] Department_DepartmentDTO Department_DepartmentDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            Department Department = ConvertDTOToEntity(Department_DepartmentDTO);
            Department = await DepartmentService.Get(Department.Id);

            return new Department_DepartmentDTO(Department);
        }

        [Route(DepartmentRoute.List), HttpPost]
        public async Task<ActionResult<List<Department_DepartmentDTO>>> List([FromBody] Department_DepartmentFilterDTO Department_DepartmentFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            DepartmentFilter DepartmentFilter = ConvertFilterDTOToFilterEntity(Department_DepartmentFilterDTO);
            DepartmentFilter = DepartmentService.ToFilter(DepartmentFilter);
            List<Department> Departments = await DepartmentService.List(DepartmentFilter);
            List<Department_DepartmentDTO> Department_DepartmentDTOs = Departments.Select(x => new Department_DepartmentDTO(x)).ToList();

            return Department_DepartmentDTOs;
        }

        [Route(DepartmentRoute.Update), HttpPost]
        public async Task<ActionResult<Department_DepartmentDTO>> Update([FromBody] Department_DepartmentDTO Department_DepartmentDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            Department Department = ConvertDTOToEntity(Department_DepartmentDTO);
            Department = await DepartmentService.Update(Department);

            return new Department_DepartmentDTO(Department);
        }

        [Route(DepartmentRoute.Delete), HttpPost]
        public async Task<ActionResult<Department_DepartmentDTO>> Delete([FromBody] Department_DepartmentDTO Department_DepartmentDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            Department Department = ConvertDTOToEntity(Department_DepartmentDTO);
            Department = await DepartmentService.Delete(Department);

            return new Department_DepartmentDTO(Department);
        }

        [Route(DepartmentRoute.BulkDelete), HttpPost]
        public async Task<ActionResult<bool>> BulkDelete([FromBody] List<long> Ids)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            DepartmentFilter DepartmentFilter = new DepartmentFilter();
            DepartmentFilter = DepartmentService.ToFilter(DepartmentFilter);
            DepartmentFilter.Id = new IdFilter { In = Ids };
            DepartmentFilter.Selects = DepartmentSelect.Id;
            DepartmentFilter.Skip = 0;
            DepartmentFilter.Take = int.MaxValue;

            List<Department> Departments = await DepartmentService.List(DepartmentFilter);
            await DepartmentService.BulkDelete(Departments);

            return true;
        }

        private DepartmentFilter ConvertFilterDTOToFilterEntity(Department_DepartmentFilterDTO Department_DepartmentFilterDTO)
        {
            DepartmentFilter DepartmentFilter = new DepartmentFilter();

            DepartmentFilter.Selects = DepartmentSelect.ALL;
            DepartmentFilter.Skip = Department_DepartmentFilterDTO.Skip;
            DepartmentFilter.Take = Department_DepartmentFilterDTO.Take;
            DepartmentFilter.OrderBy = Department_DepartmentFilterDTO.OrderBy;
            DepartmentFilter.OrderType = Department_DepartmentFilterDTO.OrderType;

            DepartmentFilter.Id = Department_DepartmentFilterDTO.Id;
            DepartmentFilter.Code = Department_DepartmentFilterDTO.Code;
            DepartmentFilter.Name = Department_DepartmentFilterDTO.Name;
            DepartmentFilter.StatusId = Department_DepartmentFilterDTO.StatusId;
            DepartmentFilter.PhoneNumber = Department_DepartmentFilterDTO.PhoneNumber;
            DepartmentFilter.VillageId = Department_DepartmentFilterDTO.VillageId;
            DepartmentFilter.DistrictId = Department_DepartmentFilterDTO.DistrictId;
            DepartmentFilter.ProvinceId = Department_DepartmentFilterDTO.ProvinceId;

            return DepartmentFilter;
        }

        private Department ConvertDTOToEntity(Department_DepartmentDTO Department_DepartmentDTO)
        {
            Department Department = new Department();
            Department.Id = Department_DepartmentDTO.Id;
            Department.Code = Department_DepartmentDTO.Code;
            Department.Name = Department_DepartmentDTO.Name;
            Department.PhoneNumber = Department_DepartmentDTO.PhoneNumber;
            Department.StatusId = Department_DepartmentDTO.StatusId;
            Department.ProvinceId = Department_DepartmentDTO.ProvinceId;
            Department.DistrictId = Department_DepartmentDTO.DistrictId;
            Department.VillageId = Department_DepartmentDTO.VillageId;
            Department.Status = Department_DepartmentDTO.Status == null ? null : new Status
            {
                Id = Department_DepartmentDTO.Status.Id,
                Code = Department_DepartmentDTO.Status.Code,
                Name = Department_DepartmentDTO.Status.Name,
            };
            Department.Village = Department_DepartmentDTO.Village == null ? null : new Village
            {
                Id = Department_DepartmentDTO.Village.Id,
                Name = Department_DepartmentDTO.Village.Name,
                Code = Department_DepartmentDTO.Village.Code,
                DistrictId = Department_DepartmentDTO.Village.DistrictId,
                StatusId = Department_DepartmentDTO.Village.StatusId,
            };
            Department.District = Department_DepartmentDTO.District == null ? null : new District
            {
                Id = Department_DepartmentDTO.District.Id,
                Name = Department_DepartmentDTO.District.Name,
                Code = Department_DepartmentDTO.District.Code,
                ProvinceId = Department_DepartmentDTO.District.ProvinceId,
                StatusId = Department_DepartmentDTO.District.StatusId,
            };
            Department.Province = Department_DepartmentDTO.Province == null ? null : new Province
            {
                Id = Department_DepartmentDTO.Province.Id,
                Name = Department_DepartmentDTO.Province.Name,
                Code = Department_DepartmentDTO.Province.Code,
                StatusId = Department_DepartmentDTO.Province.StatusId,
            };

            return Department;
        }
    }
}