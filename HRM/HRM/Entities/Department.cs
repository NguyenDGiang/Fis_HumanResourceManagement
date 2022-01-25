using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TrueSight.Common;

namespace HRM.Entities
{
    public class Department : DataEntity, IEquatable<Department>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string PhoneNumber { get; set; }
        public long? StatusId { get; set; }
        public bool Used { get; set; }
        public long? VillageId { get; set; }
        public long? DistrictId { get; set; }
        public long? ProvinceId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual District District { get; set; }
        public virtual Province Province { get; set; }
        public virtual Status Status { get; set; }
        public virtual Village Village { get; set; }

        public string Search { get; set; }

        public bool Equals(Department other)
        {
            if (other == null) return false;
            if (this.Id != other.Id) return false;
            if (this.Name != other.Name) return false;
            if (this.Code != other.Code) return false;
            if (this.PhoneNumber != other.PhoneNumber) return false;
            if (this.StatusId != other.StatusId) return false;
            if (this.Used != other.Used) return false;
            if (this.VillageId != other.VillageId) return false;
            if (this.DistrictId != other.DistrictId) return false;
            if (this.ProvinceId != other.ProvinceId) return false;

            return true;
        }
    }

    public class DepartmentFilter
    {
        public IdFilter Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string PhoneNumber { get; set; }
        public long? StatusId { get; set; }
        public bool Used { get; set; }
        public IdFilter VillageId { get; set; }
        public IdFilter DistrictId { get; set; }
        public IdFilter ProvinceId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public List<DepartmentFilter> OrFilter { get; set; }
        public DepartmentOrder OrderBy { get; set; }
        public DepartmentSelect Selects { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum DepartmentOrder
    {
        Id = 0,
        Name = 1,
        Code = 2,
        PhoneNumber = 3,
        Status = 4,
        Village = 5,
        District = 6,
        Province = 7,

    }

    [Flags]
    public enum DepartmentSelect : long
    {
        ALL = E.ALL,
        Id = E._0,
        Name = E._1,
        Code = E._2,
        PhoneNumber = E._3,
        Status = E._4,
        Village = E._5,
        District = E._6,
        Province = E._7

    }

}