using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace vivatest.fixer
{
    [DataContract]
    public class RatesResponse
    {
        [DataMember(Name = "success")]
        public bool Success { get; set; }

        [DataMember(Name = "timestamp")]
        public int Timestamp { get; set; }

        [DataMember(Name = "base")]
        public string Base { get; set; }

        [DataMember(Name = "date")]
        public DateTime Date { get; set; }

        [DataMember(Name = "rates")]
        public Dictionary<string, decimal> Rates { get; set; }
    }
}
