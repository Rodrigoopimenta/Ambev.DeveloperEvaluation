﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ambev.DeveloperEvaluation.ORM.Migrations
{
    /// <inheritdoc />
    public partial class SalesMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });
            migrationBuilder.CreateTable(
               name: "Sales",
               columns: table => new
               {
                   Id = table.Column<Guid>(type: "uuid", nullable: false),
                   SaleNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                   SaleDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                   CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                   CustomerName = table.Column<string>(type: "text", nullable: false),
                   BranchId = table.Column<Guid>(type: "uuid", nullable: false),
                   BranchName = table.Column<string>(type: "text", nullable: false),
                   TotalAmount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                   IsCancelled = table.Column<bool>(type: "boolean", nullable: false),
                   CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                   UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_Sales", x => x.Id);
                   table.ForeignKey(
                       name: "FK_Sales_Branches_BranchId",
                       column: x => x.BranchId,
                       principalTable: "Branches",
                       principalColumn: "Id",
                       onDelete: ReferentialAction.Restrict);
                   table.ForeignKey(
                       name: "FK_Sales_Customers_CustomerId",
                       column: x => x.CustomerId,
                       principalTable: "Customers",
                       principalColumn: "Id",
                       onDelete: ReferentialAction.Restrict);
               });
            migrationBuilder.CreateTable(
                name: "SaleItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SaleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ItemCode = table.Column<Guid>(type: "uuid", nullable: false),
                    ItemName = table.Column<string>(type: "text", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    Discount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    IsCancelled = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleItems_Sales_SaleId",
                        column: x => x.SaleId,
                        principalTable: "Sales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SaleItems_SaleId",
                table: "SaleItems",
                column: "SaleId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_BranchId",
                table: "Sales",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_CustomerId",
                table: "Sales",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
              name: "SaleItems");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "Branches");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
