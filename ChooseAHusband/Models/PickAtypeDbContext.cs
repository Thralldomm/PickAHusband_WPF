using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.EntityFrameworkCore;

namespace ChooseAHusband.Models;

public partial class PickAtypeDbContext : DbContext
{
    public PickAtypeDbContext()
    {
    }

    public PickAtypeDbContext(DbContextOptions<PickAtypeDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CelebsInfo> CelebsInfos { get; set; }

    public virtual DbSet<UserInfo> UserInfos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["DbConnectionString"].ToString());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CelebsInfo>(entity =>
        {
            entity.HasKey(e => e.CelebrityKod);

            entity.ToTable("CelebsInfo");

            entity.Property(e => e.CelebrityKod)
                .ValueGeneratedNever()
                .HasColumnName("Celebrity_kod");
            entity.Property(e => e.CelebAge).HasColumnName("Celeb_Age");
            entity.Property(e => e.CelebDescription)
                .HasColumnType("text")
                .HasColumnName("Celeb_Description");
            entity.Property(e => e.CelebHeight)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Celeb_Height");
            entity.Property(e => e.CelebMeaningOfChoice)
                .HasColumnType("text")
                .HasColumnName("Celeb_MeaningOfChoice");
            entity.Property(e => e.CelebName).HasColumnName("Celeb_Name");
            entity.Property(e => e.CelebWeight)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("Celeb_Weight");
        });

        modelBuilder.Entity<UserInfo>(entity =>
        {
            entity.HasKey(e => e.UserKod);

            entity.ToTable("UserInfo");

            entity.Property(e => e.UserKod)
                .ValueGeneratedNever()
                .HasColumnName("User_kod");
            entity.Property(e => e.UserEmail).HasColumnName("User_email");
            entity.Property(e => e.UserFirstChoice).HasColumnName("User_FirstChoice");
            entity.Property(e => e.UserLatestChoice).HasColumnName("User_LatestChoice");
            entity.Property(e => e.UserName)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("User_name");
            entity.Property(e => e.UserPassword)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("User_password");
            entity.Property(e => e.UserTimesOnApp)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("User_TimesOnApp");

            entity.HasOne(d => d.UserFirstChoiceNavigation).WithMany(p => p.UserInfoUserFirstChoiceNavigations)
                .HasForeignKey(d => d.UserFirstChoice)
                .HasConstraintName("FK_UserInfo_CelebsInfo");

            entity.HasOne(d => d.UserLatestChoiceNavigation).WithMany(p => p.UserInfoUserLatestChoiceNavigations)
                .HasForeignKey(d => d.UserLatestChoice)
                .HasConstraintName("FK_UserInfo_CelebsInfo1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
