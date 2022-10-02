namespace mono_financialbot_backend_cs_datalayer.Core.Dtos
{
    public abstract class BaseDtoModel
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedAtStr { get; set; }
        public string? CreatedBy { get; set; }
    }
}
