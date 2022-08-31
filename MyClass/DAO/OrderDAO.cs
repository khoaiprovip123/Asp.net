using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyClass.Models;

namespace MyClass.DAO
{

    public class OrderDAO
    {
        private MyDBcontext db = new MyDBcontext();
        public List<Order> getList()
        {
            return db.Orders.ToList();
        }
        public List<Order> getList(string status = "All")
        {
            //trả về danh sách các mẫu tin 

            List<Order> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.Orders.Where(m => m.Status != 0).ToList();
                        break;
                    }
                case "Trash":
                    {
                        list = db.Orders.Where(m => m.Status == 0).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Orders.ToList();
                        break;
                    }
            }
            return list;

        }
        // trả về 1 mẫu tin 
        public Order getRow(int? id)
        {
            if (id == null)
            {

                return null;
            }
            else
            {
                return db.Orders.Find(id);
            }
        }
        //thêm mẫu tin 
        public int Insert(Order row)
        {
            db.Orders.Add(row);
            return db.SaveChanges();
        }
        //Cập nhật mẫu tin 
        public int Update(Order row)
        {
            db.Entry(row).State = EntityState.Modified;

            return db.SaveChanges();
        }
        //Xóa mẫu tin 
        public int Delete(Order row)
        {
            db.Orders.Remove(row);

            return db.SaveChanges();
        }

    }
}

