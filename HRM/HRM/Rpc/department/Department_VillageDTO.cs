using HRM.Entities;
using TrueSight.Common;

namespace HRM.Rpc.department
{
    public class Department_VillageDTO : DataDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public long? DistrictId { get; set; }
        public long? StatusId { get; set; }
        public Department_VillageDTO()
        {

        }
        public Department_VillageDTO(Village Village)
        {
            Id = Village.Id;
            Code = Village.Code;
            Name = Village.Name;
            DistrictId = Village.DistrictId;
            StatusId = Village.StatusId;
        }
    }
    public class Department_VillageFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter Code { get; set; }
        public IdFilter DistrictId { get; set; }
        public IdFilter StatusId { get; set; }
        public VillageOrder OrderBy { get; set; }
    }

}