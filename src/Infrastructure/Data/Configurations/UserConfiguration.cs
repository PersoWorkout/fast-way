﻿using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    internal class UserConfiguration: IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.OwnsOne(x => x.Email, email =>
            {
                email.Property(x => x.Value)
                    .HasColumnName("Email");

                email.HasIndex(x => x.Value)
                    .IsUnique();
            });     

            builder.ComplexProperty(x => x.Password)
                .Property(x => x.Value)
                .HasColumnName("password");
        }
    }
}
