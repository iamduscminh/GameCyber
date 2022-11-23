using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GameCyber.Entities;

public partial class GameProjectContext : DbContext
{
    public GameProjectContext()
    {
    }

    public GameProjectContext(DbContextOptions<GameProjectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Chat> Chats { get; set; }

    public virtual DbSet<Game> Games { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserChat> UserChats { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        IConfigurationRoot configuration = builder.Build();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("ApConStr"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Chat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CHAT__3214EC27CEC7648E");

            entity.ToTable("CHAT");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Message)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("MESSAGE");
            entity.Property(e => e.SentDate)
                .HasColumnType("datetime")
                .HasColumnName("SENT_DATE");
        });

        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GAME__3214EC27C3A213FC");

            entity.ToTable("GAME");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Author)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("AUTHOR");
            entity.Property(e => e.Description)
                .HasMaxLength(5000)
                .IsUnicode(false)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("IMAGE_URL");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.Rating).HasColumnName("RATING");
            entity.Property(e => e.Title)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("TITLE");
            entity.Property(e => e.Upload)
                .HasColumnType("date")
                .HasColumnName("UPLOAD");
            entity.Property(e => e.Url)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("URL");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__USER__3214EC27B7C6B2FB");

            entity.ToTable("USER");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Cash).HasColumnName("CASH");
            entity.Property(e => e.Connection)
                .HasMaxLength(555)
                .IsUnicode(false)
                .HasColumnName("CONNECTION");
            entity.Property(e => e.Fullname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FULLNAME");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PASSWORD");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ROLE");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("USERNAME");
        });

        modelBuilder.Entity<UserChat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__USER_CHA__3214EC27CA0950BA");

            entity.ToTable("USER_CHAT");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ChatId).HasColumnName("CHAT_ID");
            entity.Property(e => e.UserRecievedId).HasColumnName("USER_RECIEVED_ID");
            entity.Property(e => e.UserSentId).HasColumnName("USER_SENT_ID");

            entity.HasOne(d => d.Chat).WithMany(p => p.UserChats)
                .HasForeignKey(d => d.ChatId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__USER_CHAT__CHAT___3C69FB99");

            entity.HasOne(d => d.UserRecieved).WithMany(p => p.UserChatUserRecieveds)
                .HasForeignKey(d => d.UserRecievedId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__USER_CHAT__USER___3B75D760");

            entity.HasOne(d => d.UserSent).WithMany(p => p.UserChatUserSents)
                .HasForeignKey(d => d.UserSentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__USER_CHAT__USER___3A81B327");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
