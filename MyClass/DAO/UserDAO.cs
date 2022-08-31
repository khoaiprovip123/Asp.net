using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyClass.Models;

namespace MyClass.DAO
{

    public class UserDAO
    {
        private MyDBcontext db = new MyDBcontext();
        public List<User> getList()
        {
            return db.Users.ToList();
        }
        public List<User> getList(string status = "All")
        {
            //trả về danh sách các mẫu tin 

            List<User> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.Users.Where(m => m.Status != 0).ToList();
                        break;
                    }
                case "Trash":
                    {
                        list = db.Users.Where(m => m.Status == 0).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Users.ToList();
                        break;
                    }
            }
            return list;

        }
        // trả về 1 mẫu tin 
        public User getRow(int? id)
        {
            if (id == null)
            {

                return null;
            }
            else
            {
                return db.Users.Find(id);
            }
        }
        public User getRow(string username,string roles)
        {
            return db.Users.Where(m => m.Status == 1 && m.Roles == roles   && (m.Username == username || m.Email == username)).FirstOrDefault();
            
        }
        public User getRow(string roles="Admin")
        {
            return db.Users.Where(m => m.Status == 1 && m.Roles == roles).FirstOrDefault();

        }
        //thêm mẫu tin
        public int Insert(User row)
        {
            db.Users.Add(row);
            return db.SaveChanges();
        }
        //Cập nhật mẫu tin 
        public int Update(User row)
        {
            db.Entry(row).State = EntityState.Modified;

            return db.SaveChanges();
        }
        //Xóa mẫu tin 
        public int Delete(User row)
        {
            db.Users.Remove(row);
            return db.SaveChanges();
        }

    }
}

