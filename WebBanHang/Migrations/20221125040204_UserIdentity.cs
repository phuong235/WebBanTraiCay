using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebBanHang.Migrations
{
    public partial class UserIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_Contact",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 150, nullable: false),
                    Email = table.Column<string>(maxLength: 200, nullable: false),
                    Message = table.Column<string>(maxLength: 2000, nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Contact", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_Menu",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    Alias = table.Column<string>(maxLength: 100, nullable: true),
                    Position = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Menu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_News",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 200, nullable: false),
                    Alias = table.Column<string>(maxLength: 200, nullable: true),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    Detail = table.Column<string>(nullable: true),
                    Image = table.Column<string>(maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Author = table.Column<string>(maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_News", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_Order",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(maxLength: 20, nullable: false),
                    CustomerName = table.Column<string>(maxLength: 150, nullable: false),
                    Phone = table.Column<string>(unicode: false, maxLength: 14, nullable: false),
                    Email = table.Column<string>(maxLength: 200, nullable: true),
                    TotalAmount = table.Column<int>(nullable: false),
                    TypePayment = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(maxLength: 100, nullable: true),
                    Street = table.Column<string>(maxLength: 50, nullable: false),
                    Ward = table.Column<string>(maxLength: 50, nullable: false),
                    District = table.Column<string>(maxLength: 50, nullable: false),
                    City = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Order", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_ProductCategory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    Alias = table.Column<string>(maxLength: 100, nullable: false),
                    Icon = table.Column<string>(maxLength: 100, nullable: true),
                    Position = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedBy = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_ProductCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_Statistic",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberOfAccess = table.Column<long>(nullable: false),
                    time = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Statistic", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_Subscibe",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(maxLength: 200, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Subscibe", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_SystemSetting",
                columns: table => new
                {
                    SettingKey = table.Column<string>(maxLength: 50, nullable: false),
                    SettingValue = table.Column<string>(maxLength: 1000, nullable: true),
                    SettingDescription = table.Column<string>(maxLength: 4000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tb_Syste__01E719AC552AFB59", x => x.SettingKey);
                });

            migrationBuilder.CreateTable(
                name: "tb_Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    Alias = table.Column<string>(maxLength: 100, nullable: true),
                    ProductCode = table.Column<string>(maxLength: 10, nullable: true),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    Detail = table.Column<string>(nullable: true),
                    Image = table.Column<string>(maxLength: 500, nullable: true),
                    Price = table.Column<int>(nullable: false),
                    PriceSale = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    ViewCount = table.Column<int>(nullable: false),
                    IsHome = table.Column<bool>(nullable: false),
                    IsSale = table.Column<bool>(nullable: false),
                    IsFeature = table.Column<bool>(nullable: false),
                    IsHot = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ProductCategoryId = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK__tb_Produc__Produ__403A8C7D",
                        column: x => x.ProductCategoryId,
                        principalTable: "tb_ProductCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_OrderDetail",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    Price = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_OrderDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK__tb_OrderD__Order__4316F928",
                        column: x => x.OrderId,
                        principalTable: "tb_Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK__tb_OrderD__Produ__440B1D61",
                        column: x => x.ProductId,
                        principalTable: "tb_Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_ProductImage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(nullable: true),
                    Image = table.Column<string>(maxLength: 100, nullable: true),
                    isDefault = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_ProductImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK__tb_Produc__Produ__02FC7413",
                        column: x => x.ProductId,
                        principalTable: "tb_Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_OrderDetail_OrderId",
                table: "tb_OrderDetail",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_OrderDetail_ProductId",
                table: "tb_OrderDetail",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_ProductImage_ProductId",
                table: "tb_ProductImage",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_Products_ProductCategoryId",
                table: "tb_Products",
                column: "ProductCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_Contact");

            migrationBuilder.DropTable(
                name: "tb_Menu");

            migrationBuilder.DropTable(
                name: "tb_News");

            migrationBuilder.DropTable(
                name: "tb_OrderDetail");

            migrationBuilder.DropTable(
                name: "tb_ProductImage");

            migrationBuilder.DropTable(
                name: "tb_Statistic");

            migrationBuilder.DropTable(
                name: "tb_Subscibe");

            migrationBuilder.DropTable(
                name: "tb_SystemSetting");

            migrationBuilder.DropTable(
                name: "tb_Order");

            migrationBuilder.DropTable(
                name: "tb_Products");

            migrationBuilder.DropTable(
                name: "tb_ProductCategory");
        }
    }
}
