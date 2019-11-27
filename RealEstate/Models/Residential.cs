namespace RealEstate.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Residential")]
    public partial class Residential
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        [StringLength(50)]
        public string Type { get; set; }

        [Display(Name = "Built up length")]
        [Required]
        public int? Built_Up_Length { get; set; }

        [Display(Name = "Built up width")]
        [Required]
        public int? Built_Up_Width { get; set; }

        [Display(Name = "Length of Land")]
        [Required]
        public int? Land_Length { get; set; }

        [Display(Name = "Width of Land")]
        [Required]
        public int? Land_Width { get; set; }

        [Display(Name = "Number of bedrooms")]
        [Required]
        public int? NoOfBedrooms { get; set; }

        public virtual Property Property { get; set; }
    }
}
