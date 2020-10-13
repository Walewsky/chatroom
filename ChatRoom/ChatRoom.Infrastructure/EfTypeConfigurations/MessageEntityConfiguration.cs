using ChatRoom.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatRoom.Infrastructure.EfTypeConfigurations
{
    public class MessageEntityConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("messages");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("Id")
                .HasColumnType("INT")
                .IsRequired()
                .UseIdentityColumn();

            builder.Property(e => e.User)
                .HasColumnName("User")
                .HasColumnType("NVARCHAR(32)")
                .IsRequired();

            builder.Property(e => e.Content)
                .HasColumnName("Content")
                .HasColumnType("NVARCHAR(512)")
                .IsRequired();

            builder.Property(e => e.Timestamp)
                .HasColumnName("Timestamp")
                .HasColumnType("DATETIME2")
                .IsRequired();
        }
    }
}
