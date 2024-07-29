using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
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
        public async Task command1(CommandContext context)// CommandContext accesses discord text
        {
            await context.Channel.SendMessageAsync($"Hello {context.User.Username}");
        }
    }
}
