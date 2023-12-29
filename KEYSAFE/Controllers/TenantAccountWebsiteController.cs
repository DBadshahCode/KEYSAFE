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
    public class TenantAccountWebsiteController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();
        // GET: Website
        public ActionResult Index()
        {
            //var tenantAccountWebsites = _context.TenantAccountWebsites.ToList();
            return View();
        }

        public JsonResult GetTenantAccountWebsites()
        {
            var tenantAccountWebsites = _context.TenantAccountWebsites
                .Select(o => new
                {
                    o.SysId,
                    CreatedDate = o.CreatedDate.ToString(),
                    UpdatedDate = o.UpdatedDate.ToString(),
                    o.Name,
                    o.Url,
                    Status = o.Status.ToString(),
                })
                .ToList();
            var js = new JavaScriptSerializer();
            var data = js.Serialize(tenantAccountWebsites);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(TenantAccountWebsites tenantAccountWebsites)
        {
            if (tenantAccountWebsites != null)
            {
                _context.TenantAccountWebsites.Add(
                new TenantAccountWebsites()
                {
                    SysId = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    UpdatedDate = null,
                    Name = tenantAccountWebsites.Name,
                    Url = tenantAccountWebsites.Url,
                    Status = tenantAccountWebsites.Status,
                });
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "TenantAccountWebsites");
        }

        public ActionResult Details(Guid id)
        {
            var tenantAccountWebsite = _context.TenantAccountWebsites.SingleOrDefault(o => o.SysId == id);
            return View(tenantAccountWebsite);
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            var tenantAccountWebsite = _context.TenantAccountWebsites.SingleOrDefault(o => o.SysId == id);
            return View(tenantAccountWebsite);
        }

        [HttpPost]
        public ActionResult Edit(Guid id, TenantAccountWebsites tenantAccountWebsites)
        {
            var tenantAccountWebsite = _context.TenantAccountWebsites.SingleOrDefault(o => o.SysId == id);
            if (tenantAccountWebsite != null)
            {
                tenantAccountWebsite.UpdatedDate = DateTime.Now;
                tenantAccountWebsite.Name = tenantAccountWebsites.Name;
                tenantAccountWebsite.Status = tenantAccountWebsites.Status;
                tenantAccountWebsite.Url = tenantAccountWebsites.Url;
                _context.SaveChanges();
            }
            return View();
        }

        public ActionResult Delete(Guid id)
        {
            var tenantAccountWebsite = _context.TenantAccountWebsites.SingleOrDefault(o => o.SysId == id);
            _context.TenantAccountWebsites.Remove(tenantAccountWebsite);
            _context.SaveChanges();
            return RedirectToAction("Index", "TenantAccountWebsites");
        }
    }
}