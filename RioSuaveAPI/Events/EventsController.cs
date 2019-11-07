using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RioSuaveLib;
using RioSuaveLib.Events;

namespace RioSuaveAPI.Events
{
    [Route("api/[controller]")]
    public class EventsController : Controller
    {
        public EventsController(EventsService eventsService, ImageService imageService, IHostingEnvironment hostingEnvironment)
        {
            _eventsService = eventsService;
            _imageService = imageService;
            _hostingEnv = hostingEnvironment;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<IEnumerable<EventDTO>> GetAsync()
        {
            return await _eventsService.GetAllEventsAsync();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<EventDTO> GetAsync(Guid id)
        {
            return await _eventsService.GetEventByIdAsync(id);
        }

        // POST api/<controller>
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Guid>> PostAsync([FromForm] IFormFile file, [FromForm]string eventJsonInput)
        {
            var evtInput = JsonConvert.DeserializeObject<EventJsonInput>(eventJsonInput);
            Image img = null;

            if (file != null)
            {
                img = Image.FromStream(file.OpenReadStream());
            }

            var id = await _eventsService.CreateEventAsync(
                evtInput.Name,
                evtInput.DateTimeStart,
                evtInput.DateTimeEnd,
                evtInput.Location,
                evtInput.Description,
                img);

            return new CreatedResult(id.ToString(), id);
        }

        // PUT api/<controller>/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task PutAsync(Guid id, [FromBody]EventJsonInput evtInput)
        {
            await _eventsService.UpdateEventAsync(id, evtInput.Name, evtInput.DateTimeStart, evtInput.DateTimeEnd, evtInput.Location, evtInput.Description);
        }

        // DELETE api/<controller>/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task DeleteAsync(Guid id)
        {
            await _eventsService.DeleteEventAsync(id);
        }

        private readonly EventsService _eventsService;
        private readonly ImageService _imageService;
        private readonly IHostingEnvironment _hostingEnv;
    }
}
