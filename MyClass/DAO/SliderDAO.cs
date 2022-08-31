﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyClass.Models;

namespace MyClass.DAO
{

    public class SliderDAO
    {
        private MyDBcontext db = new MyDBcontext();
        public List<Slider> getList()
        {
            return db.Sliders.ToList();
        }
        public List<Slider> getListByPosition(string position)
        {
            return db.Sliders
                .Where(m=>m.Position==position && m.Status==1)
                .OrderBy(m=>m.Orders)
                .ToList();
        }
        public List<Slider> getList(string status = "All")
        {
            //trả về danh sách các mẫu tin 

            List<Slider> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.Sliders.Where(m => m.Status != 0).ToList();
                        break;
                    }
                case "Trash":
                    {
                        list = db.Sliders.Where(m => m.Status == 0).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Sliders.ToList();
                        break;
                    }
            }
            return list;

        }
        // trả về 1 mẫu tin 
        public Slider getRow(int? id)
        {
            if (id == null)
            {

                return null;
            }
            else
            {
                return db.Sliders.Find(id);
            }
        }
        //thêm mẫu tin 
        public int Insert(Slider row)
        {
            db.Sliders.Add(row);
            return db.SaveChanges();
        }
        //Cập nhật mẫu tin 
        public int Update(Slider row)
        {
            db.Entry(row).State = EntityState.Modified;

            return db.SaveChanges();
        }
        //Xóa mẫu tin 
        public int Delete(Slider row)
        {
            db.Sliders.Remove(row);

            return db.SaveChanges();
        }

    }
}

