using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using OnlineForum.DAL;

namespace OnlineForum.DAL.Migrations
{
    [DbContext(typeof(OnlineForumContext))]
    [Migration("20170817205930_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OnlineForum.DAL.Entities.Comment", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<DateTime>("Created");

                    b.Property<int>("Downvotes");

                    b.Property<DateTime>("Modified");

                    b.Property<int?>("ParentCommentId");

                    b.Property<int?>("ThreadId");

                    b.Property<int>("Upvotes");

                    b.Property<int?>("UserId");

                    b.HasKey("CommentId");

                    b.HasIndex("ParentCommentId");

                    b.HasIndex("ThreadId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("OnlineForum.DAL.Entities.Thread", b =>
                {
                    b.Property<int>("ThreadId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<DateTime>("Created");

                    b.Property<int>("Downvotes");

                    b.Property<DateTime>("Modified");

                    b.Property<string>("Title");

                    b.Property<int>("Upvotes");

                    b.Property<int?>("UserId");

                    b.HasKey("ThreadId");

                    b.HasIndex("UserId");

                    b.ToTable("Threads");
                });

            modelBuilder.Entity("OnlineForum.DAL.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("UserName");

                    b.HasKey("UserId");

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("OnlineForum.DAL.Entities.Comment", b =>
                {
                    b.HasOne("OnlineForum.DAL.Entities.Comment", "Parent")
                        .WithMany()
                        .HasForeignKey("ParentCommentId");

                    b.HasOne("OnlineForum.DAL.Entities.Thread", "Thread")
                        .WithMany()
                        .HasForeignKey("ThreadId");

                    b.HasOne("OnlineForum.DAL.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("OnlineForum.DAL.Entities.Thread", b =>
                {
                    b.HasOne("OnlineForum.DAL.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });
        }
    }
}
