using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using OnlineForum.DAL;

namespace OnlineForum.DAL.Migrations
{
    [DbContext(typeof(OnlineForumContext))]
    [Migration("20170726190905_First")]
    partial class First
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

                    b.HasKey("ThreadId");

                    b.ToTable("Threads");
                });
        }
    }
}
