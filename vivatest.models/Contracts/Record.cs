using System;
using System.Runtime.Serialization;

namespace vivatest.models
{
    [DataContract]
    public class Record
    {     
        [DataMember(Name = "product")]
        public string Product { get; set; }

        [DataMember(Name = "country")]
        public string Country { get; set; }

        [DataMember(Name = "segment")]
        public string Segment { get; set; }

        [DataMember(Name = "discountBand")]
        public string DiscountBand { get; set; }

        [DataMember(Name = "unitsSold")]
        public decimal? UnitsSold { get; set; }

        [DataMember(Name = "manufacturingPrice")]
        public decimal? ManufacturingPrice { get; set; }

        [DataMember(Name = "salePrice")]
        public decimal? SalePrice { get; set; }

        [DataMember(Name = "grossSales")]
        public decimal? GrossSales { get; set; }

        [DataMember(Name = "discounts")]
        public decimal? Discounts { get; set; }

        [DataMember(Name = "sales")]
        public decimal? Sales { get; set; }

        [DataMember(Name = "profit")]
        public decimal? Profit { get; set; }

        [DataMember(Name = "costOfGoodsSold")]
        public decimal? CostOfGoodsSold { get; set; }

        [DataMember(Name = "date")]
        public DateTime? Date { get; set; }
    }
}
