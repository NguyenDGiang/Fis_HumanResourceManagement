using HRM.Entities;
using TrueSight.Common;

namespace HRM.Rpc.department
{
    public class Department_DepartmentDTO : DataDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string PhoneNumber { get; set; }
        public long? StatusId { get; set; }
        public bool Used { get; set; }
        public long? VillageId { get; set; }
        public long? DistrictId { get; set; }
        public long? ProvinceId { get; set; }

        public Department_DistrictDTO District { get; set; }
        public Department_ProvinceDTO Province { get; set; }
        public Department_StatusDTO Status { get; set; }
        public Department_VillageDTO Village { get; set; }

        public Department_DepartmentDTO()
        {
            
        }
        public Department_DepartmentDTO(Department Department)
        {
            Id = Department.Id;
            Code = Department.Code;
            Name = Department.Name;
            PhoneNumber = Department.PhoneNumber;
            StatusId = Department.StatusId;
            Used = Department.Used;
            VillageId = Department.VillageId;
            DistrictId = Department.DistrictId;
            ProvinceId = Department.ProvinceId;
            Status = new Department_StatusDTO(Department.Status);
            Village = new Department_VillageDTO(Department.Village);
            District = new Department_DistrictDTO(Department.District);
            Province = new Department_ProvinceDTO(Department.Province);
        }
    }
    public class Department_DepartmentFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter PhoneNumber { get; set; }
        public IdFilter StatusId { get; set; }
        public bool Used { get; set; }
        public IdFilter VillageId { get; set; }
        public IdFilter DistrictId { get; set; }
        public IdFilter ProvinceId { get; set; }
        public DepartmentOrder OrderBy { get; set; }

    }

}