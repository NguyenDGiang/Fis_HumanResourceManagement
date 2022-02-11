using HRM.Entities;
using System;
using TrueSight.Common;

namespace HRM.Rpc.employee
{
    public class Employee_ProvinceDTO:DataDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public long? StatusId { get; set; }
        public Guid RowId { get; set; }
        public bool Used { get; set; }
        public Employee_StatusDTO Status { get; set; }
        public Employee_ProvinceDTO() { }
        public Employee_ProvinceDTO(Province Province)
        {
            this.Id = Province.Id;
            this.Name = Province.Name;
            this.Code = Province.Code;
            this.StatusId = Province.StatusId;
            this.Used = Province.Used;
            this.Status = Province.Status == null ? null: new Employee_StatusDTO(Province.Status);
        }
    }
    public class Employee_ProvinceFilterDTO:FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter Code { get; set; }
        public IdFilter StatusId { get; set; }
        public ProvinceOrder OrderBy { get; set; }
    }
}
