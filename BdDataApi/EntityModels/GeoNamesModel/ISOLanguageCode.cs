namespace GeoNamesModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GeoNames.ISOLanguageCode")]
    public partial class ISOLanguageCode
    {
        [Key]
        [StringLength(3)]
        public string ISO_639_3 { get; set; }

        [StringLength(16)]
        public string ISO_639_2 { get; set; }

        [StringLength(2)]
        public string ISO_639_1 { get; set; }

        [StringLength(64)]
        public string languageName { get; set; }
    }
}
