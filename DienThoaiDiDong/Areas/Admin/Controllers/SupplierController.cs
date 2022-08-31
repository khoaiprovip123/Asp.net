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
    public class SupplierController : BaseController
    {
        SupplierDAO supplierDAO = new SupplierDAO();

        // GET: Admin/Supplier
        public ActionResult Index()
        {
            return View(supplierDAO.getList("Index"));
        }

        // GET: Admin/Supplier/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = supplierDAO.getRow(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // GET: Admin/Supplier/Create
        public ActionResult Create()
        {
            ViewBag.ListOrders = new SelectList(supplierDAO.getList("Index"), "Orders", "Name", 0);
            return View();
        }

        // POST: Admin/Supplier/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                if (supplier.Orders == null)
                {
                    supplier.Orders = 1;
                }
                else
                {
                    supplier.Orders += 1;
                }
                //xử lý tên Slider khi nhập vào
                string names = supplier.Name;
                names = names.First().ToString().ToUpper() + names.Substring(1);

                supplier.Name = names;

                // xử lý Slug || Links nếu có
                string name = XString.Str_Slug(names);
                name = name.First().ToString().ToUpper() + name.Substring(1);

                supplier.Link = name;
                //Xử lý thêm thông tin
                supplier.Link = XString.Str_Slug(supplier.Name);

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
                        supplier.Img = imgName;

                        //lưu lên server
                        string PathImg = Path.Combine(Server.MapPath("~/Public/images/Supplier/"), imgName);
                        Img.SaveAs(PathImg);
                    }
                }

                supplier.CreatedBy = Convert.ToInt32(Session["UserId"].ToString());
                supplier.CreatedAt = DateTime.Now;
                supplierDAO.Insert(supplier);
                TempData["message"] = new XMessage("success", "Thêm nhà cung cấp thành công");
                return RedirectToAction("Index");
            }

            ViewBag.ListOrders = new SelectList(supplierDAO.getList("Index"), "Orders", "Name", 0);
            return View(supplier);
        }

        // GET: Admin/Supplier/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = supplierDAO.getRow(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListOrders = new SelectList(supplierDAO.getList("Index"), "Orders", "Name", 0);
            return View(supplier);
        }

        // POST: Admin/Supplier/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                if (supplier.Orders == null)
                {
                    supplier.Orders = 1;
                }
                else
                {
                    supplier.Orders += 1;
                }
                //xử lý tên Slider khi nhập vào
                string names = supplier.Name;
                names = names.First().ToString().ToUpper() + names.Substring(1);

                supplier.Name = names;

                // xử lý Slug || Links nếu có
                string name = XString.Str_Slug(names);
                name = name.First().ToString().ToUpper() + name.Substring(1);

                supplier.Link = name;
                //Xử lý thêm thông tin
                supplier.Link = XString.Str_Slug(supplier.Name);

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
                        if (supplier.Img != null)
                        {
                            // xóa hình
                            string Delpath = Path.Combine(Server.MapPath("~/Public/images/Supplier/"), supplier.Img);
                            if (System.IO.File.Exists(Delpath))
                            {
                                System.IO.File.Delete(Delpath);
                            }
                        }
                        //lưu vào csdl 
                        supplier.Img = imgName;

                        //lưu lên server
                        string PathImg = Path.Combine(Server.MapPath("~/Public/images/Supplier/"), imgName);
                        Img.SaveAs(PathImg);
                    }
                supplier.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
                    supplier.UpdatedAt = DateTime.Now;
                    supplierDAO.Update(supplier);
                    TempData["message"] = new XMessage("success", "Cập nhật thành công");
                    return RedirectToAction("Index");
                }
                TempData["message"] = new XMessage("success", "Cập nhật thành công");
                return RedirectToAction("Index");
            }

            ViewBag.ListOrders = new SelectList(supplierDAO.getList("Index"), "Orders", "Name", 0);
            return View(supplier);
        }

        // GET: Admin/Supplier/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supplier = supplierDAO.getRow(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        // POST: Admin/Supplier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Supplier supplier = supplierDAO.getRow(id);
            supplierDAO.Delete(supplier);

            return RedirectToAction("Index");
        }
        //Xử lý trạng thái
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã nhà cung cấp không tồn tại");
                return RedirectToAction("Index", "Supplier");
            }
            Supplier supplier = supplierDAO.getRow(id);
            if (supplier == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Supplier");
            }
            supplier.Status = (supplier.Status == 1) ? 2 : 1;
            supplier.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            supplier.UpdatedAt = DateTime.Now;
            supplierDAO.Update(supplier);
            TempData["message"] = new XMessage("success", "Thay đổi trạng thái thành công");
            return RedirectToAction("Index", "Supplier");
        }
        public ActionResult Trash()
        {
            return View(supplierDAO.getList("Trash"));
        }

        // Xóa Vào thùng rác
        public ActionResult Deltrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã nhà cung cấp không tồn tại");
                return RedirectToAction("Index", "Supplier");
            }
            Supplier supplier = supplierDAO.getRow(id);
            if (supplier == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Supplier");
            }
            supplier.Status = 0;
            supplier.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            supplier.UpdatedAt = DateTime.Now;
            supplierDAO.Update(supplier);
            TempData["message"] = new XMessage("success", "Xóa vào thùng rác thành công");
            return RedirectToAction("Index", "Supplier");
        }

        //khôi phục
        public ActionResult Retrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Trash", "Supplier");
            }
            Supplier supplier = supplierDAO.getRow(id);
            if (supplier == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Trash", "Supplier");
            }
            supplier.Status = 2;
            supplier.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            supplier.UpdatedAt = DateTime.Now;
            supplierDAO.Update(supplier);
            TempData["message"] = new XMessage("success", "Khôi phục thành công");
            return RedirectToAction("Trash", "Supplier");
        }

    }
}
