using System;
using System.Runtime.Serialization;

namespace vivatest.models
{
    [DataContract]

    public class CreatedRecord : Record
    {
        public CreatedRecord(Record b)
        {
            Product = b.Product;
            Country = b.Country;
            Segment = b.Segment;
            DiscountBand = b.DiscountBand;
            UnitsSold = b.UnitsSold;
            ManufacturingPrice = b.ManufacturingPrice;
            SalePrice = b.SalePrice;
            GrossSales = b.GrossSales;
            Discounts = b.Discounts;
            Sales = b.Sales;
            Profit = b.Profit;
            CostOfGoodsSold = b.CostOfGoodsSold;
            Date = b.Date;
        }
 
        [DataMember(Name = "id")]
        public Guid Id { get; set; }

        [DataMember(Name = "salesInEuro")]
        public decimal? SalesInEuro { get; set; }
    }
}
