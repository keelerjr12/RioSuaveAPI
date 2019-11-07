using System;

namespace RioSuaveAPI.Events
{
    public class EventJsonInput
    {
        public string Name { get; set; } = "";

        public DateTime DateTimeStart { get; set; }

        public DateTime DateTimeEnd { get; set; }

        public string Location { get; set; } = "";

        public string Description { get; set; } = "";
    }
}
