using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.DynamicData;

namespace KEYSAFE.Models
{
    [Serializable, TableName("TenantAccounts")]
    public class TenantAccounts
    {
        #region Main
        [Key]
        public Guid SysId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        public Guid? TenantAccountCategoryId { get; set; }
        public Guid? TenantAccountWebsiteId { get; set; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNumber { get; set; }
        [Required(ErrorMessage = "Security Type is required.")]
        public SecurityType SecurityType { get; set; }
        public string Comments { get; set; }
        public bool Closed { get; set; }
        #endregion

        #region Navigations
        [ForeignKey("TenantAccountCategoryId")]
        public TenantAccountCategories TenantAccountCategories { get; set; }
        [ForeignKey("TenantAccountWebsiteId")]
        public TenantAccountWebsites TenantAccountWebsites { get; set; }

        #endregion
    }

    public enum SecurityType
    {
        Password = 0,
        Pin = 1
    }
}