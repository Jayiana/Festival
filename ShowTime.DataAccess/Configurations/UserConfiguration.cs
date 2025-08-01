﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShowTime.DataAccess.Models.UserInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime.DataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Password).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Role).IsRequired();
            builder.Property(u => u.ProfilePictureData).HasColumnType("varbinary(max)").IsRequired(false);
            builder.Property(u => u.PhoneNumber).HasMaxLength(32).IsRequired(false);
            builder.Property(u => u.Address).HasMaxLength(256).IsRequired(false);
            builder.Property(u => u.DateOfBirth).IsRequired(false);
            builder.Property(u => u.Bio).HasMaxLength(1024).IsRequired(false);
            builder.Property(u => u.Instagram).HasMaxLength(128).IsRequired(false);
            builder.Property(u => u.Facebook).HasMaxLength(128).IsRequired(false);
        }
    }

}
