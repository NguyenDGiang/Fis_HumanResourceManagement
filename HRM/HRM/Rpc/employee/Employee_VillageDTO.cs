using HRM.Entities;
using System;
using TrueSight.Common;

namespace HRM.Rpc.employee
{
    public class Employee_VillageDTO:DataDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public long? DistrictId { get; set; }
        public long? StatusId { get; set; }
        public Guid RowId { get; set; }
        public bool Used { get; set; }
        public Employee_DistrictDTO District { get; set; }
        public Employee_StatusDTO Status { get; set; }
        public Employee_VillageDTO() { }
        public Employee_VillageDTO(Village Village)
        {
            this.Id = Village.Id;
            this.Name = Village.Name;
            this.Code = Village.Code;
            this.Used = Village.Used;
            this.DistrictId = Village.DistrictId;   
            this.StatusId = Village.StatusId;
            this.Status = Village.Status == null ? null : new Employee_StatusDTO(Village.Status);
            this.District = Village.District == null ? null : new Employee_DistrictDTO(Village.District);
        }
    }
    public class Employee_VillageFilterDTO:FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter Code { get; set; }
        public IdFilter DistrictId { get; set; }
        public IdFilter StatusId { get; set; }
        public VillageOrder OrderBy { get; set; }
    }
}
