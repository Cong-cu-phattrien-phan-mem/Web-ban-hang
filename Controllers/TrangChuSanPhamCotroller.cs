using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;

namespace BookStore.Controllers
{
    public class TrangSanPhamController : Controller
    {
        dbMyPhamDataContext dt = new dbMyPhamDataContext();
        // GET: TrangSanPham
        private List<SANPHAM> Getsanpham()
        {
            return dt.SANPHAMs.ToList();

        }
        public ActionResult Index()
        {
            var gs = Getsanpham();
            return View(gs);
        }
        public ActionResult Danhmuc()
        {
            var dm = from danhmuc in dt.DANHMUCs select danhmuc;
            return PartialView(dm);
        }
        public ActionResult SPtheoDM(int id)
        {
            var DM = from s in dt.SANPHAMs where s.MaDM == id select s;
            return View(DM);
        }
        public ActionResult Loai()
        {
            var cd = from nxb in dt.LOAIs select nxb;
            return PartialView(cd);
        }
        public ActionResult SPtheoLoai(int id)
        {
            var loai = from s in dt.SANPHAMs where s.Maloai == id select s;
            return View(loai);
        }
        public ActionResult Details(int id)
        {
            var sach = from s in dt.SANPHAMs
                       where s.Masp == id
                       select s;
            return View(sach.Single());
        }
    }
}