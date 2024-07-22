using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Data.Config
{
    internal class GoodConfigurations : IEntityTypeConfiguration<Good>
    {
        public void Configure(EntityTypeBuilder<Good> builder)
        {
            builder.Property(G => G.Id).ValueGeneratedOnAdd();
            builder.Property(G => G.GoodID).IsRequired();
            builder.HasMany(G => G.Transactions).WithOne(T => T.Good).HasForeignKey(G => G.GoodID);
        }
    }
}
