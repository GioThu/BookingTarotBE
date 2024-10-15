using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TarotBooking.Models
{
    public partial class BookingTarotContext : DbContext
    {
        public BookingTarotContext()
        {
        }

        public BookingTarotContext(DbContextOptions<BookingTarotContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Booking> Bookings { get; set; } = null!;
        public virtual DbSet<BookingTopic> BookingTopics { get; set; } = null!;
        public virtual DbSet<Card> Cards { get; set; } = null!;
        public virtual DbSet<CardGame> CardGames { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<Follow> Follows { get; set; } = null!;
        public virtual DbSet<GroupCard> GroupCards { get; set; } = null!;
        public virtual DbSet<Image> Images { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<Reader> Readers { get; set; } = null!;
        public virtual DbSet<ReaderTopic> ReaderTopics { get; set; } = null!;
        public virtual DbSet<Topic> Topics { get; set; } = null!;
        public virtual DbSet<Transaction> Transactions { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserGroupCard> UserGroupCards { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("workstation id=BookingTarot.mssql.somee.com;packet size=4096;user id=htxtrungkien2002_SQLLogin_1;pwd=r544e9k2rt;data source=BookingTarot.mssql.somee.com;persist security info=False;initial catalog=BookingTarot;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>(entity =>
            {
                entity.ToTable("Booking");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasColumnName("create_at");

                entity.Property(e => e.Feedback)
                    .HasColumnType("text")
                    .HasColumnName("feedback");

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.Property(e => e.ReaderId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("reader_id");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.TimeEnd)
                    .HasColumnType("datetime")
                    .HasColumnName("time_end");

                entity.Property(e => e.TimeStart)
                    .HasColumnType("datetime")
                    .HasColumnName("time_start");

                entity.Property(e => e.Total).HasColumnName("total");

                entity.Property(e => e.UserId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("user_id");

                entity.HasOne(d => d.Reader)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.ReaderId)
                    .HasConstraintName("FK__Booking__reader___44FF419A");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Booking__user_id__440B1D61");
            });

            modelBuilder.Entity<BookingTopic>(entity =>
            {
                entity.ToTable("Booking_Topic");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.BookingId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("booking_id");

                entity.Property(e => e.TopicId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("topic_id");

                entity.HasOne(d => d.Booking)
                    .WithMany(p => p.BookingTopics)
                    .HasForeignKey(d => d.BookingId)
                    .HasConstraintName("FK__Booking_T__booki__6754599E");

                entity.HasOne(d => d.Topic)
                    .WithMany(p => p.BookingTopics)
                    .HasForeignKey(d => d.TopicId)
                    .HasConstraintName("FK__Booking_T__topic__68487DD7");
            });

            modelBuilder.Entity<Card>(entity =>
            {
                entity.ToTable("Card");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasColumnName("create_at");

                entity.Property(e => e.Element)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("element");

                entity.Property(e => e.GroupId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("group_id");

                entity.Property(e => e.IsRightWay).HasColumnName("is_right_way");

                entity.Property(e => e.Message)
                    .HasColumnType("text")
                    .HasColumnName("message");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Cards)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK__Card__group_id__412EB0B6");
            });

            modelBuilder.Entity<CardGame>(entity =>
            {
                entity.ToTable("Card_Game");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.BookingId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("booking_id");

                entity.Property(e => e.CardId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("card_id");

                entity.Property(e => e.Createdate)
                    .HasColumnType("datetime")
                    .HasColumnName("createdate");

                entity.Property(e => e.Status)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.HasOne(d => d.Booking)
                    .WithMany(p => p.CardGames)
                    .HasForeignKey(d => d.BookingId)
                    .HasConstraintName("FK__Card_Game__booki__6B24EA82");

                entity.HasOne(d => d.Card)
                    .WithMany(p => p.CardGames)
                    .HasForeignKey(d => d.CardId)
                    .HasConstraintName("FK__Card_Game__card___6C190EBB");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasColumnName("create_at");

                entity.Property(e => e.PostId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("post_id");

                entity.Property(e => e.ReaderId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("reader_id");

                entity.Property(e => e.Text)
                    .HasColumnType("text")
                    .HasColumnName("text");

                entity.Property(e => e.UserId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("user_id");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK__Comment__post_id__59063A47");

                entity.HasOne(d => d.Reader)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.ReaderId)
                    .HasConstraintName("FK__Comment__reader___5812160E");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Comment__user_id__571DF1D5");
            });

            modelBuilder.Entity<Follow>(entity =>
            {
                entity.ToTable("Follow");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasColumnName("create_at");

                entity.Property(e => e.FollowerId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("follower_id");

                entity.Property(e => e.ReaderId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("reader_id");

                entity.Property(e => e.Text)
                    .HasColumnType("text")
                    .HasColumnName("text");

                entity.HasOne(d => d.Follower)
                    .WithMany(p => p.Follows)
                    .HasForeignKey(d => d.FollowerId)
                    .HasConstraintName("FK__Follow__follower__5BE2A6F2");

                entity.HasOne(d => d.Reader)
                    .WithMany(p => p.Follows)
                    .HasForeignKey(d => d.ReaderId)
                    .HasConstraintName("FK__Follow__reader_i__5CD6CB2B");
            });

            modelBuilder.Entity<GroupCard>(entity =>
            {
                entity.ToTable("Group_Card");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasColumnName("create_at");

                entity.Property(e => e.IsPublic).HasColumnName("is_public");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.ToTable("Image");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.CardId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("card_id");

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasColumnName("create_at");

                entity.Property(e => e.GroupId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("group_id");

                entity.Property(e => e.PostId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("post_id");

                entity.Property(e => e.ReaderId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("reader_id");

                entity.Property(e => e.Url)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("url");

                entity.Property(e => e.UserId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("user_id");

                entity.HasOne(d => d.Card)
                    .WithMany(p => p.Images)
                    .HasForeignKey(d => d.CardId)
                    .HasConstraintName("FK__Image__card_id__47DBAE45");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Images)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK__Image__group_id__4AB81AF0");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Images)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK__Image__post_id__49C3F6B7");

                entity.HasOne(d => d.Reader)
                    .WithMany(p => p.Images)
                    .HasForeignKey(d => d.ReaderId)
                    .HasConstraintName("FK__Image__reader_id__4BAC3F29");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Images)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Image__user_id__48CFD27E");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.ToTable("Notification");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.IsRead).HasColumnName("is_read");

                entity.Property(e => e.ReaderId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("reader_id");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("title");

                entity.Property(e => e.UserId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("user_id");

                entity.HasOne(d => d.Reader)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.ReaderId)
                    .HasConstraintName("FK__Notificat__reade__5441852A");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Notificat__user___534D60F1");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasColumnName("create_at");

                entity.Property(e => e.Text)
                    .HasColumnType("text")
                    .HasColumnName("text");

                entity.Property(e => e.UserId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Post__user_id__3E52440B");
            });

            modelBuilder.Entity<Reader>(entity =>
            {
                entity.ToTable("Reader");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.Dob)
                    .HasColumnType("datetime")
                    .HasColumnName("DOB");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Phone)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<ReaderTopic>(entity =>
            {
                entity.ToTable("Reader_Topic");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.ReaderId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("reader_id");

                entity.Property(e => e.TopicId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("topic_id");

                entity.HasOne(d => d.Reader)
                    .WithMany(p => p.ReaderTopics)
                    .HasForeignKey(d => d.ReaderId)
                    .HasConstraintName("FK__Reader_To__reade__6383C8BA");

                entity.HasOne(d => d.Topic)
                    .WithMany(p => p.ReaderTopics)
                    .HasForeignKey(d => d.TopicId)
                    .HasConstraintName("FK__Reader_To__topic__6477ECF3");
            });

            modelBuilder.Entity<Topic>(entity =>
            {
                entity.ToTable("Topic");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("Transaction");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.BookingId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("booking_id");

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasColumnName("create_at");

                entity.Property(e => e.ReaderId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("reader_id");

                entity.Property(e => e.Total).HasColumnName("total");

                entity.Property(e => e.TransactionType)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("transaction_type");

                entity.Property(e => e.UserId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("user_id");

                entity.HasOne(d => d.Booking)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.BookingId)
                    .HasConstraintName("FK__Transacti__booki__4F7CD00D");

                entity.HasOne(d => d.Reader)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.ReaderId)
                    .HasConstraintName("FK__Transacti__reade__5070F446");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Transacti__user___4E88ABD4");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.Dob)
                    .HasColumnType("datetime")
                    .HasColumnName("DOB");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Phone)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.Property(e => e.Role).HasColumnName("role");

                entity.Property(e => e.RoleId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("roleId");

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<UserGroupCard>(entity =>
            {
                entity.ToTable("User_Group_Card");

                entity.Property(e => e.Id)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("id");

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasColumnName("create_at");

                entity.Property(e => e.GroupId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("group_id");

                entity.Property(e => e.ReaderId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("reader_id");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.UserGroupCards)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK__User_Grou__group__5FB337D6");

                entity.HasOne(d => d.Reader)
                    .WithMany(p => p.UserGroupCards)
                    .HasForeignKey(d => d.ReaderId)
                    .HasConstraintName("FK__User_Grou__reade__60A75C0F");
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
