using System.Collections.Generic;
using TrueSight.Common;

namespace HRM.Enums
{
    public class StatusEnum
    {
        public static GenericEnum ACTIVE = new GenericEnum { Id = 1, Code = "Active", Name = "Hoạt động" };
        public static GenericEnum INACTIVE = new GenericEnum { Id = 2, Code = "Inactive", Name = "Dừng hoạt động" };

        public static List<GenericEnum> StatusEnumList = new List<GenericEnum>
        {
            INACTIVE, ACTIVE
        };
    }
}