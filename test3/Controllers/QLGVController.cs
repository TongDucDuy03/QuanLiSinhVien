using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
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
        public ActionResult DanhSachGiangVien(int? page, int? pageSize)
        {
            if (page == null)
            {
                page = 1;
            }
            if (pageSize == null)
            {
                pageSize = 10;
            }
            var giangVien = db.GiangViens.ToList();
            return View(giangVien.ToPagedList((int)page, (int)pageSize));
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

            if (string.IsNullOrEmpty(giangVien.MGV))
            {
                ViewBag.MGVError = "Yêu cầu nhập mã giảng viên.";
            }
            else
            {
                using (QLSVEntities db = new QLSVEntities())
                {
                    // Kiểm tra xem MGV đã tồn tại trong cơ sở dữ liệu hay chưa
                    if (db.GiangViens.Any(gv => gv.MGV == giangVien.MGV))
                    {
                        ViewBag.MGVError = "Mã giảng viên đã tồn tại. Vui lòng sử dụng mã khác.";
                    }
                }
            }

            if (giangVien.NgaySinhGV == null || giangVien.NgaySinhGV == DateTime.MinValue)
            {
                ViewBag.NgaySinhGVError = "Yêu cầu nhập ngày sinh giảng viên.";
            }
            else
            {
                int currentYear = DateTime.Now.Year;
                int birthYear = giangVien.NgaySinhGV.Year;
                int age = currentYear - birthYear;

                if (age < 18)
                {
                    ViewBag.NgaySinhGVError = "Chưa đủ 18 tuổi.";
                }
                else
                {
                    try
                    {
                        using (QLSVEntities db = new QLSVEntities())
                        {
                            db.GiangViens.Add(giangVien);
                            db.SaveChanges();
                            return RedirectToAction("DanhSachGiangVien");
                        }
                    }
                    catch (Exception ex)
                    {
                        ViewBag.ErrorMessage = "Có lỗi xảy ra khi thêm giảng viên: " + ex.Message;
                    }
                }
            }

            // Kiểm tra trường "GioiTinh"
            if (giangVien.GioiTinh == null)
            {
                ViewBag.GioiTinhError = "Yêu cầu chọn giới tính giảng viên.";
            }

            if (string.IsNullOrEmpty(giangVien.MaKhoa))
            {
                ViewBag.MaKhoaError = "Yêu cầu chọn mã khoa giảng viên.";
            }

            return View(giangVien);
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

            ViewBag.KhoaCu = giangVien.MaKhoa;
            return View(giangVien);
        }
        [HttpPost]
        public ActionResult Suathongtin(GiangVien giangVien)
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
                ViewBag.NgaySinhGVError = "Yêu cầu nhập ngày sinh giảng viên.";
            }
            else
            {
                int currentYear = DateTime.Now.Year;
                int birthYear = giangVien.NgaySinhGV.Year;
                int age = currentYear - birthYear;

                if (age < 18)
                {
                    ViewBag.NgaySinhGVError = "Chưa đủ 18 tuổi.";
                }
                else
                {
                    try
                    {
                        using (QLSVEntities db = new QLSVEntities())
                        {
                            db.Entry(giangVien).State = EntityState.Modified;
                            db.SaveChanges();
                            return RedirectToAction("DanhSachGiangVien");
                        }
                    }
                    catch (Exception ex)
                    {
                        ViewBag.ErrorMessage = "Có lỗi xảy ra khi thêm giảng viên: " + ex.Message;
                    }
                }
            }

            // Kiểm tra trường "GioiTinh"
            if (giangVien.GioiTinh == null)
            {
                ViewBag.GioiTinhError = "Yêu cầu chọn giới tính giảng viên.";
            }

            if (string.IsNullOrEmpty(giangVien.MaKhoa))
            {
                ViewBag.MaKhoaError = "Yêu cầu chọn mã khoa giảng viên.";
            }

            return View(giangVien);
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