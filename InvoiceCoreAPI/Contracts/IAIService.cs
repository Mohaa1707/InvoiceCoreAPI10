using InvoiceCoreAPI.Models.AI;

namespace InvoiceCoreAPI.Contracts
{
    public interface IAIService
    {
        Task<AIAskResponse> AskAsync(AIAskRequest request);
    }
}
