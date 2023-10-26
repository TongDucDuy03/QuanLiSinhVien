using System;
using System.Collections.Generic;
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
        
    }
}