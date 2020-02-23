using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace vivatest.models
{
    [DataContract]
    public class Report : Record
    {
        public Report(Record b)
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
        public Guid? Id { get; set; }

        [DataMember(Name = "averageSalePrice")]
        public decimal? AverageSalePrice { get; set; }

        [DataMember(Name = "grossSalesSum")]
        public decimal? GrossSalesSum { get; set; }

        [DataMember(Name = "maxProfit")]
        public decimal? MaxProfit { get; set; }
    }
}
