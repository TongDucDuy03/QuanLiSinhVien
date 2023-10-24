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
            if (giangVien.GioiTinh)
            {
                ViewBag.NamChecked = true;
                ViewBag.NuChecked = false;
            }
            else
            {
                ViewBag.NamChecked = false;
                ViewBag.NuChecked = true;
            }
            ViewBag.KhoaCu = giangVien.MaKhoa;
            return View(giangVien);
        }
        [HttpPost]
        public ActionResult Suathongtin(GiangVien giangVien)
        {
            db.Entry(giangVien).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("DanhSachgiangVien");
        }
    }
}