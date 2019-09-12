namespace GeoNamesModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GeoNames.AdminCode")]
    public partial class AdminCode
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AdminCode()
        {
            ParentAdminHierarchies = new HashSet<AdminHierarchy>();
            ChildAdminHierarchies = new HashSet<AdminHierarchy>();
        }

        [Required]
        [StringLength(80)]
        public string code { get; set; }

        [StringLength(200)]
        public string ucs2Name { get; set; }

        [StringLength(200)]
        public string asciiName { get; set; }

        [Key, ForeignKey("Place")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public virtual Place Place { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AdminHierarchy> ParentAdminHierarchies { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AdminHierarchy> ChildAdminHierarchies { get; set; }
    }
}
