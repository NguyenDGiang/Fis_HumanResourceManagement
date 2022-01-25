using System;
using System.Collections.Generic;

namespace HRM.Models
{
    public partial class CandidateDAO
    {
        public CandidateDAO()
        {
            InterviewResults = new HashSet<InterviewResultDAO>();
        }

        public long Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public long? StatusId { get; set; }
        public bool Used { get; set; }
        public long? VillageId { get; set; }
        public long? DistrictId { get; set; }
        public long? ProvinceId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime? Birthday { get; set; }

        public virtual DistrictDAO District { get; set; }
        public virtual ProvinceDAO Province { get; set; }
        public virtual StatusDAO Status { get; set; }
        public virtual VillageDAO Village { get; set; }
        public virtual ICollection<InterviewResultDAO> InterviewResults { get; set; }
    }
}
