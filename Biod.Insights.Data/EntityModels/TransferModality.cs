using System;
using System.Collections.Generic;

namespace Biod.Insights.Data.EntityModels
{
    public partial class TransferModality
    {
        public TransferModality()
        {
            AcquisitionModes = new HashSet<AcquisitionModes>();
        }

        public int TransferModalityId { get; set; }
        public string TransferModalityName { get; set; }

        public virtual ICollection<AcquisitionModes> AcquisitionModes { get; set; }
    }
}
