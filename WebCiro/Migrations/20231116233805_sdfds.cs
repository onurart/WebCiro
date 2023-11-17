using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebCiro.Migrations
{
    /// <inheritdoc />
    public partial class sdfds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NET_001_CIRO");

            migrationBuilder.DropTable(
                name: "Sale");

            migrationBuilder.CreateTable(
                name: "WI_DashboardTbl",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalTl = table.Column<double>(type: "float", nullable: false),
                    VehicleType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VehicleBrand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PartBrand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rn = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WI_DashboardTbl", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WI_ReportApprove",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalesmanName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalTl = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VehicleType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VehicleBrand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PartBrand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WI_ReportApprove", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WI_State_Sales",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    INVOICE_NUMBER = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    INVOICE_DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    INVOICE_YEAR = table.Column<int>(type: "int", nullable: false),
                    INVOICE_MONTH = table.Column<int>(type: "int", nullable: false),
                    INVOICE_DAY = table.Column<int>(type: "int", nullable: false),
                    INVOICE_WEEK = table.Column<int>(type: "int", nullable: false),
                    CITY = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CUSTOMER_CODE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CUSTOMER_NAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PRODUCT_CATEGORY = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PRODUCT_BRAND = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PRODUCT_CODE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PRODUCT_NAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PRODUCT_QUANTITY = table.Column<int>(type: "int", nullable: false),
                    NET_AMOUNT = table.Column<double>(type: "float", nullable: false),
                    PRODUCT_SUPPLIER = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WI_State_Sales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WI_Ste_Prd_Ctr_Day",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductCategory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvoiceCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WI_Ste_Prd_Ctr_Day", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WI_DashboardTbl");

            migrationBuilder.DropTable(
                name: "WI_ReportApprove");

            migrationBuilder.DropTable(
                name: "WI_State_Sales");

            migrationBuilder.DropTable(
                name: "WI_Ste_Prd_Ctr_Day");

            migrationBuilder.CreateTable(
                name: "NET_001_CIRO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AUXCODE2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    COUNTRY = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Customer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpeCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StockCategories = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StokName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TlNet = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NET_001_CIRO", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sale",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Branch = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Customer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProdutGruop = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PurchasedOn = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sale", x => x.Id);
                });
        }
    }
}
