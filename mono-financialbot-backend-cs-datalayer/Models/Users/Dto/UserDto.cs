namespace mono_financialbot_backend_cs_datalayer.Models.User.Dto
{
    public class UserDto: IdentityUser
    {
        public string? FullName { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; internal set; }
    }
}
