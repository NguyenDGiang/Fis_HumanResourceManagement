using HRM.Entities;
using TrueSight.Common;

namespace HRM.Rpc.employee
{
    public class Employee_ChucVuDTO:DataDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public long? StatusId { get; set; }
        public bool Used { get; set; }
        public Employee_StatusDTO Status { get; set; }
        public Employee_ChucVuDTO() { }
        public Employee_ChucVuDTO(ChucVu ChucVu) { 
            this.Id = ChucVu.Id;
            this.Name = ChucVu.Name;
            this.Code = ChucVu.Code;
            this.StatusId = ChucVu.StatusId;
            this.Used = ChucVu.Used;
            this.Status = ChucVu.Status == null ? null : new Employee_StatusDTO(ChucVu.Status);
        }
    }
    public class Employee_ChucVuFilterDTO:FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter Code { get; set; }
        public IdFilter StatusId { get; set; }
        public ChucVuOrder OrderBy { get; set; }
    }
}
