using System;

namespace vivatest.models
{
    public class Products
    {
        public Guid Id { get; set; }
        public Guid CountryId { get; set; }
        public Guid SegmentId { get; set; }
        public Guid DiscountBandId { get; set; }
    }
}
