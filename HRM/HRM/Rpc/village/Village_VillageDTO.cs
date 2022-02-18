using TrueSight.Common;
using HRM.Entities;

namespace HRM.Rpc.village
{
    public class Village_VillageDTO : DataDTO
    {

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public long DistrictId { get; set; }
        public long StatusId { get; set; }
        public bool Used { get; set; }
        public Village_DistrictDTO District { get; set; }
        public Village_StatusDTO Status { get; set; }
        public Village_VillageDTO() { }
        public Village_VillageDTO(Village Village)
        {
            this.Id = Village.Id;
            this.Code = Village.Code;
            this.Name = Village.Name;
            this.DistrictId = (long)Village.DistrictId;
            this.StatusId = (long)Village.StatusId;
            this.Used = Village.Used;
            this.District = Village.District == null ? null : new Village_DistrictDTO(Village.District);
            this.Status = Village.Status == null ? null : new Village_StatusDTO(Village.Status);
            this.Errors = Village.Errors;
        }
    }

    public class Village_VillageFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public IdFilter DistrictId { get; set; }
        public IdFilter StatusId { get; set; }
        public VillageOrder OrderBy { get; set; }
    }

}
