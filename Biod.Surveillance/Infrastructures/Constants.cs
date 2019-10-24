using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biod.Surveillance.Infrastructures
{
    public class Constants
    {
        public static class Event
        {
            public static int INVALID_ID = -1;
        }

        public static class Species
        {
            public static int HUMAN = 1;
        }

        public static class Priority
        {
            public static int MEDIUM = 2;
        }

        public static class HamType
        {
            public static int ALL_NON_SPAM = 0;
            public static int SPAM = 1;
            public static int DISEASE_ACTIVITY = 3;
        }

        public static class Date
        {
            public static DateTime DEFAULT = DateTime.Parse("1900-01-01");
        }
    }
}