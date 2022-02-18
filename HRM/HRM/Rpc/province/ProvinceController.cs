using TrueSight.Common;
using HRM.Entities;
using HRM.Services.MProvince;
using HRM.Services.MStatus;
using HRM.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System;

namespace HRM.Rpc.province
{
    public class ProvinceController : RpcController
    {
        private IStatusService StatusService;
        private IProvinceService ProvinceService;
        public ProvinceController(
            IStatusService StatusService,
            IProvinceService ProvinceService
        )
        {
            this.StatusService = StatusService;
            this.ProvinceService = ProvinceService;
        }

        [Route(ProvinceRoute.Count), HttpPost]
        public async Task<ActionResult<int>> Count([FromBody] Province_ProvinceFilterDTO Province_ProvinceFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ProvinceFilter ProvinceFilter = ConvertFilterDTOToFilterEntity(Province_ProvinceFilterDTO);
            ProvinceFilter = ProvinceService.ToFilter(ProvinceFilter);
            int count = await ProvinceService.Count(ProvinceFilter);
            return count;
        }

        [Route(ProvinceRoute.List), HttpPost]
        public async Task<ActionResult<List<Province_ProvinceDTO>>> List([FromBody] Province_ProvinceFilterDTO Province_ProvinceFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ProvinceFilter ProvinceFilter = ConvertFilterDTOToFilterEntity(Province_ProvinceFilterDTO);
            ProvinceFilter = ProvinceService.ToFilter(ProvinceFilter);
            List<Province> Provinces = await ProvinceService.List(ProvinceFilter);
            List<Province_ProvinceDTO> Province_ProvinceDTOs = Provinces
                .Select(c => new Province_ProvinceDTO(c)).ToList();
            return Province_ProvinceDTOs;
        }

        [Route(ProvinceRoute.Get), HttpPost]
        public async Task<ActionResult<Province_ProvinceDTO>> Get([FromBody] Province_ProvinceDTO Province_ProvinceDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(Province_ProvinceDTO.Id))
                return Forbid();

            Province Province = await ProvinceService.Get(Province_ProvinceDTO.Id);
            return new Province_ProvinceDTO(Province);
        }

        [Route(ProvinceRoute.Create), HttpPost]
        public async Task<ActionResult<Province_ProvinceDTO>> Create([FromBody] Province_ProvinceDTO Province_ProvinceDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(Province_ProvinceDTO.Id))
                return Forbid();

