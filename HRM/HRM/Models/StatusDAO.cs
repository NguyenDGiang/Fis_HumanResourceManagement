using System;
using System.Collections.Generic;

namespace HRM.Models
{
    public partial class StatusDAO
    {
        public StatusDAO()
        {
            AcademicLevels = new HashSet<AcademicLevelDAO>();
            Candidates = new HashSet<CandidateDAO>();
            ChucVus = new HashSet<ChucVuDAO>();
            Departments = new HashSet<DepartmentDAO>();
            Districts = new HashSet<DistrictDAO>();
            Employees = new HashSet<EmployeeDAO>();
            InterviewResults = new HashSet<InterviewResultDAO>();
            JobPositions = new HashSet<JobPositionDAO>();
            Provinces = new HashSet<ProvinceDAO>();
            Villages = new HashSet<VillageDAO>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<AcademicLevelDAO> AcademicLevels { get; set; }
        public virtual ICollection<CandidateDAO> Candidates { get; set; }
        public virtual ICollection<ChucVuDAO> ChucVus { get; set; }
        public virtual ICollection<DepartmentDAO> Departments { get; set; }
        public virtual ICollection<DistrictDAO> Districts { get; set; }
        public virtual ICollection<EmployeeDAO> Employees { get; set; }
        public virtual ICollection<InterviewResultDAO> InterviewResults { get; set; }
        public virtual ICollection<JobPositionDAO> JobPositions { get; set; }
        public virtual ICollection<ProvinceDAO> Provinces { get; set; }
        public virtual ICollection<VillageDAO> Villages { get; set; }
    }
}
