using KEYSAFE.Models;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace KEYSAFE.Controllers
{
    [Authorize]
    public class TenantAccountController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();
        // GET: TenantAccount
        public ActionResult Index()
        {
            //var tenantAccounts = _context.TenantAccounts
            //    .Include(o => o.TenantAccountCategories)
            //    .Include(o => o.TenantAccountWebsites)
            //    .ToList();
            return View();
        }

        public JsonResult GetTenantAccounts()
        {
            var tenantAccounts = _context.TenantAccounts
                .Select(o => new
                {
                    o.SysId,
                    CreatedDate = o.CreatedDate.ToString(),
                    UpdatedDate = o.UpdatedDate.ToString(),
                    o.Name,
                    o.Username,
                    o.EmailAddress,
                    o.MobileNumber,
                    SecurityType = o.SecurityType.ToString(),
                    o.Comments,
                    Closed = o.Closed.ToString(),
                    Category = o.TenantAccountCategories != null ? o.TenantAccountCategories.Name : "",
                    Website = o.TenantAccountWebsites != null ? o.TenantAccountWebsites.Name : "",
                })
                .ToList();
            var js = new JavaScriptSerializer();
            var data = js.Serialize(tenantAccounts);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var tenantAccountCategories = _context.TenantAccountCategories.ToList();
            var tenantAccountWebsites = _context.TenantAccountWebsites.ToList();
            var viewModel = new TenantAccountViewModels()
            {
                TenantAccounts = new TenantAccounts(),
                TenantAccountCategories = tenantAccountCategories,
                TenantAccountWebsites = tenantAccountWebsites,
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(TenantAccounts tenantAccounts)
        {
            if (tenantAccounts != null)
            {
                _context.TenantAccounts.Add(
                new TenantAccounts()
                {
                    SysId = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    UpdatedDate = null,
                    Name = tenantAccounts.Name,
                    TenantAccountCategoryId = tenantAccounts.TenantAccountCategoryId,
                    TenantAccountWebsiteId = tenantAccounts.TenantAccountWebsiteId,
                    Username = tenantAccounts.Username,
                    EmailAddress = tenantAccounts.EmailAddress,
                    MobileNumber = tenantAccounts.MobileNumber,
                    SecurityType = tenantAccounts.SecurityType,
                    Comments = tenantAccounts.Comments,
                });
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "TenantAccount");
        }

        public ActionResult Details(Guid id)
        {
            var tenantAccounts = _context.TenantAccounts
                .Include(o => o.TenantAccountCategories)
                .Include(o => o.TenantAccountWebsites)
                .SingleOrDefault(o => o.SysId == id);
            return View(tenantAccounts);
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            var tenantAccounts = _context.TenantAccounts.SingleOrDefault(o => o.SysId == id);
            var tenantAccountCategories = _context.TenantAccountCategories.ToList();
            var tenantAccountWebsites = _context.TenantAccountWebsites.ToList();

            var viewModel = new TenantAccountViewModels()
            {
                TenantAccounts = tenantAccounts,
                TenantAccountCategories = tenantAccountCategories,
                TenantAccountWebsites = tenantAccountWebsites
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(Guid id, TenantAccounts tenantAccounts)
        {
            var tenantAccount = _context.TenantAccounts.SingleOrDefault(o => o.SysId == id);
            if (tenantAccount != null)
            {
                tenantAccount.UpdatedDate = DateTime.Now;
                tenantAccount.Name = tenantAccounts.Name;
                tenantAccount.TenantAccountCategories = tenantAccounts.TenantAccountCategories;
                tenantAccount.TenantAccountWebsites = tenantAccounts.TenantAccountWebsites;
                tenantAccount.Username = tenantAccounts.Username;
                tenantAccount.EmailAddress = tenantAccounts.EmailAddress;
                tenantAccount.MobileNumber = tenantAccounts.MobileNumber;
                tenantAccount.SecurityType = tenantAccounts.SecurityType;
                tenantAccount.Comments = tenantAccounts.Comments;
                _context.SaveChanges();
            }
            return View();
        }

        public ActionResult Delete(Guid id)
        {
            var tenantAccount = _context.TenantAccounts.SingleOrDefault(o => o.SysId == id);
            _context.TenantAccounts.Remove(tenantAccount);
            _context.SaveChanges();
            return RedirectToAction("Index", "TenantAccount");
        }
    }
}