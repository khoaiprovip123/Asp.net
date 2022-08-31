using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyClass.DAO;
using MyClass.Models;

namespace DienThoaiDiDong.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        ProductDAO productDAO = new ProductDAO();
        CategoryDAO categoryDAO = new CategoryDAO();
        ColorDAO colorDAO = new ColorDAO();

        // GET: Admin/Product
        public ActionResult Index()
        {
            return View(productDAO.getList("Index"));
        }

        // GET: Admin/Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/Product/Create
        public ActionResult Create()
        {
            ViewBag.ListCat = new SelectList(categoryDAO.getList("Index"), "Id", "Name");
            return View();
        }

        // POST: Admin/Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                //xử lý tên product khi nhập vào
                string names = product.Name;
                names = names.First().ToString().ToUpper() + names.Substring(1);

                product.Name = names;

                // xử lý Slug || Links nếu có
                string name = XString.Str_Slug(names);
                name = name.First().ToString().ToUpper() + name.Substring(1);

                //product.Name = name;
                product.Slug = name;

                // xử lý hình ảnh
                var Img = Request.Files["FileImg"];
                string[] fileExtention = { ".jpg", ".png", ".gif" };
                if (Img.ContentLength != 0)
                {
                    //string time = DateTime.Now.ToString();
                    if (fileExtention.Contains(Img.FileName.Substring(Img.FileName.LastIndexOf("."))))
                    {

                        string date = DateTime.Now.ToString();
                        date = XString.Str_Slug(date);

                        string imgName = name + "-" + date + Img.FileName.Substring(Img.FileName.LastIndexOf("."));

                        //lưu vào csdl 
                        product.Img = imgName;

                        //lưu lên server
                        string PathImg = Path.Combine(Server.MapPath("~/Public/images/Product/"), imgName);
                        Img.SaveAs(PathImg);
                    }
                }

                product.CreatedBy = Convert.ToInt32(Session["UserId"].ToString());
                product.CreatedAt = DateTime.Now;
                if (productDAO.Insert(product) == 1)
                {
                    Color color = new Color();
                    color.Name = product.ColorId;
                    color.UpdatedAt = product.UpdatedAt;
                    colorDAO.Insert(color);
                }

                TempData["message"] = new XMessage("success", "Thêm thành công");
                return RedirectToAction("Index");
            }
            ViewBag.ListCat = new SelectList(categoryDAO.getList("Index"), "Id", "Name");
            return View(product);
        }

        // GET: Admin/Product/Edit/5

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListCat = new SelectList(categoryDAO.getList("Index"), "Id", "Name");
            return View(product);
        }

        // POST: Admin/Supplier/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                //xử lý tên product khi nhập vào
                string names = product.Name;
                names = names.First().ToString().ToUpper() + names.Substring(1);

                product.Name = names;

                // xử lý Slug || Links nếu có
                string name = XString.Str_Slug(names);
                name = name.First().ToString().ToUpper() + name.Substring(1);

                //product.Name = name;
                product.Slug = name;

                // xử lý hình ảnh
                var Img = Request.Files["FileImg"];
                string[] fileExtention = { ".jpg", ".png", ".gif" };
                if (Img.ContentLength != 0)
                {
                    // nếu trong hình upload có chứa dấu . ==> thì cắt chuỗi + ráp với tên + thời gian
                    if (fileExtention.Contains(Img.FileName.Substring(Img.FileName.LastIndexOf("."))))
                    {

                        string date = DateTime.Now.ToString();
                        date = XString.Str_Slug(date);

                        string imgName = name + "-" + date + Img.FileName.Substring(Img.FileName.LastIndexOf("."));

                        //kiểm tra tồn tại của hình// nếu không rỗng thì xóa
                        if (product.Img != null)
                        {
                            // xóa hình
                            string Delpath = Path.Combine(Server.MapPath("~/Public/images/Product/"), product.Img);
                            if (System.IO.File.Exists(Delpath))
                            {
                                System.IO.File.Delete(Delpath);
                            }
                        }
                        //lưu vào csdl 
                        product.Img = imgName;

                        //lưu lên server
                        string PathImg = Path.Combine(Server.MapPath("~/Public/images/Product/"), imgName);
                        Img.SaveAs(PathImg);
                    }
                }

                product.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
                product.UpdatedAt = DateTime.Now;
                productDAO.Update(product);
                TempData["message"] = new XMessage("success", "Thêm thành công");
                return RedirectToAction("Index");
            }
            ViewBag.ListCat = new SelectList(categoryDAO.getList("Index"), "Id", "Name");
            return View(product);
        }
        // GET: Admin/Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = productDAO.getRow(id);
            productDAO.Delete(product);
            TempData["message"] = new XMessage("success", "Xóa sản phẩm thành công");
            return RedirectToAction("Index");

        }
        //thùng rác :Admin/Trash
        public ActionResult Trash()
        {
            return View(productDAO.getList("Trash"));
        }


        //thay đổi trạng thái
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "Product");
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Product");
            }
            product.Status = (product.Status == 1) ? 2 : 1;
            product.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            product.UpdatedAt = DateTime.Now;
            productDAO.Update(product);
            TempData["message"] = new XMessage("success", "Thay đổi trạng thái thành công");
            return RedirectToAction("Index", "Product");
        }

        // Xóa Vào thùng rác
        public ActionResult Deltrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "Product");
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Product");
            }
            product.Status = 0;
            product.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            product.UpdatedAt = DateTime.Now;
            productDAO.Update(product);
            TempData["message"] = new XMessage("success", "Xóa vào thùng rác thành công");
            return RedirectToAction("Index", "Product");
        }

        //khôi phục
        public ActionResult Retrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Trash", "Product");
            }
            Product product = productDAO.getRow(id);
            if (product == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Trash", "Product");
            }
            product.Status = 2;
            product.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            product.UpdatedAt = DateTime.Now;
            productDAO.Update(product);
            TempData["message"] = new XMessage("success", "khôi phục thành công");
            return RedirectToAction("Trash", "Product");
        }
        public String ProductName(int? productid)
        {
            Product product = productDAO.getRow(productid);
            if (product == null)
            {
                return "";
            }
            else
            {
                return product.Name;
            }
        }
        public String ProductImg(int? productid)
        {
            Product product = productDAO.getRow(productid);
            if (product == null)
            {
                return "";
            }
            else
            {
                return product.Img;
            }
        }
    }
}
