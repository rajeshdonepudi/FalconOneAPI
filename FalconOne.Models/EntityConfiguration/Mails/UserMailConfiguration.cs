using FalconOne.Models.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FalconOne.Models.EntityConfiguration.Mails
{
    public class UserMailConfiguration : IEntityTypeConfiguration<UserMail>
    {
        public void Configure(EntityTypeBuilder<UserMail> builder)
        {
            builder.HasKey(tu => tu.Id);

            builder.HasOne(x => x.Mail)
                   .WithMany(x => x.Recipients)
                   .HasForeignKey(x => x.MailId)
                   .IsRequired(true)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(tu => tu.Recipient)
                   .WithMany(u => u.ReceivedMails)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired(true)
                   .HasForeignKey(tu => tu.RecipientId);
        }
    }
}
