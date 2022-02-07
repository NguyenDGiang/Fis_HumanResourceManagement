using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRM.Entities;
using HRM.Services.MCandidate;
using Microsoft.AspNetCore.Mvc;
using TrueSight.Common;

namespace HRM.Rpc.candidate
{
    public class CandidateController : RpcController
    {
        private ICandidateService CandidateService;
        public CandidateController(ICandidateService CandidateService)
        {
            this.CandidateService = CandidateService;
        }

        [Route(CandidateRoute.Count)]
        public async Task<ActionResult<int>> Count(Candidate_CandidateFilterDTO Candidate_CandidateFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            
            CandidateFilter CandidateFilter = ConvertFilterDTOToFilterEntity(Candidate_CandidateFilterDTO);
            CandidateFilter = CandidateService.ToFilter(CandidateFilter);

            int result = await CandidateService.Count(CandidateFilter);

            return result;
        }

        [Route(CandidateRoute.Create)]
        public async Task<ActionResult<Candidate_CandidateDTO>> Create(Candidate_CandidateDTO Candidate_CandidateDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

                 Candidate Candidate = ConvertDTOToEntity(Candidate_CandidateDTO);
            Candidate = await CandidateService.Create(Candidate);
            Candidate_CandidateDTO = new Candidate_CandidateDTO(Candidate);

            if (Candidate.IsValidated)
                return Candidate_CandidateDTO;
            else
                return BadRequest(Candidate_CandidateDTO);
        }

        [Route(CandidateRoute.Get)]
        public async Task<ActionResult<Candidate_CandidateDTO>> Get(Candidate_CandidateDTO Candidate_CandidateDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
                 Candidate Candidate = ConvertDTOToEntity(Candidate_CandidateDTO);
            Candidate = await CandidateService.Get(Candidate.Id);

            return new Candidate_CandidateDTO(Candidate);
        }

        [Route(CandidateRoute.List)]
        public async Task<ActionResult<List<Candidate_CandidateDTO>>> List(Candidate_CandidateFilterDTO Candidate_CandidateFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

                 CandidateFilter CandidateFilter = ConvertFilterDTOToFilterEntity(Candidate_CandidateFilterDTO);
            CandidateFilter = CandidateService.ToFilter(CandidateFilter);
            List<Candidate> Candidates = await CandidateService.List(CandidateFilter);
            List<Candidate_CandidateDTO> Candidate_CandidateDTOs = Candidates.Select(x => new Candidate_CandidateDTO(x)).ToList();

            return Candidate_CandidateDTOs;
        }

        [Route(CandidateRoute.Update)]
        public async Task<ActionResult<Candidate_CandidateDTO>> Update(Candidate_CandidateDTO Candidate_CandidateDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

                 Candidate Candidate = ConvertDTOToEntity(Candidate_CandidateDTO);
            Candidate = await CandidateService.Update(Candidate);

            return new Candidate_CandidateDTO(Candidate);
        }

        [Route(CandidateRoute.Delete)]
        public async Task<ActionResult<Candidate_CandidateDTO>> Delete(Candidate_CandidateDTO Candidate_CandidateDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

                 Candidate Candidate = ConvertDTOToEntity(Candidate_CandidateDTO);
            Candidate = await CandidateService.Delete(Candidate);

            return new Candidate_CandidateDTO(Candidate);
        }

        [Route(CandidateRoute.BulkDelete)]
        public async Task<ActionResult<bool>> BulkDelete(List<long> Ids)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

                 CandidateFilter CandidateFilter = new CandidateFilter();
            CandidateFilter = CandidateService.ToFilter(CandidateFilter);
            CandidateFilter.Id = new IdFilter { In = Ids };
            CandidateFilter.Selects = CandidateSelect.Id;
            CandidateFilter.Skip = 0;
            CandidateFilter.Take = int.MaxValue;

            List<Candidate> Candidates = await CandidateService.List(CandidateFilter);
            await CandidateService.BulkDelete(Candidates);

            return true;
        }

        private CandidateFilter ConvertFilterDTOToFilterEntity(Candidate_CandidateFilterDTO Candidate_CandidateFilterDTO)
        {
            CandidateFilter CandidateFilter = new CandidateFilter();

            CandidateFilter.Selects = CandidateSelect.ALL;
            CandidateFilter.Skip = Candidate_CandidateFilterDTO.Skip;
            CandidateFilter.Take = Candidate_CandidateFilterDTO.Take;
            CandidateFilter.OrderBy = Candidate_CandidateFilterDTO.OrderBy;
            CandidateFilter.OrderType = Candidate_CandidateFilterDTO.OrderType;

            CandidateFilter.Id = Candidate_CandidateFilterDTO.Id;
            CandidateFilter.Email = Candidate_CandidateFilterDTO.Email;
            CandidateFilter.FullName = Candidate_CandidateFilterDTO.FullName;
            CandidateFilter.StatusId = Candidate_CandidateFilterDTO.StatusId;
            CandidateFilter.Birthday = Candidate_CandidateFilterDTO.Birthday;
            CandidateFilter.VillageId = Candidate_CandidateFilterDTO.VillageId;
            CandidateFilter.DistrictId = Candidate_CandidateFilterDTO.DistrictId;
            CandidateFilter.ProvinceId = Candidate_CandidateFilterDTO.ProvinceId;

            return CandidateFilter;
        }

        private Candidate ConvertDTOToEntity(Candidate_CandidateDTO Candidate_CandidateDTO)
        {
            Candidate Candidate = new Candidate();
            Candidate.Id = Candidate_CandidateDTO.Id;
            Candidate.FullName = Candidate_CandidateDTO.FullName;
            Candidate.Email = Candidate_CandidateDTO.Email;
            Candidate.PhoneNumber = Candidate_CandidateDTO.PhoneNumber;
            Candidate.Birthday = Candidate_CandidateDTO.Birthday;
            Candidate.StatusId = Candidate_CandidateDTO.StatusId;
            Candidate.Used = Candidate_CandidateDTO.Used;
            Candidate.Status = Candidate_CandidateDTO.Status == null ? null : new Status
            {
                Id = Candidate_CandidateDTO.Status.Id,
                Code = Candidate_CandidateDTO.Status.Code,
                Name = Candidate_CandidateDTO.Status.Name,
            };
            Candidate.Village = Candidate_CandidateDTO.Village == null ? null : new Village
            {
                Id = Candidate_CandidateDTO.Village.Id,
                Name = Candidate_CandidateDTO.Village.Name,
                Code = Candidate_CandidateDTO.Village.Code,
                DistrictId = Candidate_CandidateDTO.Village.DistrictId,
                StatusId = Candidate_CandidateDTO.Village.StatusId,
            };
            Candidate.District = Candidate_CandidateDTO.District == null ? null : new District
            {
                Id = Candidate_CandidateDTO.District.Id,
                Name = Candidate_CandidateDTO.District.Name,
                Code = Candidate_CandidateDTO.District.Code,
                ProvinceId = Candidate_CandidateDTO.District.ProvinceId,
                StatusId = Candidate_CandidateDTO.District.StatusId,
            };
            Candidate.Province = Candidate_CandidateDTO.Province == null ? null : new Province
            {
                Id = Candidate_CandidateDTO.Province.Id,
                Name = Candidate_CandidateDTO.Province.Name,
                Code = Candidate_CandidateDTO.Province.Code,
                StatusId = Candidate_CandidateDTO.Province.StatusId,
            };

            return Candidate;
        }
    }
}