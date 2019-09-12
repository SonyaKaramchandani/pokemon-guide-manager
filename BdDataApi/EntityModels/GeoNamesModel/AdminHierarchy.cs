namespace GeoNamesModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GeoNames.AdminHierarchy")]
    public partial class AdminHierarchy
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int parentId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int childId { get; set; }

        [StringLength(64)]
        public string type { get; set; }

        public virtual AdminCode ParentAdminCode { get; set; }

        public virtual AdminCode ChildAdminCode { get; set; }
    }
}
