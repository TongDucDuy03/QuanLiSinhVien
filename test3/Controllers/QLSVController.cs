using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using test3.Models;

namespace test3.Controllers
{
    public class QLSVController : Controller
    {
        // GET: QLSV
        QLSVEntities db = new QLSVEntities();
        public ActionResult DanhSachSinhVien()
        {
            List<SinhVien> danhSachSinhVien = db.SinhViens.ToList();
            return View(danhSachSinhVien);
        }
        [HttpGet]
        public ActionResult Search(string searchField, string searchValue)
        {
            searchValue = searchValue.ToLower(); // Chuyển giá trị tìm kiếm về chữ thường

            List<SinhVien> searchResults = new List<SinhVien>();

            switch (searchField)
            {
                case "MSSV":
                    searchResults = db.SinhViens.Where(s => s.MSSV.ToLower().StartsWith(searchValue)).ToList();
                    break;
                case "MaLop":
                    searchResults = db.SinhViens.Where(s => s.MaLop.ToLower().StartsWith(searchValue)).ToList();
                    break;
                case "MaKhoa":
                    searchResults = db.SinhViens.Where(s => s.MaKhoa.ToLower().StartsWith(searchValue)).ToList();
                    break;
                case "GioiTinh":
                    // Trường "GioiTinh" là kiểu bit (1 hoặc 0)
                    bool searchValueAsBool = (searchValue == "1" || searchValue == "true");
                    searchResults = db.SinhViens.Where(s => s.GioiTinh == searchValueAsBool).ToList();
                    break;
                case "NoiSinh":
                    searchResults = db.SinhViens.Where(s => s.NoiSinh.ToLower().StartsWith(searchValue)).ToList();
                    break;
                default:
                    // Xử lý mặc định nếu không có trường nào được chọn
                    break;
            }

            return View("DanhSachSinhVien", searchResults);
        }


        [HttpGet]
        public ActionResult ThemMoiSinhVien()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ThemMoiSinhVien(SinhVien sinhVien)
        {
            if (string.IsNullOrEmpty(sinhVien.SvUser))
            {
                ViewBag.SvUserError = "Yêu cầu nhập tài khoản sinh viên.";
            }
            if (string.IsNullOrEmpty(sinhVien.SvPass))
            {
                ViewBag.SvPassError = "Yêu cầu nhập mật khẩu sinh viên.";
            }
            if (string.IsNullOrEmpty(sinhVien.HoTenSV))
            {
                ViewBag.HoTenSVError = "Yêu cầu nhập họ tên sinh viên.";
            }

            if (sinhVien.NgaySinhSV == null || sinhVien.NgaySinhSV == DateTime.MinValue)
            {
                ModelState.Remove("NgaySinhSV"); // Xóa ModelState Error cho trường này
                ViewBag.NgaySinhSVError = "Yêu cầu nhập ngày sinh sinh viên.";
            }


            // Kiểm tra trường "NgaySinhSV" và đảm bảo năm sinh sau 2006
            // Kiểm tra trường "NgaySinhSV" và đảm bảo năm sinh sau 2006
            if (sinhVien.NgaySinhSV.Year > 2006)
            {
                ModelState.AddModelError("NgaySinhSV", "Chưa đủ 18 tuổi.");
            }

            // Kiểm tra trường "GioiTinh"
            // Kiểm tra trường "GioiTinh"
            if (sinhVien.GioiTinh == null)
            {
                ModelState.AddModelError("GioiTinh", "Yêu cầu chọn giới tính sinh viên.");
            }



            if (string.IsNullOrEmpty(sinhVien.MaKhoa))
            {
                ViewBag.MaKhoaError = "Yêu cầu chọn mã khoa sinh viên.";
            }
            if (string.IsNullOrEmpty(sinhVien.MaLop))
            {
                ViewBag.MaLopError = "Yêu cầu chọn lớp sinh viên.";
            }
            if (string.IsNullOrEmpty(sinhVien.NoiSinh))
            {
                ViewBag.NoiSinhError = "Yêu cầu nhập nơi sinh sinh viên.";
            }

            if (string.IsNullOrEmpty(sinhVien.SvUser) || string.IsNullOrEmpty(sinhVien.SvPass) || string.IsNullOrEmpty(sinhVien.HoTenSV) ||
                sinhVien.NgaySinhSV == null || sinhVien.GioiTinh == null || string.IsNullOrEmpty(sinhVien.MaKhoa) || string.IsNullOrEmpty(sinhVien.MaLop) ||string.IsNullOrEmpty(sinhVien.NoiSinh) )
            {
                ViewBag.ErrorMessage = "Yêu cầu nhập đủ thông tin.";
                return View(sinhVien);
            }

            // Nếu dữ liệu hợp lệ, tiến hành thêm mới giảng viên
            QLSVEntities db = new QLSVEntities();
            db.SinhViens.Add(sinhVien);
            db.SaveChanges();
            return RedirectToAction("DanhSachSinhVien");
        }


        [HttpGet]
        public ActionResult Suathongtin(string id)
        {
            QLSVEntities db = new QLSVEntities();
            var sinhVien = db.SinhViens.Find(id);
            if (sinhVien.GioiTinh)
            {
                ViewBag.NamChecked = true;
                ViewBag.NuChecked = false;
            }
            else 
            {
                ViewBag.NamChecked = false;
                ViewBag.NuChecked = true;
            }
            ViewBag.KhoaCu = sinhVien.MaKhoa;
            ViewBag.LopCu = sinhVien.MaLop;
            return View(sinhVien);
        }
        public ActionResult Xoa(string id)
        {
            QLSVEntities db = new QLSVEntities();
            var sinhVien = db.SinhViens.Find(id);
            db.SinhViens.Remove(sinhVien);
            db.SaveChanges();
            return RedirectToAction("DanhSachSinhVien");
        }
        [HttpPost]
        public ActionResult Suathongtin(SinhVien sinhVien)
        {
            db.Entry(sinhVien).State = EntityState.Modified;//Entity Framework biết rằng đối tượng sinhVien đã bị thay đổi và cần được cập nhật.
            db.SaveChanges();//Entity Framework sẽ tạo truy vấn SQL UPDATE để cập nhật dữ liệu của sinhVien trong cơ sở dữ liệu.
            return RedirectToAction("DanhSachSinhVien");
        }
       
    }
}