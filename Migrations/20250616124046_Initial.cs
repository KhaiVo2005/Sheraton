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
                name: "DatHangs",
                columns: table => new
                {
                    MaDatHang = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenMH = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatHangs", x => x.MaDatHang);
                });

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
                    ChucVu = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanhTiecs", x => x.MaSanh);
                });

            migrationBuilder.CreateTable(
                name: "HopDongs",
                columns: table => new
                {
                    MaHD = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NgayKy = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TienCoc = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaKH = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaNV = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HopDongs", x => x.MaHD);
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
                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MonAnMaMon = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    HopDongMaHD = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietDatTiecs", x => new { x.MaHD, x.MaMon });
                    table.ForeignKey(
                        name: "FK_ChiTietDatTiecs_HopDongs_HopDongMaHD",
                        column: x => x.HopDongMaHD,
                        principalTable: "HopDongs",
                        principalColumn: "MaHD");
                    table.ForeignKey(
                        name: "FK_ChiTietDatTiecs_MonAns_MonAnMaMon",
                        column: x => x.MonAnMaMon,
                        principalTable: "MonAns",
                        principalColumn: "MaMon");
                });

            migrationBuilder.CreateTable(
                name: "ChiTietDichVus",
                columns: table => new
                {
                    MaHD = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaDV = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietDichVus", x => new { x.MaHD, x.MaDV });
                    table.ForeignKey(
                        name: "FK_ChiTietDichVus_DichVus_MaDV",
                        column: x => x.MaDV,
                        principalTable: "DichVus",
                        principalColumn: "MaDV",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChiTietDichVus_HopDongs_MaHD",
                        column: x => x.MaHD,
                        principalTable: "HopDongs",
                        principalColumn: "MaHD",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HoaDons",
                columns: table => new
                {
                    MaHD = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoaiHD = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PTTT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaHopDong = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaDH = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaNV = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDons", x => x.MaHD);
                    table.ForeignKey(
                        name: "FK_HoaDons_DatHangs_MaDH",
                        column: x => x.MaDH,
                        principalTable: "DatHangs",
                        principalColumn: "MaDatHang",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HoaDons_HopDongs_MaHopDong",
                        column: x => x.MaHopDong,
                        principalTable: "HopDongs",
                        principalColumn: "MaHD",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HoaDons_NhanViens_MaNV",
                        column: x => x.MaNV,
                        principalTable: "NhanViens",
                        principalColumn: "MaNV",
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
                name: "IX_ChiTietDatTiecs_HopDongMaHD",
                table: "ChiTietDatTiecs",
                column: "HopDongMaHD");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDatTiecs_MonAnMaMon",
                table: "ChiTietDatTiecs",
                column: "MonAnMaMon");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietDichVus_MaDV",
                table: "ChiTietDichVus",
                column: "MaDV");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDons_MaDH",
                table: "HoaDons",
                column: "MaDH");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDons_MaHopDong",
                table: "HoaDons",
                column: "MaHopDong");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDons_MaNV",
                table: "HoaDons",
                column: "MaNV");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietDatTiecs");

            migrationBuilder.DropTable(
                name: "ChiTietDichVus");

            migrationBuilder.DropTable(
                name: "HoaDons");

            migrationBuilder.DropTable(
                name: "PhanCongs");

            migrationBuilder.DropTable(
                name: "MonAns");

            migrationBuilder.DropTable(
                name: "DichVus");

            migrationBuilder.DropTable(
                name: "DatHangs");

            migrationBuilder.DropTable(
                name: "LichDatSanhs");

            migrationBuilder.DropTable(
                name: "HopDongs");

            migrationBuilder.DropTable(
                name: "SanhTiecs");

            migrationBuilder.DropTable(
                name: "KhachHangs");

            migrationBuilder.DropTable(
                name: "NhanViens");
        }
    }
}
