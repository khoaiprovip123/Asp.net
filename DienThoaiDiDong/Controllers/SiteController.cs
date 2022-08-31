using MyClass.DAO;
using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
namespace DienThoaiDiDong.Controllers
{
    public class SiteController : Controller
    {
        private LinkDAO linkDAO = new LinkDAO();
        private ProductDAO productDAO = new ProductDAO();
        private PostDAO postDAO = new PostDAO();
        private CategoryDAO categoryDAO = new CategoryDAO();
        private TopicDAO topicDAO = new TopicDAO();
        private SupplierDAO supplierDAO = new SupplierDAO();
        // GET: Site
        public ActionResult Index(string slug = null, int? page = null)
        {
            if (slug == null)
            {
                return this.Home();//Trang chủ
            }
            else
            {
                Link link = linkDAO.getRow(slug);
                if (link != null)
                { // link khác rỗng thì kiểm tra slug
                    string typelink = link.TypeLink;
                    switch (typelink)
                    {
                        case "category":
                            {
                                return this.ProductCategory(slug, page);
                            }
                        case "topic":
                            {
                                return this.PostTopic(slug);
                            }
                        case "page":
                            {
                                return this.PostPage(slug);
                            }
                        case "supplier":
                            {
                                return this.PostSupplier(slug);
                            }
                        default:
                            {
                                return this.Error404(slug);
                            }
                    }
                }
                else
                {
                    Product product = productDAO.getRow(slug);
                    if (product != null)
                    {
                        return this.ProductDetail(product);
                    }
                    else
                    {
                        Post post = postDAO.getRow(slug, "Post");
                        if (post != null)
                        {
                            return this.PostDetail(post);
                        }
                        else
                        {
                            return this.Error404(slug);
                        }
                    }
                }
            }
        }
        public ActionResult Home()
        {
            List<ProductInfo> productNew = productDAO.getList(8);
            ViewBag.ProductNew = productNew;
            List<Category> list = categoryDAO.getListByParentId(0);
            return View("Home", list);
        }
        public ActionResult HomeProduct(int id)
        {
            Category category = categoryDAO.getRow(id);
            ViewBag.Category = category;
            // xử lý danh mục loại sản phẩm 3 cấp

            /* Tạo 1 dánh sách để lấy tất cả các id của các danh mục có chứa cấp con ( parentid khác 0) */
            List<int> listcatid = new List<int>();
            listcatid.Add(id); // nhập cấp đầu tiên

            /*  tạo danh sách quét các cấp con của cấp 1 (cấp đầu tiên) */
            List<Category> listcategory2 = categoryDAO.getListByParentId(id);

            /* kiểm tra xem trong cấp 1 có cấp con hay không // nếu có chạy vòng lặp để quét rồi thêm vào cấp cha */
            if (listcategory2.Count() != 0)
            {
                foreach (var category2 in listcategory2)
                {
                    listcatid.Add(category2.Id);    /* thêm vào cấp cha (cấp cao nhất) */
                    List<Category> listcategory3 = categoryDAO.getListByParentId(category2.Id);

                    /* kiểm tra xem trong cấp 2 có cấp con hay không // nếu có chạy vòng lặp để quét rồi thêm vào cấp cha */
                    if (listcategory3.Count() != 0)
                    {
                        foreach (var category3 in listcategory3)
                        {
                            listcatid.Add(category3.Id);    /* thêm vào cấp cha (cấp cao nhất) */
                        }
                    }
                }
            }
            List<ProductInfo> list = productDAO.getListByListCatId(listcatid, 4);
            return View("HomeProduct", list);
        }
        public ActionResult ProductCategory(string slug, int? page)
        {

            int pageNumber = page ?? 1; //trang hiện tại 
            int pageSize = 8;
            Category category = categoryDAO.getRow(slug);
            // lấy category dựa vào slug
            ViewBag.Category = category;
            List<int> listcatid = new List<int>();
            listcatid.Add(category.Id);
            List<Category> listcategory2 = categoryDAO.getListByParentId(category.Id);
            if (listcategory2.Count() != 0)
            {
                foreach (var category2 in listcategory2)
                {
                    listcatid.Add(category2.Id);
                    List<Category> listcategory3 = categoryDAO.getListByParentId(category2.Id);

                    if (listcategory3.Count() != 0)
                    {
                        foreach (var category3 in listcategory3)
                        {
                            listcatid.Add(category3.Id);
                        }
                    }
                }
            }
            // đưa số lượng mẫy tin cần lấy
            IPagedList<ProductInfo> list = productDAO.getListByListCatId(listcatid, pageSize, pageNumber);
            return View("ProductCategory", list);
        }
        public ActionResult ProductDetail(Product product)
        {
            //ViewBag.Supplier = supplierDAO.getRow(product.SupplierId);

            return View("ProductDetail", product);
        }
        public ActionResult PostTopic(string slug)
        {
            Topic topic = topicDAO.getRow(slug);
            ViewBag.Topic = topic;
            List<PostInfo> list = postDAO.getListByTopicId(topic.Id, "Post", null);
            return View("PostTopic", list);
        }
        public ActionResult PostPage(string slug)
        {
            Post post = postDAO.getRow(slug, "Page");
            return View("PostPage", post);
        }
        public ActionResult PostDetail(Post post)
        {
            ViewBag.ListOrther = postDAO.getListByTopicId(post.Topid, "Post", post.Id);
            //bài viết liên quan
            return View("PostDetail", post);
        }
        public ActionResult PostSupplier(string slug)
        {
            return View("PostSupplier");
        }
        public ActionResult Error404(string slug)
        {
            return View("Error404");
        }

        public ActionResult Product(int? page)
        {
            int pageNumber = page ?? 1; //trang hiện tại 
            int pageSize = 8;

            IPagedList<ProductInfo> list = productDAO.getList(pageSize, pageNumber);
            return View("Product", list);
        }
        public ActionResult Post(/*int? page*/)
        {
            //int pageNumber = page ?? 1; //trang hiện tại 
            //int pageSize = 6;
            List<PostInfo> list = postDAO.getList("Post"/*, pageSize, pageNumber*/);
            return View("Post", list);
        }
        public ActionResult HomePost(/*int? page*/)
        {
            //int pageNumber = page ?? 1; //trang hiện tại 
            //int pageSize = 4;
            List<PostInfo> list = postDAO.getListByPost("Post"/*, pageSize, pageNumber*/);
            return View("HomePost", list);
        }

    }
}