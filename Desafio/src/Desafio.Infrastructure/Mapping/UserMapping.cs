using Desafio.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Desafio.Infrastructure.Mapping;

public class UserMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> user)
    {
        user.ToTable("user");

        user.Property(x => x.Id).HasColumnName("id").IsRequired();
        user.HasKey(x => x.Id);

        user.Property( x=> x.Name).HasColumnName("name").IsRequired();
        user.Property( x=> x.NickName).HasColumnName("nick_name");
        user.Property( x=> x.Email).HasColumnName("email");
        user.Property( x=> x.Document).HasColumnName("document");
        user.Property( x=> x.Password).HasColumnName("password").IsRequired();
        user.Property( x=> x.UserLevel).HasColumnName("user_level").IsRequired();

    }
}
