using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyClass.Models;
using PagedList;
using PagedList.Mvc;
namespace MyClass.DAO
{

    public class PostDAO
    {
        private MyDBcontext db = new MyDBcontext();

        public List<PostInfo> getListByTopicId(int? topid, string type = "Post", int? notid = null)
        {
            List<PostInfo> list = null;
            if (notid == null)
            {
                list = db.Posts
                  .Join(
                  db.Topics,
                  p => p.Topid,
                  t => t.Id,
                  (p, t) => new PostInfo
                  {
                      Id = p.Id,
                      Topid = p.Topid,
                      TopicName = t.Name,
                      Title = p.Title,
                      Slug = p.Slug,
                      Detail = p.Detail,
                      Img = p.Img,
                      PostType = p.PostType,
                      MetaDesc = p.MetaDesc,
                      Metakey = p.Metakey,
                      CreatedAt = p.CreatedAt,
                      CreatedBy = p.CreatedBy,
                      UpdatedAt = p.UpdatedAt,
                      UpdatedBy = p.UpdatedBy,
                      Status = p.Status

                  }

                  ).Where(m => m.Status == 1 && m.PostType == type && m.Topid == topid).ToList();
            }
            else
            {
                list = db.Posts
                .Join(
                db.Topics,
                p => p.Topid,
                t => t.Id,
                (p, t) => new PostInfo
                {
                    Id = p.Id,
                    Topid = p.Topid,
                    TopicName = t.Name,
                    Title = p.Title,
                    Slug = p.Slug,
                    Detail = p.Detail,
                    Img = p.Img,
                    PostType = p.PostType,
                    MetaDesc = p.MetaDesc,
                    Metakey = p.Metakey,
                    CreatedAt = p.CreatedAt,
                    CreatedBy = p.CreatedBy,
                    UpdatedAt = p.UpdatedAt,
                    UpdatedBy = p.UpdatedBy,
                    Status = p.Status

                }

                ).Where(m => m.Status == 1 && m.PostType == type && m.Topid == topid && m.Id != notid).ToList();
            }

            return list;
        }
      
        public List<PostInfo> getList(string type = "Post")
        {
            List<PostInfo> list = db.Posts
                .Join(
                db.Topics,
                p => p.Topid,
                t => t.Id,
                (p, t) => new PostInfo
                {
                    Id = p.Id,
                    Topid = p.Topid,
                    TopicName = t.Name,
                    Title = p.Title,
                    Slug = p.Slug,
                    Detail = p.Detail,
                    Img = p.Img,
                    PostType = p.PostType,
                    MetaDesc = p.MetaDesc,
                    Metakey = p.Metakey,
                    CreatedAt = p.CreatedAt,
                    CreatedBy = p.CreatedBy,
                    UpdatedAt = p.UpdatedAt,
                    UpdatedBy = p.UpdatedBy,
                    Status = p.Status

                }

                ).Where(m => m.Status == 1 && m.PostType == type).ToList();
            return list;
        }
        public List<PostInfo> getListByPost(string type = "Post")
        {
            List<PostInfo> list = db.Posts
                .Join(
                db.Topics,
                p => p.Topid,
                t => t.Id,
                (p, t) => new PostInfo
                {
                    Id = p.Id,
                    Topid = p.Topid,
                    TopicName = t.Name,
                    Title = p.Title,
                    Slug = p.Slug,
                    Detail = p.Detail,
                    Img = p.Img,
                    PostType = p.PostType,
                    MetaDesc = p.MetaDesc,
                    Metakey = p.Metakey,
                    CreatedAt = p.CreatedAt,
                    CreatedBy = p.CreatedBy,
                    UpdatedAt = p.UpdatedAt,
                    UpdatedBy = p.UpdatedBy,
                    Status = p.Status

                }

                ).Where(m => m.Status == 1 && m.PostType == type&&m.Topid==1).ToList();
            return list;
        }
        //public IPagedList<PostInfo> getList(string type = "Post", int pageNumber, int pageSize)
        //{
        //    IPagedList<PostInfo> list = db.Posts
        //        .Join(
        //        db.Topics,
        //        p => p.Topid,
        //        t => t.Id,
        //        (p, t) => new PostInfo
        //        {
        //            Id = p.Id,
        //            Topid = p.Topid,
        //            TopicName = t.Name,
        //            Title = p.Title,
        //            Slug = p.Slug,
        //            Detail = p.Detail,
        //            Img = p.Img,
        //            PostType = p.PostType,
        //            MetaDesc = p.MetaDesc,
        //            Metakey = p.Metakey,
        //            CreatedAt = p.CreatedAt,
        //            CreatedBy = p.CreatedBy,
        //            UpdatedAt = p.UpdatedAt,
        //            UpdatedBy = p.UpdatedBy,
        //            Status = p.Status

