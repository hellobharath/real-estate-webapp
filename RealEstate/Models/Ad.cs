namespace RealEstate.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ad")]
    public partial class Ad
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Property_Id { get; set; }

        public string Status { get; set; }

        [StringLength(128)]
        public string Owner_Id { get; set; }

        [StringLength(200)]
        public string PropertyDesc { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? Date_Posted { get; set; }

        [StringLength(50)]
        public string Ad_Type { get; set; }

        public int? Price { get; set; }

        [ForeignKey("Property_Id")]
        public virtual Property Property { get; set; }

        [ForeignKey("Owner_Id")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
