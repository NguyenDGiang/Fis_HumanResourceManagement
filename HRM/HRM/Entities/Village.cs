using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TrueSight.Common;

namespace HRM.Entities
{
    public class Village : DataEntity, IEquatable<Province>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public long? DistrictId { get; set; }
        public long? StatusId { get; set; }
        public Guid RowId { get; set; }

        public bool Used { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public District District { get; set; }
        public Status Status { get; set; }
        public List<Candidate> Candidates { get; set; }
        public List<Department> Departments { get; set; }
        public List<Employee> Employees { get; set; }

        public bool Equals(Province other)
        {
            return other != null && Id == other.Id;
        }
    }

    public class VillageFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter Code { get; set; }
        public IdFilter DistrictId { get; set; }
        public IdFilter StatusId { get; set; }

        public List<VillageFilter> OrFilter { get; set; }
        public VillageOrder OrderBy { get; set; }
        public VillageSelect Selects { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum VillageOrder
    {
        Id = 0,
        Name = 1,
        Code = 2,
        District = 3,
        Status = 4,
    }

    [Flags]
    public enum VillageSelect : long
    {
        ALL = E.ALL,
        Id = E._0,
        Name = E._1,
        Code = E._2,
        District = E._3,
        Status = E._4,
    }
}