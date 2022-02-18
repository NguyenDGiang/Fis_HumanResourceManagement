using HRM.Entities;
using System;
using TrueSight.Common;

namespace HRM.Rpc.interviewresult
{
    public class InterviewResult_InterviewResultDTO:DataDTO
    {
        public long Id { get; set; }
        public long? CandidateId { get; set; }
        public DateTime InterviewTime { get; set; }
        public bool PassInterview { get; set; }
        public DateTime BeginJobTime { get; set; }
        public DateTime TrialTime { get; set; }
        public long? StatusId { get; set; }
        public bool Used { get; set; }
        public InterviewResult_CandidateDTO Candidate { get; set; }
        public InterviewResult_StatusDTO Status { get; set; }
        public InterviewResult_InterviewResultDTO() { }
        public InterviewResult_InterviewResultDTO(InterviewResult InterviewResult) {
            this.Id = InterviewResult.Id;
            this.CandidateId  = InterviewResult.CandidateId;
            this.PassInterview = InterviewResult.PassInterview;
            this.BeginJobTime = InterviewResult.BeginJobTime;
            this.InterviewTime = InterviewResult.InterviewTime;
            this.TrialTime = InterviewResult.TrialTime;
            this.Used = InterviewResult.Used;
            this.StatusId = InterviewResult.StatusId;
            this.Status = InterviewResult.Status == null ? null :new InterviewResult_StatusDTO(InterviewResult.Status);
            this.Candidate = InterviewResult.Candidate == null ? null : new InterviewResult_CandidateDTO(InterviewResult.Candidate);
        }
    }
    public class InterviewResult_InterviewResultFilterDTO:FilterDTO
    {
        public IdFilter Id { get; set; }
        public IdFilter CandidateId { get; set; }
        public DateFilter InterviewTime { get; set; }
        public bool PassInterview { get; set; }
        public DateFilter BeginJobTime { get; set; }
        public DateFilter TrialTime { get; set; }
        public IdFilter StatusId { get; set; }
        public bool Used { get; set; }
        public InterviewResultOrder OrderBy { get; set; }
        public InterviewResultSelect Selects { get; set; }
    }
}
