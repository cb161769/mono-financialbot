


namespace mono_financialbot_backend_cs_datalayer.Persistence.Context
{
    /// <summary>
    /// class 
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            if (Database.IsRelational() && Database.GetPendingMigrations().Any())
                Database.Migrate();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //apply all ef configurations and filters
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BaseEFConfiguration<>).Assembly);
        }
    }
}
