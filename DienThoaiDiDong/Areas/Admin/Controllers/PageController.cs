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
    public class PageController : BaseController
    {
        PostDAO postDAO = new PostDAO();
        LinkDAO linkDAO = new LinkDAO();
        // GET: Admin/Page
       
        public ActionResult Index()
        {
            return View(postDAO.getListPage("Index", "Page"));
        }

        // GET: Admin/Post/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = postDAO.getRow(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Admin/Post/Create
        public ActionResult Create()
        {
           
            return View();
        }

        // POST: Admin/Post/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post post)
        {
            if (ModelState.IsValid)
            {
                post.PostType = "Page";
                //xử lý tên Post khi nhập vào
                string names = post.Title;
                names = names.First().ToString().ToUpper() + names.Substring(1);

                // lưu lại tên sau khi xử lý
                post.Title = names;

                // xử lý Slug || Links nếu có
                string slug = XString.Str_Slug(post.Title);

                //post.Name = name;
                post.Slug = slug;

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

                        string imgName = slug + "-" + date + Img.FileName.Substring(Img.FileName.LastIndexOf("."));

                        //lưu vào csdl 
                        post.Img = imgName;

                        //lưu lên server
                        string PathImg = Path.Combine(Server.MapPath("~/Public/images/Post/"), imgName);
                        Img.SaveAs(PathImg);
                    }
                }

                post.CreatedBy = Convert.ToInt32(Session["UserId"].ToString());
                post.CreatedAt = DateTime.Now;
                TempData["message"] = new XMessage("success", "Thêm trang đơn thành công");

                if(postDAO.Insert(post)==1)
                {
                    Link link = new Link();
                    link.Slug = post.Slug;
                    link.TableId = post.Id;
                    link.TypeLink = "page";
                    linkDAO.Insert(link);
                }    
               
                return RedirectToAction("Index");
            }
           
            return View(post);
        }

        // GET: Admin/Post/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = postDAO.getRow(id);
            if (post == null)
            {
                return HttpNotFound();
            }
           
            return View(post);
        }

        // POST: Admin/Post/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Post post)
        {
            if (ModelState.IsValid)
            {
                post.PostType = "Page";
                //xử lý tên Slider khi nhập vào
                string names = post.Title;
                names = names.First().ToString().ToUpper() + names.Substring(1);

                post.Title = names;

                // xử lý Slug || Links nếu có
                string name = XString.Str_Slug(names);
                name = name.First().ToString().ToUpper() + name.Substring(1);

                post.Slug = name;
                //Xử lý thêm thông tin
                post.Slug = XString.Str_Slug(post.Title);

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
                        if (post.Img != null)
                        {
                            // xóa hình
                            string Delpath = Path.Combine(Server.MapPath("~/Public/images/Post/"), post.Img);
                            if (System.IO.File.Exists(Delpath))
                            {
                                System.IO.File.Delete(Delpath);
                            }
                        }
                        //lưu vào csdl 
                        post.Img = imgName;

                        //lưu lên server
                        string PathImg = Path.Combine(Server.MapPath("~/Public/images/Post/"), imgName);
                        Img.SaveAs(PathImg);
                    }
                }

                post.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
                post.UpdatedAt = DateTime.Now;
               if( postDAO.Update(post)==1)
                {
                    Link link = linkDAO.getRow(post.Id, "page");
                    link.Slug = post.Slug;
                    linkDAO.Update(link);
                }    
                TempData["message"] = new XMessage("success", "Cập nhật  thành công");
                return RedirectToAction("Index");
            }
           
            return View(post);
        }

        // GET: Admin/Post/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = postDAO.getRow(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Admin/Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = postDAO.getRow(id);
            //kiểm tra tồn tại của hình// nếu không rỗng thì xóa
            if (post.Img != null)
            {
                // xóa hình
                string Delpath = Path.Combine(Server.MapPath("~/Public/images/Post/"), post.Img);
                if (System.IO.File.Exists(Delpath))
                {
                    System.IO.File.Delete(Delpath);
                }
            }

            if (postDAO.Delete(post) == 1)
            {
                Link link = linkDAO.getRow(post.Id, "page");
                linkDAO.Delete(link);
            }
            TempData["message"] = new XMessage("danger", "Xóa không tồn tại");
            return RedirectToAction("Index");
        }
        //Xử lý trạng thái
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Bài viết không tồn tại");
                return RedirectToAction("Index", "Page");
            }
            Post post = postDAO.getRow(id);
            if (post == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Page");
            }
            post.Status = (post.Status == 1) ? 2 : 1;
            post.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            post.UpdatedAt = DateTime.Now;
            postDAO.Update(post);
            TempData["message"] = new XMessage("success", "Thay đổi trạng thái thành công");
            return RedirectToAction("Index", "Page");
        }
        public ActionResult Trash()
        {
            return View(postDAO.getListPage("Trash","Page"));
        }

        // Xóa Vào thùng rác
        public ActionResult Deltrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Bài viết không tồn tại");
                return RedirectToAction("Index", "Page");
            }
            Post post = postDAO.getRow(id);
            if (post == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Index", "Page");
            }
            post.Status = 0;
            post.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            post.UpdatedAt = DateTime.Now;
            postDAO.Update(post);
            TempData["message"] = new XMessage("success", "Xóa vào thùng rác thành công");
            return RedirectToAction("Index", "Page");
        }

        //khôi phục
        public ActionResult Retrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Bài viết không tồn tại");
                return RedirectToAction("Trash", "Page");
            }
            Post post = postDAO.getRow(id);
            if (post == null)
            {
                TempData["message"] = new XMessage("danger", "Mẫu tin không tồn tại");
                return RedirectToAction("Trash", "Page");
            }
            post.Status = 2;
            post.UpdatedBy = Convert.ToInt32(Session["UserId"].ToString());
            post.UpdatedAt = DateTime.Now;
            postDAO.Update(post);
            TempData["message"] = new XMessage("success", "Khôi phục thành công");
            return RedirectToAction("Trash", "Page");
        }

    }
}
