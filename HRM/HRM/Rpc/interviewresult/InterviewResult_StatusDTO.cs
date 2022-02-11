using HRM.Entities;
using TrueSight.Common;

namespace HRM.Rpc.interviewresult
{
    public class InterviewResult_StatusDTO:DataDTO
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public InterviewResult_StatusDTO() { }
        public InterviewResult_StatusDTO(Status Status)
        {
            this.Id = Status.Id;
            this.Code = Status.Code;
            this.Name = Status.Name;
        }
    }
    public class InterviewResult_StatusFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public StatusOrder OrderBy { get; set; }
    }
}
