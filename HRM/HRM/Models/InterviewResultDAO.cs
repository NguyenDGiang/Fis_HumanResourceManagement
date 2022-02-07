using System;
using System.Collections.Generic;

namespace HRM.Models
{
    public partial class InterviewResultDAO
    {
        public long Id { get; set; }
        public long? CandidateId { get; set; }
        public DateTime InterviewTime { get; set; }
        public bool PassInterview { get; set; }
        public DateTime BeginJobTime { get; set; }
        public DateTime TrialTime { get; set; }
        public long? StatusId { get; set; }
        public bool Used { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual CandidateDAO Candidate { get; set; }
        public virtual StatusDAO Status { get; set; }
    }
}
