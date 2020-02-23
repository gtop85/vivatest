using System;

namespace vivatest.models
{
    public class FinancialRecords
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public decimal? UnitsSold { get; set; }
        public decimal? ManufacturingPrice { get; set; }
        public decimal? SalePrice { get; set; }
        public decimal? GrossSales { get; set; }
        public decimal? Discounts { get; set; }
        public decimal? Sales { get; set; }
        public decimal? SalesInEuro { get; set; }
        public decimal? Profit { get; set; }
        public decimal? CostOfGoodsSold { get; set; }
        public DateTime? Date { get; set; }
    }
}
