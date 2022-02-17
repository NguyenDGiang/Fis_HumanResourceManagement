using System;
using System.Collections.Generic;

namespace HRM.Models
{
    public partial class DistrictDAO
    {
        public DistrictDAO()
        {
            Candidates = new HashSet<CandidateDAO>();
            Departments = new HashSet<DepartmentDAO>();
            Employees = new HashSet<EmployeeDAO>();
            Villages = new HashSet<VillageDAO>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public long? ProvinceId { get; set; }
        public long? StatusId { get; set; }
        public bool Used { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ProvinceDAO Province { get; set; }
        public virtual StatusDAO Status { get; set; }
        public virtual ICollection<CandidateDAO> Candidates { get; set; }
        public virtual ICollection<DepartmentDAO> Departments { get; set; }
        public virtual ICollection<EmployeeDAO> Employees { get; set; }
        public virtual ICollection<VillageDAO> Villages { get; set; }
    }
}
