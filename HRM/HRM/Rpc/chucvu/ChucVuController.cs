using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRM.Entities;
using HRM.Services.MChucVu;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TrueSight.Common;

namespace HRM.Rpc.chucvu
{
    public class ChucVuController : RpcController
    {
        private IChucVuService ChucVuService;
        public ChucVuController(IChucVuService ChucVuService)
        {
            this.ChucVuService = ChucVuService;
        }

        [Route(ChucVuRoute.Count)]

        public async Task<ActionResult<int>> Count(ChucVu_ChucVuFilterDTO ChucVu_ChucVuFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ChucVuFilter ChucVuFilter = ConvertFilterDTOToFilterEntity(ChucVu_ChucVuFilterDTO);
            ChucVuFilter = ChucVuService.ToFilter(ChucVuFilter);
            int count = await ChucVuService.Count(ChucVuFilter);

            return count;
        }

        [Route(ChucVuRoute.Create)]

        public async Task<ActionResult<ChucVu_ChucVuDTO>> Create(ChucVu_ChucVuDTO ChucVu_ChucVuDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            System.Console.WriteLine(JsonConvert.SerializeObject(ChucVu_ChucVuDTO));

            ChucVu ChucVu = ConvertDTOToEntity(ChucVu_ChucVuDTO);
            ChucVu = await ChucVuService.Create(ChucVu);
            ChucVu_ChucVuDTO = new ChucVu_ChucVuDTO(ChucVu);
            if (ChucVu.IsValidated)
                return ChucVu_ChucVuDTO;
            else
                return BadRequest(ChucVu_ChucVuDTO);
        }

        [Route(ChucVuRoute.Get)]

        public async Task<ActionResult<ChucVu_ChucVuDTO>> Get(ChucVu_ChucVuDTO ChucVu_ChucVuDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ChucVu ChucVu = await ChucVuService.Get(ChucVu_ChucVuDTO.Id);
            return new ChucVu_ChucVuDTO(ChucVu);
        }

        [Route(ChucVuRoute.List)]
        public async Task<ActionResult<List<ChucVu_ChucVuDTO>>> List(ChucVu_ChucVuFilterDTO ChucVu_ChucVuFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ChucVuFilter ChucVuFilter = ConvertFilterDTOToFilterEntity(ChucVu_ChucVuFilterDTO);
            ChucVuFilter = ChucVuService.ToFilter(ChucVuFilter);
            List<ChucVu> ChucVus = await ChucVuService.List(ChucVuFilter);
            List<ChucVu_ChucVuDTO> ChucVu_ChucVuDTOs = ChucVus.Select(x => new ChucVu_ChucVuDTO(x)).ToList();

            return ChucVu_ChucVuDTOs;
        }

        [Route(ChucVuRoute.Update)]
        public async Task<ActionResult<ChucVu_ChucVuDTO>> Update(ChucVu_ChucVuDTO ChucVu_ChucVuDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ChucVu ChucVu = ConvertDTOToEntity(ChucVu_ChucVuDTO);
            ChucVu = await ChucVuService.Update(ChucVu);
            ChucVu_ChucVuDTO = new ChucVu_ChucVuDTO(ChucVu);

            return ChucVu_ChucVuDTO;
        }

        [Route(ChucVuRoute.Delete)]

        public async Task<ActionResult<ChucVu_ChucVuDTO>> Delete(ChucVu_ChucVuDTO ChucVu_ChucVuDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ChucVu ChucVu = ConvertDTOToEntity(ChucVu_ChucVuDTO);
            ChucVu = await ChucVuService.Delete(ChucVu);
            ChucVu_ChucVuDTO = new ChucVu_ChucVuDTO(ChucVu);

            if (ChucVu.IsValidated)

                return ChucVu_ChucVuDTO;
            else
                return BadRequest(ChucVu_ChucVuDTO);

        }

        [Route(ChucVuRoute.BulkDelete)]
        public async Task<ActionResult<bool>> BulkDelete(List<long> Ids)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            ChucVuFilter ChucVuFilter = new ChucVuFilter();
            ChucVuFilter = ChucVuService.ToFilter(ChucVuFilter);
            ChucVuFilter.Id = new IdFilter { In = Ids };
            ChucVuFilter.Selects = ChucVuSelect.Id;
            ChucVuFilter.Skip = 0;
            ChucVuFilter.Take = int.MaxValue;

            List<ChucVu> ChucVus = await ChucVuService.List(ChucVuFilter);
            ChucVus = await ChucVuService.BulkDelete(ChucVus);

            return true;
        }

        private ChucVuFilter ConvertFilterDTOToFilterEntity(ChucVu_ChucVuFilterDTO ChucVu_ChucVuFilterDTO)
        {
            ChucVuFilter ChucVuFilter = new ChucVuFilter();

            ChucVuFilter.Selects = ChucVuSelect.ALL;
            ChucVuFilter.Skip = ChucVu_ChucVuFilterDTO.Skip;
            ChucVuFilter.Take = ChucVu_ChucVuFilterDTO.Take;
            ChucVuFilter.OrderBy = ChucVu_ChucVuFilterDTO.OrderBy;
            ChucVuFilter.OrderType = ChucVu_ChucVuFilterDTO.OrderType;

            ChucVuFilter.Id = ChucVu_ChucVuFilterDTO.Id;
            ChucVuFilter.Code = ChucVu_ChucVuFilterDTO.Code;
            ChucVuFilter.Name = ChucVu_ChucVuFilterDTO.Name;
            ChucVuFilter.StatusId = ChucVu_ChucVuFilterDTO.StatusId;
            return ChucVuFilter;

        }

        private ChucVu ConvertDTOToEntity(ChucVu_ChucVuDTO ChucVu_ChucVuDTO)
        {
            ChucVu ChucVu = new ChucVu();
            ChucVu.Id = ChucVu_ChucVuDTO.Id;
            ChucVu.Code = ChucVu_ChucVuDTO.Code;
            ChucVu.Name = ChucVu_ChucVuDTO.Name;
            ChucVu.StatusId = ChucVu_ChucVuDTO.StatusId;
            ChucVu.Status = ChucVu_ChucVuDTO.Status == null ? null : new Status
            {
                Id = ChucVu_ChucVuDTO.Status.Id,
                Code = ChucVu_ChucVuDTO.Status.Code,
                Name = ChucVu_ChucVuDTO.Status.Name,
            };

            return ChucVu;
        }




    }
}
