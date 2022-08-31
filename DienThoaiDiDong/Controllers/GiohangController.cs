using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyClass.DAO;
using MyClass.Models;

namespace DienThoaiDiDong.Controllers
{
    public class GiohangController : Controller
    {
        ProductDAO productDAO = new ProductDAO();
        UserDAO userDAO = new UserDAO();
        OrderDAO orrderDAO = new OrderDAO();
        OrderdetailDAO orrderdetailDAO = new OrderdetailDAO();
        XCart xcart = new XCart();
        // GET: Giohang
        public ActionResult Index()
        {
            List<CartItem> listcart = xcart.getCart();
            return View("Index",listcart);
        }
        public ActionResult CartAdd(int productid)
        {
            Product product = productDAO.getRow(productid);
            CartItem cartitem = new CartItem(product.Id, product.Name, product.Img, product.Price, 1);
            //add vào giỏ hàng
            List<CartItem> listcart = xcart.AddCart(cartitem, productid);
            return RedirectToAction("Index","Giohang");
        }
        public ActionResult CartDel(int productid)
        {
            xcart.DelCart(productid);
            return RedirectToAction("Index", "Giohang");
        }
        //CartUpdate
        public ActionResult CartUpdate(FormCollection form)
        {
            if (!string.IsNullOrEmpty(form["CAPNHAT"]))
            {
                var listqty = form["qty"];
                var listarr = listqty.Split(',');
                xcart.UpdateCart(listarr);

            }
            return RedirectToAction("Index", "Giohang");
        }
        //CartDelAll
        public ActionResult CartDelAll()
        {
            xcart.DelCart();
            return RedirectToAction("Index", "Giohang");
        }
        //thanhtoasn
        public ActionResult ThanhToan()
        {
            List <CartItem> listcart = xcart.getCart();
            //kiểm tra trang thái đăng nhập người dùng ==> khách hàng
            if (Session["UserCustomer"].Equals(""))
            {
                return Redirect("~/dang-nhap");//Chuyển hướng URL đến đăng nhập 
            }
            int userid = int.Parse(Session["CustomerId"].ToString());
            User user = userDAO.getRow(userid);
            ViewBag.user = user;
            return View("ThanhToan",listcart);
        }
        //DatMua
        public ActionResult DatMua(FormCollection filed)
        {
            //Lưu thông tin vào CSDL Order và OrderDetail
            int userid = int.Parse(Session["CustomerId"].ToString());
            User user = userDAO.getRow(userid);

            String note = filed["Note"];
            String name = filed["ReceiverName"];
            String email = filed["ReceiverEmail"];
            String phone = filed["ReceiverPhone"];
            String addres = filed["ReceiverAddress"];
            //Tạo đối tượng dươn hàng
            Order order = new Order();
            order.UserId = userid;
            order.Note = note;
            order.ReceiverName = name;
            order.ReceiverEmail = email;
            order.ReceiverPhone = phone;
            order.ReceiverAdress = addres;
            order.CreatedAt = DateTime.Now;
            order.CreatedBy = Convert.ToInt32(Session["UserId"].ToString());
            order.Status = 1;
            if(orrderDAO.Insert(order)==1)
            {
                //Thêm vào chi toeets đơn hàng
                List<CartItem> listcart = xcart.getCart();
                foreach(CartItem cartItem in listcart)
                {
                    OrderDetail orderdetail = new OrderDetail();
                    orderdetail.Orderid = order.Id;
                    orderdetail.Productid = cartItem.ProductId;
                    orderdetail.Price = cartItem.Price;
                    orderdetail.Quantity = cartItem.Qty;
                    orderdetail.Amount = cartItem.Amount;
                    orrderdetailDAO.Insert(orderdetail);
                }    
            }    

            return Redirect("~/thanh-cong");//Chuyển hướng URL đến đăng nhập 
           
        }
        public ActionResult ThanhCong()
        {
            List<CartItem> listcart = xcart.getCart();
            return View("ThanhCong", listcart);
        }
    }
}