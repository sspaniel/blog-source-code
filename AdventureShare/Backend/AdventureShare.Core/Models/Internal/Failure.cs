using System;

namespace AdventureShare.Core.Models.Internal
{
    public class Failure
    {
        public string Source { get; set; }
        public string Message { get; set; }
        public Exception Error { get; set; }
        public object Data { get; set; }
    }
}
