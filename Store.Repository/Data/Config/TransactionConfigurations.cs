using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Store.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Data.Config
{
    internal class TransactionConfigurations : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.Property(T => T.Id).ValueGeneratedOnAdd();

            builder.HasKey(T => T.TransactionID);
            builder.Property(T => T.TransactionID).IsRequired();
            builder.Property(T => T.Comment).IsRequired(false);

            //var dateOnlyConverter = new ValueConverter<DateOnly, DateTime>(
            //v => v.ToDateTime(TimeOnly.MinValue),
            // v => DateOnly.FromDateTime(v));

            //builder.Property(T => T.TransactionDate)
            //    .HasConversion(dateOnlyConverter);



        }
    }
}
