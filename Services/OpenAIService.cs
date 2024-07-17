using OpenAI_API;
using WebApplication2.Entities;
using WebApplication2.Models;
using Newtonsoft.Json;
using OpenAI_API.Chat;

namespace WebApplication2.Services
{
    public class OpenAIService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public OpenAIService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public async Task<string> GetTestResponse(string userInput)
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

            return "response";
        }

        public async Task<string> GetResponseWithoutSaving(string userInput)
        {
            var scope = _scopeFactory.CreateScope();
            var api = scope.ServiceProvider.GetRequiredService<OpenAIAPI>();

            List<ChatMessage> conversationHistory =
            [
                new ChatMessage(ChatMessageRole.System, "You are Bartender, a helpful assistant to provide suggestions about cocktails and other mixed drinks. A user may ask you about a certain recipe, or ask you to provide a recipe that suits the user's requirements."),
                new ChatMessage(ChatMessageRole.System, "You can provide with all well-known recipes, but also be creative to provide new recipes if you are asked to."),
                new ChatMessage(ChatMessageRole.System, "Only provide drink recipes as your answers. a recipe should include following contents: a name, a description, a list of ingredients, a list of instructions, and a list of tags."),
                new ChatMessage(ChatMessageRole.System, "Please provide your recipes in html with the following structure: <html><body><!---Your recipes here including name, description, ingredients, instructions and tags---></body></html>."),
                new ChatMessage(ChatMessageRole.System, "If a user asks questions that can't be answered as recipes, politely tell the user to ask another question."),
                new ChatMessage(ChatMessageRole.User, userInput),
            ];

            var chatResponse = await api.Chat.CreateChatCompletionAsync(
                new ChatRequest()
                {
                    Messages = conversationHistory
                }
            );

            var response = chatResponse.Choices[0].Message.Content;
            Console.WriteLine(response);

            return response;
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

            SaveResponseToDatabase(Response);

            return Response;
        }

        public async void SaveResponseToDatabase(string userInput)
        {
            var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<TestDbContext>();
            var api = scope.ServiceProvider.GetRequiredService<OpenAIAPI>();

            List<ChatMessage> conversationHistory =
            [
                    new ChatMessage(ChatMessageRole.System, "You are Bartender, a helpful assistant to provide recipes for drink making."),
                    new ChatMessage(ChatMessageRole.User, userInput),
                    new ChatMessage (ChatMessageRole.User, "Please rewrite this recipe in the form of a valid JSON object with the following structure: {\"name\": \"\", \"description\": \"\", \"ingredients\": [\"\"], \"instructions\": [\"\"], \"tags\": [\"\"]}."),
            ];

            var chatResponse = await api.Chat.CreateChatCompletionAsync(
                new ChatRequest()
                {
                    Messages = conversationHistory
                }
            );

            var response = chatResponse.Choices[0].Message.Content;
            Console.WriteLine(response);

            RecipeModel? item = null;
            try
            {
                item = JsonConvert.DeserializeObject<RecipeModel>(response);
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Deserialization error: {ex.Message}");
                Console.WriteLine($"Response: {response}");
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
                    Status = Entities.RecipeStatusType.Testing,
                };

                dbContext.Recipe.Add(recipe);
                await dbContext.SaveChangesAsync();
            }
        }

    }
}
