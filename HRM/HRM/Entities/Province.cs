using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TrueSight.Common;

namespace HRM.Entities
{
    public class Province : DataEntity, IEquatable<Province>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public long? StatusId { get; set; }
        public Guid RowId { get; set; }

        public bool Used { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual Status Status { get; set; }
        public virtual List<Candidate> Candidates { get; set; }
        public virtual List<Department> Departments { get; set; }
        public virtual List<District> Districts { get; set; }
        public virtual List<Employee> Employees { get; set; }

        public bool Equals(Province other)
        {
            return other != null && Id == other.Id;
        }
    }

    public class ProvinceFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter Code { get; set; }
        public IdFilter StatusId { get; set; }

        public List<ProvinceFilter> OrFilter { get; set; }
        public ProvinceOrder OrderBy { get; set; }
        public ProvinceSelect Selects { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum ProvinceOrder
    {
        Id = 0,
        Name = 1,
        Code = 2,
        Status = 3,
    }

    public enum ProvinceSelect : long
    {
        ALL = E.ALL,
        Id = E._0,
        Name = E._1,
        Code = E._2,
        Status = E._3,
        Districts = E._4,
        RowId = E._5
    }
}