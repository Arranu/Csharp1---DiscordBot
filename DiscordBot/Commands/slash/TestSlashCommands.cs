using DSharpPlus.SlashCommands;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;

namespace DiscordBot.Commands.slash
{
    public class TestSlashCommands : ApplicationCommandModule //base class for slash commands
    {
        [SlashCommand("test", "First slash command test")]
        public async Task TestTask(InteractionContext context) // slash commands count as interactions (required - first message MUST be response)
        {
            await context.DeferAsync(); // sends no text ("defers") but acts as the first message and therefore a response so no error when firing
            await context.EditResponseAsync(new DSharpPlus.Entities.DiscordWebhookBuilder().WithContent($"Hello {context.User.Username} your task fired successfully")); 
            // edits the deferred response to desired texts via webhook 

        }

    }
}
