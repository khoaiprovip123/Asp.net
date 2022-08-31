using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyClass.Models;

namespace MyClass.DAO
{

    public class SupplierDAO
    {
        private MyDBcontext db = new MyDBcontext();
        public List<Supplier> getList()
        {
            return db.Suppliers.ToList();
        }
        public List<Supplier> getList(string status = "All")
        {
            //trả về danh sách các mẫu tin 

            List<Supplier> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.Suppliers.Where(m => m.Status != 0).ToList();
                        break;
                    }
                case "Trash":
                    {
                        list = db.Suppliers.Where(m => m.Status == 0).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Suppliers.ToList();
                        break;
                    }
            }
            return list;

        }
        // trả về 1 mẫu tin 
        public Supplier getRow(int? id)
        {
            if (id == null)
            {

                return null;
            }
            else
            {
                return db.Suppliers.Find(id);
            }
        }
        //thêm mẫu tin 
        public int Insert(Supplier row)
        {
            db.Suppliers.Add(row);
            return db.SaveChanges();
        }
        //Cập nhật mẫu tin 
        public int Update(Supplier row)
        {
            db.Entry(row).State = EntityState.Modified;

            return db.SaveChanges();
        }
        //Xóa mẫu tin 
        public int Delete(Supplier row)
        {
            db.Suppliers.Remove(row);

            return db.SaveChanges();
        }

    }
}

