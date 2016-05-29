using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using TeamTodo.Models;

namespace TeamTodo.Migrations
{
    [DbContext(typeof(TodoContext))]
    partial class TodoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rc2-20896");

            modelBuilder.Entity("TeamTodo.Models.Todo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<bool>("IsComplete");

                    b.Property<string>("Name");

                    b.Property<int>("TodoTypeId");

                    b.Property<int?>("TodoTypeId1");

                    b.Property<int>("TodoUserId");

                    b.Property<int?>("TodoUserId1");

                    b.Property<string>("Type");

                    b.Property<string>("User");

                    b.HasKey("Id");

                    b.HasIndex("TodoTypeId1");

                    b.HasIndex("TodoUserId1");

                    b.ToTable("Todos");
                });

            modelBuilder.Entity("TeamTodo.Models.TodoType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("TodoTypes");
                });

            modelBuilder.Entity("TeamTodo.Models.TodoUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("TodoUsers");
                });

            modelBuilder.Entity("TeamTodo.Models.Todo", b =>
                {
                    b.HasOne("TeamTodo.Models.TodoType")
                        .WithMany()
                        .HasForeignKey("TodoTypeId1");

                    b.HasOne("TeamTodo.Models.TodoUser")
                        .WithMany()
                        .HasForeignKey("TodoUserId1");
                });
        }
    }
}
