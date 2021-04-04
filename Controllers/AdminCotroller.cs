using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;
using System.IO;

namespace BookStore.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        dbMyPhamDataContext data = new dbMyPhamDataContext();
        public ActionResult Index()
        {
            return View();

        }
        public ActionResult Sanpham()
        {
            return View(data.SANPHAMs.ToList());
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection f)
        {
            var tendn = f["username"];
            var matkhau = f["password"];
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Phai nhap ten dang nhap";

            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Phai nhap mat khau";
            }
            else
            {
                ADMIN ad = data.ADMINs.SingleOrDefault(n => n.UserAdmin == tendn && n.PassAdmin == matkhau);
                if (ad != null)
                {
                    Session["Taikhoanadmin"] = ad;
                    return RedirectToAction("Index", "Admin");
                }
                else
                    ViewBag.Thongbao = "Tai khoan khong ton tai";
            }
            return View();
        }
        [HttpGet]
        public ActionResult Themmoisp()
        {
            ViewBag.MaLoai = new SelectList(data.LOAIs.ToList().OrderBy(n => n.Tenloai), "Maloai", "Tenloai");
            ViewBag.MaDM = new SelectList(data.DANHMUCs.ToList().OrderBy(n => n.TenDM), "MaDM", "TenDM");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Themmoisp(SANPHAM sp, HttpPostedFileBase fup)
        {
            ViewBag.MaLoai = new SelectList(data.LOAIs.ToList().OrderBy(n => n.Tenloai), "Maloai", "Tenloai");
            ViewBag.MaDM = new SelectList(data.DANHMUCs.ToList().OrderBy(n => n.TenDM), "MaDM", "TenDM");
            if (fup == null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh bìa";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var fileName = Path.GetFileName(fup.FileName);
                    var path = Path.Combine(Server.MapPath("~/HinhSanPham"), fileName);
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại ";
                    }
                    else
                    {
                        fup.SaveAs(path);
                    }
                    sp.AnhSP = fileName;
                    data.SANPHAMs.InsertOnSubmit(sp);
                    data.SubmitChanges();
                }
                return RedirectToAction("Sanpham");
            }
        }
        public ActionResult Chitiet(int id)
        {
            SANPHAM sp = data.SANPHAMs.SingleOrDefault(n => n.Masp == id);
            ViewBag.Masp = sp.Masp;
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sp);
        }
        public ActionResult Xoasp(int id)
        {

            SANPHAM sp = data.SANPHAMs.SingleOrDefault(n => n.Masp == id);
            ViewBag.Masach = sp.Masp;
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sp);
        }
        [HttpPost, ActionName("Xoasp")]
        public ActionResult Xacnhanxoa(int id)
        {
            SANPHAM sp = data.SANPHAMs.SingleOrDefault(n => n.Masp == id);
            ViewBag.Masp = sp.Masp;
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            data.SANPHAMs.DeleteOnSubmit(sp);
            data.SubmitChanges();
            return RedirectToAction("Sanpham");
        }
        [HttpGet]
        public ActionResult Suasp(int id)
        {
            SANPHAM sp = data.SANPHAMs.SingleOrDefault(n => n.Masp == id);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaLoai = new SelectList(data.LOAIs.ToList().OrderBy(n => n.Tenloai), "Maloai", "Tenloai");
            ViewBag.MaDM = new SelectList(data.DANHMUCs.ToList().OrderBy(n => n.TenDM), "MaDM", "TenDM");
            return View(sp);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Suasp(SANPHAM sp, HttpPostedFileBase fup)
        {
            ViewBag.MaLoai = new SelectList(data.LOAIs.ToList().OrderBy(n => n.Tenloai), "Maloai", "Tenloai");
            ViewBag.MaDM = new SelectList(data.DANHMUCs.ToList().OrderBy(n => n.TenDM), "MaDM", "TenDM");
            if (fup == null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh bìa";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var filename = Path.GetFileName(fup.FileName);
                    var path = Path.Combine(Server.MapPath("~/HinhSanPham"), filename);
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                    }
                    else
                    {
                        fup.SaveAs(path);
                    }
                    sp.AnhSP = filename;
                    UpdateModel(sp);
                    data.SubmitChanges();
                }
                return RedirectToAction("Sanpham");
            }
        }
        public ActionResult Danhmuc()
        {
            return View(data.DANHMUCs.ToList());
        }
        [HttpGet]
        public ActionResult ThemmoiDM()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemmoiDM(DANHMUC dm)
        {
            data.DANHMUCs.InsertOnSubmit(dm);
            data.SubmitChanges();
            return RedirectToAction("Danhmuc");
        }
        public ActionResult Chitietdm(int id)
        {
            DANHMUC dm = data.DANHMUCs.SingleOrDefault(n => n.MaDM == id);
            ViewBag.MaDM = dm.MaDM;
            if (dm == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(dm);
        }
        [HttpGet]
        public ActionResult Xoadm(int id)
        {
            DANHMUC dm = data.DANHMUCs.SingleOrDefault(n => n.MaDM == id);
            ViewBag.MaDM = dm.MaDM;
            if (dm == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(dm);
        }
        [HttpPost, ActionName("Xoadm")]
        public ActionResult xacnhanxoadm(int id)
        {
            DANHMUC dm = data.DANHMUCs.SingleOrDefault(n => n.MaDM == id);
            ViewBag.MaDM = dm.MaDM;
            if (dm == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            data.DANHMUCs.InsertOnSubmit(dm);
            data.SubmitChanges();
            return View(dm);
        }
        [HttpGet]
        public ActionResult Suadm(int id)
        {
            DANHMUC dm = data.DANHMUCs.SingleOrDefault(n => n.MaDM == id);
            ViewBag.MaDM = dm.MaDM;
            if (dm == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(dm);
        }
        public DANHMUC getId(int id)
        {
            return data.DANHMUCs.Where(x => x.MaDM == id).FirstOrDefault();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Suadm(DANHMUC dm)
        {
            ViewBag.MaDM = new SelectList(data.SANPHAMs.ToList().OrderBy(n => n.TenSP), "Masp", "TenSP");
            DANHMUC temp = getId(dm.MaDM);
            if (ModelState.IsValid)
            {
                temp.TenDM = dm.TenDM;
                //Luu vao CSDL   
                UpdateModel(dm);
                data.SubmitChanges();

            }
            return RedirectToAction("Danhmuc");
        }
        public ActionResult Loai()
        {
            return View(data.LOAIs.ToList());
        }
        [HttpGet]
        public ActionResult Themmoiloai()
        {
            ViewBag.MaDM = new SelectList(data.DANHMUCs.ToList().OrderBy(n => n.TenDM), "MaDM", "TenDM");
            return View();

        }
        [HttpPost]
        public ActionResult Themmoiloai(LOAI l)
        {
            data.LOAIs.InsertOnSubmit(l);
            data.SubmitChanges();
            return RedirectToAction("loai");
        }
        public ActionResult Chitietloai(int id)
        {
            LOAI l = data.LOAIs.SingleOrDefault(n => n.Maloai == id);
            ViewBag.MaLoai = l.Maloai;
            if (l == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(l);
        }
        [HttpGet]
        public ActionResult SuaLoai(int id)
        {
            //Lay ra doi tuong sach theo ma
            LOAI l = data.LOAIs.SingleOrDefault(n => n.Maloai == id);
            ViewBag.Maloai = l.Maloai;
            if (l == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            return View(l);
        }
        public LOAI getIdLoai(int id)
        {
            return data.LOAIs.Where(x => x.Maloai == id).FirstOrDefault();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaLoai(LOAI l)
        {
            ViewBag.MaQA = new SelectList(data.SANPHAMs.ToList().OrderBy(n => n.TenSP), "Masp", "TenSP");
            LOAI temp = getIdLoai(l.Maloai);
            if (ModelState.IsValid)
            {
                temp.Tenloai = l.Tenloai;

                //Luu vao CSDL   
                UpdateModel(l);
                data.SubmitChanges();

            }
            return RedirectToAction("Loai");

        }
        public ActionResult DonHang()
        {
            return View(data.DONHANGs.ToList());
        }
        public ActionResult Chitietdh(int id, bool? giaohang)
        {
            ViewBag.ID = id;
            //return View(data.CHITIETDONTHANGs.ToList());
            var _donhang = from ddh in data.DONHANGs where ddh.MaDH == id select ddh;
            var donhang = _donhang.FirstOrDefault();
            ViewBag.GiaoHang = donhang.Tinhtranggiaohang;
            if (giaohang != null)
            {
                donhang.Tinhtranggiaohang = giaohang;
                data.SubmitChanges();
            }
            var sanpham = from sp in data.CHITIETDONHANGs where sp.MaDH == id select sp;

            return View(sanpham.ToList());

        }
    }
}