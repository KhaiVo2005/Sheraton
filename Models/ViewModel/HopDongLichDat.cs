﻿namespace Sheraton.Models.ViewModel
{
    public class HopDongLichDat
    {
        public HopDong HopDong { get; set; } = new HopDong();
        public LichDatSanh LichDatSanh { get; set; } = new LichDatSanh();

        // Gợi ý mở rộng: VD lấy số lượng khách từ người dùng (để lọc sảnh đủ chỗ)
        public int SoLuongKhach { get; set; }

        // Có thể bổ sung thêm ngày tổ chức để lọc sảnh theo ngày
        public DateTime NgayTiec => LichDatSanh.BatDau.Date;
        public ChiTietDatTiec[] monAns { get; set; } = new ChiTietDatTiec[50];
    }
}
