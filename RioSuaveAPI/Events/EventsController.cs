using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RioSuaveLib.Events;

namespace RioSuaveAPI.Events
{
    [Route("api/[controller]")]
    public class EventsController : Controller
    {
        public EventsController(EventsService eventsService)
        {
            _eventsService = eventsService;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<EventDTO> Get()
        {
            return _eventsService.GetAllEvents();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public EventDTO Get(Guid id)
        {
            return _eventsService.GetEventById(id);
        }

        // POST api/<controller>
        [Authorize]
        [HttpPost]
        public void Post([FromBody]EventInput evtInput)
        {
            _eventsService.CreateEvent(evtInput.Name, evtInput.DateTimeStart, evtInput.DateTimeEnd, evtInput.Location, evtInput.Description);
        }

        // PUT api/<controller>/5
        [Authorize]
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody]EventInput evtInput)
        {
            _eventsService.UpdateEvent(id, evtInput.Name, evtInput.DateTimeStart, evtInput.DateTimeEnd, evtInput.Location, evtInput.Description);
        }

        // DELETE api/<controller>/5
        [Authorize]
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _eventsService.DeleteEvent(id);
        }

        private readonly EventsService _eventsService;
    }
}
