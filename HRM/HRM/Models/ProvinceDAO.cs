using System;
using System.Collections.Generic;

namespace HRM.Models
{
    public partial class ProvinceDAO
    {
        public ProvinceDAO()
        {
            Candidates = new HashSet<CandidateDAO>();
            Departments = new HashSet<DepartmentDAO>();
            Districts = new HashSet<DistrictDAO>();
            Employees = new HashSet<EmployeeDAO>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public long? StatusId { get; set; }
        public bool Used { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual StatusDAO Status { get; set; }
        public virtual ICollection<CandidateDAO> Candidates { get; set; }
        public virtual ICollection<DepartmentDAO> Departments { get; set; }
        public virtual ICollection<DistrictDAO> Districts { get; set; }
        public virtual ICollection<EmployeeDAO> Employees { get; set; }
    }
}
