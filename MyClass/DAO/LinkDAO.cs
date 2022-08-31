using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyClass.Models;


namespace MyClass.DAO
{
    public class LinkDAO
    {
        private MyDBcontext db = new MyDBcontext();
        //lấy 1 mẫu tin 

        public Link getRow(int tableid, string typelink)
        {
            return db.Links.Where(m => m.TableId == tableid && m.TypeLink == typelink).FirstOrDefault();
        }


        //trả về danh sách các mẫu tin  
        public List<Link> getList()
        {
            var list = db.Links.ToList();
            return list;
        }
        // trả về sô lượng
        public long getCount()
        {
            var count = db.Links.Count();
            return count;
        }

        public Link getRow(int? id)
        {
            //throw new NotImplementedException();
            return null;
        }

        //trả về 1 mẫu tin
        public Link getRow(string slug)
        {
            var row = db.Links.Where(m => m.Slug == slug).FirstOrDefault();
            return row;
        }

        public int Insert(Link row)
        {
            db.Links.Add(row);
            return db.SaveChanges();
        }
        //Cập nhật mẫu tin 
        public int Update(Link row)
        {
            db.Entry(row).State = EntityState.Modified;

            return db.SaveChanges();
        }
        //Xóa mẫu tin 
        public int Delete(Link row)
        {
            db.Links.Remove(row);

            return db.SaveChanges();
        }

    }
}