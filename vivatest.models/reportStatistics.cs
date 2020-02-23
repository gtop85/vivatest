using System;

namespace vivatest.models
{
    public class ReportStatistics
    {
        public ReportStatistics()
        {
            MaxProfit = Decimal.MinValue;
        }
        public ReportStatistics Accumulate(Report report)
        {
            Count += 1;
            TotalSalePrice += report.SalePrice.Value;

            GrossSalesSum += report.GrossSales.Value;

            MaxProfit = Math.Max(MaxProfit, report.Profit.Value); 

            return this;
        } 
        public decimal TotalSalePrice { get; set; }
        public decimal AverageSalePrice { get; set; }
        public decimal GrossSalesSum { get; set; }
        public decimal MaxProfit { get; set; }
        public int Count { get; set; }

        public ReportStatistics Compute()
        {
            AverageSalePrice = TotalSalePrice / Count;

            return this;
        }
    }
}
