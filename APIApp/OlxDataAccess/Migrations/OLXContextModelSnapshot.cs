﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OlxDataAccess.DBContext;

#nullable disable

namespace OlxDataAccess.Migrations
{
    [DbContext(typeof(OLXContext))]
    partial class OLXContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.19")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Admin_Permission", b =>
                {
                    b.Property<int>("Admin")
                        .HasColumnType("int");

                    b.Property<int>("Permission")
                        .HasColumnType("int");

                    b.HasKey("Admin", "Permission")
                        .HasName("PK_admin_permission");

                    b.HasIndex("Permission");

                    b.ToTable("Admin_Permission", (string)null);
                });

            modelBuilder.Entity("AdminPermission", b =>
                {
                    b.Property<int>("Admin")
                        .HasColumnType("int");

                    b.Property<int>("Permission")
                        .HasColumnType("int");

                    b.HasKey("Admin", "Permission");

                    b.ToTable("AdminPermission");
                });

            modelBuilder.Entity("OlxDataAccess.Models.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("Added_Date")
                        .HasColumnType("datetime");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Avatar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Birth_Date")
                        .HasColumnType("datetime");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool?>("Gender")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Phone")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Admin");
                });

            modelBuilder.Entity("OlxDataAccess.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("Admin_Id")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Created_Date")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Label_Ar")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("Parent_Id")
                        .HasColumnType("int");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Tags")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Admin_Id");

                    b.HasIndex("Parent_Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("OlxDataAccess.Models.Chat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool?>("Block")
                        .HasColumnType("bit");

                    b.Property<int>("User_One")
                        .HasColumnType("int");

                    b.Property<int>("User_Two")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("User_One");

                    b.HasIndex("User_Two");

                    b.ToTable("Chat");
                });

            modelBuilder.Entity("OlxDataAccess.Models.Chat_Message", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("Chat_Id")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Send_Date")
                        .HasColumnType("datetime");

                    b.Property<int>("Sender_Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Chat_Id");

                    b.HasIndex("Sender_Id");

                    b.ToTable("Chat_Message");
                });

            modelBuilder.Entity("OlxDataAccess.Models.Choice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Field_Id")
                        .HasColumnType("int");

                    b.Property<string>("Icon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Label_Ar")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Slug")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Field_Id");

                    b.ToTable("Choice");
                });

            modelBuilder.Entity("OlxDataAccess.Models.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("City_Name_Ar")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("City_Name_En")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("Governorate_Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Governorate_Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("OlxDataAccess.Models.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Cover_Url")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Logo_Url")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Owner")
                        .HasColumnType("int");

                    b.Property<int>("OwnerID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Register_Date")
                        .HasColumnType("datetime");

                    b.Property<string>("Tax_Number")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("OwnerID");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("OlxDataAccess.Models.Favorite", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Post_Id")
                        .HasColumnType("int");

                    b.Property<int>("User_Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Post_Id");

                    b.HasIndex("User_Id");

                    b.ToTable("Favorite");
                });

            modelBuilder.Entity("OlxDataAccess.Models.Field", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Cat_Id")
                        .HasColumnType("int");

                    b.Property<string>("Choice_Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("Is_Required")
                        .HasColumnType("bit");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Label_Ar")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("Max_Length")
                        .HasColumnType("int");

                    b.Property<int?>("Max_Value")
                        .HasColumnType("int");

                    b.Property<int?>("Min_Length")
                        .HasColumnType("int");

                    b.Property<int?>("Min_Value")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("Parent_Id")
                        .HasColumnType("int");

                    b.Property<string>("Value_Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Cat_Id");

                    b.HasIndex("Parent_Id");

                    b.ToTable("Field");
                });

            modelBuilder.Entity("OlxDataAccess.Models.Field_Role", b =>
                {
                    b.Property<int>("Field_Id")
                        .HasColumnType("int");

                    b.Property<bool>("Filterable")
                        .HasColumnType("bit");

                    b.Property<bool>("Included_In_Breadcrumbs")
                        .HasColumnType("bit");

                    b.Property<bool>("Included_In_Pathname")
                        .HasColumnType("bit");

                    b.Property<bool>("Included_In_Sitemap")
                        .HasColumnType("bit");

                    b.Property<bool>("Included_In_Title")
                        .HasColumnType("bit");

                    b.Property<bool>("Searchable")
                        .HasColumnType("bit");

                    b.HasIndex("Field_Id");

                    b.ToTable("Field_Role");
                });

            modelBuilder.Entity("OlxDataAccess.Models.Governorate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Governorate_Name_Ar")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Governorate_Name_En")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Governorates");
                });

            modelBuilder.Entity("OlxDataAccess.Models.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Can_Add")
                        .HasColumnType("bit");

                    b.Property<bool>("Can_Delete")
                        .HasColumnType("bit");

                    b.Property<bool>("Can_Edit")
                        .HasColumnType("bit");

                    b.Property<bool>("Can_View")
                        .HasColumnType("bit");

                    b.Property<string>("Section")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Permission");
                });

            modelBuilder.Entity("OlxDataAccess.Models.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Cat_Id")
                        .HasColumnType("int");

                    b.Property<int>("Contact_Method")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Created_Date")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Fields")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Is_Special")
                        .HasColumnType("bit");

                    b.Property<bool?>("Is_Visible")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((1))");

                    b.Property<int?>("Post_Location")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("((0))");

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.Property<int?>("Price_Type")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("User_Id")
                        .HasColumnType("int");

                    b.Property<int>("Views")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Cat_Id");

                    b.HasIndex("Post_Location");

                    b.HasIndex("User_Id");

                    b.ToTable("Post");
                });

            modelBuilder.Entity("OlxDataAccess.Models.Post_Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Post_Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Post_Id");

                    b.ToTable("Post_Image");
                });

            modelBuilder.Entity("OlxDataAccess.Models.Setting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("OlxDataAccess.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Bio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Birth_Date")
                        .HasColumnType("datetime");

                    b.Property<int?>("Company")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool?>("Gender")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Phone")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("Post_Count")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Register_Date")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Admin_Permission", b =>
                {
                    b.HasOne("OlxDataAccess.Models.Admin", null)
                        .WithMany()
                        .HasForeignKey("Admin")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_admin_permission_admin");

                    b.HasOne("OlxDataAccess.Models.Permission", null)
                        .WithMany()
                        .HasForeignKey("Permission")
                        .IsRequired()
                        .HasConstraintName("FK_admin_permission_permission");
                });

            modelBuilder.Entity("OlxDataAccess.Models.Category", b =>
                {
                    b.HasOne("OlxDataAccess.Models.Admin", "Admin")
                        .WithMany("Categories")
                        .HasForeignKey("Admin_Id")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("FK_category_admin");

                    b.HasOne("OlxDataAccess.Models.Category", "Parent")
                        .WithMany("InverseParent")
                        .HasForeignKey("Parent_Id")
                        .HasConstraintName("FK_category_category");

                    b.Navigation("Admin");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("OlxDataAccess.Models.Chat", b =>
                {
                    b.HasOne("OlxDataAccess.Models.User", "User_OneNavigation")
                        .WithMany("ChatUser_OneNavigations")
                        .HasForeignKey("User_One")
                        .IsRequired()
                        .HasConstraintName("FK_chat_user2");

                    b.HasOne("OlxDataAccess.Models.User", "User_TwoNavigation")
                        .WithMany("ChatUser_TwoNavigations")
                        .HasForeignKey("User_Two")
                        .IsRequired()
                        .HasConstraintName("FK_chat_user3");

                    b.Navigation("User_OneNavigation");

                    b.Navigation("User_TwoNavigation");
                });

            modelBuilder.Entity("OlxDataAccess.Models.Chat_Message", b =>
                {
                    b.HasOne("OlxDataAccess.Models.Chat", "Chat")
                        .WithMany("Chat_Messages")
                        .HasForeignKey("Chat_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_chat_message_chat");

                    b.HasOne("OlxDataAccess.Models.User", "Sender")
                        .WithMany("Chat_Messages")
                        .HasForeignKey("Sender_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_chat_message_user");

                    b.Navigation("Chat");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("OlxDataAccess.Models.Choice", b =>
                {
                    b.HasOne("OlxDataAccess.Models.Field", "Field")
                        .WithMany("Choices")
                        .HasForeignKey("Field_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_choice_field");

                    b.Navigation("Field");
                });

            modelBuilder.Entity("OlxDataAccess.Models.City", b =>
                {
                    b.HasOne("OlxDataAccess.Models.Governorate", "Governorate")
                        .WithMany("Cities")
                        .HasForeignKey("Governorate_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_cities_governorates");

                    b.Navigation("Governorate");
                });

            modelBuilder.Entity("OlxDataAccess.Models.Company", b =>
                {
                    b.HasOne("OlxDataAccess.Models.User", "OwnerNavigation")
                        .WithMany("Companies")
                        .HasForeignKey("OwnerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_company_user");

                    b.Navigation("OwnerNavigation");
                });

            modelBuilder.Entity("OlxDataAccess.Models.Favorite", b =>
                {
                    b.HasOne("OlxDataAccess.Models.Post", "Post")
                        .WithMany("Favorites")
                        .HasForeignKey("Post_Id")
                        .IsRequired()
                        .HasConstraintName("FK_favorite_post");

                    b.HasOne("OlxDataAccess.Models.User", "User")
                        .WithMany("Favorites")
                        .HasForeignKey("User_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_favorite_user");

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("OlxDataAccess.Models.Field", b =>
                {
                    b.HasOne("OlxDataAccess.Models.Category", "Cat")
                        .WithMany("Fields")
                        .HasForeignKey("Cat_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_field_category");

                    b.HasOne("OlxDataAccess.Models.Field", "Parent")
                        .WithMany("InverseParent")
                        .HasForeignKey("Parent_Id")
                        .HasConstraintName("FK_field_field");

                    b.Navigation("Cat");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("OlxDataAccess.Models.Field_Role", b =>
                {
                    b.HasOne("OlxDataAccess.Models.Field", "Field")
                        .WithMany()
                        .HasForeignKey("Field_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_fieldRole_field");

                    b.Navigation("Field");
                });

            modelBuilder.Entity("OlxDataAccess.Models.Post", b =>
                {
                    b.HasOne("OlxDataAccess.Models.Category", "Cat")
                        .WithMany("Posts")
                        .HasForeignKey("Cat_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_post_category");

                    b.HasOne("OlxDataAccess.Models.City", "Post_LocationNavigation")
                        .WithMany("Posts")
                        .HasForeignKey("Post_Location")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("FK_post_cities");

                    b.HasOne("OlxDataAccess.Models.User", "User")
                        .WithMany("Posts")
                        .HasForeignKey("User_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_post_user");

                    b.Navigation("Cat");

                    b.Navigation("Post_LocationNavigation");

                    b.Navigation("User");
                });

            modelBuilder.Entity("OlxDataAccess.Models.Post_Image", b =>
                {
                    b.HasOne("OlxDataAccess.Models.Post", "Post")
                        .WithMany("Post_Images")
                        .HasForeignKey("Post_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_postImage_post");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("OlxDataAccess.Models.Admin", b =>
                {
                    b.Navigation("Categories");
                });

            modelBuilder.Entity("OlxDataAccess.Models.Category", b =>
                {
                    b.Navigation("Fields");

                    b.Navigation("InverseParent");

                    b.Navigation("Posts");
                });

            modelBuilder.Entity("OlxDataAccess.Models.Chat", b =>
                {
                    b.Navigation("Chat_Messages");
                });

            modelBuilder.Entity("OlxDataAccess.Models.City", b =>
                {
                    b.Navigation("Posts");
                });

            modelBuilder.Entity("OlxDataAccess.Models.Field", b =>
                {
                    b.Navigation("Choices");

                    b.Navigation("InverseParent");
                });

            modelBuilder.Entity("OlxDataAccess.Models.Governorate", b =>
                {
                    b.Navigation("Cities");
                });

            modelBuilder.Entity("OlxDataAccess.Models.Post", b =>
                {
                    b.Navigation("Favorites");

                    b.Navigation("Post_Images");
                });

            modelBuilder.Entity("OlxDataAccess.Models.User", b =>
                {
                    b.Navigation("ChatUser_OneNavigations");

                    b.Navigation("ChatUser_TwoNavigations");

                    b.Navigation("Chat_Messages");

                    b.Navigation("Companies");

                    b.Navigation("Favorites");

                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}
