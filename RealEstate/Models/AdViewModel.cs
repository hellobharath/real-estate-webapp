using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealEstate.Models
{
    public class AdViewModel
    {
        public Ad Ad { get; set; }
        public Property Property { get; set; }
        public Plot Plot { get; set; }
        public Residential Residential { get; set; }
        public AdViewModel()
        {

        }
        public AdViewModel(Ad ad, Property property, Plot plot, Residential residential)
        {
            this.Ad = ad;
            this.Property = property;
            this.Plot = plot;
            this.Residential = residential;
        }
    }
}