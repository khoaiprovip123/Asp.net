using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyClass.Models;

namespace MyClass.DAO
{

    public class ColorDAO
    {
        private MyDBcontext db = new MyDBcontext();

        public List<Color> getList(string status = "All")
        {
            //trả về danh sách các mẫu tin 

            List<Color> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.Colors.Where(m => m.Status != 0).ToList();
                        break;
                    }
                case "Trash":
                    {
                        list = db.Colors.Where(m => m.Status == 0).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Colors.ToList();
                        break;
                    }
            }
            return list;

        }
        // trả về 1 mẫu tin 
        public Color getRow(int? id)
        {
            if (id == null)
            {

                return null;
            }
            else
            {
                return db.Colors.Find(id);
            }
        }
        //thêm mẫu tin 
        public int Insert(Color row)
        {
            db.Colors.Add(row);
            return db.SaveChanges();
        }
        //Cập nhật mẫu tin 
        public int Update(Color row)
        {
            db.Entry(row).State = EntityState.Modified;

            return db.SaveChanges();
        }
        //Xóa mẫu tin 
        public int Delete(Color row)
        {
            db.Colors.Remove(row);

            return db.SaveChanges();
        }

    }
}

