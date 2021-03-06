using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configuration
{
    public class FileConfiguration : IEntityTypeConfiguration<File>
    {
        public void Configure(EntityTypeBuilder<File> builder)
        {
            builder.HasKey(x => x.FileID);

            builder.Property(x => x.FileName)
                .HasMaxLength(100)
                .IsRequired();       

            builder.Property(x => x.FileTypeID)
                .IsRequired();

            builder.ToTable("Files");

            builder.HasOne(x => x.User)
                .WithMany(x => x.Files)
                .HasForeignKey(x => x.UserID);

            builder.Property(x => x.Files)
                .HasColumnType("image");

            builder.HasOne(x => x.FileType)
                .WithMany(x => x.Files)
                .HasForeignKey(x => x.FileTypeID);
        }
    }
}
