using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;
using PagedList;
using PagedList.Mvc;

namespace BookStore.Controllers
{
    public class TrangChuController : Controller
    {
        public ActionResult LienHe()
        {
            return View();
        }
        public ActionResult ThanhVien()
        {
            return View();
        }
        // GET: TrangChu
        dbMyPhamDataContext db = new dbMyPhamDataContext();
        private List<SANPHAM> Laysanphammoi(int count)
        {
            return db.SANPHAMs.OrderByDescending(a => a.Ngaycapnhat).Take(count).ToList();

        }
        public ActionResult Index(int? page, string searchString)
        {
            //Tao bien quy dinh so san pham tren moi trang
            int pageSize = 3;
            //Tao bien so trang
            int pageNum = (page ?? 1);


            //Lấy top 5 Album bán chạy nhất
            var aoquanmoi = Laysanphammoi(15);
            return View(aoquanmoi.ToPagedList(pageNum, pageSize));



        }
    }
}