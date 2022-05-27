using System.Collections.Generic;
using System.Linq;

namespace AdventureShare.Core.Models.Common
{
    public class Response<TData>
    {
        public Response()
        {
            Errors = Enumerable.Empty<string>();
        }

        public ResponseCode Code { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public TData Data { get; set; }
    }
}
