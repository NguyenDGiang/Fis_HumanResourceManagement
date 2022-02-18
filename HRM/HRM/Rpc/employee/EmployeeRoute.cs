namespace HRM.Rpc.employee
{
    public class EmployeeRoute:Root
    {
        private const string Default = Rpc + Module + "/employee";
        public const string Count = Default + "/count";
        public const string List = Default + "/list";
        public const string Get = Default + "/get";
        public const string Create = Default + "/create";
        public const string Update = Default + "/update";
        public const string Delete = Default + "/delete";
    }
}
