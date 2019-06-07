using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Spatem.Core.Models;

namespace Spatem.Data.Ef.Configurations
{
    public class WalletConfiguration : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> builder)
        {
            builder.HasKey(w => w.WalletId);
            builder.Property(w => w.Created)
            .IsRequired()
            .HasColumnType("Date")
            .HasDefaultValueSql("GetDate()");
        }
    }
}