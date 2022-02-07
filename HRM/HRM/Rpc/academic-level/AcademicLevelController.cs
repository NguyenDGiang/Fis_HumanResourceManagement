using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRM.Entities;
using HRM.Services.MAcademicLevel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TrueSight.Common;

namespace HRM.Rpc.academic_level
{
    public class AcademicLevelController : RpcController
    {
        private IAcademicLevelService AcademicLevelService;
        public AcademicLevelController(IAcademicLevelService AcademicLevelService)
        {
            this.AcademicLevelService = AcademicLevelService;
        }

        [Route(AcademicLevelRoute.Count)]
        public async Task<ActionResult<int>> Count(AcademicLevel_AcademicLevelFilterDTO AcademicLevel_AcademicLevelFilterDTO)
        {
            System.Console.WriteLine(JsonConvert.SerializeObject(AcademicLevel_AcademicLevelFilterDTO));
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            AcademicLevelFilter AcademicLevelFilter = ConvertFilterDTOToFilterEntity(AcademicLevel_AcademicLevelFilterDTO);

            AcademicLevelFilter = AcademicLevelService.ToFilter(AcademicLevelFilter);

            int result = await AcademicLevelService.Count(AcademicLevelFilter);

            return result;
        }

        [Route(AcademicLevelRoute.Create)]
        public async Task<ActionResult<AcademicLevel_AcademicLevelDTO>> Create(AcademicLevel_AcademicLevelDTO AcademicLevel_AcademicLevelDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            AcademicLevel AcademicLevel = ConvertDTOToEntity(AcademicLevel_AcademicLevelDTO);
            AcademicLevel = await AcademicLevelService.Create(AcademicLevel);
            AcademicLevel_AcademicLevelDTO = new AcademicLevel_AcademicLevelDTO(AcademicLevel);

            if (AcademicLevel.IsValidated)
                return AcademicLevel_AcademicLevelDTO;
            else
                return BadRequest(AcademicLevel_AcademicLevelDTO);
        }

        [Route(AcademicLevelRoute.Get)]
        public async Task<ActionResult<AcademicLevel_AcademicLevelDTO>> Get(AcademicLevel_AcademicLevelDTO AcademicLevel_AcademicLevelDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            if (!await HasPermission(AcademicLevel_AcademicLevelDTO.Id))
                return Forbid();

            AcademicLevel AcademicLevel = ConvertDTOToEntity(AcademicLevel_AcademicLevelDTO);
            AcademicLevel = await AcademicLevelService.Get(AcademicLevel.Id);

            return new AcademicLevel_AcademicLevelDTO(AcademicLevel);

        }

        [Route(AcademicLevelRoute.List)]
        public async Task<ActionResult<List<AcademicLevel_AcademicLevelDTO>>> List(AcademicLevel_AcademicLevelFilterDTO AcademicLevel_AcademicLevelFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            AcademicLevelFilter AcademicLevelFilter = ConvertFilterDTOToFilterEntity(AcademicLevel_AcademicLevelFilterDTO);
            AcademicLevelFilter = AcademicLevelService.ToFilter(AcademicLevelFilter);
            List<AcademicLevel> AcademicLevels = await AcademicLevelService.List(AcademicLevelFilter);
            List<AcademicLevel_AcademicLevelDTO> AcademicLevel_AcademicLevelDTOs = AcademicLevels.Select(x => new AcademicLevel_AcademicLevelDTO(x)).ToList();

            return AcademicLevel_AcademicLevelDTOs;
        }

        [Route(AcademicLevelRoute.Update)]
        public async Task<ActionResult<AcademicLevel_AcademicLevelDTO>> Update(AcademicLevel_AcademicLevelDTO AcademicLevel_AcademicLevelDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            AcademicLevel AcademicLevel = ConvertDTOToEntity(AcademicLevel_AcademicLevelDTO);
            AcademicLevel = await AcademicLevelService.Update(AcademicLevel);

            return new AcademicLevel_AcademicLevelDTO(AcademicLevel);
        }