        //        }

        //        ).Where(m => m.Status == 1 && m.PostType == type).ToPagedList(pageNumber, pageSize);
        //    return list;
        //}

        public List<PostInfo> getList(string status = "All", string type = "Post")
        {
            //trả về danh sách các mẫu tin 

            List<PostInfo> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.Posts
                            .Join(
                db.Topics,
                p => p.Topid,
                t => t.Id,
                (p, t) => new PostInfo
                {
                    Id = p.Id,
                    Topid = p.Topid,
                    TopicName = t.Name,
                    Title = p.Title,
                    Slug = p.Slug,
                    Detail = p.Detail,
                    Img = p.Img,
                    PostType = p.PostType,
                    MetaDesc = p.MetaDesc,
                    Metakey = p.Metakey,
                    CreatedAt = p.CreatedAt,
                    CreatedBy = p.CreatedBy,
                    UpdatedAt = p.UpdatedAt,
                    UpdatedBy = p.UpdatedBy,
                    Status = p.Status
                }).Where(m => m.Status != 0 && m.PostType == type).OrderByDescending(m => m.CreatedAt).ToList();
                        break;
                    }
                case "Trash":
                    {
                        list = db.Posts
                             .Join(
                db.Topics,
                p => p.Topid,
                t => t.Id,
                (p, t) => new PostInfo
                {
                    Id = p.Id,
                    Topid = p.Topid,
                    TopicName = t.Name,
                    Title = p.Title,
                    Slug = p.Slug,
                    Detail = p.Detail,
                    Img = p.Img,
                    PostType = p.PostType,
                    MetaDesc = p.MetaDesc,
                    Metakey = p.Metakey,
                    CreatedAt = p.CreatedAt,
                    CreatedBy = p.CreatedBy,
                    UpdatedAt = p.UpdatedAt,
                    UpdatedBy = p.UpdatedBy,
                    Status = p.Status,

                }

                ).Where(m => m.Status == 0 && m.PostType == type).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Posts
                             .Join(
                db.Topics,
                p => p.Topid,
                t => t.Id,
                (p, t) => new PostInfo
                {
                    Id = p.Id,
                    Topid = p.Topid,
                    TopicName = t.Name,
                    Title = p.Title,
                    Slug = p.Slug,
                    Detail = p.Detail,
                    Img = p.Img,
                    PostType = p.PostType,
                    MetaDesc = p.MetaDesc,
                    Metakey = p.Metakey,
                    CreatedAt = p.CreatedAt,
                    CreatedBy = p.CreatedBy,
                    UpdatedAt = p.UpdatedAt,
                    UpdatedBy = p.UpdatedBy,
                    Status = p.Status,

                }

                ).Where(m => m.PostType == type).ToList();
                        break;
                    }
            }
            return list;

        }

        public List<Post> getListPage(string status = "All", string type = "Post")
        {
            List<Post> list = null;
            switch (status)
            {
                case "Index":
                    list = db.Posts.Where(m => m.Status != 0 && m.PostType == type).ToList();
                    break;
                case "Trash":
                    list = db.Posts.Where(m => m.Status == 0 && m.PostType == type).ToList();
                    break;
                default:
                    list = db.Posts.Where(m => m.PostType == type).ToList();
                    break;
            }
            return list;

        }
            // trả về 1 mẫu tin 
            public Post getRow(int? id)
        {
            if (id == null)
            {

                return null;
            }
            else
            {
                return db.Posts.Find(id);
            }
        }
        public Post getRow(string slug)
        {

            return db.Posts.Where(m => m.Slug == slug && m.Status == 1).FirstOrDefault();

        }
        public Post getRow(string slug, string posttype)
        {

            return db.Posts.Where(m => m.Slug == slug && m.PostType == posttype && m.Status == 1).FirstOrDefault();

        }

        public List<Post> SearchByKey(string key)
        {
            return db.Posts.SqlQuery("Select * from Posts where Title like '%" + key + "%'").ToList();

        }

        //thêm mẫu tin 
        public int Insert(Post row)
        {
            db.Posts.Add(row);
            return db.SaveChanges();
        }
        //Cập nhật mẫu tin 
        public int Update(Post row)
        {
            db.Entry(row).State = EntityState.Modified;

            return db.SaveChanges();
        }
        //Xóa mẫu tin 
        public int Delete(Post row)
        {
            db.Posts.Remove(row);

            return db.SaveChanges();
        }

    }
}

