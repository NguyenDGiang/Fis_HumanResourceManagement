using HRM.Entities;
using System;
using TrueSight.Common;

namespace HRM.Rpc.interviewresult
{
    public class InterviewResult_CandidateDTO:DataDTO
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Birthday { get; set; }
        public long? StatusId { get; set; }
        public bool Used { get; set; }
        public long? VillageId { get; set; }
        public long? DistrictId { get; set; }
        public long? ProvinceId { get; set; }
        public InterviewResult_StatusDTO Status { get; set; }
        public InterviewResult_CandidateDTO() { }
        public InterviewResult_CandidateDTO(Candidate Cadidate)
        {
            this.Id = Cadidate.Id;
            this.PhoneNumber = Cadidate.PhoneNumber;
            this.FullName = Cadidate.FullName;
            this.Email = Cadidate.Email;    
            this.Birthday = Cadidate.Birthday;
            this.DistrictId = Cadidate.DistrictId;
            this.Used = Cadidate.Used;
            this.VillageId = Cadidate.VillageId;
            this.ProvinceId = Cadidate.ProvinceId;
            this.StatusId = Cadidate.StatusId;
            this.Status =  Cadidate.Status == null ? null : new InterviewResult_StatusDTO(Cadidate.Status);
        }
    }
    public class InterviewResult_CandidateFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter FullName { get; set; }
        public StringFilter Email { get; set; }
        public StringFilter PhoneNumber { get; set; }
        public DateFilter Birthday { get; set; }
        public IdFilter StatusId { get; set; }
        public bool Used { get; set; }
        public IdFilter VillageId { get; set; }
        public IdFilter DistrictId { get; set; }
        public IdFilter ProvinceId { get; set; }
        public CandidateOrder OrderBy { get; set; }
    }
}
