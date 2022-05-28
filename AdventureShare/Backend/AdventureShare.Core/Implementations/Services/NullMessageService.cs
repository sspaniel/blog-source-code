using AdventureShare.Core.Abstractions.Services;
using AdventureShare.Core.Models.Internal;

namespace AdventureShare.Core.Implementations.Services
{
    public class NullMessageService : IMessageService
    {
        public void PublishFailure(Failure failure)
        {
            // do nothing
        }
    }
}
