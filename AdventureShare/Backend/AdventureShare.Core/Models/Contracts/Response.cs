using System.Collections.Generic;
using System.Linq;

namespace AdventureShare.Core.Models.Contracts
{
    public class Response<TData>
    {
        public Response()
        {
            ErrorMessages = Enumerable.Empty<string>();
        }

        public ResponseCode Code { get; set; }
        public IEnumerable<string> ErrorMessages { get; set; }
        public TData Data { get; set; }
    }
}
