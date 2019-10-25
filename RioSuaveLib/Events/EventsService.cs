using System;
using System.Collections.Generic;
using System.Linq;

namespace RioSuaveLib.Events
{
    public class EventsService
    {
        public EventsService(RioSuaveContext rsContext)
        {
            _rsContext = rsContext;
        }

        public IEnumerable<EventDTO> GetAllEvents()
        {
            return _rsContext.Events.Select(e =>
                new EventDTO {Id = e.Id, Name = e.Name, DateTimeStart = e.DateTimeStart, DateTimeEnd = e.DateTimeEnd, Description = e.Description}
            );
        }

        public EventDTO? GetEventById(Guid id)
        {
            var evt = FindById(id);

            var evtDTO = new EventDTO
            {
                Id = evt.Id,
                Name = evt.Name,
                DateTimeStart = evt.DateTimeStart,
                DateTimeEnd = evt.DateTimeEnd,
                Description = evt.Description
            };

            return evtDTO;
        }

        public void CreateEvent(string name, DateTime dateTimeStart, DateTime dateTimeEnd, string description)
        {
            _rsContext.Events?.Add(new Event
            {
                Id = Guid.NewGuid(),
                Name = name,
                DateTimeStart = dateTimeStart,
                DateTimeEnd = dateTimeEnd,
                Description = description
            });

            _rsContext.SaveChanges();
        }

        public void UpdateEvent(Guid id, string name, DateTime dateTimeStart, DateTime dateTimeEnd, string description)
        {
            var evt = FindById(id);
            evt.Name = name;
            evt.DateTimeStart = dateTimeStart;
            evt.DateTimeEnd = dateTimeEnd;
            evt.Description = description;

            _rsContext.SaveChanges();
        }

        public void DeleteEvent(Guid id)
        {
            var evt = FindById(id);
            _rsContext.Events?.Remove(evt);

            _rsContext.SaveChanges();
        }

        private Event FindById(Guid id)
        {
            return _rsContext.Events.SingleOrDefault(e => e.Id == id);
        }


        private readonly RioSuaveContext _rsContext;
    }
}
