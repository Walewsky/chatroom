﻿// <auto-generated />
using System;
using ChatRoom.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ChatRoom.Api.Migrations
{
    [DbContext(typeof(ChatRoomDbContext))]
    partial class ChatRoomDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ChatRoom.Domain.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("INT")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnName("Content")
                        .HasColumnType("NVARCHAR(512)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnName("Timestamp")
                        .HasColumnType("DATETIME2");

                    b.Property<string>("User")
                        .IsRequired()
                        .HasColumnName("User")
                        .HasColumnType("NVARCHAR(32)");

                    b.HasKey("Id");

                    b.ToTable("messages");
                });
#pragma warning restore 612, 618
        }
    }
}
