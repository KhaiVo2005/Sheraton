using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sheraton.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DichVus",
                columns: table => new
                {
                    MaDV = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenDV = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DonGia = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DichVus", x => x.MaDV);
                });

            migrationBuilder.CreateTable(
                name: "KhachHangs",
                columns: table => new
                {
                    MaKH = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenKH = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SDT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GioiTinh = table.Column<bool>(type: "bit", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachHangs", x => x.MaKH);
                });

            migrationBuilder.CreateTable(
                name: "MonAns",
                columns: table => new
                {
                    MaMon = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenMon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DonGia = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonAns", x => x.MaMon);
                });

            migrationBuilder.CreateTable(
                name: "NhanViens",
                columns: table => new
                {
                    MaNV = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenNV = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SDT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GioiTinh = table.Column<bool>(type: "bit", nullable: false),
                    ChucVu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TK = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MK = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanViens", x => x.MaNV);
                });

            migrationBuilder.CreateTable(
                name: "SanhTiecs",
                columns: table => new
                {
                    MaSanh = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenSanh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SucChua = table.Column<int>(type: "int", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gia = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HinhAnh = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanhTiecs", x => x.MaSanh);
                });

            migrationBuilder.CreateTable(
                name: "BangLuongs",
                columns: table => new
                {
                    MaBL = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TongGioLam = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TongLuong = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    MaNV = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BangLuongs", x => x.MaBL);
                    table.ForeignKey(
                        name: "FK_BangLuongs_NhanViens_MaNV",
                        column: x => x.MaNV,
                        principalTable: "NhanViens",
                        principalColumn: "MaNV",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DatHangs",
                columns: table => new
                {
                    MaDatHang = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenMH = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NhanVienMaNV = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatHangs", x => x.MaDatHang);
                    table.ForeignKey(
                        name: "FK_DatHangs_NhanViens_NhanVienMaNV",
                        column: x => x.NhanVienMaNV,
                        principalTable: "NhanViens",
                        principalColumn: "MaNV");
                });

            migrationBuilder.CreateTable(
                name: "HopDongs",
                columns: table => new
                {
                    MaHD = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NgayKy = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TienCoc = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PTTT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaKH = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaNV = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaDV = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HopDongs", x => x.MaHD);
                    table.ForeignKey(
                        name: "FK_HopDongs_DichVus_MaDV",
                        column: x => x.MaDV,
                        principalTable: "DichVus",
                        principalColumn: "MaDV",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HopDongs_KhachHangs_MaKH",
                        column: x => x.MaKH,
                        principalTable: "KhachHangs",
                        principalColumn: "MaKH",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HopDongs_NhanViens_MaNV",
                        column: x => x.MaNV,
                        principalTable: "NhanViens",
                        principalColumn: "MaNV",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietDatTiecs",
                columns: table => new
                {
                    MaMon = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaHD = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietDatTiecs", x => new { x.MaHD, x.MaMon });
                    table.ForeignKey(
                        name: "FK_ChiTietDatTiecs_HopDongs_MaHD",
                        column: x => x.MaHD,
                        principalTable: "HopDongs",
                        principalColumn: "MaHD",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChiTietDatTiecs_MonAns_MaMon",
                        column: x => x.MaMon,
                        principalTable: "MonAns",
                        principalColumn: "MaMon",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LichDatSanhs",
                columns: table => new
                {
                    MaLDS = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BatDau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KetThuc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaSanh = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaHD = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LichDatSanhs", x => x.MaLDS);
                    table.ForeignKey(
                        name: "FK_LichDatSanhs_HopDongs_MaHD",
                        column: x => x.MaHD,
                        principalTable: "HopDongs",
                        principalColumn: "MaHD",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LichDatSanhs_SanhTiecs_MaSanh",
                        column: x => x.MaSanh,
                        principalTable: "SanhTiecs",
                        principalColumn: "MaSanh",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    RatingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Score = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaHD = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.RatingId);
                    table.ForeignKey(
                        name: "FK_Ratings_HopDongs_MaHD",
                        column: x => x.MaHD,
                        principalTable: "HopDongs",
                        principalColumn: "MaHD",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "PhanCongs",
                columns: table => new
                {
                    MaNV = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaLDS = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CaLamViec = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhanCongs", x => new { x.MaLDS, x.MaNV });
                    table.ForeignKey(
                        name: "FK_PhanCongs_LichDatSanhs_MaLDS",
                        column: x => x.MaLDS,
                        principalTable: "LichDatSanhs",
                        principalColumn: "MaLDS",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PhanCongs_NhanViens_MaNV",
                        column: x => x.MaNV,
                        principalTable: "NhanViens",
                        principalColumn: "MaNV",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BangLuongs_MaNV",
                table: "BangLuongs",
                column: "MaNV");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDatTiecs_MaMon",
                table: "ChiTietDatTiecs",
                column: "MaMon");

            migrationBuilder.CreateIndex(
                name: "IX_DatHangs_NhanVienMaNV",
                table: "DatHangs",
                column: "NhanVienMaNV");

            migrationBuilder.CreateIndex(
                name: "IX_HopDongs_MaDV",
                table: "HopDongs",
                column: "MaDV");

            migrationBuilder.CreateIndex(
                name: "IX_HopDongs_MaKH",
                table: "HopDongs",
                column: "MaKH");

            migrationBuilder.CreateIndex(
                name: "IX_HopDongs_MaNV",
                table: "HopDongs",
                column: "MaNV");

            migrationBuilder.CreateIndex(
                name: "IX_LichDatSanhs_MaHD",
                table: "LichDatSanhs",
                column: "MaHD");

            migrationBuilder.CreateIndex(
                name: "IX_LichDatSanhs_MaSanh",
                table: "LichDatSanhs",
                column: "MaSanh");

            migrationBuilder.CreateIndex(
                name: "IX_PhanCongs_MaNV",
                table: "PhanCongs",
                column: "MaNV");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_MaHD",
                table: "Ratings",
                column: "MaHD");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BangLuongs");

            migrationBuilder.DropTable(
                name: "ChiTietDatTiecs");

            migrationBuilder.DropTable(
                name: "DatHangs");

            migrationBuilder.DropTable(
                name: "PhanCongs");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "MonAns");

            migrationBuilder.DropTable(
                name: "LichDatSanhs");

            migrationBuilder.DropTable(
                name: "HopDongs");

            migrationBuilder.DropTable(
                name: "SanhTiecs");

            migrationBuilder.DropTable(
                name: "DichVus");

            migrationBuilder.DropTable(
                name: "KhachHangs");

            migrationBuilder.DropTable(
                name: "NhanViens");
        }
    }
}