        [Route(AcademicLevelRoute.Delete)]
        public async Task<ActionResult<AcademicLevel_AcademicLevelDTO>> Delete(AcademicLevel_AcademicLevelDTO AcademicLevel_AcademicLevelDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            AcademicLevel AcademicLevel = ConvertDTOToEntity(AcademicLevel_AcademicLevelDTO);
            AcademicLevel = await AcademicLevelService.Delete(AcademicLevel);

            return new AcademicLevel_AcademicLevelDTO(AcademicLevel);
        }

        [Route(AcademicLevelRoute.BulkDelete)]
        public async Task<ActionResult<bool>> BulkDelete(List<long> Ids)
        {
            System.Console.WriteLine("ids:" + JsonConvert.SerializeObject(Ids));
            AcademicLevelFilter AcademicLevelFilter = new AcademicLevelFilter();
            AcademicLevelFilter = AcademicLevelService.ToFilter(AcademicLevelFilter);
            AcademicLevelFilter.Id = new IdFilter { In = Ids };
            AcademicLevelFilter.Selects = AcademicLevelSelect.Id;
            AcademicLevelFilter.Skip = 0;
            AcademicLevelFilter.Take = int.MaxValue;

            List<AcademicLevel> AcademicLevels = await AcademicLevelService.List(AcademicLevelFilter);
            await AcademicLevelService.BulkDelete(AcademicLevels);

            return true;
        }

        private async Task<bool> HasPermission(long Id)
        {
            AcademicLevelFilter AcademicLevelFilter = new AcademicLevelFilter();
            if (Id == 0)
            {

            }
            else
            {
                AcademicLevelFilter.Id = new IdFilter { Equal = Id };
                int count = await AcademicLevelService.Count(AcademicLevelFilter);
                if (count == 0)
                    return false;
            }
            return true;
        }

        private AcademicLevelFilter ConvertFilterDTOToFilterEntity(AcademicLevel_AcademicLevelFilterDTO AcademicLevel_AcademicLevelFilterDTO)
        {
            System.Console.WriteLine("Id");
            System.Console.WriteLine(JsonConvert.SerializeObject(AcademicLevel_AcademicLevelFilterDTO.Id));


            AcademicLevelFilter AcademicLevelFilter = new AcademicLevelFilter();
            AcademicLevelFilter.Selects = AcademicLevelSelect.ALL;
            AcademicLevelFilter.Skip = AcademicLevel_AcademicLevelFilterDTO.Skip;
            AcademicLevelFilter.Take = AcademicLevel_AcademicLevelFilterDTO.Take;
            AcademicLevelFilter.OrderBy = AcademicLevel_AcademicLevelFilterDTO.OrderBy;
            AcademicLevelFilter.OrderType = AcademicLevel_AcademicLevelFilterDTO.OrderType;

            AcademicLevelFilter.Id = AcademicLevel_AcademicLevelFilterDTO.Id;
            AcademicLevelFilter.Code = AcademicLevel_AcademicLevelFilterDTO.Code;
            AcademicLevelFilter.Name = AcademicLevel_AcademicLevelFilterDTO.Name;
            AcademicLevelFilter.StatusId = AcademicLevel_AcademicLevelFilterDTO.StatusId;

            return AcademicLevelFilter;
        }
        private AcademicLevel ConvertDTOToEntity(AcademicLevel_AcademicLevelDTO AcademicLevel_AcademicLevelDTO)
        {
            AcademicLevel AcademicLevel = new AcademicLevel();
            AcademicLevel.Id = AcademicLevel_AcademicLevelDTO.Id;
            AcademicLevel.Code = AcademicLevel_AcademicLevelDTO.Code;
            AcademicLevel.Name = AcademicLevel_AcademicLevelDTO.Name;
            AcademicLevel.StatusId = AcademicLevel_AcademicLevelDTO.StatusId;
            AcademicLevel.Status = AcademicLevel_AcademicLevelDTO.Status == null ? null : new Status
            {
                Id = AcademicLevel_AcademicLevelDTO.Status.Id,
                Code = AcademicLevel_AcademicLevelDTO.Status.Code,
                Name = AcademicLevel_AcademicLevelDTO.Status.Name,
            };

            return AcademicLevel;
        }
    }
}