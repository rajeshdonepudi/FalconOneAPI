using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FalconOne.Models.EntityConfiguration.Mails
{
    public class MailConfiguration : IEntityTypeConfiguration<Entities.Mails.Mail>
    {
        public void Configure(EntityTypeBuilder<Entities.Mails.Mail> builder)
        {
            builder.HasOne(x => x.Sender)
                   .WithMany(x => x.SentMails)
                   .HasForeignKey(x => x.SenderId)
                   .IsRequired(true)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
