

namespace mono_financialbot_backend_cs_datalayer.Core.Models
{
    public abstract class BaseModel : IBaseModel
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdateAt { get; set; }

        [NotMapped]
        public string CreatedAtStr => CreatedAt.ToString("dd/MM/yyyy");

        [NotMapped]
        public string UpdateAtStr => CreatedAt.ToString("dd/MM/yyyy");

        public bool IsDeleted { get; set; }

        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
    }
}