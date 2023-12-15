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

        user.Property( x=> x.Name).HasColumnName("name").HasMaxLength(200).IsRequired();
        user.Property( x=> x.NickName).HasColumnName("nick_name").HasMaxLength(100).IsRequired();
        user.Property( x=> x.Email).HasColumnName("email").HasMaxLength(100).IsRequired();
        user.Property( x=> x.Document).HasColumnName("document").HasMaxLength(14).IsRequired();
        user.Property( x=> x.Password).HasColumnName("password").HasMaxLength(20).IsRequired();
        user.Property( x=> x.UserLevel).HasColumnName("user_level").IsRequired();

    }
}
