using System;

namespace RioSuaveLib.Events
{
    public class EventDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public DateTime DateTimeStart { get; set; }
        public DateTime DateTimeEnd { get; set; }
        public string Description { get; set; } = "";
    }
}
