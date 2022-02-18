using HRM.Entities;
using TrueSight.Common;

namespace HRM.Rpc.employee
{
    public class Employee_AcademicLevelDTO:DataDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public long? StatusId { get; set; }
        public bool Used { get; set; }
        public Employee_StatusDTO Status { get; set; }
        public Employee_AcademicLevelDTO() { }
        public Employee_AcademicLevelDTO(AcademicLevel AcademicLevel)
        {
            this.Id = AcademicLevel.Id;
            this.Name = AcademicLevel.Name;
            this.Code = AcademicLevel.Code;
            this.StatusId = AcademicLevel.StatusId;
            this.Used = AcademicLevel.Used;
            this.Status = AcademicLevel.Status == null ? null : new Employee_StatusDTO(AcademicLevel.Status);

        }
    }
    public class Employee_AcademicLevelFilterDTO:FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter Code { get; set; }
        public IdFilter StatusId { get; set; }
        public ChucVuOrder OrderBy { get; set; }
    }
}
