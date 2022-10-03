namespace mono_financialbot_backend_cs_datalayer.Core.Models
{
    public abstract class BaseEditModel
    {
        public Guid Id { get; set; }

        [JsonIgnore]
        public DateTime CreatedAt { get; set; }

        [JsonIgnore]
        public DateTime UpdateAt { get; set; }

        [JsonIgnore]
        public string? CreatedBy { get; set; }

        [JsonIgnore]
        public string? UpdatedBy { get; set; }
    }
}
