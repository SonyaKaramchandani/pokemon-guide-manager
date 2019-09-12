﻿using System;

namespace Biod.Diseases.SyncConsole.Models
{
    public class GeorgeMessagings
    {
        public GeorgeMessagingClass[] GeorgeMessagingList { get; set; }
    }

    public class GeorgeMessagingClass
    {
        public int diseaseId { get; set; }
        public int sectionId { get; set; }
        public string section { get; set; }
        public string message { get; set; }
        public DateTime lastModified { get; set; }
    }

}