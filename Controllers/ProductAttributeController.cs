//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using InventoryBeginners.Data;
//using InventoryBeginners.Models;
//using System.Collections.Generic;
//using System;
//using System.Linq;
//using InventoryBeginners.Interfaces;
//using CodesByAniz.Tools;
//using Microsoft.AspNetCore.Authorization;

//namespace InventoryBeginners.Controllers
//{
//    [Authorize]
//    public class ProductAttributeController : Controller
//    {
//        private readonly IProductAttribute _Repo;
//        public ProductAttributeController(IProductAttribute repo) // here the repository will be passed by the dependency injection.
//        {
//            _Repo = repo;
//        }


//        public IActionResult Index(AttributeType attributeType, string sortExpression = "", string SearchText = "", int pg = 1, int pageSize = 5)
//        {
//            SortModel sortModel = new SortModel();
//            sortModel.AddColumn("name");            
//            sortModel.ApplySort(sortExpression);
//            ViewData["sortModel"] = sortModel;

//            if(attributeType==0 && TempData["attributeType"]!=null)
//                attributeType=(AttributeType)TempData["attributeType"];


//            ViewBag.SearchText = SearchText;
//            ViewBag.AttributeId = attributeType;
//            ViewBag.AttributeName = attributeType.ToString();

//            TempData["attributeType"] = attributeType;
//            TempData.Keep();


//            PaginatedList<ProductAttribute> items = _Repo.GetItems(attributeType,sortModel.SortedProperty, sortModel.SortedOrder, SearchText, pg, pageSize);

//            var pager = new PagerModel(items.TotalRecords, pg, pageSize);
//            pager.SortExpression = sortExpression;
//            this.ViewBag.Pager = pager;

//            TempData["CurrentPage"] = pg;

//            return View(items);
//        }


//        public IActionResult Create(AttributeType attributeType)
//        {
//            ProductAttribute item = new ProductAttribute();
//            item.AttributeId = attributeType;

//            ViewBag.AttributeId = attributeType;
//            ViewBag.AttributeName = attributeType.ToString();

//            return View(item);
//        }

//        [HttpPost]
//        public IActionResult Create(ProductAttribute item)
//        {
//            bool bolret = false;
//            string errMessage = "";
//            try
//            {    

//               if (_Repo.IsItemExists(item.Name, item.AttributeId) == true)
//                    errMessage = errMessage + " " + " Name " + item.Name + " Exists Already";

//                if (errMessage == "")
//                {
//                    item = _Repo.Create(item);
//                    bolret = true;
//                }

//            }
//            catch (Exception ex)
//            {
//                errMessage = errMessage + " " + ex.Message;
//            }
//            if (bolret == false)
//            {
//                TempData["ErrorMessage"] = errMessage;
//                ModelState.AddModelError("", errMessage);
//                return View(item);
//            }
//            else
//            {
//                TempData["SuccessMessage"] = "" + item.Name + " Created Successfully";
//                return RedirectToAction(nameof(Index),new { attributeType = item.AttributeId });
//            }
//        }

//        public IActionResult Details(int id) //Read
//        {
//            ProductAttribute item = _Repo.GetItem(id);
//            AttributeType attributeType = item.AttributeId;
//            ViewBag.AttributeId = attributeType;
//            ViewBag.AttributeName = attributeType.ToString();
//            return View(item);
//        }


//        public IActionResult Edit(int id)
//        {
//            ProductAttribute item = _Repo.GetItem(id);
//            TempData.Keep();

//            AttributeType attributeType = item.AttributeId;
//            ViewBag.AttributeId = attributeType;
//            ViewBag.AttributeName = attributeType.ToString();

//            return View(item);
//        }

//        [HttpPost]
//        public IActionResult Edit(ProductAttribute item)
//        {
//            bool bolret = false;
//            string errMessage = "";

//            try
//            {                
//                if (_Repo.IsItemExists(item.Name, item.Id,item.AttributeId) == true)
//                    errMessage = errMessage + item.Name + " Already Exists";

//                if (errMessage == "")
//                {
//                    item = _Repo.Edit(item);
//                    TempData["SuccessMessage"] = item.Name + ", Saved Successfully";
//                    bolret = true;
//                }
//            }
//            catch (Exception ex)
//            {
//                errMessage = errMessage + " " + ex.Message;
//            }

//            int currentPage = 1;
//            if (TempData["CurrentPage"] != null)
//                currentPage = (int)TempData["CurrentPage"];

//            if (bolret == false)
//            {
//                TempData["ErrorMessage"] = errMessage;
//                ModelState.AddModelError("", errMessage);
//                return View(item);
//            }
//            else
//                return RedirectToAction(nameof(Index), new { pg = currentPage, attributeType = item.AttributeId });
//        }

//        public IActionResult Delete(int id)
//        {
//            ProductAttribute item = _Repo.GetItem(id);
//            TempData.Keep();
//            AttributeType attributeType = item.AttributeId;
//            ViewBag.AttributeId = attributeType;
//            ViewBag.AttributeName = attributeType.ToString();
//            return View(item);
//        }


//        [HttpPost]
//        public IActionResult Delete(ProductAttribute item)
//        {
//            try
//            {
//                item = _Repo.Delete(item);
//            }
//            catch (Exception ex)
//            {
//                string errMessage = ex.Message;
//                TempData["ErrorMessage"] = errMessage;
//                ModelState.AddModelError("", errMessage);
//                return View(item);

//            }

//            int currentPage = 1;
//            if (TempData["CurrentPage"] != null)
//                currentPage = (int)TempData["CurrentPage"];

//            TempData["SuccessMessage"] = item.Name + " Deleted Successfully";
//            return RedirectToAction(nameof(Index), new { pg = currentPage , attributeType = item.AttributeId});


//        }


//    }
//}
