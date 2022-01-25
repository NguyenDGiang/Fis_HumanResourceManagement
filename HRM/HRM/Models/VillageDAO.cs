using System;
using System.Collections.Generic;

namespace HRM.Models
{
    public partial class VillageDAO
    {
        public VillageDAO()
        {
            Candidates = new HashSet<CandidateDAO>();
            Departments = new HashSet<DepartmentDAO>();
            Employees = new HashSet<EmployeeDAO>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public long? DistrictId { get; set; }
        public long? StatusId { get; set; }

        public virtual DistrictDAO District { get; set; }
        public virtual StatusDAO Status { get; set; }
        public virtual ICollection<CandidateDAO> Candidates { get; set; }
        public virtual ICollection<DepartmentDAO> Departments { get; set; }
        public virtual ICollection<EmployeeDAO> Employees { get; set; }
    }
}
