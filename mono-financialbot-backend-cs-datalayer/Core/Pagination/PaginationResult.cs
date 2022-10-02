namespace mono_financialbot_backend_cs_datalayer.Core.Pagination
{
    public class PaginationResult<T> where T : class
    {
        public int ActualPage { get; set; } = 1;
        public int Qyt { get; set; }
        public int PageTotal { get; set; }
        public int Total { get; set; }
        public virtual IEnumerable<string>? OrderOptions { get; set; }
        public virtual IReadOnlyCollection<T>? Results { get; set; }
    }
}
