namespace mono_financialbot_backend_cs_datalayer.Persistence.Core
{
    /// <summary>
    /// Represents an entity frameworks configuration base model
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class BaseEFConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
           where TEntity : BaseModel
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            else
            {
            builder.HasQueryFilter(x => !x.IsDeleted);
            ConfigureEF(builder);

            }

        }

        public abstract void ConfigureEF(EntityTypeBuilder<TEntity> builder);
    }
}