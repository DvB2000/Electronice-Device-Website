using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BanHang
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
              name: "product Detail",
              url: "chi-tiet/{slug}-{Id}",
              defaults: new { controller = "ProductDetail", action = "Detail", id = UrlParameter.Optional },
              new[] { "BanHang.Controllers" }
          );
            routes.MapRoute(
              name: "myDetail",
              url: "thong-tin/{Id}",
              defaults: new { controller = "User", action = "Main", id = UrlParameter.Optional },
              new[] { "BanHang.Controllers" }
          );
            routes.MapRoute(
              name: "myOrder",
              url: "dat-hang/{Id}",
              defaults: new { controller = "User", action = "Order", id = UrlParameter.Optional },
              new[] { "BanHang.Controllers" }
          );
            routes.MapRoute(
              name: "product sale",
              url: "san-pham/giam-gia",
              defaults: new { controller = "ProductDetail", action = "Sale", id = UrlParameter.Optional },
              new[] { "BanHang.Controllers" }
          );
            routes.MapRoute(
              name: "Login",
              url: "dang-nhap",
              defaults: new { controller = "Home", action = "Login", id = UrlParameter.Optional },
              new[] { "BanHang.Controllers" }
          );
            routes.MapRoute(
             name: "Payment",
             url: "thanh-toan",
             defaults: new { controller = "Payment", action = "Payment", id = UrlParameter.Optional },
             new[] { "BanHang.Controllers" }
         );
            routes.MapRoute(
              name: "Cart",
              url: "gio-hang",
              defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional },
              new[] { "BanHang.Controllers" }
          );
            routes.MapRoute(
              name: "product Brand",
              url: "thuong-hieu/{slug}-{Id}",
              defaults: new { controller = "Brand", action = "ProductBrand", id = UrlParameter.Optional },
              new[] { "BanHang.Controllers" }
          );
            routes.MapRoute(
              name: "Category",
              url: "danh-muc",
              defaults: new { controller = "Category", action = "Category", id = UrlParameter.Optional },
              new[] { "BanHang.Controllers" }
          );

            routes.MapRoute(
              name: "product Category",
              url: "danh-muc/{slug}-{Id}",
              defaults: new { controller = "Category", action = "ProductCategory", id = UrlParameter.Optional },
              new[] { "BanHang.Controllers" }
          );
            routes.MapRoute(
             name: "Contact",
             url: "lien-he",
             defaults: new { controller = "Polici", action = "Contact", id = UrlParameter.Optional },
             new[] { "BanHang.Controllers" }
         );
            routes.MapRoute(
            name: "Infor",
            url: "gioi-thieu",
            defaults: new { controller = "Polici", action = "Infor", id = UrlParameter.Optional },
            new[] { "BanHang.Controllers" }
        );
            routes.MapRoute(
              name: "Ship",
              url: "giao-hang",
              defaults: new { controller = "Polici", action = "Ship", id = UrlParameter.Optional },
              new[] { "BanHang.Controllers" }
          );
             routes.MapRoute(
              name: "Change",
              url: "doi-tra",
              defaults: new { controller = "Polici", action = "Change", id = UrlParameter.Optional },
              new[] { "BanHang.Controllers" }
          );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "BanHang.Controllers" }
            );
          
        }
    }
}
