using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
            QLSVEntities db = new QLSVEntities();
            db.SinhViens.Add(sinhVien);
            db.SaveChanges();
            return RedirectToAction("DanhSachSinhVien");
        }

        public ActionResult Xoa(string id)
        {
            QLSVEntities db = new QLSVEntities();
            var sinhVien = db.SinhViens.Find(id);
            db.SinhViens.Remove(sinhVien);
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
            return View(sinhVien);
        }
        [HttpPost]
        public ActionResult Suathongtin(SinhVien sinhVien)
        {
            db.Entry(sinhVien).State = EntityState.Modified;//Entity Framework biết rằng đối tượng sinhVien đã bị thay đổi và cần được cập nhật.
            db.SaveChanges();//Entity Framework sẽ tạo truy vấn SQL UPDATE để cập nhật dữ liệu của sinhVien trong cơ sở dữ liệu.
            return RedirectToAction("DanhSachSinhVien");
        }
        public ActionResult GetLopByKhoa(string selectedKhoa)
        {
            // Thực hiện truy vấn để lấy danh sách lớp dựa trên khoa
            // selectedKhoa là mã khoa đã chọn
            List<Lop> dsLop = db.Lops.Where(l => l.MaKhoa == selectedKhoa).ToList();

            // Trả về danh sách lớp dưới dạng HTML
            return Content(RenderPartialViewToString("PartialLopList", dsLop));
        }

        private string RenderPartialViewToString(string v, List<Lop> dsLop)
        {
            throw new NotImplementedException();
        }
    }
}