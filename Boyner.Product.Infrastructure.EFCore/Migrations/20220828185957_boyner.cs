using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boyner.Product.Infrastructure.EFCore.Migrations
{
    public partial class boyner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Attribute",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attribute", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryStatus",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currency",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrencyCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductStatus",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AttributeValue",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AttributeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeValue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttributeValue_Attribute_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "Attribute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_CategoryStatus_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "dbo",
                        principalTable: "CategoryStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategoryAttribute",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttributeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryAttribute", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryAttribute_Attribute_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "Attribute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryAttribute_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "dbo",
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    CurrencyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "dbo",
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_ProductStatus_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "dbo",
                        principalTable: "ProductStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductAttribute",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttributeValueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAttribute", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductAttribute_AttributeValue_AttributeValueId",
                        column: x => x.AttributeValueId,
                        principalTable: "AttributeValue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductAttribute_Product_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "dbo",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Attribute",
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "Name", "UpdatedOn" },
                values: new object[,]
                {
                    { new Guid("87d331c9-70cb-4d1a-943b-4febba5abdb3"), new DateTime(2022, 8, 28, 21, 59, 56, 944, DateTimeKind.Local).AddTicks(4686), null, "Brand", null },
                    { new Guid("d444a5b5-5216-4a67-9831-fbd85320aebb"), new DateTime(2022, 8, 28, 21, 59, 56, 944, DateTimeKind.Local).AddTicks(4718), null, "Size", null },
                    { new Guid("e7c78158-611f-4342-901a-2292077956f2"), new DateTime(2022, 8, 28, 21, 59, 56, 944, DateTimeKind.Local).AddTicks(4640), null, "Color", null }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "CategoryStatus",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Active" },
                    { 2, "Passive" }
                });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "Id", "CreatedOn", "CurrencyCode", "DeletedOn", "Name", "UpdatedOn" },
                values: new object[,]
                {
                    { new Guid("36ca7562-7e7b-4e7f-bbff-fc9d410b41cd"), new DateTime(2022, 8, 28, 21, 59, 56, 944, DateTimeKind.Local).AddTicks(4566), "USD", null, "American Dollar", null },
                    { new Guid("3a44d44f-2b39-41f3-9bf5-d92659008f13"), new DateTime(2022, 8, 28, 21, 59, 56, 944, DateTimeKind.Local).AddTicks(4464), "TL", null, "Turkish Lira", null }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "ProductStatus",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Active" },
                    { 2, "Passive" }
                });

            migrationBuilder.InsertData(
                table: "AttributeValue",
                columns: new[] { "Id", "AttributeId", "CreatedOn", "DeletedOn", "Name", "UpdatedOn" },
                values: new object[,]
                {
                    { new Guid("010f6e70-84fa-4eb0-b9df-ad6fda2234e2"), new Guid("d444a5b5-5216-4a67-9831-fbd85320aebb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "S", null },
                    { new Guid("156c3f47-f80f-45d8-b6d0-9d8013656220"), new Guid("87d331c9-70cb-4d1a-943b-4febba5abdb3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Sneckhers", null },
                    { new Guid("16d7c732-7393-438f-9450-9581dc1890c6"), new Guid("d444a5b5-5216-4a67-9831-fbd85320aebb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "XL", null },
                    { new Guid("1ef0875f-d8be-4460-a84b-0f89effce12d"), new Guid("d444a5b5-5216-4a67-9831-fbd85320aebb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "M", null },
                    { new Guid("42d0590a-91a6-4891-8ca8-7e683302a89e"), new Guid("87d331c9-70cb-4d1a-943b-4febba5abdb3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Tommy Hilfiger", null },
                    { new Guid("45a1848b-36cf-4913-801d-6b98071599b8"), new Guid("d444a5b5-5216-4a67-9831-fbd85320aebb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "L", null },
                    { new Guid("5db83dcf-7b06-43ff-9d38-2a844635e421"), new Guid("87d331c9-70cb-4d1a-943b-4febba5abdb3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Nike", null },
                    { new Guid("9e269352-04df-4d97-98cf-00acd5841680"), new Guid("e7c78158-611f-4342-901a-2292077956f2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "White", null },
                    { new Guid("c0040901-984f-4bf9-8602-bc8dee723467"), new Guid("d444a5b5-5216-4a67-9831-fbd85320aebb"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "2XL", null },
                    { new Guid("db278eaa-570d-4915-ad04-004b5f46e55c"), new Guid("e7c78158-611f-4342-901a-2292077956f2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Black", null },
                    { new Guid("db51cb73-0317-43d4-99e4-63c008e24629"), new Guid("e7c78158-611f-4342-901a-2292077956f2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Red", null }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Category",
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "Name", "StatusId", "UpdatedOn" },
                values: new object[] { new Guid("3f9a23db-435c-41b8-8d1b-69ac3c4f3143"), new DateTime(2022, 8, 28, 21, 59, 56, 944, DateTimeKind.Local).AddTicks(4808), null, "Clothes", 1, null });

            migrationBuilder.InsertData(
                table: "CategoryAttribute",
                columns: new[] { "Id", "AttributeId", "CategoryId", "CreatedOn", "DeletedOn", "UpdatedOn" },
                values: new object[] { new Guid("c22701fe-58c6-4bfb-9b42-9db8c0ddaea3"), new Guid("d444a5b5-5216-4a67-9831-fbd85320aebb"), new Guid("3f9a23db-435c-41b8-8d1b-69ac3c4f3143"), new DateTime(2022, 8, 28, 21, 59, 56, 944, DateTimeKind.Local).AddTicks(4843), null, null });

            migrationBuilder.InsertData(
                table: "CategoryAttribute",
                columns: new[] { "Id", "AttributeId", "CategoryId", "CreatedOn", "DeletedOn", "UpdatedOn" },
                values: new object[] { new Guid("d014529a-14a7-413f-94f3-dbc5ef3b326d"), new Guid("e7c78158-611f-4342-901a-2292077956f2"), new Guid("3f9a23db-435c-41b8-8d1b-69ac3c4f3143"), new DateTime(2022, 8, 28, 21, 59, 56, 944, DateTimeKind.Local).AddTicks(4835), null, null });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Product",
                columns: new[] { "Id", "CategoryId", "CreatedOn", "CurrencyId", "DeletedOn", "Name", "Price", "StatusId", "UpdatedOn" },
                values: new object[] { new Guid("ad0586ca-9e8d-4394-a7b7-e7ae81ca67b6"), new Guid("3f9a23db-435c-41b8-8d1b-69ac3c4f3143"), new DateTime(2022, 8, 28, 21, 59, 56, 944, DateTimeKind.Local).AddTicks(4890), new Guid("3a44d44f-2b39-41f3-9bf5-d92659008f13"), null, "Tshirt", 150m, 1, null });

            migrationBuilder.InsertData(
                table: "ProductAttribute",
                columns: new[] { "Id", "AttributeValueId", "CreatedOn", "DeletedOn", "ProductId", "UpdatedOn" },
                values: new object[] { new Guid("4be4d5d0-876e-4ab2-ba4a-93f689ea1c69"), new Guid("9e269352-04df-4d97-98cf-00acd5841680"), new DateTime(2022, 8, 28, 21, 59, 56, 944, DateTimeKind.Local).AddTicks(4909), null, new Guid("ad0586ca-9e8d-4394-a7b7-e7ae81ca67b6"), null });

            migrationBuilder.InsertData(
                table: "ProductAttribute",
                columns: new[] { "Id", "AttributeValueId", "CreatedOn", "DeletedOn", "ProductId", "UpdatedOn" },
                values: new object[] { new Guid("4d6506c9-489b-4104-aefc-2f9ff0edd9d0"), new Guid("16d7c732-7393-438f-9450-9581dc1890c6"), new DateTime(2022, 8, 28, 21, 59, 56, 944, DateTimeKind.Local).AddTicks(4925), null, new Guid("ad0586ca-9e8d-4394-a7b7-e7ae81ca67b6"), null });

            migrationBuilder.InsertData(
                table: "ProductAttribute",
                columns: new[] { "Id", "AttributeValueId", "CreatedOn", "DeletedOn", "ProductId", "UpdatedOn" },
                values: new object[] { new Guid("a0887d7d-c466-4da2-9a5d-f21701d16526"), new Guid("5db83dcf-7b06-43ff-9d38-2a844635e421"), new DateTime(2022, 8, 28, 21, 59, 56, 944, DateTimeKind.Local).AddTicks(4918), null, new Guid("ad0586ca-9e8d-4394-a7b7-e7ae81ca67b6"), null });

            migrationBuilder.CreateIndex(
                name: "IX_AttributeValue_AttributeId",
                table: "AttributeValue",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_StatusId",
                schema: "dbo",
                table: "Category",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryAttribute_AttributeId",
                table: "CategoryAttribute",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryAttribute_CategoryId",
                table: "CategoryAttribute",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoryId",
                schema: "dbo",
                table: "Product",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CurrencyId",
                schema: "dbo",
                table: "Product",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_StatusId",
                schema: "dbo",
                table: "Product",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttribute_AttributeValueId",
                table: "ProductAttribute",
                column: "AttributeValueId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttribute_ProductId",
                table: "ProductAttribute",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryAttribute");

            migrationBuilder.DropTable(
                name: "ProductAttribute");

            migrationBuilder.DropTable(
                name: "AttributeValue");

            migrationBuilder.DropTable(
                name: "Product",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Attribute");

            migrationBuilder.DropTable(
                name: "Category",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Currency");

            migrationBuilder.DropTable(
                name: "ProductStatus",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "CategoryStatus",
                schema: "dbo");
        }
    }
}
