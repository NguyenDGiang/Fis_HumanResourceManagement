using TrueSight.Common;
using HRM.Entities;


namespace HRM.Rpc.village
{
    public class Village_DistrictDTO : DataDTO
    {


        public long Id { get; set; }

        public string Name { get; set; }


        public long ProvinceId { get; set; }

        public long StatusId { get; set; }


        public Village_DistrictDTO() { }
        public Village_DistrictDTO(District District)
        {

            this.Id = District.Id;

            this.Name = District.Name;


            this.ProvinceId =(long) District.ProvinceId;

            this.StatusId = (long)District.StatusId;

        }
    }

    public class Village_DistrictFilterDTO : FilterDTO
    {

        public IdFilter Id { get; set; }

        public StringFilter Name { get; set; }

        public IdFilter ProvinceId { get; set; }

        public IdFilter StatusId { get; set; }

        public DistrictOrder OrderBy { get; set; }
    }

}
