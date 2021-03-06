﻿using shopPhone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;


namespace shopPhone.Controllers
{
    public class UserController : Controller
    {
        private ShopDienThoaiEntities db = new ShopDienThoaiEntities();
        // GET: User
        public ActionResult Index()
        {

            var data = db.rela_phone_gallery.Include(r => r.gallery).Include(r => r.phone)
                            .Select(g => new { g.id_gallery,g.gallery.gallery_name, g.phone,g.phone.id_brand})
                            .Join(db.brands,b=>b.id_brand,brand=>brand.id_brand,(b,brand)=>new { b, brand })
                            .Where(bg=>bg.b.id_brand== bg.brand.id_brand)
                            .Select(bg=>new { bg.b.id_gallery,bg.b.gallery_name,bg.b.phone,bg.brand})
                            .GroupBy(g => new { g.id_gallery, g.gallery_name}).ToList()
                            .Select(a => new { id = a.Key, listItemS = a.Select(bp => new brand_phone(bp.phone, bp.brand))});

            List<ItemSlide> list = new List<ItemSlide>();

            foreach (var x in data.ToList())
            {
                //ViewBag.list = x.listItemS.ToList();
                //List<phone> a = x.listItemS.ToList<phone>();
                ItemSlide item = new ItemSlide();
                item.nameCatlg = x.id.gallery_name;
                item.idCatlg = x.id.id_gallery;
                
                item.listItemS = x.listItemS.ToList();
                list.Add(item);
            }
            return View(list);
        }

        public ActionResult Detail(int ID)
        {
            phone aPhone = db.phones.Find(ID);
            return View(aPhone);
        }
        public ActionResult ListPhones(string SortParam)
        {
            ViewBag.SortParam = String.IsNullOrEmpty(SortParam) ? "position":"";
            List<phone> list = new List<phone>();
            switch (SortParam)
            {
                case "sort_price_down":
                    {
                        list = db.phones.OrderByDescending(p => p.price).ToList<phone>();
                        break;
                    }
                case "sort_price_up":
                    {
                        list = db.phones.OrderBy(p => p.price).ToList<phone>();
                        break;
                    }
                default:
                {
                    list = db.phones.OrderBy(p => p.position).ToList<phone>();
                        break;
                }
                    

            }
            
            //
            //list = db.phones.OrderByDescending(p => p.price).ToList<phone>();
            return View(list);
        }

        /*Sort Action*/



    }
}