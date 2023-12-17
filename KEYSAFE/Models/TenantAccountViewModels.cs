using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KEYSAFE.Models
{
    public class TenantAccountViewModels
    {
        public TenantAccounts TenantAccounts { get; set; }
        public IEnumerable<TenantAccountCategories> TenantAccountCategories { get; set; }
        public IEnumerable<TenantAccountWebsites> TenantAccountWebsites { get; set; }
    }

    public class TenantAccountPasswordViewModels
    {
        public TenantAccountPasswords TenantAccountPasswords { get; set; }
        public IEnumerable<TenantAccounts> TenantAccounts { get; set; }
    }

    public class TenantAccountPinViewModels
    {
        public TenantAccountPins TenantAccountPins { get; set; }
        public IEnumerable<TenantAccounts> TenantAccounts { get; set; }
    }
}