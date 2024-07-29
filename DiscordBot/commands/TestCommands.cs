using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.commands
{
    public class TestCommands: BaseCommandModule // inherits base class structure
    {
        [Command("test")]
        public async Task Speak(CommandContext context)// CommandContext accesses discord text
        {
            await context.Channel.SendMessageAsync($"Hello {context.User.Username}");
        }
        [Command("add")]
        public async Task Add(CommandContext context, int num1,int num2 )
        {
            int result = num1 + num2;
            await context.Channel.SendMessageAsync(result.ToString());
        }
        [Command("embed")]
        public async Task EmbedMsg(CommandContext context)
        {
            var msg = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()
                .WithTitle("Test Message - Embed")
                .WithDescription($"Executed by {context.User.Username}")
                .WithColor(DiscordColor.Blurple));

            await context.Channel.SendMessageAsync(msg);
        }
    }
}
