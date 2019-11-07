using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using MailKit;
using Microsoft.EntityFrameworkCore;

namespace RioSuaveLib.Events
{
    public class EventsService
    {
        public EventsService(RioSuaveContext rsContext, ImageService imageService)
        {
            _rsContext = rsContext;
            _imageService = imageService;
        }

        public async Task<IEnumerable<EventDTO>> GetAllEventsAsync()
        {
            return await _rsContext.Events.Select(e =>
                new EventDTO
                {
                    Id = e.Id, 
                    Name = e.Name, 
                    DateTimeStart = e.DateTimeStart, 
                    DateTimeEnd = e.DateTimeEnd, 
                    Location = e.Location,
                    Description = e.Description, 
                    ImageUrl = e.ImageUrl
                }
            ).OrderBy(e => e.DateTimeStart).ThenBy(e => e.DateTimeEnd).ThenBy(e => e.Name).ToListAsync();
        }

        public async Task<EventDTO?> GetEventByIdAsync(Guid id)
        {
            var evt = await FindByIdAsync(id);

            var evtDTO = new EventDTO
            {
                Id = evt.Id,
                Name = evt.Name,
                DateTimeStart = evt.DateTimeStart,
                DateTimeEnd = evt.DateTimeEnd,
                Location = evt.Location,
                Description = evt.Description,
                ImageUrl = evt.ImageUrl
            };

            return evtDTO;
        }

        public async Task<Guid> CreateEventAsync(string name, DateTime dateTimeStart, DateTime dateTimeEnd, string location, string description, Image image)
        {
            var id = Guid.NewGuid();
            var imgUrl = "";

            if (image != null)
            {
                imgUrl = _imageService.SaveImage(id.ToString(), image);
            }

            var newEvent = new Event
            {
                Id = id,
                Name = name,
                DateTimeStart = dateTimeStart,
                DateTimeEnd = dateTimeEnd,
                Location = location,
                Description = description,
                ImageUrl = imgUrl
            };

            if (_rsContext?.Events == null)
                throw new NullReferenceException("Database context is null -- CreateEventAsync");

            await _rsContext.Events.AddAsync(newEvent);
            await _rsContext.SaveChangesAsync();

            return newEvent.Id;
        }

        public async Task UpdateEventAsync(Guid id, string name, DateTime dateTimeStart, DateTime dateTimeEnd, string location, string description)
        {
            var evt = await FindByIdAsync(id);

            evt.Name = name;
            evt.DateTimeStart = dateTimeStart;
            evt.DateTimeEnd = dateTimeEnd;
            evt.Location = location;
            evt.Description = description;

            await _rsContext.SaveChangesAsync();
        }

        public async Task DeleteEventAsync(Guid id)
        {
            var evt = await FindByIdAsync(id);

            _rsContext.Events?.Remove(evt);
            await _rsContext.SaveChangesAsync();

            if (evt.ImageUrl != null)
                _imageService.DeleteImage(evt.ImageUrl);
        }

        private async Task<Event> FindByIdAsync(Guid id)
        {
            return await _rsContext.Events.SingleOrDefaultAsync(e => e.Id == id);
        }

        private readonly RioSuaveContext _rsContext;
        private readonly ImageService _imageService
            ;
    }
}
