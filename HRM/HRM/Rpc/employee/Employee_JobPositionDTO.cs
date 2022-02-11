using HRM.Entities;
using TrueSight.Common;

namespace HRM.Rpc.employee
{
    public class Employee_JobPositionDTO:DataDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public long? StatusId { get; set; }
        public bool? Used { get; set; }
        public Employee_StatusDTO Status { get; set; }
        public Employee_JobPositionDTO() { }
        public Employee_JobPositionDTO(JobPosition JobPosition)
        {
            this.Id = JobPosition.Id;
            this.Name = JobPosition.Name;
            this.Code = JobPosition.Code;
            this.Used = JobPosition.Used;
            this.StatusId = JobPosition.StatusId;
            this.Status = JobPosition.Status == null ? null : new Employee_StatusDTO(JobPosition.Status);
        }
    }
    public class Employee_JobPositionFilterDTO:FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter Code { get; set; }
        public IdFilter StatusId { get; set; }
        public bool? Used { get; set; }
        public JobPositionOrder OrderBy { get; set; }
    }
}
