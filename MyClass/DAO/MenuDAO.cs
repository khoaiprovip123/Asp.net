using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyClass.Models;

namespace MyClass.DAO
{

    public class MenuDAO
    {
        private MyDBcontext db = new MyDBcontext();


        public List<Menu> getListByParentId(string position, int parentid = 0)
        {
            return db.Menus
                .Where(m => m.Parentid == parentid && m.Status == 1 && m.Position == position).OrderBy(m => m.Orders)
                .ToList();
        }
        public List<Menu> getList(string status = "All")
        {
            //trả về danh sách các mẫu tin 

            List<Menu> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.Menus.Where(m => m.Status != 0).ToList();
                        break;
                    }
                case "Trash":
                    {
                        list = db.Menus.Where(m => m.Status == 0).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Menus.ToList();
                        break;
                    }
            }
            return list;

        }
        // trả về 1 mẫu tin 
        public Menu getRow(int? id)
        {
            if (id == null)
            {

                return null;
            }
            else
            {
                return db.Menus.Find(id);
            }
        }
        //thêm mẫu tin 
        public int Insert(Menu row)
        {
            db.Menus.Add(row);
            return db.SaveChanges();
        }
        //Cập nhật mẫu tin 
        public int Update(Menu row)
        {
            db.Entry(row).State = EntityState.Modified;

            return db.SaveChanges();
        }
        //Xóa mẫu tin 
        public int Delete(Menu row)
        {
            db.Menus.Remove(row);

            return db.SaveChanges();
        }

    }
}

