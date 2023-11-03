using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using test3.Models;

namespace test3.Controllers
{
    public class QLGVController : Controller
    {
        // GET: QLGV
        QLSVEntities db = new QLSVEntities();
        public ActionResult DanhSachGiangVien()
        {
            List<GiangVien> danhSachGiangVien = db.GiangViens.ToList();
            return View(danhSachGiangVien);
        }
        [HttpGet]
        public ActionResult Search(string searchMGV)
        {
            List<GiangVien> searchMgv = db.GiangViens.Where(s => s.MGV == searchMGV).ToList();

            return View("DanhSachGiangVien", searchMgv);
        }
        [HttpGet]
        public ActionResult ThemMoiGiangVien()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ThemMoiGiangVien(GiangVien giangVien)
        {
            if (string.IsNullOrEmpty(giangVien.GvUser))
            {
                ViewBag.GvUserError = "Yêu cầu nhập tài khoản giảng viên.";
            }
            if (string.IsNullOrEmpty(giangVien.GvPass))
            {
                ViewBag.GvPassError = "Yêu cầu nhập mật khẩu giảng viên.";
            }
            if (string.IsNullOrEmpty(giangVien.HoTenGV))
            {
                ViewBag.HoTenGVError = "Yêu cầu nhập họ tên giảng viên.";
            }

            if (giangVien.NgaySinhGV == null || giangVien.NgaySinhGV == DateTime.MinValue)
            {
                ModelState.Remove("NgaySinhGV"); // Xóa ModelState Error cho trường này
                ViewBag.NgaySinhGVError = "Yêu cầu nhập ngày sinh giảng viên.";
            }


            // Kiểm tra trường "NgaySinhGV" và đảm bảo năm sinh sau 2006
            // Kiểm tra trường "NgaySinhGV" và đảm bảo năm sinh sau 2006
            if (giangVien.NgaySinhGV.Year > 2006)
            {
                ModelState.AddModelError("NgaySinhGV", "Chưa đủ 18 tuổi.");
            }

            // Kiểm tra trường "GioiTinh"
            // Kiểm tra trường "GioiTinh"
            if (giangVien.GioiTinh == null)
            {
                ModelState.AddModelError("GioiTinh", "Yêu cầu chọn giới tính giảng viên.");
            }

            if (string.IsNullOrEmpty(giangVien.MaKhoa))
            {
                ViewBag.MaKhoaError = "Yêu cầu chọn mã khoa giảng viên.";
            }

            if (string.IsNullOrEmpty(giangVien.GvUser) || string.IsNullOrEmpty(giangVien.GvPass) || string.IsNullOrEmpty(giangVien.HoTenGV) ||
                giangVien.NgaySinhGV == null || giangVien.GioiTinh == null || string.IsNullOrEmpty(giangVien.MaKhoa))
            {
                ViewBag.ErrorMessage = "Yêu cầu nhập đủ thông tin.";
                return View(giangVien);
            }

            // Nếu dữ liệu hợp lệ, tiến hành thêm mới giảng viên
            QLSVEntities db = new QLSVEntities();
            db.GiangViens.Add(giangVien);
            db.SaveChanges();
            return RedirectToAction("DanhSachGiangVien");
        }

        public ActionResult Xoa(string id)
        {
            QLSVEntities db = new QLSVEntities();
            var giangVien = db.GiangViens.Find(id);
            db.GiangViens.Remove(giangVien);
            db.SaveChanges();
            return RedirectToAction("DanhSachGiangVien");
        }
        [HttpGet]
        public ActionResult Suathongtin(string id)
        {
            QLSVEntities db = new QLSVEntities();
            var giangVien = db.GiangViens.Find(id);
           
           
            return View(giangVien);
        }
        [HttpPost]
        public ActionResult Suathongtin(GiangVien giangVien)
        {
            QLSVEntities db = new QLSVEntities();
            db.Entry(giangVien).State = EntityState.Modified;
            if (string.IsNullOrEmpty(giangVien.GvUser))
            {
                ViewBag.GvUserError = "Yêu cầu nhập tài khoản giảng viên.";
            }
            if (string.IsNullOrEmpty(giangVien.GvPass))
            {
                ViewBag.GvPassError = "Yêu cầu nhập mật khẩu giảng viên.";
            }
            if (string.IsNullOrEmpty(giangVien.HoTenGV))
            {
                ViewBag.HoTenGVError = "Yêu cầu nhập họ tên giảng viên.";
            }

            if (giangVien.NgaySinhGV == null || giangVien.NgaySinhGV == DateTime.MinValue)
            {
                ModelState.Remove("NgaySinhGV"); // Xóa ModelState Error cho trường này
                ViewBag.NgaySinhGVError = "Yêu cầu nhập ngày sinh giảng viên.";
            }


            // Kiểm tra trường "NgaySinhGV" và đảm bảo năm sinh sau 2006
            // Kiểm tra trường "NgaySinhGV" và đảm bảo năm sinh sau 2006
            if (giangVien.NgaySinhGV.Year > 2006)
            {
                ModelState.AddModelError("NgaySinhGV", "Chưa đủ 18 tuổi.");
            }

            // Kiểm tra trường "GioiTinh"
            // Kiểm tra trường "GioiTinh"
            if (giangVien.GioiTinh == null)
            {
                ModelState.AddModelError("GioiTinh", "Yêu cầu chọn giới tính giảng viên.");
            }



            if (string.IsNullOrEmpty(giangVien.MaKhoa))
            {
                ViewBag.MaKhoaError = "Yêu cầu chọn mã khoa giảng viên.";
            }

            if (string.IsNullOrEmpty(giangVien.GvUser) || string.IsNullOrEmpty(giangVien.GvPass) || string.IsNullOrEmpty(giangVien.HoTenGV) ||
                giangVien.NgaySinhGV == null || giangVien.GioiTinh == null || string.IsNullOrEmpty(giangVien.MaKhoa))
            {
                ViewBag.ErrorMessage = "Yêu cầu nhập đủ thông tin.";
                return View(giangVien);
            }

            // Nếu dữ liệu hợp lệ, tiến hành thêm mới giảng viên
            
           




            db.SaveChanges();
            return RedirectToAction("DanhSachgiangVien");
        }
        [HttpGet]
        public ActionResult ThemMoiTinTuc()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ThemMoiTinTuc(TinTuc tintuc)
        {
            if (string.IsNullOrEmpty(tintuc.TieuDe))
            {
                ViewBag.TieuDeError = "Yêu cầu nhập tiêu đề.";
                return View(tintuc);
            }
            if (string.IsNullOrEmpty(tintuc.NoiDung))
            {
                ViewBag.HoTenGVError = "Yêu cầu nhập nội dung.";
                return View(tintuc);
            }
            QLSVEntities db = new QLSVEntities();
            db.TinTucs.Add(tintuc);
            db.SaveChanges();
            return RedirectToAction("DashBoard","Home");
        }

    }
}