using HRM.Entities;
using TrueSight.Common;

namespace HRM.Rpc.candidate
{
    public class Candidate_ProvinceDTO : DataDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public long? StatusId { get; set; }
        public Candidate_ProvinceDTO()
        {

        }
        public Candidate_ProvinceDTO(Province Province)
        {
            Id = Province.Id;
            Code = Province.Code;
            Name = Province.Name;
            StatusId = Province.StatusId;
        }
    }

    public class Candidate_ProvinceFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter Code { get; set; }
        public ProvinceOrder OrderBy { get; set; }
    }

}