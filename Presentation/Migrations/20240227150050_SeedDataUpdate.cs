using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Presentation.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Name", "Price", "Url" },
                values: new object[] { "Bir Yıl dinlendirilmiş Eski Kars Kaşarı", "Eski Kaşar Bir Yılık", 350.0, "eksi-kasar-bir-yillik" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Name", "Price", "Url" },
                values: new object[] { "Taze Kars Kaşarı %100 Süt", "Yeni Kaşar", 300.0, "yeni-kasar" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Price" },
                values: new object[] { "Natural Beyaz Peynir %100 Süt", 300.0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "Description",
                value: "Kara Kovan Petek Bal %100 Natural");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "Description",
                value: "Süzme Bal %100 Natural");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Description", "Price" },
                values: new object[] { "Çeçil Peyniri", 275.0 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "ImageUrl", "IsApproved", "IsHome", "Name", "Price", "StockQuantity", "Url", "Weight" },
                values: new object[,]
                {
                    { 7, "İki Yıl dinlendirilmiş Eski Kars Kaşarı", "7.jpg", true, true, "Eski Kaşar İki Yılık", 375.0, 100, "eksi-kasar-bir-yillik", 1000.0 },
                    { 8, "Üç Yıl dinlendirilmiş Eski Kars Kaşarı", "8.jpg", true, true, "Eski Kaşar Üç Yılık", 400.0, 100, "eksi-kasar-uc-yillik", 1000.0 },
                    { 9, "Stilton Mavi Kalıp İthal Peynir", "9.jpg", true, true, "Stilton Peynir", 500.0, 100, "stilton-peynir", 1000.0 },
                    { 10, "Hollanda İthal Peyniri", "10.jpg", true, true, "Hollanda Peyniri", 600.0, 100, "hollanda-peyniri", 1000.0 },
                    { 11, "Graviyer Füme Peyniri", "11.jpg", true, true, "Graviyer Peyniri", 650.0, 100, "graviyer-peyniri", 1000.0 },
                    { 12, "Süzme Bal Oğul Balı", "12.jpg", true, true, "Süzme Bal Oğul Balı", 750.0, 100, "suzme-bal-ogul-bali", 1000.0 }
                });

            migrationBuilder.InsertData(
                table: "ProductCategory",
                columns: new[] { "CategoryId", "ProductId" },
                values: new object[,]
                {
                    { 1, 7 },
                    { 3, 7 },
                    { 4, 7 },
                    { 1, 8 },
                    { 3, 8 },
                    { 4, 8 },
                    { 1, 9 },
                    { 3, 9 },
                    { 1, 10 },
                    { 3, 10 },
                    { 1, 11 },
                    { 3, 11 },
                    { 2, 12 },
                    { 7, 12 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 7 });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 3, 7 });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 4, 7 });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 8 });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 3, 8 });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 4, 8 });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 9 });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 3, 9 });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 10 });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 3, 10 });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 11 });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 3, 11 });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 2, 12 });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 7, 12 });

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Name", "Price", "Url" },
                values: new object[] { "Eski Kars Kaşarı Açıklaması", "Eski Kars Kaşarı", 300.0, "eksi-kars-kasari" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Name", "Price", "Url" },
                values: new object[] { "Yeni Kars Kaşarı Açıklaması", "Yeni Kars Kaşarı", 280.0, "yeni-kars-kasari" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Price" },
                values: new object[] { "Natural Beyaz Peynir Açıklaması", 250.0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "Description",
                value: "Kara Kovan Petek Bal Açıklaması");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "Description",
                value: "Süzme Bal Açıklaması");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Description", "Price" },
                values: new object[] { "Çeçil Peyniri Açıklaması", 260.0 });
        }
    }
}
