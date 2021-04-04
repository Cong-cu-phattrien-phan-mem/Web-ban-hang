using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Models;

namespace BookStore.Models
{
	public class Giohang
	{
		//Tao doi tuong data chua dữ liệu từ model dbBansach đã tạo. 
		dbMyPhamDataContext data = new dbMyPhamDataContext();
        

        public int iMaSP { set; get; }
        public string sTenSP { set; get; }
        public string sAnhSP { set; get; }
        public Double dDongia { set; get; }
        public int iSoluong { set; get; }
        public Double dThanhtien
        {
			get { return iSoluong * dDongia; }

		}
        //Khoi tao gio hàng theo Masach duoc truyen vao voi Soluong mac dinh la 1
        public Giohang(int Masp)
        {
            iMaSP = Masp;
            SANPHAM sp = data.SANPHAMs.Single(n => n.Masp == iMaSP);
            sTenSP = sp.TenSP;
            sAnhSP = sp.AnhSP;
            dDongia = double.Parse(sp.Giaban.ToString());
            iSoluong = 1;
        }
    }

}