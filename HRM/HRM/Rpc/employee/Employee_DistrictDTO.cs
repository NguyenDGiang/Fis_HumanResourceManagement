using HRM.Entities;
using System;
using TrueSight.Common;

namespace HRM.Rpc.employee
{
    public class Employee_DistrictDTO:DataDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public long? ProvinceId { get; set; }
        public long? StatusId { get; set; }
        public Guid RowId { get; set; }
        public bool Used { get; set; }
        public Employee_ProvinceDTO Province { get; set; }
        public Employee_StatusDTO Status { get; set; }
        public Employee_DistrictDTO() { }
        public Employee_DistrictDTO(District District)
        {
            this.Id = District.Id;
            this.Name = District.Name;
            this.Code = District.Code;
            this.StatusId = District.StatusId;
            this.Used = District.Used;
            this.ProvinceId = District.ProvinceId;
            this.Status = District.Status == null ? null : new Employee_StatusDTO(District.Status);
            this.Province = District.Province == null ? null : new Employee_ProvinceDTO(District.Province);
        }
    }
    public class Employee_DistrictFilterDTO:FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter Code { get; set; }
        public IdFilter ProvinceId { get; set; }
        public IdFilter StatusId { get; set; }
        public DistrictOrder OrderBy { get; set; }
    }
}
