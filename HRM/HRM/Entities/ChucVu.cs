using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TrueSight.Common;

namespace HRM.Entities
{
    public class ChucVu : DataEntity, IEquatable<ChucVu>
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


        public bool Equals(ChucVu other)
        {
            if (other == null) return false;
            if (this.Id != other.Id) return false;
            if (this.Name != other.Name) return false;
            if (this.Code != other.Code) return false;
            if (this.StatusId != other.StatusId) return false;
            if (this.Used != other.Used) return false;

            return true;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class ChucVuFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter Code { get; set; }
        public IdFilter StatusId { get; set; }
        public List<ChucVuFilter> OrFilter { get; set; }
        public ChucVuOrder OrderBy { get; set; }
        public ChucVuSelect Selects { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum ChucVuOrder
    {
        Id = 0,
        Name = 1,
        Code = 2,
        Status = 3,
    }
    public enum ChucVuSelect : long
    {
        ALL = E.ALL,
        Id = E._0,
        Name = E._1,
        Code = E._2,
        Status = E._3
    }
}