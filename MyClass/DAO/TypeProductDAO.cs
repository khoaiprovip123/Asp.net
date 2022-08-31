using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyClass.Models;

namespace MyClass.DAO
{
    public class TypeProductDAO
    {
        private MyDBcontext db = new MyDBcontext();
        public List<TypeProduct> getList(string status = "All")
        {
            //trả về danh sách các mẫu tin 

            List<TypeProduct> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.TypeProducts.Where(m => m.Status != 0).ToList();
                        break;
                    }
                case "Trash":
                    {
                        list = db.TypeProducts.Where(m => m.Status == 0).ToList();
                        break;
                    }
                default:
                    {
                        list = db.TypeProducts.ToList();
                        break;
                    }
            }
            return list;

        }
        // trả về 1 mẫu tin 
        public TypeProduct getRow(int? id)
        {
            if (id == null)
            {

                return null;
            }
            else
            {
                return db.TypeProducts.Find(id);
            }
        }
        //thêm mẫu tin 
        public int Insert(TypeProduct row)
        {
            db.TypeProducts.Add(row);
            return db.SaveChanges();
        }
        //Cập nhật mẫu tin 
        public int Update(TypeProduct row)
        {
            db.Entry(row).State = EntityState.Modified;

            return db.SaveChanges();
        }
        //Xóa mẫu tin 
        public int Delete(TypeProduct row)
        {
            db.TypeProducts.Remove(row);

            return db.SaveChanges();
        }

    }
}


