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

    public class ProductDAO
    {
        private MyDBcontext db = new MyDBcontext();

        public IPagedList<ProductInfo> getListByListCatId(List<int> listcatid, int pageSize, int pageNumber)
        {
            IPagedList<ProductInfo> list = db.Products
                .Join(
                    db.Categorys,
                    p => p.CatId,
                    c => c.Id,
                    (p, c) => new ProductInfo
                    {
                        Id = p.Id,
                        Name = p.Name,
                        CatName = c.Name,
                        Slug = p.Slug,
                        Detail = p.Detail,
                        Metakey = p.Metakey,
                        Metadesc = p.Metadesc,
                        Img = p.Img,
                        CatId = p.CatId,
                        ColorId = p.ColorId,
                        SupplierId = p.SupplierId,
                        Number = p.Number,
                        Price = p.Price,
                        Pricesale = p.Pricesale,
                        CreatedBy = p.CreatedBy,
                        CreatedAt = p.CreatedAt,
                        UpdatedBy = p.UpdatedBy,
                        UpdatedAt = p.UpdatedAt,
                        Status = p.Status
                    }
                )
                .Where(m => m.Status == 1 && listcatid.Contains(m.CatId))
                .OrderByDescending(m => m.CreatedAt)
               .ToPagedList(pageNumber, pageSize);
            return list;

        }
        public List<ProductInfo> getListByListCatId(List<int> listcatid, int take)
        {
            List<ProductInfo> list = db.Products
                .Join(
                    db.Categorys,
                    p => p.CatId,
                    c => c.Id,
                    (p, c) => new ProductInfo
                    {
                        Id = p.Id,
                        Name = p.Name,
                        CatName = c.Name,
                        Slug = p.Slug,
                        Detail = p.Detail,
                        Metakey = p.Metakey,
                        Metadesc = p.Metadesc,
                        Img = p.Img,
                        CatId = p.CatId,
                        ColorId = p.ColorId,
                        SupplierId = p.SupplierId,
                        Number = p.Number,
                        Price = p.Price,
                        Pricesale = p.Pricesale,
                        CreatedBy = p.CreatedBy,
                        CreatedAt = p.CreatedAt,
                        UpdatedBy = p.UpdatedBy,
                        UpdatedAt = p.UpdatedAt,
                        Status = p.Status
                    }
                )
                .Where(m => m.Status == 1 && listcatid.Contains(m.CatId))
                .OrderByDescending(m => m.CreatedAt)
                .Take(take)
                .ToList();
            return list;

        }
       
        public List<ProductInfo> getList(string status = "All")
        {
            //trả về danh sách các mẫu tin 

            List<ProductInfo> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.Products
                     .Join(
                    db.Categorys,
                    p => p.CatId,
                    c => c.Id,
                    (p, c) => new ProductInfo
                    {
                        Id = p.Id,
                        Name = p.Name,
                        CatName = c.Name,
                        Slug = p.Slug,
                        Detail = p.Detail,
                        Metakey = p.Metakey,
                        Metadesc = p.Metadesc,
                        Img = p.Img,
                        CatId = p.CatId,
                        ColorId = p.ColorId,
                        SupplierId = p.SupplierId,
                        Number = p.Number,
                        Price = p.Price,
                        Pricesale = p.Pricesale,
                        CreatedBy = p.CreatedBy,
                        CreatedAt = p.CreatedAt,
                        UpdatedBy = p.UpdatedBy,
                        UpdatedAt = p.UpdatedAt,
                        Status = p.Status
                    }
                )
                     .Where(m => m.Status != 0).ToList();
                        break;
                    }
                case "Trash":
                    {
                        list = db.Products
                                    .Join(
                            db.Categorys,
                            p => p.CatId,
                            c => c.Id,
                            (p, c) => new ProductInfo
                            {
                                Id = p.Id,
                                Name = p.Name,
                                CatName = c.Name,
                                Slug = p.Slug,
                                Detail = p.Detail,
                                Metakey = p.Metakey,
                                Metadesc = p.Metadesc,
                                Img = p.Img,
                                CatId = p.CatId,
                                ColorId = p.ColorId,
                                SupplierId = p.SupplierId,
                                Number = p.Number,
                                Price = p.Price,
                                Pricesale = p.Pricesale,
                                CreatedBy = p.CreatedBy,
                                CreatedAt = p.CreatedAt,
                                UpdatedBy = p.UpdatedBy,
                                UpdatedAt = p.UpdatedAt,
                                Status = p.Status
                            }
                        )
                                    .Where(m => m.Status == 0).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Products
                                    .Join(
                            db.Categorys,
                            p => p.CatId,
                            c => c.Id,
                            (p, c) => new ProductInfo
                            {
                                Id = p.Id,
                                Name = p.Name,
                                CatName = c.Name,
                                Slug = p.Slug,
                                Detail = p.Detail,
                                Metakey = p.Metakey,
                                Metadesc = p.Metadesc,
                                Img = p.Img,
                                CatId = p.CatId,
                                ColorId = p.ColorId,
                                SupplierId = p.SupplierId,
                                Number = p.Number,
                                Price = p.Price,
                                Pricesale = p.Pricesale,
                                CreatedBy = p.CreatedBy,
                                CreatedAt = p.CreatedAt,
                                UpdatedBy = p.UpdatedBy,
                                UpdatedAt = p.UpdatedAt,
                                Status = p.Status
                            }
                        )
                                    .ToList();
                        break;
                    }
            }
            return list;
        }
        public List<ProductInfo> getList(int take)
        {
            List<ProductInfo> list = db.Products
                .Join(db.Categorys,
                    p => p.CatId,
                    c => c.Id,
                    (p, c) => new ProductInfo
                    {
                        Id = p.Id,
                        Name = p.Name,
                        CatName = c.Name,
                        Slug = p.Slug,
                        Detail = p.Detail,
                        Metakey = p.Metakey,
                        Metadesc = p.Metadesc,
                        Img = p.Img,
                        CatId = p.CatId,
                        ColorId = p.ColorId,
                        SupplierId = p.SupplierId,
                        Number = p.Number,
                        Price = p.Price,
                        Pricesale = p.Pricesale,
                        CreatedBy = p.CreatedBy,
                        CreatedAt = p.CreatedAt,
                        UpdatedBy = p.UpdatedBy,
                        UpdatedAt = p.UpdatedAt,
                        Status = p.Status
                    }
                ).Where(m => m.Status == 1)
                .OrderByDescending(m => m.CreatedAt)
                .Take(take).ToList();
            return list;
        }

        public IPagedList<ProductInfo> getList(int pageSize, int pageNumber)
        {
            IPagedList<ProductInfo> list = db.Products
                .Join(db.Categorys,
                    p => p.CatId,
                    c => c.Id,
                    (p, c) => new ProductInfo
                    {
                        Id = p.Id,
                        Name = p.Name,
                        CatName = c.Name,
                        Slug = p.Slug,
                        Detail = p.Detail,
                        Metakey = p.Metakey,
                        Metadesc = p.Metadesc,
                        Img = p.Img,
                        CatId = p.CatId,
                        ColorId = p.ColorId,
                        SupplierId = p.SupplierId,
                        Number = p.Number,
                        Price = p.Price,
                        Pricesale = p.Pricesale,
                        CreatedBy = p.CreatedBy,
                        CreatedAt = p.CreatedAt,
                        UpdatedBy = p.UpdatedBy,
                        UpdatedAt = p.UpdatedAt,
                        Status = p.Status
                    }
                ).Where(m => m.Status == 1)
                .OrderByDescending(m => m.CreatedAt)
               
                .ToPagedList(pageNumber,pageSize);
            return list;
        }

        public List< Product > SearchByKey(string key)
        {
            return db.Products.Where(m => m.Name.Contains(key) || m.Metakey.Contains(key) || m.SupplierId.Contains(key)||m.ColorId.Contains(key)).ToList();
        }
        // trả về 1 mẫu tin 
        public Product getRow(int? id)
        {
            return db.Products.Find(id);

        }
        public Product getRow(string slug)
        {
            return db.Products.Where(m => m.Slug == slug && m.Status == 1).FirstOrDefault();

        }
        //thêm mẫu tin 
        public int Insert(Product row)
        {
            db.Products.Add(row);
            return db.SaveChanges();
        }
        //Cập nhật mẫu tin 
        public int Update(Product row)
        {
            db.Entry(row).State = EntityState.Modified;

            return db.SaveChanges();
        }
        //Xóa mẫu tin 
        public int Delete(Product row)
        {
            db.Products.Remove(row);

            return db.SaveChanges();
        }

    }
}

