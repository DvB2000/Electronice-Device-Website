using BanHang.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanHang.Models
{
    public class HomeModel
    {
        public List<Product> ListProduct { get; set; }
        public List<Category> ListCategory { get; set; }
        public List<Brand> ListBrand { get; set; }
        public List<Order> ListOrder { get; set; }
        public List<User> ListUser { get; set; }
        public List<OrderDetail> ListOrDerDetail { get; set; }
        internal object ToPagedList(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}