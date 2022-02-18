using HRM.Entities;
using HRM.Services.MJobPosition;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrueSight.Common;

namespace HRM.Rpc.jobposition
{
    public class JobPositionController : Controller
    {
        IJobPositionService JobPositionService;
        public JobPositionController(IJobPositionService JobPositionService)
        {
            this.JobPositionService = JobPositionService;
        }

        [Route(JobPositionRoute.Count), HttpPost]
        public async Task<ActionResult<int>> Count([FromBody] JobPosition_JobPositionFilterDTO JobPosition_JobPositionFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            JobPositionFilter JobPositionFilter = ConvertFilterDTOToFilterEntity(JobPosition_JobPositionFilterDTO);
            JobPositionFilter = JobPositionService.ToFilter(JobPositionFilter);
            int count = await JobPositionService.Count(JobPositionFilter);
            return count;
        }

        [Route(JobPositionRoute.Get), HttpPost]

        public async Task<ActionResult<JobPosition_JobPositionDTO>> Get([FromBody] JobPosition_JobPositionDTO JobPosition_JobPositionDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            JobPosition JobPosition = await JobPositionService.Get(JobPosition_JobPositionDTO.Id);
            return new JobPosition_JobPositionDTO(JobPosition);
        }

        [Route(JobPositionRoute.List), HttpPost]
        public async Task<ActionResult<List<JobPosition_JobPositionDTO>>> List([FromBody] JobPosition_JobPositionFilterDTO JobPosition_JobPositionFilterDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            JobPositionFilter JobPositionFilter = ConvertFilterDTOToFilterEntity(JobPosition_JobPositionFilterDTO);
            JobPositionFilter = JobPositionService.ToFilter(JobPositionFilter);
            List<JobPosition> JobPositions = await JobPositionService.List(JobPositionFilter);
            List<JobPosition_JobPositionDTO> JobPosition_JobPositionDTOs = JobPositions
                .Select(c => new JobPosition_JobPositionDTO(c)).ToList();
            return JobPosition_JobPositionDTOs;
        }

        [Route(JobPositionRoute.Create), HttpPost]
        public async Task<ActionResult<JobPosition_JobPositionDTO>> Create([FromBody] JobPosition_JobPositionDTO JobPosition_JobPositionDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);
            JobPosition JobPosition = ConvertDTOToEntity(JobPosition_JobPositionDTO);
            JobPosition = await JobPositionService.Create(JobPosition);
            JobPosition_JobPositionDTO = new JobPosition_JobPositionDTO(JobPosition);
            if (JobPosition.IsValidated)
                return JobPosition_JobPositionDTO;
            else
                return BadRequest(JobPosition_JobPositionDTO);
        }

        [Route(JobPositionRoute.Update), HttpPost]
        public async Task<ActionResult<JobPosition_JobPositionDTO>> Update([FromBody] JobPosition_JobPositionDTO JobPosition_JobPositionDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            JobPosition JobPosition = ConvertDTOToEntity(JobPosition_JobPositionDTO);
            JobPosition = await JobPositionService.Update(JobPosition);
            JobPosition_JobPositionDTO = new JobPosition_JobPositionDTO(JobPosition);

            return JobPosition_JobPositionDTO;
        }

        [Route(JobPositionRoute.Delete), HttpPost]

        public async Task<ActionResult<JobPosition_JobPositionDTO>> Delete([FromBody] JobPosition_JobPositionDTO JobPosition_JobPositionDTO)
        {
            if (!ModelState.IsValid)
                throw new BindException(ModelState);

            JobPosition JobPosition = ConvertDTOToEntity(JobPosition_JobPositionDTO);
            JobPosition = await JobPositionService.Delete(JobPosition);
            JobPosition_JobPositionDTO = new JobPosition_JobPositionDTO(JobPosition);

            if (JobPosition.IsValidated)

                return JobPosition_JobPositionDTO;
            else
                return BadRequest(JobPosition_JobPositionDTO);

        }

        private JobPosition ConvertDTOToEntity(JobPosition_JobPositionDTO JobPosition_JobPositionDTO)
        {
            JobPosition JobPosition = new JobPosition();
            JobPosition.Id = JobPosition_JobPositionDTO.Id;
            JobPosition.Name = JobPosition_JobPositionDTO.Name;
            JobPosition.Code = JobPosition_JobPositionDTO.Code;
            JobPosition.StatusId = JobPosition_JobPositionDTO.StatusId;
            JobPosition.Used = JobPosition_JobPositionDTO.Used;

            JobPosition.Status = JobPosition_JobPositionDTO.Status == null ? null : new Status
            {
                Id = JobPosition_JobPositionDTO.Status.Id,
                Code = JobPosition_JobPositionDTO.Status.Code,
                Name = JobPosition_JobPositionDTO.Status.Name,
            };
            


            return JobPosition;
        }

        private JobPositionFilter ConvertFilterDTOToFilterEntity(JobPosition_JobPositionFilterDTO JobPosition_JobPositionFilterDTO)
        {
            JobPositionFilter JobPositionFilter = new JobPositionFilter();
            JobPositionFilter.Selects = JobPositionSelect.ALL;
            JobPositionFilter.Skip = JobPosition_JobPositionFilterDTO.Skip;
            JobPositionFilter.Take = JobPosition_JobPositionFilterDTO.Take;
            JobPositionFilter.OrderBy = JobPosition_JobPositionFilterDTO.OrderBy;
            JobPositionFilter.OrderType = JobPosition_JobPositionFilterDTO.OrderType;

            JobPositionFilter.Id = JobPosition_JobPositionFilterDTO.Id;
            JobPositionFilter.Name = JobPosition_JobPositionFilterDTO.Name;
            JobPositionFilter.Used = JobPosition_JobPositionFilterDTO.Used;
            JobPositionFilter.StatusId = JobPosition_JobPositionFilterDTO.StatusId;
            JobPositionFilter.Code = JobPosition_JobPositionFilterDTO.Code;


            return JobPositionFilter;
        }
    }
}
