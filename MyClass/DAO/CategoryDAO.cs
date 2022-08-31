﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyClass.Models;

namespace MyClass.DAO
{

    public class CategoryDAO
    {
        private MyDBcontext db = new MyDBcontext();

        public List<Category> getListByParentId(int parentid = 0)
        {
            return db.Categorys
                .Where(m => m.ParentId == parentid && m.Status == 1).OrderBy(m => m.Orders)
                .ToList();
        }

        public List<Category> getList(string status = "All")
        {
            //trả về danh sách các mẫu tin 

            List<Category> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.Categorys.Where(m => m.Status != 0).ToList();
                        break;
                    }
                case "Trash":
                    {
                        list = db.Categorys.Where(m => m.Status == 0).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Categorys.ToList();
                        break;
                    }
            }
            return list;

        }


        // trả về 1 mẫu tin 
        public Category getRow(string slug)
        {

            return db.Categorys.Where(m => m.Slug == slug && m.Status == 1).FirstOrDefault();

        }
        public Category getRow(int? id)
        {
            if (id == null)
            {

                return null;
            }
            else
            {
                return db.Categorys.Find(id);
            }
        }
        //thêm mẫu tin 
        public int Insert(Category row)
        {
            db.Categorys.Add(row);
            return db.SaveChanges();
        }
        //Cập nhật mẫu tin 
        public int Update(Category row)
        {
            db.Entry(row).State = EntityState.Modified;

            return db.SaveChanges();
        }
        //Xóa mẫu tin 
        public int Delete(Category row)
        {
            db.Categorys.Remove(row);

            return db.SaveChanges();
        }

    }
}

