using AdventureShare.Core.Models.Internal;

namespace AdventureShare.Core.Abstractions.Services
{
    public interface IMessageService
    {
        void PublishFailure(Failure failure);
    }
}
