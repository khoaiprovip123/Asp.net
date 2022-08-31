using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyClass.Models;
using MyClass.DAO;

using System.IO;

namespace DienThoaiDiDong.Areas.Admin.Controllers
{
    public class SliderController : BaseController
    {
        SliderDAO sliderDAO = new SliderDAO();

        // GET: Admin/Slider
        public ActionResult Index()
        {
            return View(sliderDAO.getList("Index"));
        }

        // GET: Admin/Slider/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = sliderDAO.getRow(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // GET: Admin/Slider/Create
        public ActionResult Create()
        {
            ViewBag.ListOrders = new SelectList(sliderDAO.getList("Index"), "Orders", "Name");
            return View();
        }

        // POST: Admin/Slider/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Slider slider)
        {
            if (ModelState.IsValid)
            {
                //xử lý tên Slider khi nhập vào
                string names = slider.Name;
                names = names.First().ToString().ToUpper() + names.Substring(1);

                slider.Name = names;

                // xử lý Slug || Links nếu có
                string name = XString.Str_Slug(names);
                name = name.First().ToString().ToUpper() + name.Substring(1);

                //slider.Name = name;
                slider.Link = name;
                if (slider.Orders == null)
                {
                    slider.Orders = 1;
                }
                else
                {
                    slider.Orders += 1;
                }

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
                        slider.Img = imgName;

                        //lưu lên server
                        string PathImg = Path.Combine(Server.MapPath("~/Public/images/Slider/"), imgName);
                        Img.SaveAs(PathImg);
                    }
                }
               
                slider.CreatedBy = Convert.ToInt32(Session["UserId"].ToString());
                slider.CreatedAt = DateTime.Now;
                sliderDAO.Insert(slider);
              
                TempData["message"] = new XMessage("success", "Thêm thành công");
                return RedirectToAction("Index");
            }

            ViewBag.ListOrders = new SelectList(sliderDAO.getList("Index"), "Orders", "Name");
            return View(slider);
        }

        // GET: Admin/Slider/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = sliderDAO.getRow(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListOrders = new SelectList(sliderDAO.getList("Index"), "Orders", "Name");
            return View(slider);
        }
        // POST: Admin/Slider/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Slider slider)
        {

            if (ModelState.IsValid)
            {

                //xử lý tên Slider khi nhập vào
                string names = slider.Name;
                names = names.First().ToString().ToUpper() + names.Substring(1);

                slider.Name = names;

                // xử lý Slug || Links nếu có
                string name = XString.Str_Slug(names);
                name = name.First().ToString().ToUpper() + name.Substring(1);

                //slider.Name = name;
                slider.Link = name;
                if (slider.Orders == null)
                {
                    slider.Orders = 1;
                }
                else
                {
                    slider.Orders += 1;
                }

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
                        if (slider.Img != null)
                        {
                            // xóa hình
                            string Delpath = Path.Combine(Server.MapPath("~/Public/images/Slider/"), slider.Img);
                            if (System.IO.File.Exists(Delpath))
                            {
                                System.IO.File.Delete(Delpath);
                            }
                        }
                        //lưu vào csdl 
                        slider.Img = imgName;

                        //lưu lên server
                        string PathImg = Path.Combine(Server.MapPath("~/Public/images/Slider/"), imgName);
                        Img.SaveAs(PathImg);
                    }
                }

                slider.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
                slider.UpdatedAt = DateTime.Now;
                sliderDAO.Update(slider);
                TempData["message"] = new XMessage("success", "Thêm thành công");
                return RedirectToAction("Index");
            }

            ViewBag.ListOrders = new SelectList(sliderDAO.getList("Index"), "Orders", "Name");
            return View(slider);
        }

        // GET: Admin/slider/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = sliderDAO.getRow(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // POST: Admin/slider/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Slider slider = sliderDAO.getRow(id);
            sliderDAO.Delete(slider);
            TempData["message"] = new XMessage("success", "Xóa nhà cung cấp thành công");
            return RedirectToAction("Index");
        }
        //thùng rác :Admin/Trash
        public ActionResult Trash()
        {
            return View(sliderDAO.getList("Trash"));
        }


        //thay đổi trạng thái
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "Slider");
            }
            Slider slider = sliderDAO.getRow(id);
            if (slider == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Slider");
            }
            slider.Status = (slider.Status == 1) ? 2 : 1;
            slider.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            slider.UpdatedAt = DateTime.Now;
            sliderDAO.Update(slider);
            TempData["message"] = new XMessage("success", "Thay đổi trạng thái thành công");
            return RedirectToAction("Index", "Slider");
        }

        // Xóa Vào thùng rác
        public ActionResult Deltrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Index", "Slider");
            }
            Slider slider = sliderDAO.getRow(id);
            if (slider == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Slider");
            }
            slider.Status = 0;
            slider.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            slider.UpdatedAt = DateTime.Now;
            sliderDAO.Update(slider);
            TempData["message"] = new XMessage("success", "Xóa vào thùng rác thành công");
            return RedirectToAction("Index", "Slider");
        }

        //khôi phục
        public ActionResult Retrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Mã loại sản phẩm không tồn tại");
                return RedirectToAction("Trash", "Slider");
            }
            Slider slider = sliderDAO.getRow(id);
            if (slider == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Trash", "Slider");
            }
            slider.Status = 2;
            slider.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            slider.UpdatedAt = DateTime.Now;
            sliderDAO.Update(slider);
            TempData["message"] = new XMessage("success", "khôi phục thành công");
            return RedirectToAction("Trash", "Slider");
        }

    }
}
