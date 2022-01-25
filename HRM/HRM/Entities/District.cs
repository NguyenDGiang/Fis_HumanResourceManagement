using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TrueSight.Common;

namespace HRM.Entities
{
    public class District : DataEntity, IEquatable<District>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public long? ProvinceId { get; set; }
        public long? StatusId { get; set; }
        public bool Used { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public Province Province { get; set; }
        public Status Status { get; set; }
        public List<Candidate> Candidates { get; set; }
        public List<Department> Departments { get; set; }
        public List<Employee> Employees { get; set; }
        public List<Village> Villages { get; set; }

        public bool Equals(District other)
        {
            return other != null && this.Id == other.Id;
        }
    }

    public class DistrictFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter Code { get; set; }
        public IdFilter ProvinceId { get; set; }
        public IdFilter StatusId { get; set; }

        public List<DistrictFilter> OrFilter { get; set; }
        public DistrictOrder OrderBy { get; set; }
        public DistrictSelect Selects { get; set; }

    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum DistrictOrder
    {
        Id = 0,
        Name = 1,
        Code = 2,
        Province = 3,
        Status = 4,
    }

    public enum DistrictSelect : long
    {
        ALL = E.ALL,
        Id = E._0,
        Name = E._1,
        Code = E._2,
        Province = E._3,
        Status = E._4,
    }
}