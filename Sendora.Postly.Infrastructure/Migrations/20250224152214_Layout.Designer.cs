﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sendora.Core.Database;

#nullable disable

namespace Sendora.Core.Migrations
{
    [DbContext(typeof(MailsContext))]
    [Migration("20250224152214_Layout")]
    partial class Layout
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.2");

            modelBuilder.Entity("Sendora.Postly.Domain.Entities.AddressEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasColumnName("address");

                    b.Property<string>("Left")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasColumnName("left");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasColumnName("name");

                    b.Property<string>("Right")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasColumnName("right");

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("Sendora.Postly.Domain.Entities.MailEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<DateTime>("DeliveredAt")
                        .HasColumnType("TEXT")
                        .HasColumnName("delivered_at");

                    b.Property<int>("Flags")
                        .HasColumnType("INTEGER")
                        .HasColumnName("flags");

                    b.Property<string>("SenderId")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("TEXT")
                        .HasColumnName("sender_id");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT")
                        .HasColumnName("subject");

                    b.HasKey("Id");

                    b.HasIndex("SenderId");

                    b.ToTable("Mails");
                });

            modelBuilder.Entity("Sendora.Postly.Domain.Entities.RecipientEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<int>("Category")
                        .HasColumnType("INTEGER")
                        .HasColumnName("recipient_category");

                    b.Property<string>("MailEntityId")
                        .HasColumnType("TEXT");

                    b.Property<string>("MailId")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("TEXT")
                        .HasColumnName("mail_id");

                    b.HasKey("Id");

                    b.HasIndex("MailEntityId");

                    b.HasIndex("MailId");

                    b.ToTable("Recipients");
                });

            modelBuilder.Entity("Sendora.Postly.Domain.Entities.UserEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(36)
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<string>("AddressId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("password");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasColumnName("username");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Sendora.Postly.Domain.Entities.MailEntity", b =>
                {
                    b.HasOne("Sendora.Postly.Domain.Entities.UserEntity", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("Sendora.Postly.Domain.Entities.RecipientEntity", b =>
                {
                    b.HasOne("Sendora.Postly.Domain.Entities.MailEntity", null)
                        .WithMany("Recipients")
                        .HasForeignKey("MailEntityId");

                    b.HasOne("Sendora.Postly.Domain.Entities.MailEntity", "Mail")
                        .WithMany()
                        .HasForeignKey("MailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Mail");
                });

            modelBuilder.Entity("Sendora.Postly.Domain.Entities.UserEntity", b =>
                {
                    b.HasOne("Sendora.Postly.Domain.Entities.AddressEntity", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");

                    b.Navigation("Address");
                });

            modelBuilder.Entity("Sendora.Postly.Domain.Entities.MailEntity", b =>
                {
                    b.Navigation("Recipients");
                });
#pragma warning restore 612, 618
        }
    }
}
