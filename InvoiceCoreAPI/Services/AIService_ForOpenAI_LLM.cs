using OpenAI.Responses;
using InvoiceCoreAPI.Contracts;
using InvoiceCoreAPI.Models.AI;

namespace InvoiceCoreAPI.Services
{
    public class AIService_ForOpenAI_LLM : IAIService
    {
        private readonly ILogger<AIService_ForOpenAI_LLM> _logger;
        private readonly OpenAISettings _settings;
        private readonly ResponsesClient _client;

        public AIService_ForOpenAI_LLM(
            ILogger<AIService_ForOpenAI_LLM> logger,
            IConfiguration configuration)
        {
            _logger = logger;

            _settings = configuration
                .GetSection("OpenAI")
                .Get<OpenAISettings>()
                ?? throw new InvalidOperationException(
                    "OpenAI configuration is missing.");

            if (string.IsNullOrWhiteSpace(_settings.ApiKey))
            {
                throw new InvalidOperationException(
                    "OpenAI API key is missing.");
            }

            _client = new ResponsesClient(_settings.ApiKey);
        }

        public async Task<AIAskResponse> AskAsync(AIAskRequest request)
        {
            _logger.LogInformation(
                "Sending question to OpenAI: {Question}",
                request.Question);

            var options = new CreateResponseOptions
            {
                Model = _settings.Model
            };

            options.InputItems.Add(
                ResponseItem.CreateUserMessageItem(request.Question));

            var result = await _client.CreateResponseAsync(options);

            return new AIAskResponse
            {
                Answer = result.Value.GetOutputText()
            };
        }
    }
}
