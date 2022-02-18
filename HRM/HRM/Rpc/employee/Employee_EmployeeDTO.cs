using HRM.Entities;
using System;
using TrueSight.Common;

namespace HRM.Rpc.employee
{
    public class Employee_EmployeeDTO:DataDTO
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime BeginJobTime { get; set; }
        public bool ContractStatus { get; set; }
        public long? StatusId { get; set; }
        public long? ChucVuId { get; set; }
        public long? AcademicLevelId { get; set; }
        public long? JobPositionId { get; set; }
        public long? VillageId { get; set; }
        public long? DistrictId { get; set; }
        public long? ProvinceId { get; set; }
        public bool Used { get; set; }
        public Employee_AcademicLevelDTO AcademicLevel { get; set; }
        public Employee_ChucVuDTO ChucVu { get; set; }
        public Employee_DistrictDTO District { get; set; }
        public Employee_JobPositionDTO JobPosition { get; set; }
        public Employee_ProvinceDTO Province { get; set; }
        public Employee_StatusDTO Status { get; set; }
        public Employee_VillageDTO Village { get; set; }
        public Employee_EmployeeDTO() { }
        public Employee_EmployeeDTO(Employee Employee)
        {
            this.Id = Employee.Id;
            this.FullName = Employee.FullName;
            this.Email = Employee.Email;    
            this.PhoneNumber = Employee.PhoneNumber;
            this.BeginJobTime = Employee.BeginJobTime;
            this.Birthday = Employee.Birthday;
            this.ContractStatus = Employee.ContractStatus;
            this.StatusId = Employee.StatusId;
            this.DistrictId = Employee.DistrictId;
            this.JobPositionId = Employee.JobPositionId;
            this.VillageId = Employee.VillageId;
            this.AcademicLevelId = Employee.AcademicLevelId;
            this.ChucVuId = Employee.ChucVuId;
            this.ProvinceId = Employee.ProvinceId;
            this.Status = Employee.Status == null ? null : new Employee_StatusDTO(Employee.Status);
            this.District = Employee.District == null ? null : new Employee_DistrictDTO(Employee.District);
            this.JobPosition = Employee.JobPosition == null ? null : new Employee_JobPositionDTO(Employee.JobPosition); 
            this.Village = Employee.Village == null ? null : new Employee_VillageDTO(Employee.Village);
            this.AcademicLevel = Employee.AcademicLevel == null ? null : new Employee_AcademicLevelDTO(Employee.AcademicLevel);
            this.ChucVu = Employee.ChucVu == null ? null : new Employee_ChucVuDTO(Employee.ChucVu);
            this.Province = Employee.Province == null ? null : new Employee_ProvinceDTO(Employee.Province);
        }
    }
    public class Employee_EmployeeFilterDTO:FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter FullName { get; set; }
        public StringFilter Email { get; set; }
        public StringFilter PhoneNumber { get; set; }
        public DateFilter Birthday { get; set; }
        public DateFilter BeginJobTime { get; set; }
        public bool ContractStatus { get; set; }
        public IdFilter StatusId { get; set; }
        public IdFilter ChucVuId { get; set; }
        public IdFilter AcademicLevelId { get; set; }
        public IdFilter JobPositionId { get; set; }
        public IdFilter VillageId { get; set; }
        public IdFilter DistrictId { get; set; }
        public IdFilter ProvinceId { get; set; }
        public EmployeeOrder OrderBy { get; set; }
        public EmployeeSelect Selects { get; set; }
        public string Search { get; set; }
    }
}
