namespace RealEstate.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Agreement")]
    public partial class Agreement
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(50)]
        public string Type { get; set; }

        [Display(Name = "Terms of Agreement")]
        [Required]
        public string Agreement_Terms { get; set; }

        [Display(Name = "Advance Amount")]
        [Required]
        public int? Token_Advance { get; set; }

        [Display(Name = "Total Amount")]
        [Required]
        public int? Total_Amt { get; set; }

        [Display(Name = "Duration of Agreement")]
        [Required]
        public int? Agreement_Duration { get; set; }

        [StringLength(128)]
        public string Payer_Id { get; set; }

        [StringLength(128)]
        public string Payee_Id { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        public string Property_Id { get; set; }

        [ForeignKey("Property_Id")]
        public virtual Property Property { get; set; }

        [ForeignKey("Payer_Id")]
        public virtual ApplicationUser Payer { get; set; }

        [ForeignKey("Payee_Id")]
        public virtual ApplicationUser Payee { get; set; }
    }
}
