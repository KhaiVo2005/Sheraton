﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sheraton.Data;

#nullable disable

namespace Sheraton.Migrations
{
    [DbContext(typeof(SheratonDbContext))]
    [Migration("20250721075128_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Sheraton.Models.BangLuong", b =>
                {
                    b.Property<Guid>("MaBL")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MaNV")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("TongGioLam")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("TongLuong")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("MaBL");

                    b.HasIndex("MaNV");

                    b.ToTable("BangLuongs");
                });

            modelBuilder.Entity("Sheraton.Models.ChiTietDatTiec", b =>
                {
                    b.Property<Guid>("MaHD")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MaMon")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.Property<string>("TrangThai")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("MaHD", "MaMon");

                    b.HasIndex("MaMon");

                    b.ToTable("ChiTietDatTiecs");
                });

            modelBuilder.Entity("Sheraton.Models.DatHang", b =>
                {
                    b.Property<Guid>("MaDatHang")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("GhiChu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("NhanVienMaNV")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.Property<string>("TenMH")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaDatHang");

                    b.HasIndex("NhanVienMaNV");

                    b.ToTable("DatHangs");
                });

            modelBuilder.Entity("Sheraton.Models.DichVu", b =>
                {
                    b.Property<Guid>("MaDV")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("DonGia")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("MoTa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenDV")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaDV");

                    b.ToTable("DichVus");
                });

            modelBuilder.Entity("Sheraton.Models.HopDong", b =>
                {
                    b.Property<Guid>("MaHD")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MaDV")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MaKH")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MaNV")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("NgayKy")
                        .HasColumnType("datetime2");

                    b.Property<string>("PTTT")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.Property<decimal>("TienCoc")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("TrangThai")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaHD");

                    b.HasIndex("MaDV");

                    b.HasIndex("MaKH");

                    b.HasIndex("MaNV");

                    b.ToTable("HopDongs");
                });

