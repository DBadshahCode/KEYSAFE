using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.DynamicData;

namespace KEYSAFE.Models
{
    [Serializable, TableName("TenantAccountPins")]
    public class TenantAccountPins
    {
        #region Main
        [Key]
        public Guid SysId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        [Required(ErrorMessage = "Account is required.")]
        public Guid TenantAccountId { get; set; }
        public int Pin { get; set; }
        [Required(ErrorMessage = "Length is required.")]
        public int Length { get; set; }
        public bool Generated { get; set; }
        public bool Status { get; set; }
        #endregion

        #region Navigations
        [ForeignKey("TenantAccountId")]
        public TenantAccounts TenantAccounts { get; set; }
        #endregion
    }
}