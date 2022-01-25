using System;
using System.Collections.Generic;

namespace HRM.Models
{
    public partial class EmployeeDAO
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

        public virtual AcademicLevelDAO AcademicLevel { get; set; }
        public virtual ChucVuDAO ChucVu { get; set; }
        public virtual DistrictDAO District { get; set; }
        public virtual JobPositionDAO JobPosition { get; set; }
        public virtual ProvinceDAO Province { get; set; }
        public virtual StatusDAO Status { get; set; }
        public virtual VillageDAO Village { get; set; }
    }
}
