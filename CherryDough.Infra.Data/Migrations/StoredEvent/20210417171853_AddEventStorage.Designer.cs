﻿// <auto-generated />
using System;
using CherryDough.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CherryDough.Infra.Data.Migrations.StoredEvent
{
    [DbContext(typeof(StoredEventContext))]
    [Migration("20210417171853_AddEventStorage")]
    partial class AddEventStorage
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("CherryDough.Doamin.Core.Events.StoredEvent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AggregateId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Data")
                        .HasColumnType("TEXT");

                    b.Property<string>("MessageType")
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Action");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("TEXT")
                        .HasColumnName("CreationDate");

                    b.Property<string>("User")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("StoredEvents");
                });
#pragma warning restore 612, 618
        }
    }
}
