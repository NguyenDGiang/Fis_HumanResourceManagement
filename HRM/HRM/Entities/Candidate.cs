using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TrueSight.Common;

namespace HRM.Entities
{
    public class Candidate : DataEntity, IEquatable<Candidate>
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
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public District District { get; set; }
        public Province Province { get; set; }
        public Village Village { get; set; }
        public Status Status { get; set; }
        public List<InterviewResult> InterviewResults { get; set; }

        public bool Equals(Candidate other)
        {
            if (other == null) return false;
            if (this.Id != other.Id) return false;
            if (this.FullName != other.FullName) return false;
            if (this.Email != other.Email) return false;
            if (this.PhoneNumber != other.PhoneNumber) return false;
            if (this.Birthday != other.Birthday) return false;
            if (this.StatusId != other.StatusId) return false;
            if (this.Used != other.Used) return false;

            return true;
        }
    }

    public class CandidateFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter FullName { get; set; }
        public StringFilter Email { get; set; }
        public StringFilter PhoneNumber { get; set; }
        public DateTime Birthday { get; set; }
        public IdFilter StatusId { get; set; }
        public bool Used { get; set; }
        public IdFilter VillageId { get; set; }
        public IdFilter DistrictId { get; set; }
        public IdFilter ProvinceId { get; set; }

        public List<CandidateFilter> OrFilter { get; set; }
        public CandidateOrder OrderBy { get; set; }
        public CandidateSelect Selects { get; set; }

    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum CandidateOrder
    {
        Id = 0,
        FullName = 1,
        Email = 2,
        PhoneNumber = 3,
        Birthday = 4,
        Status = 5,
        Village = 6,
        District = 7,
        Province = 8,

    }

    [Flags]
    public enum CandidateSelect : long
    {
        ALL = E.ALL,
        Id = E._0,
        FullName = E._1,
        Email = E._2,
        PhoneNumber = E._3,
        Birthday = E._4,
        Status = E._5,
        Village = E._6,
        District = E._7,
        Province = E._8,
    }

}