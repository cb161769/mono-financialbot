namespace mono_financialbot_backend_cs_datalayer.Models.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<Users.User>
    {
        public void Configure(EntityTypeBuilder<Users.User> builder)
        {
            builder.HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
