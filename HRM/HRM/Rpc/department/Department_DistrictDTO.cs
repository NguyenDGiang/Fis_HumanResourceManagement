using HRM.Entities;
using TrueSight.Common;

namespace HRM.Rpc.department
{
    public class Department_DistrictDTO : DataDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public long? ProvinceId { get; set; }
        public long? StatusId { get; set; }

        public Department_DistrictDTO()
        {

        }
        public Department_DistrictDTO(District District)
        {
            Id = District.Id;
            Name = District.Name;
            Code = District.Code;
            ProvinceId = District.ProvinceId;
            StatusId = District.StatusId;
        }
    }
    public class Department_DistrictFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter Code { get; set; }
        public IdFilter ProvinceId { get; set; }
        public IdFilter StatusId { get; set; }
        public DistrictOrder OrderBy { get; set; }

    }

}