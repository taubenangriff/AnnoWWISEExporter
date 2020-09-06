using System;
using System.Collections.Generic;
using System.Text;

namespace AnnoWWISEExporter.JsonWWISEConvert
{
    class AutoGuiding
    {
        static int StartGuid = 1414030000;
        static int Offset = 0;

        static public int GiveGuid() {
            Offset++;
            return StartGuid + Offset -1;
        }
    }
}
