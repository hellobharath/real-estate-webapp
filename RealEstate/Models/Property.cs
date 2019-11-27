namespace RealEstate.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Property")]
    public partial class Property
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Property()
        {
            Ads = new HashSet<Ad>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(128)]
        public string Id { get; set; }

        [StringLength(50)]
        public string Category { get; set; }

        [StringLength(50)]
        public string Location { get; set; }

        [StringLength(50)]
        public string City { get; set; }
        
        [StringLength(128)]
        public string Owner_Id { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        [Display(Name ="Image")]
        public byte[] image1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ad> Ads { get; set; }

        public virtual Plot Plot { get; set; }

        public virtual Residential Residential { get; set; }

        [InverseProperty("Property")]
        public virtual ICollection<Agreement> Agreements { get; set; }

        [ForeignKey("Owner_Id")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
