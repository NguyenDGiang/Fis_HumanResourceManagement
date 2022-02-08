namespace HRM.Rpc.department
{
    public class DepartmentRoute : Root
    {
        public const string Parent = Module + "/department";
        public const string Master = Module + "/department/department/department-master";
        public const string Detail = Module + "/department/department/department-detail/*";
        private const string Default = Rpc + Module + "/department";
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Get = Default + "/get";
        public const string Create = Default + "/create";
        public const string Update = Default + "/update";
        public const string Delete = Default + "/delete";
        public const string Import = Default + "/import";
        public const string Export = Default + "/export";
        public const string BulkDelete = Default + "/bulk-delete";
    }
}