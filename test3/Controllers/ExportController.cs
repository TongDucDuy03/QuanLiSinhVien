using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using test3.Models;

namespace test3.Controllers
{
    public class ExportController : Controller
    {
        // GET: Export
        public ActionResult ExportSinhVien()
        {
            // Lấy danh sách sinh viên từ cơ sở dữ liệu hoặc từ nơi bạn lưu trữ dữ liệu
            List<SinhVien> SinhViens = GetSinhVienListFromDatabase();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("DanhSachSinhVien");

                // Định dạng tiêu đề
                worksheet.Cell(1, 1).Value = "MSSV";
                worksheet.Cell(1, 2).Value = "Họ tên";
                worksheet.Cell(1, 3).Value = "Ngày sinh";
                worksheet.Cell(1, 4).Value = "Giới tính";
                worksheet.Cell(1, 5).Value = "Khoa";
                worksheet.Cell(1, 6).Value = "Lớp";
                worksheet.Cell(1, 7).Value = "Nơi sinh";
                // Ghi danh sách sinh viên vào file Excel
                for (int i = 0; i < SinhViens.Count; i++)
                {
                    var row = i + 2;
                    worksheet.Cell(row, 1).Value = SinhViens[i].MSSV;
                    worksheet.Cell(row, 2).Value = SinhViens[i].HoTenSV;
                    worksheet.Cell(row, 3).Value = SinhViens[i].NgaySinhSV.ToString("dd-MM-yyyy");
                    worksheet.Cell(row, 4).Value = SinhViens[i].GioiTinh ? "Nam" : "Nữ";
                    worksheet.Cell(row, 5).Value = SinhViens[i].Khoa.TenKhoa;
                    worksheet.Cell(row, 6).Value = SinhViens[i].Lop.MaLop;
                    worksheet.Cell(row, 7).Value = SinhViens[i].NoiSinh;
                }

                // Tạo tệp Excel và trả về nó cho người dùng
                var memoryStream = new MemoryStream();
                workbook.SaveAs(memoryStream);

                return File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DanhSachSinhVien.xlsx");
            }
        }

        private List<SinhVien> GetSinhVienListFromDatabase()
        {
            var db = new QLSVEntities();
            // Viết mã lấy danh sách sinh viên từ cơ sở dữ liệu của bạn ở đây
            return db.SinhViens.ToList();
        }
    }
}