            Province Province = ConvertDTOToEntity(Province_ProvinceDTO);
            Province = await ProvinceService.Create(Province);
            Province_ProvinceDTO = new Province_ProvinceDTO(Province);
            if (Province.IsValidated)
                return Province_ProvinceDTO;
            else
                return BadRequest(Province_ProvinceDTO);
        }

        [Route(ProvinceRoute.Update), HttpPost]
        public async Task<ActionResult<Province_ProvinceDTO>> Update([FromBody] Province_ProvinceDTO Province_ProvinceDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(Province_ProvinceDTO.Id))
                return Forbid();

            Province Province = ConvertDTOToEntity(Province_ProvinceDTO);
            Province = await ProvinceService.Update(Province);
            Province_ProvinceDTO = new Province_ProvinceDTO(Province);
            if (Province.IsValidated)
                return Province_ProvinceDTO;
            else
                return BadRequest(Province_ProvinceDTO);
        }

        [Route(ProvinceRoute.Delete), HttpPost]
        public async Task<ActionResult<Province_ProvinceDTO>> Delete([FromBody] Province_ProvinceDTO Province_ProvinceDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            if (!await HasPermission(Province_ProvinceDTO.Id))
                return Forbid();

            Province Province = ConvertDTOToEntity(Province_ProvinceDTO);
            Province = await ProvinceService.Delete(Province);
            Province_ProvinceDTO = new Province_ProvinceDTO(Province);
            if (Province.IsValidated)
                return Province_ProvinceDTO;
            else
                return BadRequest(Province_ProvinceDTO);
        }


        [Route(ProvinceRoute.Export), HttpPost]
        public async Task<ActionResult> Export([FromBody] Province_ProvinceFilterDTO Province_ProvinceFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ProvinceFilter ProvinceFilter = ConvertFilterDTOToFilterEntity(Province_ProvinceFilterDTO);
            ProvinceFilter.Skip = 0;
            ProvinceFilter.Take = int.MaxValue;
            ProvinceFilter = ProvinceService.ToFilter(ProvinceFilter);

            List<Province> Provinces = await ProvinceService.List(ProvinceFilter);
            MemoryStream memoryStream = new MemoryStream();
            using (ExcelPackage excel = new ExcelPackage(memoryStream))
            {
                var ProvinceHeaders = new List<string>()
                {
                    "Tỉnh Thành Phố",
                    "Mã TP",
                    "Quận Huyện",
                    "Mã QH",
                    "Phường Xã" ,
                    "Mã PX"
                };
                List<object[]> data = new List<object[]>();
                for (int i = 0; i < Provinces.Count; i++)
                {
                    var Province = Provinces[i];
                    foreach (var district in Province.Districts)
                    {
                        foreach (var Village in district.Villages)
                        {
                            data.Add(new Object[]
                            {
                                 Province.Name,
                                 Province.Code,
                                 district.Name,
                                 district.Code,
                                 Village.Name,
                                 Village.Code
                            });
                        }
                    }
                }
                excel.GenerateWorksheet("Province", ProvinceHeaders, data);
                excel.Save();
            }

            return File(memoryStream.ToArray(), "application/octet-stream", "Province.xlsx");

        }

        [Route(ProvinceRoute.BulkDelete), HttpPost]
        public async Task<ActionResult<bool>> BulkDelete([FromBody] List<long> Ids)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ProvinceFilter ProvinceFilter = new ProvinceFilter();
            ProvinceFilter = ProvinceService.ToFilter(ProvinceFilter);
            ProvinceFilter.Id = new IdFilter { In = Ids };
            ProvinceFilter.Skip = 0;
            ProvinceFilter.Take = int.MaxValue;

            List<Province> Provinces = await ProvinceService.List(ProvinceFilter);
            Provinces = await ProvinceService.BulkDelete(Provinces);
            return true;
        }

        private async Task<bool> HasPermission(long Id)
        {
            ProvinceFilter ProvinceFilter = new ProvinceFilter();
            ProvinceFilter = ProvinceService.ToFilter(ProvinceFilter);
            if (Id == 0)
            {

            }
            else
            {
                ProvinceFilter.Id = new IdFilter { Equal = Id };
                int count = await ProvinceService.Count(ProvinceFilter);
                if (count == 0)
                    return false;
            }
            return true;
        }

        private Province ConvertDTOToEntity(Province_ProvinceDTO Province_ProvinceDTO)
        {
            Province Province = new Province();
            Province.Id = Province_ProvinceDTO.Id;
            Province.Code = Province_ProvinceDTO.Code;
            Province.Name = Province_ProvinceDTO.Name;
            Province.StatusId = Province_ProvinceDTO.StatusId;
            Province.Status = Province_ProvinceDTO.Status == null ? null : new Status
            {
                Id = Province_ProvinceDTO.Status.Id,
                Code = Province_ProvinceDTO.Status.Code,
                Name = Province_ProvinceDTO.Status.Name,
            };
            return Province;
        }

        private ProvinceFilter ConvertFilterDTOToFilterEntity(Province_ProvinceFilterDTO Province_ProvinceFilterDTO)
        {
            ProvinceFilter ProvinceFilter = new ProvinceFilter();
            ProvinceFilter.Selects = ProvinceSelect.ALL;
            ProvinceFilter.Skip = Province_ProvinceFilterDTO.Skip;
            ProvinceFilter.Take = Province_ProvinceFilterDTO.Take;
            ProvinceFilter.OrderBy = Province_ProvinceFilterDTO.OrderBy;
            ProvinceFilter.OrderType = Province_ProvinceFilterDTO.OrderType;

            ProvinceFilter.Id = Province_ProvinceFilterDTO.Id;
            ProvinceFilter.Code = Province_ProvinceFilterDTO.Code;
            ProvinceFilter.Name = Province_ProvinceFilterDTO.Name;
            ProvinceFilter.StatusId = Province_ProvinceFilterDTO.StatusId;
            return ProvinceFilter;
        }


        [Route(ProvinceRoute.FilterListStatus), HttpPost]
        public async Task<List<Province_StatusDTO>> FilterListStatus([FromBody] Province_StatusFilterDTO Province_StatusFilterDTO)
        {
            StatusFilter StatusFilter = new StatusFilter();
            StatusFilter.Skip = 0;
            StatusFilter.Take = 20;
            StatusFilter.OrderBy = StatusOrder.Id;
            StatusFilter.OrderType = OrderType.ASC;
            StatusFilter.Selects = StatusSelect.ALL;

            List<Status> Statuses = await StatusService.List(StatusFilter);
            List<Province_StatusDTO> Province_StatusDTOs = Statuses
                .Select(x => new Province_StatusDTO(x)).ToList();
            return Province_StatusDTOs;
        }

        [Route(ProvinceRoute.SingleListStatus), HttpPost]
        public async Task<List<Province_StatusDTO>> SingleListStatus([FromBody] Province_StatusFilterDTO Province_StatusFilterDTO)
        {
            StatusFilter StatusFilter = new StatusFilter();
            StatusFilter.Skip = 0;
            StatusFilter.Take = 20;
            StatusFilter.OrderBy = StatusOrder.Id;
            StatusFilter.OrderType = OrderType.ASC;
            StatusFilter.Selects = StatusSelect.ALL;

            List<Status> Statuses = await StatusService.List(StatusFilter);
            List<Province_StatusDTO> Province_StatusDTOs = Statuses
                .Select(x => new Province_StatusDTO(x)).ToList();
            return Province_StatusDTOs;
        }

    }
}
