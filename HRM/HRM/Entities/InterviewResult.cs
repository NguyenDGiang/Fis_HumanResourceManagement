using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TrueSight.Common;

namespace HRM.Entities
{
    public class InterviewResult : DataEntity, IEquatable<InterviewResult>
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

        public Candidate Candidate { get; set; }
        public Status Status { get; set; }

        public bool Equals([AllowNull] InterviewResult other)
        {
            return other != null && Id == other.Id;
        }
    }

    public class InterviewResultFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public IdFilter CandidateId { get; set; }
        public DateTime InterviewTime { get; set; }
        public bool PassInterview { get; set; }
        public DateTime BeginJobTime { get; set; }
        public DateTime TrialTime { get; set; }
        public IdFilter StatusId { get; set; }
        public bool Used { get; set; }

        public List<InterviewResultFilter> OrFilter { get; set; }
        public InterviewResultOrder OrderBy { get; set; }
        public InterviewResultSelect Selects { get; set; }
    }


    [JsonConverter(typeof(StringEnumConverter))]
    public enum InterviewResultOrder
    {
        Id = 0,
        CandidateId = 1,
        InterviewTime = 2,
        PassInterview = 3,
        BeginJobTime = 4,
        TrialTime = 5,
        Status = 6,
    }

    [Flags]
    public enum InterviewResultSelect : long
    {
        ALL = E.ALL,
        Id = E._0,
        CandidateId = E._1,
        InterviewTime = E._2,
        PassInterview = E._3,
        BeginJobTime = E._4,
        TrialTime = E._5,
        Status = E._6,
    }
}