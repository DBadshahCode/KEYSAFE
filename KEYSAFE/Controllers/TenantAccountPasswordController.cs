using KEYSAFE.Models;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Text;

namespace KEYSAFE.Controllers
{
    public class TenantAccountPasswordController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();
        // GET: TenantAccountPassword
        public ActionResult Index()
        {
            //var tenantAccountPasswords = _context.TenantAccountPasswords.Include(o => o.TenantAccounts).ToList();
            return View();
        }

        public JsonResult GetTenantAccountPasswords()
        {
            var tenantAccountPasswords = _context.TenantAccountPasswords
                .Select(o => new
                {
                    o.SysId,
                    CreatedDate = o.CreatedDate.ToString(),
                    UpdatedDate = o.UpdatedDate.ToString(),
                    Account = o.TenantAccounts.Name,
                    o.Password,
                    o.Length,
                    Generated = o.Generated.ToString(),
                    Status = o.Status.ToString(),
                })
                .ToList();
            var js = new JavaScriptSerializer();
            var data = js.Serialize(tenantAccountPasswords);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var tenantAccounts = _context.TenantAccounts.Where(o => o.SecurityType == SecurityType.Password).ToList();

            var viewModel = new TenantAccountPasswordViewModels()
            {
                TenantAccountPasswords = new TenantAccountPasswords(),
                TenantAccounts = tenantAccounts
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(TenantAccountPasswords tenantAccountPasswords)
        {
            if (tenantAccountPasswords != null)
            {
                if (tenantAccountPasswords.Generated)
                {
                    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_-+=<>?";

                    Random random = new Random();
                    StringBuilder password = new StringBuilder();

                    for (int i = 0; i < password.Length; i++)
                    {
                        int index = random.Next(chars.Length);
                        password.Append(chars[index]);
                    }

                    _context.TenantAccountPasswords.Add(new TenantAccountPasswords()
                    {
                        SysId = Guid.NewGuid(),
                        CreatedDate = DateTime.Now,
                        UpdatedDate = null,
                        TenantAccountId = tenantAccountPasswords.TenantAccountId,
                        Password = password.ToString(),
                        Length = tenantAccountPasswords.Length,
                        Generated = tenantAccountPasswords.Generated,
                        Status = tenantAccountPasswords.Status,
                    });
                }
                else
                {
                    _context.TenantAccountPasswords.Add(new TenantAccountPasswords()
                    {
                        SysId = Guid.NewGuid(),
                        CreatedDate = DateTime.Now,
                        UpdatedDate = null,
                        TenantAccountId = tenantAccountPasswords.TenantAccountId,
                        Password = tenantAccountPasswords.Password,
                        Length = tenantAccountPasswords.Length,
                        Generated = tenantAccountPasswords.Generated,
                        Status = tenantAccountPasswords.Status,
                    });
                }

                _context.SaveChanges();
            }
            return RedirectToAction("Index", "Password");
        }

        public ActionResult Details(Guid id)
        {
            var tenantAccountPassword = _context.TenantAccountPasswords.Include(o => o.TenantAccounts).SingleOrDefault(o => o.SysId == id);
            return View(tenantAccountPassword);
        }

        //[HttpGet]
        //public ActionResult Edit(Guid id)
        //{
        //    var tenantAccountPassword = _context.TenantAccountPasswords.SingleOrDefault(o => o.SysId == id);
        //    return View(tenantAccountPassword);
        //}

        //[HttpPost]
        //public ActionResult Edit(Guid id, TenantAccountPasswords tenantAccountPasswords)
        //{
        //    var tenantAccountPassword = _context.TenantAccountPasswords.SingleOrDefault(o => o.SysId == id);
        //    if (tenantAccountPassword != null)
        //    {
        //        tenantAccountPassword.UpdatedDate = DateTime.Now;
        //        tenantAccountPassword.Password = tenantAccountPasswords.Password;
        //        tenantAccountPassword.Status = tenantAccountPasswords.Status;
        //        tenantAccountPassword.Generated = false;
        //        _context.SaveChanges();
        //    }
        //    return View();
        //}

        public ActionResult Delete(Guid id)
        {
            var tenantAccountPassword = _context.TenantAccountPasswords.SingleOrDefault(o => o.SysId == id);
            _context.TenantAccountPasswords.Remove(tenantAccountPassword);
            _context.SaveChanges();
            return RedirectToAction("Index", "Password");
        }
    }
}