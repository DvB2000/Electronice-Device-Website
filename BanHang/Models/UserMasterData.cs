using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanHang.Models
{
    public partial class UserMasterData
    {
        public int Id { get; set; }
        [Display(Name = "Tên")]
        public string FirstName { get; set; }
        [Display(Name = "Họ")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Nhập địa chỉ Email!")]
        [Display(Name = "Email")]
        
        public string Email { get; set; }
        [Required(ErrorMessage = "Nhập mật khẩu!")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }
        [Display(Name = "Quyền admin")]
        public Nullable<bool> IsAdmin { get; set; }
    }
}