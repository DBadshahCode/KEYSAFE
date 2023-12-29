using KEYSAFE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace KEYSAFE.Controllers
{
    [Authorize]
    public class TenantAccountCategoryController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();
        // GET: Category
        public ActionResult Index()
        {
            //var tenantAccountCategories = _context.TenantAccountCategories.ToList();
            return View();
        }

        public JsonResult GetTenantAccountCategories()
        {
            var tenantAccountCategories = _context.TenantAccountCategories
                .Select(o => new
                {
                    o.SysId,
                    CreatedDate = o.CreatedDate.ToString(),
                    UpdatedDate = o.UpdatedDate.ToString(),
                    o.Name,
                    Status = o.Status.ToString(),
                })
                .ToList();
            var js = new JavaScriptSerializer();
            var data = js.Serialize(tenantAccountCategories);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(TenantAccountCategories tenantAccountCategories)
        {
            if (tenantAccountCategories != null)
            {
                _context.TenantAccountCategories.Add(
                new TenantAccountCategories()
                {
                    SysId = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    UpdatedDate = null,
                    Name = tenantAccountCategories.Name,
                });
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "TenantAccountCategory");
        }

        public ActionResult Details(Guid id)
        {
            var tenantAccountCategory = _context.TenantAccountCategories.SingleOrDefault(o => o.SysId == id);
            return View(tenantAccountCategory);
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            var tenantAccountCategory = _context.TenantAccountCategories.SingleOrDefault(o => o.SysId == id);
            return View(tenantAccountCategory);
        }

        [HttpPost]
        public ActionResult Edit(Guid id, TenantAccountCategories tenantAccountCategories)
        {
            var tenantAccountCategory = _context.TenantAccountCategories.SingleOrDefault(o => o.SysId == id);
            if (tenantAccountCategory != null)
            {
                tenantAccountCategory.UpdatedDate = DateTime.Now;
                tenantAccountCategory.Name = tenantAccountCategories.Name;
                _context.SaveChanges();
            }
            return View();
        }

        public ActionResult Delete(Guid id)
        {
            var tenantAccountCategory = _context.TenantAccountCategories.SingleOrDefault(o => o.SysId == id);
            _context.TenantAccountCategories.Remove(tenantAccountCategory);
            _context.SaveChanges();
            return RedirectToAction("Index", "TenantAccountCategory");
        }
    }
}