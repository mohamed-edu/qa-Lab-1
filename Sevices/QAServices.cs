using Azure;
using Azure.AI.Language.QuestionAnswering;
using Microsoft.Extensions.Options;
using qa_Lab_1.Models;
namespace qa_Lab_1.Sevices
{
    public class QAServices
    {
        private readonly QuestionAnsweringClient _client; //Klient för att kommunicera med Azure QA-tjänsten
        private readonly AzureLanguageService _options; // Håller konfigurationsalternativen för Azure-tjänsten

        public QAServices(IOptions<AzureLanguageService> options) // constructor
        {
            _options = options.Value;

            // Kontrollera att ProjectName och DeploymentName är angivna
            if (string.IsNullOrWhiteSpace(_options.ProjectName) ||
                string.IsNullOrWhiteSpace(_options.DeploymentName))
            {
                throw new ArgumentException("ProjectName and DeploymentName must be provided in the configuration.");
            }

            var endpoint = new Uri(_options.Endpoint);
            var credential = new AzureKeyCredential(_options.ApiKey);
            _client = new QuestionAnsweringClient(endpoint, credential);
        }

        public string GetAnswer(string question)
        {
            var project = new QuestionAnsweringProject(_options.ProjectName, _options.DeploymentName);

            try
            {

                var response = _client.GetAnswers(question, project); // Skicka frågan till Azure och hämta svar


                return response.Value.Answers.FirstOrDefault()?.Answer ?? "No answer found."; // Returnera första svaret eller standardmeddelande
            }
            catch (Exception ex)
            {

                return $"Request error: {ex.Message}";
            }
        }
    }
}
