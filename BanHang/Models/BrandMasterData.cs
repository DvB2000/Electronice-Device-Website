using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanHang.Models
{
    public partial class BrandMasterData
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập đủ thông tin")]
        [Display(Name = "Tên thương hiệu")]
        public string Name { get; set; }
        [Display(Name = "Hình ảnh")]
        public string Avatar { get; set; }
        [Display(Name = "Tên")]
        public string Slug { get; set; }
        [Display(Name = "Hiển thị trang chủ")]
        public Nullable<bool> ShowOnHomePage { get; set; }
        [Display(Name = "Thứ tự hiện")]
        public Nullable<int> DisplayOrder { get; set; }
        public Nullable<System.DateTime> CreatedOnUtc { get; set; }
        public Nullable<System.DateTime> UpdatedOnUtc { get; set; }
        [Display(Name = "Xóa")]
        public Nullable<bool> Deleted { get; set; }
    }
}