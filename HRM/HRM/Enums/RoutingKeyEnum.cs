using TrueSight.Common;using HRM.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRM.Enums
{
    public class RoutingKeyEnum
    {
        public static GenericEnum AuditLogSend = new GenericEnum { Id = 298, Code = "AuditLog.Send", Name = "Audit Log" };
        public static GenericEnum SystemLogSend = new GenericEnum { Id = 299, Code = "SystemLog.Send", Name = "System Log" };
    }
}
