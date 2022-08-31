using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyClass.Models;

namespace MyClass.DAO
{

    public class OrderdetailDAO
    {
        private MyDBcontext db = new MyDBcontext();
        // GET: Admin/Order
        public List<OrderDetail> getList(int? orderid)
        {
            var list = db.OrderDetails.Where(m=>m.Orderid==orderid).ToList();
            return list;
        }
     
        // trả về 1 mẫu tin 
        public OrderDetail getRow(int? id)
        {
            if (id == null)
            {

                return null;
            }
            else
            {
                return db.OrderDetails.Find(id);
            }
        }
        //thêm mẫu tin 
        public int Insert(OrderDetail row)
        {
            db.OrderDetails.Add(row);
            return db.SaveChanges();
        }
        //Cập nhật mẫu tin 
        public int Update(OrderDetail row)
        {
            db.Entry(row).State = EntityState.Modified;

            return db.SaveChanges();
        }
        //Xóa mẫu tin 
        public int Delete(OrderDetail row)
        {
            db.OrderDetails.Remove(row);

            return db.SaveChanges();
        }

    }
}

