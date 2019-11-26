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

        public int? Built_Up_Length { get; set; }

        public int? Built_Up_Width { get; set; }

        public int? Land_Length { get; set; }

        public int? Land_Width { get; set; }

        public int? NoOfBedrooms { get; set; }

        public virtual Property Property { get; set; }
    }
}
