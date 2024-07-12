using OpenAI_API;
using WebApplication2.Entities;
using WebApplication2.Models;
using Newtonsoft.Json;

namespace WebApplication2.Services
{
    public class OpenAIService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public OpenAIService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public async Task<string> GetFakeResponse(string userInput)
        {
            var scope = _scopeFactory.CreateScope();
            var api = scope.ServiceProvider.GetRequiredService<OpenAIAPI>();

            var chat = api.Chat.CreateConversation();

            chat.AppendSystemMessage("You are a teacher who helps children understand if things are animals or not.  If the user tells you an animal, you say \"yes\".  If the user tells you something that is not an animal, you say \"no\".  You only ever respond with \"yes\" or \"no\".  You do not say anything else.");

            chat.AppendUserInput("Is this an animal? Cat");
            chat.AppendExampleChatbotOutput("Yes");
            chat.AppendUserInput("Is this an animal? House");
            chat.AppendExampleChatbotOutput("No");

            chat.AppendUserInput(userInput);
            string response = await chat.GetResponseFromChatbotAsync();

            //<ChatResult> result = api.Chat.CreateChatCompletionAsync("Hello!");
            //Console.WriteLine(result.Result.Choices[0].Message);

            return "response";
        }


        public async Task<string> GetChatResponse(string userInput)
        {
            var scope = _scopeFactory.CreateScope();

            var api = scope.ServiceProvider.GetRequiredService<OpenAIAPI>();
            var dbContext = scope.ServiceProvider.GetRequiredService<TestDbContext>();

            string Query = userInput;
            //    + " Please provide your answer in the form of a valid JSON object with the following structure: "
            //    + "{\"name\": \"\", \"description\": \"\", \"ingredients\": [\"\"], \"instructions\": [\"\"], \"tags\": [\"\"]}.";
            
            var result = await api.Completions.CreateCompletionAsync(
                new OpenAI_API.Completions.CompletionRequest
                {
                    Prompt = Query,
                    MaxTokens = 250
                }
            );
            string Response = result.Completions[0].Text;

            RecipeModel? item = null;
            try
            {
                item = JsonConvert.DeserializeObject<RecipeModel>(Response);
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Deserialization error: {ex.Message}");
                Console.WriteLine($"Response: {Response}");
            }

            if (item != null)
            {
                Recipe recipe = new Recipe()
                {
                    RecipeId = Guid.NewGuid(),
                    Name = item.Name,
                    Description = item.Description,
                    Ingredients = item.Ingredients,
                    Instructions = item.Instructions,
                    Tags = item.Tags,
                };

                dbContext.Recipe.Add(recipe);
                await dbContext.SaveChangesAsync();
            }

            return Response;
        }
    }
}
