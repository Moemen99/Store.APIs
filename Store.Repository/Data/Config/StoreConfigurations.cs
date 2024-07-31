using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Store.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Data.Config
{
    internal class StoreConfigurations : IEntityTypeConfiguration<StoreInfo>
    {
        public void Configure(EntityTypeBuilder<StoreInfo> builder)
        {
            builder.Property(S => S.Id).ValueGeneratedOnAdd();

            builder.HasKey(S => S.Name);
            builder.HasMany(S => S.Goods).WithOne(G => G.Store).HasForeignKey(S => S.StoreName);

            //var dateOnlyConverter = new ValueConverter<DateOnly, DateTime>(
            //v => v.ToDateTime(TimeOnly.MinValue),
            // v => DateOnly.FromDateTime(v));

            //builder.Property(S => S.StoreFileDate)
            //    .HasConversion(dateOnlyConverter);
        }
    }
}
