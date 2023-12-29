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
    [Authorize]
    public class TenantAccountPinController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();
        // GET: TenantAccountPin
        public ActionResult Index()
        {
            var tenantAccountPins = _context.TenantAccountPins.Include(o => o.TenantAccounts).ToList();
            return View(tenantAccountPins);
        }

        public JsonResult GetTenantAccountPins()
        {
            var tenantAccountPins = _context.TenantAccountPins
                .Select(o => new
                {
                    o.SysId,
                    CreatedDate = o.CreatedDate.ToString(),
                    UpdatedDate = o.UpdatedDate.ToString(),
                    Account = o.TenantAccounts.Name,
                    o.Pin,
                    o.Length,
                    Generated = o.Generated.ToString(),
                    Status = o.Status.ToString(),
                })
                .ToList();
            var js = new JavaScriptSerializer();
            var data = js.Serialize(tenantAccountPins);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var tenantAccounts = _context.TenantAccounts.Where(o => o.SecurityType == SecurityType.Pin).ToList();

            var viewModel = new TenantAccountPinViewModels()
            {
                TenantAccountPins = new TenantAccountPins(),
                TenantAccounts = tenantAccounts
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(TenantAccountPins tenantAccountPins)
        {
            if (tenantAccountPins != null)
            {
                if (tenantAccountPins.Generated)
                {
                    StringBuilder builder = new StringBuilder();
                    Random random = new Random();
                    char character;
                    for (int i = 0; i < tenantAccountPins.Length; i++)
                    {
                        character = Convert.ToChar(Convert.ToInt32(Math.Floor(10 * random.NextDouble() + 48)));
                        builder.Append(character);
                    }

                    _context.TenantAccountPins.Add(new TenantAccountPins()
                    {
                        SysId = Guid.NewGuid(),
                        CreatedDate = DateTime.Now,
                        UpdatedDate = null,
                        TenantAccountId = tenantAccountPins.TenantAccountId,
                        Pin = Convert.ToInt32(builder.ToString()),
                        Length = tenantAccountPins.Length,
                        Generated = tenantAccountPins.Generated,
                        Status = tenantAccountPins.Status,
                    });
                }
                else
                {
                    _context.TenantAccountPins.Add(new TenantAccountPins()
                    {
                        SysId = Guid.NewGuid(),
                        CreatedDate = DateTime.Now,
                        UpdatedDate = null,
                        TenantAccountId = tenantAccountPins.TenantAccountId,
                        Pin = tenantAccountPins.Pin,
                        Length = tenantAccountPins.Length,
                        Generated = tenantAccountPins.Generated,
                        Status = tenantAccountPins.Status,
                    });
                }

                _context.SaveChanges();
            }
            return RedirectToAction("Index", "TenantAccountPin");
        }

        public ActionResult Details(Guid id)
        {
            var tenantAccountPin = _context.TenantAccountPins.Include(o => o.TenantAccounts).SingleOrDefault(o => o.SysId == id);
            return View(tenantAccountPin);
        }

        //[HttpGet]
        //public ActionResult Edit(Guid id)
        //{
        //    var tenantAccountPin = _context.TenantAccountPins.SingleOrDefault(o => o.SysId == id);
        //    return View(tenantAccountPin);
        //}

        //[HttpPost]
        //public ActionResult Edit(Guid id, TenantAccountPins tenantAccountPins)
        //{
        //    var tenantAccountPin = _context.TenantAccountPins.SingleOrDefault(o => o.SysId == id);
        //    if (tenantAccountPin != null)
        //    {
        //        tenantAccountPin.UpdatedDate = DateTime.Now;
        //        tenantAccountPin.Pin = tenantAccountPins.Pin;
        //        tenantAccountPin.Status = tenantAccountPins.Status;
        //        tenantAccountPin.Generated = false;
        //        _context.SaveChanges();
        //    }
        //    return View();
        //}

        public ActionResult Delete(Guid id)
        {
            var tenantAccountPin = _context.TenantAccountPins.SingleOrDefault(o => o.SysId == id);
            _context.TenantAccountPins.Remove(tenantAccountPin);
            _context.SaveChanges();
            return RedirectToAction("Index", "TenantAccountPin");
        }
    }
}