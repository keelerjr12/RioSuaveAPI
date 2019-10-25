﻿using System;

namespace RioSuaveAPI.Events
{
    public class EventInput
    {
        public string Name { get; set; } = "";

        public DateTime DateTimeStart { get; set; }

        public DateTime DateTimeEnd { get; set; }

        public string Description { get; set; } = "";
    }
}
