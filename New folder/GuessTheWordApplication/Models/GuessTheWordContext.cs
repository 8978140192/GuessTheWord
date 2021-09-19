using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace GuessTheWordApplication.Models
{
    public partial class GuessTheWordContext : DbContext
    {
        public GuessTheWordContext()
        {
        }

        public GuessTheWordContext(DbContextOptions<GuessTheWordContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ScoreBoard> ScoreBoards { get; set; }
        public virtual DbSet<UserAssignedWord> UserAssignedWords { get; set; }
        public virtual DbSet<UserDetail> UserDetails { get; set; }
        public virtual DbSet<Word> Words { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data source=KANINI-LTP-455\\SQLSERVER2019G3;user id=sa;password=Admin@123;Initial catalog=GuessTheWord");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<ScoreBoard>(entity =>
            {
                entity.HasKey(e => e.UserName)
                    .HasName("PK__ScoreBoa__66DCF95D4D29AABB");

                entity.ToTable("ScoreBoard");

                entity.Property(e => e.UserName)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("userName");

                entity.Property(e => e.Score).HasColumnName("score");

                entity.HasOne(d => d.UserNameNavigation)
                    .WithOne(p => p.ScoreBoard)
                    .HasForeignKey<ScoreBoard>(d => d.UserName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ScoreBoar__userN__3D5E1FD2");
            });

            modelBuilder.Entity<UserAssignedWord>(entity =>
            {
                entity.HasKey(e => new { e.Word, e.ToUser })
                    .HasName("PK__UserAssi__D6DC61EF09110A4C");

                entity.ToTable("UserAssignedWord");

                entity.Property(e => e.Word)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("word");

                entity.Property(e => e.ToUser)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("toUser");

                entity.Property(e => e.FromUser)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("fromUser");

                entity.HasOne(d => d.FromUserNavigation)
                    .WithMany(p => p.UserAssignedWordFromUserNavigations)
                    .HasForeignKey(d => d.FromUser)
                    .HasConstraintName("FK__UserAssig__fromU__412EB0B6");

                entity.HasOne(d => d.ToUserNavigation)
                    .WithMany(p => p.UserAssignedWordToUserNavigations)
                    .HasForeignKey(d => d.ToUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserAssig__toUse__403A8C7D");
            });

            modelBuilder.Entity<UserDetail>(entity =>
            {
                entity.HasKey(e => e.UserName)
                    .HasName("PK__UserDeta__66DCF95D678AF15E");

                entity.Property(e => e.UserName)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("userName");

                entity.Property(e => e.UserContact)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("userContact");

                entity.Property(e => e.UserFullName)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("userFullName");

                entity.Property(e => e.UserPassword)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("userPassword");
            });

            modelBuilder.Entity<Word>(entity =>
            {
                entity.HasKey(e => e.Word1)
                    .HasName("PK__Words__8397405560CD9555");

                entity.Property(e => e.Word1)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("word");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
