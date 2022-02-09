using HRM.Entities;
using HRM.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRM.Services.MJobPosition
{
    public interface IJobPositionService
    {
        Task<int> Count(JobPositionFilter JobPositionFilter);
        Task<List<JobPosition>> List(JobPositionFilter JobPositionFilter);
        Task<JobPosition> Get(long Id);
        Task<JobPosition> Create(JobPosition JobPosition);
        Task<JobPosition> Update(JobPosition JobPosition);
        Task<JobPosition> Delete(JobPosition JobPosition);
        JobPositionFilter ToFilter(JobPositionFilter JobPositionFilter);
    }
    public class JobPositionService:IJobPositionService
    {
        private IUOW UOW;
        private IJobPositionValidator JobPositionValidator;
        public JobPositionService(IUOW UOW, IJobPositionValidator JobPositionValidator)
        {
            this.UOW = UOW;
            this.JobPositionValidator = JobPositionValidator;
        }
        public async Task<int> Count(JobPositionFilter JobPositionFilter)
        {
            int result = await UOW.JobPositionRepository.Count(JobPositionFilter);
            return result;
        }

        public async Task<JobPosition> Create(JobPosition JobPosition)
        {
            if (!await JobPositionValidator.Create(JobPosition))
                return JobPosition;
            await UOW.JobPositionRepository.Create(JobPosition);
            List<JobPosition> JobPositions = await UOW.JobPositionRepository.List(new List<long> { JobPosition.Id });
            JobPosition = JobPositions.FirstOrDefault();
            return JobPosition;
        }

        public async Task<JobPosition> Delete(JobPosition JobPosition)
        {
            if (!await JobPositionValidator.Delete(JobPosition))
                return JobPosition;
            List<JobPosition> JobPositions = await UOW.JobPositionRepository.List(new List<long> { JobPosition.Id });
            JobPosition = JobPositions.FirstOrDefault();
            return JobPosition;
        }

        public async Task<JobPosition> Get(long Id)
        {
            JobPosition JobPosition = await UOW.JobPositionRepository.Get(Id);
            if (JobPosition == null)
                return null;
            return JobPosition;
        }

        public async Task<List<JobPosition>> List(JobPositionFilter JobPositionFilter)
        {
            List<JobPosition> JobPositions = await UOW.JobPositionRepository.List(JobPositionFilter);
            return JobPositions;
        }
        public JobPositionFilter ToFilter(JobPositionFilter filter)
        {

            return filter;
        }

        public async Task<JobPosition> Update(JobPosition JobPosition)
        {
            if (!await JobPositionValidator.Update(JobPosition))
                return JobPosition;
            var oldData = await UOW.JobPositionRepository.Get(JobPosition.Id);
            await UOW.JobPositionRepository.Update(JobPosition);
            List<JobPosition> JobPositions = await UOW.JobPositionRepository.List(new List<long> { JobPosition.Id });
            JobPosition = JobPositions.FirstOrDefault();
            return JobPosition;

        }
    }
}
