using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Mapping
{
    public class PersonMap : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("People");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
            builder.Property(x => x.UrlPhoto).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Phone).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Birthday).IsRequired().HasMaxLength(100);
            builder.Property(x => x.StateId).IsRequired().HasMaxLength(100);
            builder.Property(x => x.CountryId).IsRequired().HasMaxLength(100);

            builder.HasMany<Person>(x => x.Friends);
        }
    }
}
