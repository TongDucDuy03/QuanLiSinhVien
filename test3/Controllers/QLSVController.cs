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
        public ActionResult Search(string searchMSSV)
        {
            // Tìm kiếm sinh viên dựa trên MSSV
            List<SinhVien> searchMssv = db.SinhViens.Where(s => s.MSSV == searchMSSV).ToList();

            // Trả về view hiển thị danh sách sinh viên kết quả
            return View("DanhSachSinhVien", searchMssv);
        }

        [HttpGet]
        public ActionResult ThemMoiSinhVien()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ThemMoiSinhVien(SinhVien sinhVien)
        {
            // Kiểm tra họ tên sinh viên
            if (string.IsNullOrEmpty(sinhVien.HoTenSV))
            {
                ModelState.AddModelError("HoTen", "Họ tên sinh viên là bắt buộc.");
            }
            else if (!Regex.IsMatch(sinhVien.HoTenSV, @"^[a-zA-Z\s]+$"))
            {
                ModelState.AddModelError("HoTen", "Họ tên sinh viên chỉ được chứa chữ cái và khoảng trắng.");
            }

            // Kiểm tra ngày sinh
            if (sinhVien.NgaySinhSV >= DateTime.Now.Date)
            {
                ModelState.AddModelError("NgaySinh", "Ngày sinh phải nhỏ hơn ngày hiện tại.");
            }
            else
            {
                DateTime today = DateTime.Today;
                int age = today.Year - sinhVien.NgaySinhSV.Year;
                if (sinhVien.NgaySinhSV > today.AddYears(-age))
                {
                    age--;
                }
                if (age < 18)
                {
                    ModelState.AddModelError("NgaySinh", "Sinh viên phải đủ 18 tuổi.");
                }
            }

            // Kiểm tra giới tính
            if (sinhVien.GioiTinh == null)
            {
                ModelState.AddModelError("GioiTinh", "Giới tính là bắt buộc.");
            }

            // Kiểm tra các điều kiện hợp lệ
            if (ModelState.IsValid)
            {
                QLSVEntities db = new QLSVEntities();
                db.SinhViens.Add(sinhVien);
                db.SaveChanges();
                return RedirectToAction("DanhSachSinhVien");
            }

            return View(sinhVien); // Trả về view với thông tin sinh viên và các thông báo lỗi
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
        [HttpPost]
        public ActionResult Suathongtin(SinhVien sinhVien)
        {
            db.Entry(sinhVien).State = EntityState.Modified;//Entity Framework biết rằng đối tượng sinhVien đã bị thay đổi và cần được cập nhật.
            db.SaveChanges();//Entity Framework sẽ tạo truy vấn SQL UPDATE để cập nhật dữ liệu của sinhVien trong cơ sở dữ liệu.
            return RedirectToAction("DanhSachSinhVien");
        }
       
    }
}