using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanHang.Models
{
    public partial class OrderMasterData 
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập đủ thông tin")]
        [Display(Name = "Tên đơn hàng")]
        public string Name { get; set; }
        [Display(Name = "Id tài khoản")]
        public Nullable<int> UserId { get; set; }
        [Display(Name = "Trạng thái")]
        public Nullable<int> Status { get; set; }
        [Display(Name = "Thời gian")]
        public Nullable<System.DateTime> CreatedOnUtc { get; set; }
    }
}