using System;

namespace RioSuaveLib.Events
{
    public class Event
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = "";

        public DateTime DateTimeStart { get; set; }

        public DateTime DateTimeEnd { get; set; }

        public string Location { get; set; } = "";

        public string Description { get; set; } = "";

        public string ImageUrl { get; set; } = "";
    }
}
