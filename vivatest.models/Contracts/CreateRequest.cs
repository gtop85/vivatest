using System.Collections.Generic;
using System.Runtime.Serialization;

namespace vivatest.models
{
    [DataContract]
    public class CreateRequest
    {
        [DataMember(Name = "records")]
        public List<Record> Records { get; set; }
    }
}
