using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace vivatest.models
{
    public class ProductDMO
    {
        [DataMember(Name = "id")]
        public Guid? Id { get; set; }

        [DataMember(Name = "product")]
        public string Product { get; set; }

        [DataMember(Name = "country")]
        public string Country { get; set; }

        [DataMember(Name = "segment")]
        public string Segment { get; set; }

        [DataMember(Name = "discountBand")]
        public string DiscountBand { get; set; }
    }
}
