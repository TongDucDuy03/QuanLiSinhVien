using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using test3.Models;

namespace test3.Controllers
{
    public class QLMHController : Controller
    {
        // GET: QLMH
        QLSVEntities db = new QLSVEntities();
        public ActionResult DanhSachMonHoc()
        {
            List<MonHoc> danhSachMonHoc = db.MonHocs.ToList();
            return View(danhSachMonHoc);
        }
        [HttpGet]
        public ActionResult Search(string searchMaMon)
        {
            List<MonHoc> searchmamon = db.MonHocs.Where(s => s.MaMon == searchMaMon).ToList();
            return View("DanhSachMonHoc", searchMaMon);
        }
        [HttpGet]
        public ActionResult ThemMonHoc()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemMonHoc(MonHoc monHoc)
        {
            QLSVEntities db = new QLSVEntities();
            db.MonHocs.Add(monHoc);
            db.SaveChanges();
            return RedirectToAction("DanhSachMonHoc");
        }
        public ActionResult Xoa(string maMon)
        {
            QLSVEntities db = new QLSVEntities();
            var monhoc = db.MonHocs.Find(maMon);
            db.MonHocs.Remove(monhoc);
            db.SaveChanges();
            return RedirectToAction("DanhSachMonHoc");
        }
        [HttpGet]
        public ActionResult Suathongtinmonhoc(string maMon)
        {
            QLSVEntities db = new QLSVEntities();
            var monhoc = db.MonHocs.Find(maMon);
            return View(monhoc);
        }
        [HttpPost]
        public ActionResult Suathongtinmonhoc(MonHoc monhoc)
        {
            db.Entry(monhoc).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("DanhSachMonHoc");
        }
    }
}