using System;
using System.IO; 
using System.Text.Json;
using System.Text.Json.Serialization;
using AnnoWWISEExporter.JsonWWISEConvert;
using AnnoWWISEExporter.lib; 

namespace AnnoWWISEExporter
{
    class Program
    {
        static void Main(string[] args)
        {
            Converter c = new Converter("french", "german", "english");
            c.Convert(); 
        }
    }
}
