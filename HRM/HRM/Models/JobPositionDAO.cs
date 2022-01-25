using System;
using System.Collections.Generic;

namespace HRM.Models
{
    public partial class JobPositionDAO
    {
        public JobPositionDAO()
        {
            Employees = new HashSet<EmployeeDAO>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public long? StatusId { get; set; }
        public bool? Used { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual StatusDAO Status { get; set; }
        public virtual ICollection<EmployeeDAO> Employees { get; set; }
    }
}
