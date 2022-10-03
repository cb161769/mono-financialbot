

namespace mono_financialbot_backend_cs_datalayer.Core.Models
{
    public abstract class BaseInputModel
    {
        [JsonIgnore]
        public DateTime CreatedAt { get; set; }


        [JsonIgnore]
        public string? CreatedBy { get; set; }
    }
}
