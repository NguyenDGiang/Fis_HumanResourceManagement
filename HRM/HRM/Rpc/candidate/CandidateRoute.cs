namespace HRM.Rpc.candidate
{
    public class CandidateRoute : Root
    {
        public const string Parent = Module + "/candidate";
        public const string Master = Module + "/candidate/candidate/candidate-master";
        public const string Detail = Module + "/candidate/candidate/candidate-detail/*";
        private const string Default = Rpc + Module + "/candidate";
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