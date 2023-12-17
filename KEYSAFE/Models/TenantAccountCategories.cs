using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.DynamicData;

namespace KEYSAFE.Models
{
    [Serializable, TableName("TenantAccountCategories")]
    public class TenantAccountCategories
    {
        #region Main
        [Key]
        public Guid SysId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        public bool Status { get; set; }
        #endregion
    }
}