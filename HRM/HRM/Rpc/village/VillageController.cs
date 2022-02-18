using TrueSight.Common;
using HRM.Entities;
using HRM.Enums;
using HRM.Services.MDistrict;
using HRM.Services.MStatus;
using HRM.Services.MVillage;
using HRM.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRM.Rpc.village
{
    public class VillageController : RpcController
    {
        private IDistrictService DistrictService;
        private IStatusService StatusService;
        private IVillageService VillageService;
        public VillageController(
            IDistrictService DistrictService,
            IStatusService StatusService,
            IVillageService VillageService
        )
        {
            this.DistrictService = DistrictService;
            this.StatusService = StatusService;
            this.VillageService = VillageService;
        }

        [Route(VillageRoute.Count), HttpPost]
        public async Task<ActionResult<int>> Count([FromBody] Village_VillageFilterDTO Village_VillageFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            VillageFilter VillageFilter = ConvertFilterDTOToFilterEntity(Village_VillageFilterDTO);
            VillageFilter = VillageService.ToFilter(VillageFilter);
            int count = await VillageService.Count(VillageFilter);
            return count;
        }

        [Route(VillageRoute.List), HttpPost]
        public async Task<ActionResult<List<Village_VillageDTO>>> List([FromBody] Village_VillageFilterDTO Village_VillageFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            VillageFilter VillageFilter = ConvertFilterDTOToFilterEntity(Village_VillageFilterDTO);
            VillageFilter = VillageService.ToFilter(VillageFilter);
            List<Village> Villages = await VillageService.List(VillageFilter);
            List<Village_VillageDTO> Village_VillageDTOs = Villages
                .Select(c => new Village_VillageDTO(c)).ToList();
            return Village_VillageDTOs;
        }

        [Route(VillageRoute.Get), HttpPost]
        public async Task<ActionResult<Village_VillageDTO>> Get([FromBody] Village_VillageDTO Village_VillageDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(Village_VillageDTO.Id))
                return Forbid();

            Village Village = await VillageService.Get(Village_VillageDTO.Id);
            return new Village_VillageDTO(Village);
        }

        [Route(VillageRoute.Create), HttpPost]
        public async Task<ActionResult<Village_VillageDTO>> Create([FromBody] Village_VillageDTO Village_VillageDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(Village_VillageDTO.Id))
                return Forbid();

            Village Village = ConvertDTOToEntity(Village_VillageDTO);
            Village = await VillageService.Create(Village);
            Village_VillageDTO = new Village_VillageDTO(Village);
            if (Village.IsValidated)
                return Village_VillageDTO;
            else
                return BadRequest(Village_VillageDTO);
        }

        [Route(VillageRoute.Update), HttpPost]
        public async Task<ActionResult<Village_VillageDTO>> Update([FromBody] Village_VillageDTO Village_VillageDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(Village_VillageDTO.Id))
                return Forbid();

            Village Village = ConvertDTOToEntity(Village_VillageDTO);
            Village = await VillageService.Update(Village);
            Village_VillageDTO = new Village_VillageDTO(Village);
            if (Village.IsValidated)
                return Village_VillageDTO;
            else
                return BadRequest(Village_VillageDTO);
        }

        [Route(VillageRoute.Delete), HttpPost]
        public async Task<ActionResult<Village_VillageDTO>> Delete([FromBody] Village_VillageDTO Village_VillageDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(Village_VillageDTO.Id))
                return Forbid();

            Village Village = ConvertDTOToEntity(Village_VillageDTO);
            Village = await VillageService.Delete(Village);
            Village_VillageDTO = new Village_VillageDTO(Village);
            if (Village.IsValidated)
                return Village_VillageDTO;
            else
                return BadRequest(Village_VillageDTO);
        }

        [Route(VillageRoute.BulkDelete), HttpPost]
        public async Task<ActionResult<bool>> BulkDelete([FromBody] List<long> Ids)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            VillageFilter VillageFilter = new VillageFilter();
            VillageFilter.Id = new IdFilter { In = Ids };
            VillageFilter.Skip = 0;
            VillageFilter.Take = int.MaxValue;

            List<Village> Villages = await VillageService.List(VillageFilter);
            Villages = await VillageService.BulkDelete(Villages);
            if (Villages.Any(x => !x.IsValidated))
                return BadRequest(Villages.Where(x => !x.IsValidated));
            return true;
        }

        private async Task<bool> HasPermission(long Id)
        {
            VillageFilter VillageFilter = new VillageFilter();
            VillageFilter = VillageService.ToFilter(VillageFilter);
            if (Id == 0)
            {

            }
            else
            {
                VillageFilter.Id = new IdFilter { Equal = Id };
                int count = await VillageService.Count(VillageFilter);
                if (count == 0)
                    return false;
            }
            return true;
        }

        private Village ConvertDTOToEntity(Village_VillageDTO Village_VillageDTO)
        {
            Village Village = new Village();
            Village.Id = Village_VillageDTO.Id;
            Village.Code = Village_VillageDTO.Code;
            Village.Name = Village_VillageDTO.Name;
            Village.DistrictId = Village_VillageDTO.DistrictId;
            Village.StatusId = Village_VillageDTO.StatusId;
            Village.District = Village_VillageDTO.District == null ? null : new District
            {
                Id = Village_VillageDTO.District.Id,
                Name = Village_VillageDTO.District.Name,
                ProvinceId = Village_VillageDTO.District.ProvinceId,
                StatusId = Village_VillageDTO.District.StatusId,
            };
            Village.Status = Village_VillageDTO.Status == null ? null : new Status
            {
                Id = Village_VillageDTO.Status.Id,
                Code = Village_VillageDTO.Status.Code,
                Name = Village_VillageDTO.Status.Name,
            };
            return Village;
        }

        private VillageFilter ConvertFilterDTOToFilterEntity(Village_VillageFilterDTO Village_VillageFilterDTO)
        {
            VillageFilter VillageFilter = new VillageFilter();
            VillageFilter.Selects = VillageSelect.ALL;
            VillageFilter.Skip = Village_VillageFilterDTO.Skip;
            VillageFilter.Take = Village_VillageFilterDTO.Take;
            VillageFilter.OrderBy = Village_VillageFilterDTO.OrderBy;
            VillageFilter.OrderType = Village_VillageFilterDTO.OrderType;

            VillageFilter.Id = Village_VillageFilterDTO.Id;
            VillageFilter.Code = Village_VillageFilterDTO.Code;
            VillageFilter.Name = Village_VillageFilterDTO.Name;
            VillageFilter.DistrictId = Village_VillageFilterDTO.DistrictId;
            VillageFilter.StatusId = Village_VillageFilterDTO.StatusId;
            return VillageFilter;
        }

        [Route(VillageRoute.FilterListDistrict), HttpPost]
        public async Task<List<Village_DistrictDTO>> FilterListDistrict([FromBody] Village_DistrictFilterDTO Village_DistrictFilterDTO)
        {
            DistrictFilter DistrictFilter = new DistrictFilter();
            DistrictFilter.Skip = 0;
            DistrictFilter.Take = 20;
            DistrictFilter.OrderBy = DistrictOrder.Id;
            DistrictFilter.OrderType = OrderType.ASC;
            DistrictFilter.Selects = DistrictSelect.ALL;
            DistrictFilter.Id = Village_DistrictFilterDTO.Id;
            DistrictFilter.Name = Village_DistrictFilterDTO.Name;
            DistrictFilter.ProvinceId = Village_DistrictFilterDTO.ProvinceId;
            DistrictFilter.StatusId = null;

            List<District> Districts = await DistrictService.List(DistrictFilter);
            List<Village_DistrictDTO> Village_DistrictDTOs = Districts
                .Select(x => new Village_DistrictDTO(x)).ToList();
            return Village_DistrictDTOs;
        }

        [Route(VillageRoute.FilterListStatus), HttpPost]
        public async Task<List<Village_StatusDTO>> FilterListStatus([FromBody] Village_StatusFilterDTO Village_StatusFilterDTO)
        {
            StatusFilter StatusFilter = new StatusFilter();
            StatusFilter.Skip = 0;
            StatusFilter.Take = 20;
            StatusFilter.OrderBy = StatusOrder.Id;
            StatusFilter.OrderType = OrderType.ASC;
            StatusFilter.Selects = StatusSelect.ALL;

            List<Status> Statuses = await StatusService.List(StatusFilter);
            List<Village_StatusDTO> Village_StatusDTOs = Statuses
                .Select(x => new Village_StatusDTO(x)).ToList();
            return Village_StatusDTOs;
        }

        [Route(VillageRoute.SingleListDistrict), HttpPost]
        public async Task<List<Village_DistrictDTO>> SingleListDistrict([FromBody] Village_DistrictFilterDTO Village_DistrictFilterDTO)
        {
            DistrictFilter DistrictFilter = new DistrictFilter();
            DistrictFilter.Skip = 0;
            DistrictFilter.Take = 20;
            DistrictFilter.OrderBy = DistrictOrder.Id;
            DistrictFilter.OrderType = OrderType.ASC;
            DistrictFilter.Selects = DistrictSelect.ALL;
            DistrictFilter.Id = Village_DistrictFilterDTO.Id;
            DistrictFilter.Name = Village_DistrictFilterDTO.Name;
            DistrictFilter.ProvinceId = Village_DistrictFilterDTO.ProvinceId;
            DistrictFilter.StatusId = new IdFilter { Equal = StatusEnum.ACTIVE.Id };

            List<District> Districts = await DistrictService.List(DistrictFilter);
            List<Village_DistrictDTO> Village_DistrictDTOs = Districts
                .Select(x => new Village_DistrictDTO(x)).ToList();
            return Village_DistrictDTOs;
        }
        [Route(VillageRoute.SingleListStatus), HttpPost]
        public async Task<List<Village_StatusDTO>> SingleListStatus([FromBody] Village_StatusFilterDTO Village_StatusFilterDTO)
        {
            StatusFilter StatusFilter = new StatusFilter();
            StatusFilter.Skip = 0;
            StatusFilter.Take = 20;
            StatusFilter.OrderBy = StatusOrder.Id;
            StatusFilter.OrderType = OrderType.ASC;
            StatusFilter.Selects = StatusSelect.ALL;

            List<Status> Statuses = await StatusService.List(StatusFilter);
            List<Village_StatusDTO> Village_StatusDTOs = Statuses
                .Select(x => new Village_StatusDTO(x)).ToList();
            return Village_StatusDTOs;
        }
    }

}
