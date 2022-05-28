using AdventureShare.Core.Models.Internal;
using System.Threading.Tasks;

namespace AdventureShare.Core.Abstractions.Services
{
    public interface IMessageService
    {
        void PublishFailure(Failure failure);
    }
}
