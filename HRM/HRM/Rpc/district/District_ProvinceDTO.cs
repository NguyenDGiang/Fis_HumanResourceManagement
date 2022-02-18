using TrueSight.Common;
using HRM.Entities;

namespace HRM.Rpc.district
{
    public class District_ProvinceDTO : DataDTO
    {

        public long Id { get; set; }

        public string Name { get; set; }

        public long StatusId { get; set; }

        public District_ProvinceDTO() { }
        public District_ProvinceDTO(Province Province)
        {

            this.Id = Province.Id;

            this.Name = Province.Name;

            this.StatusId =(long) Province.StatusId;

        }
    }

    public class District_ProvinceFilterDTO : FilterDTO
    {

        public IdFilter Id { get; set; }

        public StringFilter Name { get; set; }

        public IdFilter StatusId { get; set; }

        public ProvinceOrder OrderBy { get; set; }
    }

}
