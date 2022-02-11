using HRM.Entities;
using TrueSight.Common;

namespace HRM.Rpc.department
{
    public class Department_ProvinceDTO : DataDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public long? StatusId { get; set; }
        public Department_ProvinceDTO()
        {
            
        }
        public Department_ProvinceDTO(Province Province)
        {
            Id =Province.Id;
            Name = Province.Name;
            Code = Province.Code;
            StatusId = Province.StatusId;
        }
    }
    public class Department_ProvinceFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter Code { get; set; }
        public IdFilter StatusId { get; set; }
        public ProvinceOrder OrderBy { get; set; }

    }

}