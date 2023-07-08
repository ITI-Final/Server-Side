using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OlxDataAccess.Migrations
{
    public partial class m0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<bool>(type: "bit", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Birth_Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    Added_Date = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdminPermission",
                columns: table => new
                {
                    Admin = table.Column<int>(type: "int", nullable: false),
                    Permission = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminPermission", x => new { x.Admin, x.Permission });
                });

            migrationBuilder.CreateTable(
                name: "Governorates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Governorate_Name_Ar = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Governorate_Name_En = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Governorates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Section = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Can_View = table.Column<bool>(type: "bit", nullable: false),
                    Can_Add = table.Column<bool>(type: "bit", nullable: false),
                    Can_Edit = table.Column<bool>(type: "bit", nullable: false),
                    Can_Delete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<bool>(type: "bit", nullable: true),
                    Company = table.Column<int>(type: "int", nullable: true),
                    Birth_Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    Post_Count = table.Column<int>(type: "int", nullable: true),
                    Register_Date = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Label = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Label_Ar = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Parent_Id = table.Column<int>(type: "int", nullable: true),
                    Slug = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Admin_Id = table.Column<int>(type: "int", nullable: true),
                    Created_Date = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_category_admin",
                        column: x => x.Admin_Id,
                        principalTable: "Admin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_category_category",
                        column: x => x.Parent_Id,
                        principalTable: "Category",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Governorate_Id = table.Column<int>(type: "int", nullable: false),
                    City_Name_Ar = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    City_Name_En = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cities_governorates",
                        column: x => x.Governorate_Id,
                        principalTable: "Governorates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Admin_Permission",
                columns: table => new
                {
                    Admin = table.Column<int>(type: "int", nullable: false),
                    Permission = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admin_permission", x => new { x.Admin, x.Permission });
                    table.ForeignKey(
                        name: "FK_admin_permission_admin",
                        column: x => x.Admin,
                        principalTable: "Admin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_admin_permission_permission",
                        column: x => x.Permission,
                        principalTable: "Permission",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Chat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_One = table.Column<int>(type: "int", nullable: false),
                    User_Two = table.Column<int>(type: "int", nullable: false),
                    Block = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_chat_user2",
                        column: x => x.User_One,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_chat_user3",
                        column: x => x.User_Two,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Logo_Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cover_Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tax_Number = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Owner = table.Column<int>(type: "int", nullable: false),
                    Register_Date = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                    table.ForeignKey(
                        name: "FK_company_user",
                        column: x => x.Owner,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Field",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cat_Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Label = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Label_Ar = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Value_Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Choice_Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Max_Length = table.Column<int>(type: "int", nullable: true),
                    Min_Length = table.Column<int>(type: "int", nullable: true),
                    Max_Value = table.Column<int>(type: "int", nullable: true),
                    Min_Value = table.Column<int>(type: "int", nullable: true),
                    Is_Required = table.Column<bool>(type: "bit", nullable: false),
                    Parent_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Field", x => x.Id);
                    table.ForeignKey(
                        name: "FK_field_category",
                        column: x => x.Cat_Id,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_field_field",
                        column: x => x.Parent_Id,
                        principalTable: "Field",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_Id = table.Column<int>(type: "int", nullable: false),
                    Cat_Id = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    Price_Type = table.Column<int>(type: "int", nullable: true),
                    Contact_Method = table.Column<int>(type: "int", nullable: false),
                    Post_Location = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    Created_Date = table.Column<DateTime>(type: "date", nullable: true, defaultValueSql: "(getdate())"),
                    Is_Visible = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    Views = table.Column<int>(type: "int", nullable: false),
                    Is_Special = table.Column<bool>(type: "bit", nullable: false),
                    Fields = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.Id);
                    table.ForeignKey(
                        name: "FK_post_category",
                        column: x => x.Cat_Id,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_post_cities",
                        column: x => x.Post_Location,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_post_user",
                        column: x => x.User_Id,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Chat_Message",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Chat_Id = table.Column<int>(type: "int", nullable: false),
                    Sender_Id = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Send_Date = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat_Message", x => x.Id);
                    table.ForeignKey(
                        name: "FK_chat_message_chat",
                        column: x => x.Chat_Id,
                        principalTable: "Chat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_chat_message_user",
                        column: x => x.Sender_Id,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Choice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Field_Id = table.Column<int>(type: "int", nullable: false),
                    Label = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Label_Ar = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Choice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_choice_field",
                        column: x => x.Field_Id,
                        principalTable: "Field",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Field_Role",
                columns: table => new
                {
                    Field_Id = table.Column<int>(type: "int", nullable: false),
                    Filterable = table.Column<bool>(type: "bit", nullable: false),
                    Included_In_Breadcrumbs = table.Column<bool>(type: "bit", nullable: false),
                    Included_In_Pathname = table.Column<bool>(type: "bit", nullable: false),
                    Included_In_Sitemap = table.Column<bool>(type: "bit", nullable: false),
                    Included_In_Title = table.Column<bool>(type: "bit", nullable: false),
                    Searchable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_fieldRole_field",
                        column: x => x.Field_Id,
                        principalTable: "Field",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Favorite",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_Id = table.Column<int>(type: "int", nullable: false),
                    Post_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_favorite_post",
                        column: x => x.Post_Id,
                        principalTable: "Post",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_favorite_user",
                        column: x => x.User_Id,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Post_Image",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Post_Id = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post_Image", x => x.Id);
                    table.ForeignKey(
                        name: "FK_postImage_post",
                        column: x => x.Post_Id,
                        principalTable: "Post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admin_Permission_Permission",
                table: "Admin_Permission",
                column: "Permission");

            migrationBuilder.CreateIndex(
                name: "IX_Category_Admin_Id",
                table: "Category",
                column: "Admin_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Category_Parent_Id",
                table: "Category",
                column: "Parent_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Chat_User_One",
                table: "Chat",
                column: "User_One");

            migrationBuilder.CreateIndex(
                name: "IX_Chat_User_Two",
                table: "Chat",
                column: "User_Two");

            migrationBuilder.CreateIndex(
                name: "IX_Chat_Message_Chat_Id",
                table: "Chat_Message",
                column: "Chat_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Chat_Message_Sender_Id",
                table: "Chat_Message",
                column: "Sender_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Choice_Field_Id",
                table: "Choice",
                column: "Field_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_Governorate_Id",
                table: "Cities",
                column: "Governorate_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Company_Owner",
                table: "Company",
                column: "Owner");

            migrationBuilder.CreateIndex(
                name: "IX_Favorite_Post_Id",
                table: "Favorite",
                column: "Post_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Favorite_User_Id",
                table: "Favorite",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Field_Cat_Id",
                table: "Field",
                column: "Cat_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Field_Parent_Id",
                table: "Field",
                column: "Parent_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Field_Role_Field_Id",
                table: "Field_Role",
                column: "Field_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Post_Cat_Id",
                table: "Post",
                column: "Cat_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Post_Post_Location",
                table: "Post",
                column: "Post_Location");

            migrationBuilder.CreateIndex(
                name: "IX_Post_User_Id",
                table: "Post",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Post_Image_Post_Id",
                table: "Post_Image",
                column: "Post_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin_Permission");

            migrationBuilder.DropTable(
                name: "AdminPermission");

            migrationBuilder.DropTable(
                name: "Chat_Message");

            migrationBuilder.DropTable(
                name: "Choice");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "Favorite");

            migrationBuilder.DropTable(
                name: "Field_Role");

            migrationBuilder.DropTable(
                name: "Post_Image");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "Chat");

            migrationBuilder.DropTable(
                name: "Field");

            migrationBuilder.DropTable(
                name: "Post");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "Governorates");
        }
    }
}
