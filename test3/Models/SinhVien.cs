//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace test3.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SinhVien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SinhVien()
        {
            this.BangDiems = new HashSet<BangDiem>();
        }
    
        public string MSSV { get; set; }
        public string SvUser { get; set; }
        public string SvPass { get; set; }
        public string HoTenSV { get; set; }
        public System.DateTime NgaySinhSV { get; set; }
        public bool GioiTinh { get; set; }
        public string MaLop { get; set; }
        public string NoiSinh { get; set; }
        public string MaKhoa { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BangDiem> BangDiems { get; set; }
        public virtual Khoa Khoa { get; set; }
        public virtual Lop Lop { get; set; }
    }
}
