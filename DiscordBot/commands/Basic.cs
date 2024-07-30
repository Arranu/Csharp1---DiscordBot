using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.commands
{
    public class Basic : BaseCommandModule
    {
        [Command("test")]
        public async Task TestCommand(CommandContext context)
        {
            var interact = Program.Client.GetInteractivity();

            var msgRetrieve = await interact.WaitForMessageAsync(msg => msg.Content == "Hello");
            if(msgRetrieve.Result.Content == "Hello")
            {
                await context.Channel.SendMessageAsync($"{context.User.Username} said Hello");
            }          
        }
    }
}
