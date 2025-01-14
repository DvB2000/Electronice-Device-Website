﻿using BanHang.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BanHang.Context
{
    
    [MetadataType(typeof(ProductMasterData))]
    public partial class Product
    {
        [NotMapped]
        public System.Web.HttpPostedFileBase ImageUpload { get; set; }
    }

    [MetadataType(typeof(CategoryMasterData))]
    public partial class Category
    {
        [NotMapped]
        public System.Web.HttpPostedFileBase ImageUpload { get; set; }
    }

    [MetadataType(typeof(BrandMasterData))]
    public partial class Brand
    {
        [NotMapped]
        public System.Web.HttpPostedFileBase ImageUpload { get; set; }
    }
    [MetadataType(typeof(UserMasterData))]
    public partial class User
    {
        [NotMapped]
        [Required]
        [System.ComponentModel.DataAnnotations.Compare("Password")]
        [Display(Name ="Nhập lại mật khẩu")]
        [DataType(DataType.Password)]
        public string ConfirmPasswords { get; set; }
    }
    [MetadataType(typeof(OrderMasterData))]
    public partial class Order
    {
        [NotMapped]
        public System.Web.HttpPostedFileBase ImageUpload { get; set; }
    }
}