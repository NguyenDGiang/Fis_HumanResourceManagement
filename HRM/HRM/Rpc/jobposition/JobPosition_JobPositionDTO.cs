using HRM.Entities;
using TrueSight.Common;

namespace HRM.Rpc.jobposition
{
    public class JobPosition_JobPositionDTO:DataDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public long? StatusId { get; set; }
        public bool Used { get; set; }
        public JobPosition_StatusDTO Status { get; set; }
        public JobPosition_JobPositionDTO() { }
        public JobPosition_JobPositionDTO(JobPosition JobPosition)
        {
            this.Id = JobPosition.Id;
            this.Name = JobPosition.Name;
            this.Code = JobPosition.Code;
            this.StatusId = JobPosition.StatusId;   
            this.Used = JobPosition.Used;
            this.Status = JobPosition.Status == null ? null : new JobPosition_StatusDTO(JobPosition.Status);
        }
    }
    public class JobPosition_JobPositionFilterDTO:FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter Code { get; set; }
        public IdFilter StatusId { get; set; }
        public bool? Used { get; set; }
        public JobPositionOrder OrderBy { get; set; }
        public JobPositionSelect Selects { get; set; }
    }
}
