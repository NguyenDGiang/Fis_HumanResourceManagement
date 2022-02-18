using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TrueSight.Common;

namespace HRM.Entities
{
    public class JobPosition : DataEntity, IEquatable<JobPosition>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public long? StatusId { get; set; }
        public bool Used { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public Status Status { get; set; }
        public List<Employee> Employees { get; set; }

        public bool Equals(JobPosition other)
        {
            return other != null && this.Id == other.Id;
        }
    }

    public class JobPositionFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter Code { get; set; }
        public IdFilter StatusId { get; set; }
        public bool? Used { get; set; }

        public List<JobPositionFilter> OrFilter { get; set; }
        public JobPositionOrder OrderBy { get; set; }
        public JobPositionSelect Selects { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum JobPositionOrder
    {
        Id = 0,
        Name = 1,
        Code = 2,
        Status = 3,
    }

    [Flags]
    public enum JobPositionSelect : long
    {
        ALL = E.ALL,
        Id = E._0,
        Name = E._1,
        Code = E._2,
        Status = E._3,
    }
}