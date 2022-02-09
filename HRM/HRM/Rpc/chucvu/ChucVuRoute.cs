namespace HRM.Rpc.chucvu
{
    public class ChucVuRoute : Root
    {
        public const string Parent = Module + "/chuc-vu";
        public const string Master = Module + "/chuc-vu/chuc-vu/chuc-vu-master";
        public const string Detail = Module + "/chuc-vu/chuc-vu/chuc-vu-detail/*";
        private const string Default = Rpc + Module + "/chuc-vu";
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