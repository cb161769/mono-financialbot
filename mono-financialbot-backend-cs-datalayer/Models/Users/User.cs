


namespace mono_financialbot_backend_cs_datalayer.Models.Users
{
    public class User: IdentityUser
    {
        [MaxLength(50)]
        public string? FullName { get; set; }
        public DateTime CreatedAt { get; set; }
        [NotMapped]
        public string CreatedAtStr => CreatedAt.ToString("dd/MM/yyyyy");

        public bool IsDeleted { get; internal set; }
    }
}
