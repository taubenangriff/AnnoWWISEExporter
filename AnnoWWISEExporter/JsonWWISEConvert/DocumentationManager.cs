using System;
using System.Collections.Generic;
using System.Text;
using System.IO; 

namespace AnnoWWISEExporter.JsonWWISEConvert
{
    class DocumentationManager
    {
        private string OutputFileName = "kek.txt";
        private StreamWriter writer; 
        public DocumentationManager() {
            writer = new StreamWriter(File.Create(OutputFileName));
        }

        public void AddAudio(String GUID, String WwiseName, String WwiseId) {
            writer.Write("Name: {0} \n", WwiseName);
            writer.Write("GUID: {0} \n", GUID);
            writer.Write("WwiseId: {0} \n\n", WwiseId);
            writer.Flush(); 
        }
    }
}
