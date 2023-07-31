using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OlxDataAccess.Migrations
{
    public partial class m0000 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_chat_user2",
                table: "Chat");

            migrationBuilder.DropForeignKey(
                name: "FK_chat_user3",
                table: "Chat");

            migrationBuilder.DropForeignKey(
                name: "FK_chat_message_chat",
                table: "Chat_Message");

            migrationBuilder.DropForeignKey(
                name: "FK_chat_message_user",
                table: "Chat_Message");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Chat_Message");

            migrationBuilder.DropColumn(
                name: "Send_Date",
                table: "Chat_Message");

            migrationBuilder.RenameColumn(
                name: "Sender_Id",
                table: "Chat_Message",
                newName: "Sender_ID");

            migrationBuilder.RenameColumn(
                name: "Chat_Id",
                table: "Chat_Message",
                newName: "Receiver_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Chat_Message_Sender_Id",
                table: "Chat_Message",
                newName: "IX_Chat_Message_Sender_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Chat_Message_Chat_Id",
                table: "Chat_Message",
                newName: "IX_Chat_Message_Receiver_ID");

            migrationBuilder.RenameColumn(
                name: "User_Two",
                table: "Chat",
                newName: "Sender_ID");

            migrationBuilder.RenameColumn(
                name: "User_One",
                table: "Chat",
                newName: "Receiver_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Chat_User_Two",
                table: "Chat",
                newName: "IX_Chat_User_One");

            migrationBuilder.RenameIndex(
                name: "IX_Chat_User_One",
                table: "Chat",
                newName: "IX_Chat_User_Two");

            migrationBuilder.RenameColumn(
                name: "Permission",
                table: "AdminPermission",
                newName: "Permission_Id");

            migrationBuilder.RenameColumn(
                name: "Admin",
                table: "AdminPermission",
                newName: "Admin_Id");

            migrationBuilder.RenameColumn(
                name: "Permission",
                table: "Admin_Permission",
                newName: "Permission_Id");

            migrationBuilder.RenameColumn(
                name: "Admin",
                table: "Admin_Permission",
                newName: "Admin_Id");

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "Chat_Message",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Chat_Message",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Chat_Message",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Time",
                table: "Chat_Message",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.CreateTable(
                name: "User_Connection",
                columns: table => new
                {
                    User_ID = table.Column<int>(type: "int", nullable: false),
                    Connection_ID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__User_Con__51B222138C204A39", x => new { x.User_ID, x.Connection_ID });
                    table.ForeignKey(
                        name: "FK_User_Connection_User",
                        column: x => x.User_ID,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "nonclusteredEmail",
                table: "Admin",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "NonClusteredIndex-20230716-201110",
                table: "Admin",
                column: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_Chat_Message_User",
                table: "Chat_Message",
                column: "Sender_ID",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Chat_Message_User1",
                table: "Chat_Message",
                column: "Receiver_ID",
                principalTable: "User",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chat_Message_User",
                table: "Chat_Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Chat_Message_User1",
                table: "Chat_Message");

            migrationBuilder.DropTable(
                name: "User_Connection");

            migrationBuilder.DropIndex(
                name: "nonclusteredEmail",
                table: "Admin");

            migrationBuilder.DropIndex(
                name: "NonClusteredIndex-20230716-201110",
                table: "Admin");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Chat_Message");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Chat_Message");

            migrationBuilder.RenameColumn(
                name: "Sender_ID",
                table: "Chat_Message",
                newName: "Sender_Id");

            migrationBuilder.RenameColumn(
                name: "Receiver_ID",
                table: "Chat_Message",
                newName: "Chat_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Chat_Message_Sender_ID",
                table: "Chat_Message",
                newName: "IX_Chat_Message_Sender_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Chat_Message_Receiver_ID",
                table: "Chat_Message",
                newName: "IX_Chat_Message_Chat_Id");

            migrationBuilder.RenameColumn(
                name: "Sender_ID",
                table: "Chat",
                newName: "User_Two");

            migrationBuilder.RenameColumn(
                name: "Receiver_ID",
                table: "Chat",
                newName: "User_One");

            migrationBuilder.RenameIndex(
                name: "IX_Chat_User_Two",
                table: "Chat",
                newName: "IX_Chat_User_One");

            migrationBuilder.RenameIndex(
                name: "IX_Chat_User_One",
                table: "Chat",
                newName: "IX_Chat_User_Two");

            migrationBuilder.RenameColumn(
                name: "Permission_Id",
                table: "AdminPermission",
                newName: "Permission");

            migrationBuilder.RenameColumn(
                name: "Admin_Id",
                table: "AdminPermission",
                newName: "Admin");

            migrationBuilder.RenameColumn(
                name: "Permission_Id",
                table: "Admin_Permission",
                newName: "Permission");

            migrationBuilder.RenameColumn(
                name: "Admin_Id",
                table: "Admin_Permission",
                newName: "Admin");

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "Chat_Message",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Chat_Message",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Chat_Message",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Send_Date",
                table: "Chat_Message",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_chat_user2",
                table: "Chat",
                column: "User_One",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_chat_user3",
                table: "Chat",
                column: "User_Two",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_chat_message_chat",
                table: "Chat_Message",
                column: "Chat_Id",
                principalTable: "Chat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_chat_message_user",
                table: "Chat_Message",
                column: "Sender_Id",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
