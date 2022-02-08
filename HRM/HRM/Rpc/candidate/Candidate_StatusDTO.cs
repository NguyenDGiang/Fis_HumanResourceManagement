using HRM.Entities;
using TrueSight.Common;

namespace HRM.Rpc.candidate
{
    public class Candidate_StatusDTO : DataDTO
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Candidate_StatusDTO()
        {

        }
        public Candidate_StatusDTO(Status Status)
        {
            Id = Status.Id;
            Code = Status.Code;
            Name = Status.Name;
        }
    }
    public class Candidate_StatusFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public StatusOrder OrderBy { get; set; }
    }

}