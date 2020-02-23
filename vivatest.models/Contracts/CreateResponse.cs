using System.Collections.Generic;
using System.Runtime.Serialization;

namespace vivatest.models
{
    [DataContract]
    public class CreateResponse
    {
        [DataMember(Name = "newRecords")]
        public List<CreatedRecord> NewRecords { get; set; }

        [DataMember(Name = "recordsCreated")]
        public int RecordsCreated { get; set; }

        [DataMember(Name = "recordsFailed")]
        public int RecordsFailed { get; set; }
    }
}
