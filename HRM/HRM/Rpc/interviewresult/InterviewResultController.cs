using HRM.Entities;
using HRM.Services.MInterviewResult;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrueSight.Common;

namespace HRM.Rpc.interviewresult
{
    public class InterviewResultController : Controller
    {
        private IInterviewResultService InterviewResultService;
        public InterviewResultController(IInterviewResultService InterviewResultService)
        {
            this.InterviewResultService = InterviewResultService; 
        }
        [Route(InterviewResultRoute.Count), HttpPost]
        public async Task<ActionResult<int>> Count([FromBody] InterviewResult_InterviewResultFilterDTO InterviewResult_InterviewResultFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            InterviewResultFilter InterviewResultFilter = ConvertFilterDTOToFilterEntity(InterviewResult_InterviewResultFilterDTO);
            InterviewResultFilter = InterviewResultService.ToFilter(InterviewResultFilter);
            int count = await InterviewResultService.Count(InterviewResultFilter);
            return count;
        }
        [Route(InterviewResultRoute.Get), HttpPost]

        public async Task<ActionResult<InterviewResult_InterviewResultDTO>> Get([FromBody] InterviewResult_InterviewResultDTO InterviewResult_InterviewResultDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            InterviewResult InterviewResult = await InterviewResultService.Get(InterviewResult_InterviewResultDTO.Id);
            return new InterviewResult_InterviewResultDTO(InterviewResult);
        }

        [Route(InterviewResultRoute.List), HttpPost]
        public async Task<ActionResult<List<InterviewResult_InterviewResultDTO>>> List([FromBody] InterviewResult_InterviewResultFilterDTO InterviewResult_InterviewResultFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            InterviewResultFilter InterviewResultFilter = ConvertFilterDTOToFilterEntity(InterviewResult_InterviewResultFilterDTO);
            InterviewResultFilter = InterviewResultService.ToFilter(InterviewResultFilter);
            List<InterviewResult> InterviewResults = await InterviewResultService.List(InterviewResultFilter);
            List<InterviewResult_InterviewResultDTO> InterviewResult_InterviewResultDTOs = InterviewResults
                .Select(c => new InterviewResult_InterviewResultDTO(c)).ToList();
            return InterviewResult_InterviewResultDTOs;
        }

        [Route(InterviewResultRoute.Create), HttpPost]
        public async Task<ActionResult<InterviewResult_InterviewResultDTO>> Create([FromBody] InterviewResult_InterviewResultDTO InterviewResult_InterviewResultDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            InterviewResult InterviewResult = ConvertDTOToEntity(InterviewResult_InterviewResultDTO);
            InterviewResult = await InterviewResultService.Create(InterviewResult);
            InterviewResult_InterviewResultDTO = new InterviewResult_InterviewResultDTO(InterviewResult);
            if (InterviewResult.IsValidated)
                return InterviewResult_InterviewResultDTO;
            else
                return BadRequest(InterviewResult_InterviewResultDTO);
        }

        [Route(InterviewResultRoute.Update), HttpPost]
        public async Task<ActionResult<InterviewResult_InterviewResultDTO>> Update([FromBody] InterviewResult_InterviewResultDTO InterviewResult_InterviewResultDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            InterviewResult InterviewResult = ConvertDTOToEntity(InterviewResult_InterviewResultDTO);
            InterviewResult = await InterviewResultService.Update(InterviewResult);
            InterviewResult_InterviewResultDTO = new InterviewResult_InterviewResultDTO(InterviewResult);

            return InterviewResult_InterviewResultDTO;
        }

        [Route(InterviewResultRoute.Delete), HttpPost]

        public async Task<ActionResult<InterviewResult_InterviewResultDTO>> Delete([FromBody] InterviewResult_InterviewResultDTO InterviewResult_InterviewResultDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            InterviewResult InterviewResult = ConvertDTOToEntity(InterviewResult_InterviewResultDTO);
            InterviewResult = await InterviewResultService.Delete(InterviewResult);
            InterviewResult_InterviewResultDTO = new InterviewResult_InterviewResultDTO(InterviewResult);

            if (InterviewResult.IsValidated)

                return InterviewResult_InterviewResultDTO;
            else
                return BadRequest(InterviewResult_InterviewResultDTO);

        }

        private InterviewResult ConvertDTOToEntity(InterviewResult_InterviewResultDTO InterviewResult_InterviewResultDTO)
        {
            InterviewResult InterviewResult = new InterviewResult();
            InterviewResult.Id = InterviewResult_InterviewResultDTO.Id;
            InterviewResult.CandidateId = InterviewResult_InterviewResultDTO.CandidateId;
            InterviewResult.InterviewTime = InterviewResult_InterviewResultDTO.InterviewTime;
            InterviewResult.PassInterview = InterviewResult_InterviewResultDTO.PassInterview;
            InterviewResult.BeginJobTime = InterviewResult_InterviewResultDTO.BeginJobTime;
            InterviewResult.TrialTime = InterviewResult_InterviewResultDTO.TrialTime;
            InterviewResult.StatusId = InterviewResult_InterviewResultDTO.StatusId;
            InterviewResult.Used = InterviewResult_InterviewResultDTO.Used;
            InterviewResult.Status = InterviewResult_InterviewResultDTO.Status == null ? null : new Status
            {
                Id = InterviewResult_InterviewResultDTO.Status.Id,
                Code = InterviewResult_InterviewResultDTO.Status.Code,
                Name = InterviewResult_InterviewResultDTO.Status.Name,
            };
    
            return InterviewResult;
        }
        private InterviewResultFilter ConvertFilterDTOToFilterEntity(InterviewResult_InterviewResultFilterDTO InterviewResult_InterviewResultFilterDTO)
        {
            InterviewResultFilter InterviewResultFilter = new InterviewResultFilter();
            InterviewResultFilter.Selects = InterviewResultSelect.ALL;
            InterviewResultFilter.Skip = InterviewResult_InterviewResultFilterDTO.Skip;
            InterviewResultFilter.Take = InterviewResult_InterviewResultFilterDTO.Take;
            InterviewResultFilter.OrderBy = InterviewResult_InterviewResultFilterDTO.OrderBy;
            InterviewResultFilter.OrderType = InterviewResult_InterviewResultFilterDTO.OrderType;
            InterviewResultFilter.Id = InterviewResult_InterviewResultFilterDTO.Id;
            InterviewResultFilter.Id = InterviewResult_InterviewResultFilterDTO.Id;
            InterviewResultFilter.CandidateId = InterviewResult_InterviewResultFilterDTO.CandidateId;
            InterviewResultFilter.InterviewTime = InterviewResult_InterviewResultFilterDTO.InterviewTime;
            InterviewResultFilter.PassInterview = InterviewResult_InterviewResultFilterDTO.PassInterview;
            InterviewResultFilter.BeginJobTime = InterviewResult_InterviewResultFilterDTO.BeginJobTime;
            InterviewResultFilter.TrialTime = InterviewResult_InterviewResultFilterDTO.TrialTime;
            InterviewResultFilter.StatusId = InterviewResult_InterviewResultFilterDTO.StatusId;
            InterviewResultFilter.Used = InterviewResult_InterviewResultFilterDTO.Used;
            return InterviewResultFilter;
        }
    }
}
