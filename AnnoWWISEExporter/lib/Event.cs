using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AnnoWWISEExporter.lib
{
    class Event
    {
        public String Id { get; set; }
        public String Name { get; set; }
        public String ObjectPath { get; set; }
        public String GUID { get; set; }
        public String DurationType { get; set; }
        public String DurationMin { get; set; }
        public String DurationMax { get; set; }
    }
}
