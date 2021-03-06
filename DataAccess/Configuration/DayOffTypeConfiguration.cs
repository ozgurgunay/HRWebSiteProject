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
    public class DayOffTypeConfiguration : IEntityTypeConfiguration<DayOffType>
    {
        public void Configure(EntityTypeBuilder<DayOffType> builder)
        {
            builder.HasKey(x => x.DayOffTypeID);

            builder.Property(x => x.TypeName)
                .HasMaxLength(150)
                .IsRequired();
            builder.Property(x => x.Description)
                .HasMaxLength(200);
                

            builder.ToTable("DayOffTypes");
        }
    }
}
