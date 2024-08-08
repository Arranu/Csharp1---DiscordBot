using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using System.Threading.Tasks;

namespace DiscordBot.Commands.slash
{
    public class TestSlashCommands : ApplicationCommandModule //base class for slash commands
    {
        [SlashCommand("test", "First slash command test")]
        public async Task TestTask(InteractionContext intContext) // slash commands count as interactions (required - first message MUST be response)
        {
            await intContext.DeferAsync(); // sends no text ("defers") but acts as the first message and therefore a response so no error when firing
            var embedMsg = new DiscordEmbedBuilder
            {
                Color = DiscordColor.IndianRed,
                Title = $"Hello {intContext.User.Username} your task fired successfully"
            };
            await intContext.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embedMsg)); 
            // edits the deferred response to desired texts via webhook 

        }
        [SlashCommand("param","allows custom parameter to then send to channel")]
        public async Task Param(InteractionContext intContext, [Option("Param","paramater, an option to send to current text channel")] string parameter)
        {
            await intContext.DeferAsync();
            var embedMsg = new DiscordEmbedBuilder
            {
                Color = DiscordColor.IndianRed,
                Title = "Param",
                Description = parameter
            };
            await intContext.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embedMsg));
        }
    }
}
