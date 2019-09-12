using System;
using System.Collections.Generic;

namespace Biod.Api.Core.Models
{
    public partial class ProvisionMarkerDss
    {
        public int ObjectId { get; set; }
        public int OwnerScopeLocalId { get; set; }
        public int? ProvisionScopeLocalId { get; set; }
        public long ProvisionTimestamp { get; set; }
        public int ProvisionLocalPeerKey { get; set; }
        public int? ProvisionScopePeerKey { get; set; }
        public long? ProvisionScopePeerTimestamp { get; set; }
        public DateTime? ProvisionDatetime { get; set; }
        public int? State { get; set; }
        public byte[] Version { get; set; }
    }
}
