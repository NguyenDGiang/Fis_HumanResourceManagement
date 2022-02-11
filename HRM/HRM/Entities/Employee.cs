using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TrueSight.Common;

namespace HRM.Entities
{
    public class Employee : DataEntity, IEquatable<Employee>
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime BeginJobTime { get; set; }
        public bool ContractStatus { get; set; }
        public long? StatusId { get; set; }
        public long? ChucVuId { get; set; }
        public long? AcademicLevelId { get; set; }
        public long? JobPositionId { get; set; }
        public long? VillageId { get; set; }
        public long? DistrictId { get; set; }
        public long? ProvinceId { get; set; }
        public bool Used { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public AcademicLevel AcademicLevel { get; set; }
        public ChucVu ChucVu { get; set; }
        public District District { get; set; }
        public JobPosition JobPosition { get; set; }
        public Province Province { get; set; }
        public Status Status { get; set; }
        public Village Village { get; set; }

        public bool Equals(Employee other)
        {
            return other != null && this.Id == other.Id;
        }
    }

    public class EmployeeFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter FullName { get; set; }
        public StringFilter Email { get; set; }
        public StringFilter PhoneNumber { get; set; }
        public DateFilter Birthday { get; set; }
        public DateFilter BeginJobTime { get; set; }
        public bool ContractStatus { get; set; }
        public IdFilter StatusId { get; set; }
        public IdFilter ChucVuId { get; set; }
        public IdFilter AcademicLevelId { get; set; }
        public IdFilter JobPositionId { get; set; }
        public IdFilter VillageId { get; set; }
        public IdFilter DistrictId { get; set; }
        public IdFilter ProvinceId { get; set; }

        public List<EmployeeFilter> OrFilter { get; set; }
        public EmployeeOrder OrderBy { get; set; }
        public EmployeeSelect Selects { get; set; }
        public string Search { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum EmployeeOrder
    {
        Id = 0,
        FullName = 1,
        Email = 2,
        PhoneNumber = 3,
        Birthday = 4,
        BeginJobTime = 5,
        ContractStatus = 6,
        Status = 7,
        ChucVu = 8,
        AcademicLevel = 9,
        JobPosition = 10,
        Village = 11,
        District = 12,
        Province = 13,
    }

    [Flags]
    public enum EmployeeSelect : long
    {
        ALL = E.ALL,
        Id = E._0,
        FullName = E._1,
        Email = E._2,
        PhoneNumber = E._3,
        Birthday = E._4,
        BeginJobTime = E._5,
        ContractStatus = E._6,
        Status = E._7,
        ChucVu = E._8,
        AcademicLevel = E._9,
        JobPosition = E._10,
        Village = E._11,
        District = E._12,
        Province = E._13,

    }

}