            modelBuilder.Entity("Sheraton.Models.KhachHangg", b =>
                {
                    b.Property<Guid>("MaKH")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("GioiTinh")
                        .HasColumnType("bit");

                    b.Property<string>("SDT")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenKH")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaKH");

                    b.ToTable("KhachHangs");
                });

            modelBuilder.Entity("Sheraton.Models.LichDatSanh", b =>
                {
                    b.Property<Guid>("MaLDS")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("BatDau")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("KetThuc")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("MaHD")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MaSanh")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("TrangThai")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaLDS");

                    b.HasIndex("MaHD");

                    b.HasIndex("MaSanh");

                    b.ToTable("LichDatSanhs");
                });

            modelBuilder.Entity("Sheraton.Models.MonAn", b =>
                {
                    b.Property<Guid>("MaMon")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("DonGia")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("MoTa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenMon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaMon");

                    b.ToTable("MonAns");
                });

            modelBuilder.Entity("Sheraton.Models.NhanVien", b =>
                {
                    b.Property<Guid>("MaNV")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ChucVu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("GioiTinh")
                        .HasColumnType("bit");

                    b.Property<string>("MK")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SDT")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TK")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenNV")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaNV");

                    b.ToTable("NhanViens");
                });

            modelBuilder.Entity("Sheraton.Models.PhanCong", b =>
                {
                    b.Property<Guid>("MaLDS")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MaNV")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CaLamViec")
                        .HasColumnType("datetime2");

                    b.Property<string>("GhiChu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaLDS", "MaNV");

                    b.HasIndex("MaNV");

                    b.ToTable("PhanCongs");
                });

            modelBuilder.Entity("Sheraton.Models.Rating", b =>
                {
                    b.Property<int>("RatingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RatingId"));

                    b.Property<string>("Comment")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("MaHD")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.HasKey("RatingId");

                    b.HasIndex("MaHD");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("Sheraton.Models.SanhTiec", b =>
                {
                    b.Property<Guid>("MaSanh")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Gia")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("HinhAnh")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MoTa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SucChua")
                        .HasColumnType("int");

                    b.Property<string>("TenSanh")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TrangThai")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MaSanh");

                    b.ToTable("SanhTiecs");
                });

            modelBuilder.Entity("Sheraton.Models.BangLuong", b =>
                {
                    b.HasOne("Sheraton.Models.NhanVien", "NhanVien")
                        .WithMany("BangLuongs")
                        .HasForeignKey("MaNV")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NhanVien");
                });

            modelBuilder.Entity("Sheraton.Models.ChiTietDatTiec", b =>
                {
                    b.HasOne("Sheraton.Models.HopDong", "HopDong")
                        .WithMany("ChiTietDatTiecs")
                        .HasForeignKey("MaHD")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Sheraton.Models.MonAn", "MonAn")
                        .WithMany("ChiTietDatTiecs")
                        .HasForeignKey("MaMon")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("HopDong");

                    b.Navigation("MonAn");
                });

            modelBuilder.Entity("Sheraton.Models.DatHang", b =>
                {
                    b.HasOne("Sheraton.Models.NhanVien", null)
                        .WithMany("DatHangs")
                        .HasForeignKey("NhanVienMaNV");
                });

            modelBuilder.Entity("Sheraton.Models.HopDong", b =>
                {
                    b.HasOne("Sheraton.Models.DichVu", "DichVu")
                        .WithMany("HopDongs")
                        .HasForeignKey("MaDV")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Sheraton.Models.KhachHangg", "KhachHang")
                        .WithMany("HopDongs")
                        .HasForeignKey("MaKH")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Sheraton.Models.NhanVien", "NhanVien")
                        .WithMany("HopDongs")
                        .HasForeignKey("MaNV")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("DichVu");

                    b.Navigation("KhachHang");

                    b.Navigation("NhanVien");
                });

            modelBuilder.Entity("Sheraton.Models.LichDatSanh", b =>
                {
                    b.HasOne("Sheraton.Models.HopDong", "HopDong")
                        .WithMany("LichDatSanhs")
                        .HasForeignKey("MaHD")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Sheraton.Models.SanhTiec", "Sanh")
                        .WithMany("LichDatSanhs")
                        .HasForeignKey("MaSanh")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("HopDong");

                    b.Navigation("Sanh");
                });

            modelBuilder.Entity("Sheraton.Models.PhanCong", b =>
                {
                    b.HasOne("Sheraton.Models.LichDatSanh", "LichDatSanh")
                        .WithMany("PhanCongs")
                        .HasForeignKey("MaLDS")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Sheraton.Models.NhanVien", "NhanVien")
                        .WithMany("PhanCongs")
                        .HasForeignKey("MaNV")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("LichDatSanh");

                    b.Navigation("NhanVien");
                });

            modelBuilder.Entity("Sheraton.Models.Rating", b =>
                {
                    b.HasOne("Sheraton.Models.HopDong", "HopDong")
                        .WithMany("Ratings")
                        .HasForeignKey("MaHD")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("HopDong");
                });

            modelBuilder.Entity("Sheraton.Models.DichVu", b =>
                {
                    b.Navigation("HopDongs");
                });

            modelBuilder.Entity("Sheraton.Models.HopDong", b =>
                {
                    b.Navigation("ChiTietDatTiecs");

                    b.Navigation("LichDatSanhs");

                    b.Navigation("Ratings");
                });

            modelBuilder.Entity("Sheraton.Models.KhachHangg", b =>
                {
                    b.Navigation("HopDongs");
                });

            modelBuilder.Entity("Sheraton.Models.LichDatSanh", b =>
                {
                    b.Navigation("PhanCongs");
                });

            modelBuilder.Entity("Sheraton.Models.MonAn", b =>
                {
                    b.Navigation("ChiTietDatTiecs");
                });

            modelBuilder.Entity("Sheraton.Models.NhanVien", b =>
                {
                    b.Navigation("BangLuongs");

                    b.Navigation("DatHangs");

                    b.Navigation("HopDongs");

                    b.Navigation("PhanCongs");
                });

            modelBuilder.Entity("Sheraton.Models.SanhTiec", b =>
                {
                    b.Navigation("LichDatSanhs");
                });
#pragma warning restore 612, 618
        }
    }
}
