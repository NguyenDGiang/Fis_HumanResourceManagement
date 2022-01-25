using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TrueSight.Common;

namespace HRM.Entities
{
    public class Status : DataEntity, IEquatable<Status>
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public bool Equals(Status other)
        {
            return other != null && Id == other.Id;
        }
    }
    public class StatusFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public List<StatusFilter> OrFilter { get; set; }
        public StatusOrder OrderBy { get; set; }
        public StatusSelect Selects { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum StatusOrder
    {
        Id = 0,
        Code = 1,
        Name = 2,
    }

    [Flags]
    public enum StatusSelect : long
    {
        ALL = E.ALL,
        Id = E._0,
        Code = E._1,
        Name = E._2,
    }
}

