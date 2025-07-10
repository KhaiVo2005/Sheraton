using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Sheraton.Models;
using System.Data;
using System.Security.Claims;

namespace Sheraton
{
    public class SidebarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var area = ViewComponentContext.ViewContext.RouteData.Values["area"]?.ToString()?.ToLower();
            var role = area switch
            {
                "sale" => "Sale",
                "humanresources" => "HumanResources",
                "kitchen" => "Kitchen",
                "banquet" => "Banquet",
                "accounting" => "Accounting",
                _ => null
            };


            var menus = GetMenusForRole(role);
            return View("~/Views/Shared/Components/Default.cshtml", menus);
        }

        private List<MenuItem> GetMenusForRole(string role)
        {
            var menus = new List<MenuItem>();

            if (role == "Banquet")
            {
                menus.Add(new MenuItem { Area = "Banquet", Controller = "HopDongs", Action = "getHopDong", Icon = "fa-calendar-check", Title = "Quản lý sự kiện" });
                menus.Add(new MenuItem { Area = "Banquet", Controller = "SanhTiecs", Action = "getSanhTiec", Icon = "fa-door-open", Title = "Quản lý sảnh" });
                menus.Add(new MenuItem { Area = "Banquet", Controller = "DichVu", Action = "Index", Icon = "fa-bell-concierge", Title = "Quản lý dịch vụ" });
            }
            else if (role == "Sale")
            {
                menus.Add(new MenuItem { Area = "Sale", Controller = "HopDongs", Action = "getHopDong", Icon = "fa-clipboard-list", Title = "Quản lý đặt tiệc" });
                menus.Add(new MenuItem { Area = "Sale", Controller = "KhachHangs", Action = "getKhachHang", Icon = "fa-user-friends", Title = "Quản lý khách hàng" });
            }
            else if (role == "HumanResources")
            {
                menus.Add(new MenuItem { Area = "HumanResources", Controller = "NhanViens", Action = "getNhanVien", Icon = "fa-id-card", Title = "Quản lý nhân sự" });
                menus.Add(new MenuItem { Area = "HumanResources", Controller = "LuongNV", Action = "Index", Icon = "fa-money-check-dollar", Title = "Tính lương nhân viên" });
            }
            else if (role == "Kitchen")
            {
                menus.Add(new MenuItem { Area = "Kitchen", Controller = "MonAns", Action = "getMonAn", Icon = "fa-utensils", Title = "Quản lý thực đơn" });
                menus.Add(new MenuItem { Area = "Kitchen", Controller = "DSSuKien", Action = "Index", Icon = "fa-calendar-day", Title = "Danh sách sự kiện" });
                menus.Add(new MenuItem { Area = "Kitchen", Controller = "NguyenLieu", Action = "Index", Icon = "fa-carrot", Title = "Quản lý nguyên liệu" });
            }
            else if (role == "Accounting")
            {
                menus.Add(new MenuItem { Area = "Accounting", Controller = "Home", Action = "ThongKeDoanhThu", Icon = "fa-chart-line", Title = "Thống kê doanh thu" });
                menus.Add(new MenuItem { Area = "Accounting", Controller = "HoaDon", Action = "Index", Icon = "fa-file-invoice-dollar", Title = "Thanh toán hóa đơn" });
                menus.Add(new MenuItem { Area = "Accounting", Controller = "LuongNV", Action = "Index", Icon = "fa-sack-dollar", Title = "Thanh toán lương" });
            }

            return menus;
        }

    }

}
