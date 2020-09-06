using System;
using System.Collections.Generic;
using System.Text;

namespace AnnoWWISEExporter.Exceptions
{
    class FilesNotSimilarException : Exception
    {
        public FilesNotSimilarException() { 
        
        }

        public FilesNotSimilarException(string message)
            : base(message)
        {
        }
        public FilesNotSimilarException(string message, Exception inner)
        : base(message)
        { 
        }
    }
}
