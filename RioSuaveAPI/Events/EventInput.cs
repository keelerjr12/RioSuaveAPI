using System;
using Microsoft.AspNetCore.Http;

namespace RioSuaveAPI.Events
{
    public class EventInput
    {
        public IFormFile Image { get; set; }
    }
}
