namespace HRM.Rpc.academic_level
{
    public class AcademicLevelRoute : Root
    {
        public const string Parent = Module + "/academic-level";
        public const string Master = Module + "/academic-level/academic-level/academic-level-master";
        public const string Detail = Module + "/academic-level/academic-level/academic-level-detail/*";
        private const string Default = Rpc + Module + "/academic-level";
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