using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AnnoWWISEExporter.lib
{
    class MemoryFile
    {
        public String Id { get; set; }
        public String Language { get; set; }
        public String ShortName { get; set; }
        public String Path { get; set; }
    }
}
