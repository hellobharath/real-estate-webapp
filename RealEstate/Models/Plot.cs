namespace RealEstate.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Plot")]
    public partial class Plot
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        public int? Length { get; set; }

        public int? Width { get; set; }

        public int? Price_Per_Sqft { get; set; }

        public virtual Property Property { get; set; }
    }
}
