using DiscordBot.misc;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.commands
{
    public class TestCommands : BaseCommandModule // inherits base class structure
    {
        [Command("test")]
        public async Task Speak(CommandContext context)// CommandContext accesses discord text
        {
            await context.Channel.SendMessageAsync($"Hello {context.User.Username}");
        }
        [Command("add")]
        public async Task Add(CommandContext context, int num1, int num2)
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
        [Command("cardgame")]
        public async Task CardGame(CommandContext context)
        {
            var userCard = new CardSys();
            var userCardEmbed = new DiscordEmbedBuilder  //alternative way of declaring embed config
            {
                Title = $"Your card is {userCard.SelectedCard}",
                Color = DiscordColor.DarkGreen

            };
            await context.Channel.SendMessageAsync(embed: userCardEmbed);
            var botCard = new CardSys();
            var botCardEmbed = new DiscordEmbedBuilder
            {
                Title = $"The Bot drew {botCard.SelectedCard}",
                Color = DiscordColor.Orange
            };
            await context.Channel.SendMessageAsync(embed: botCardEmbed);
        }
        [Command("test2")]
        public async Task Respond(CommandContext context)
        {
            var interact = Program.Client.GetInteractivity(); //always use program.client unless you want it to read a specific channel

            var msgRetrieve = await interact.WaitForMessageAsync(msg => msg.Content == "Hello");
            if (msgRetrieve.Result.Content == "Hello")
            {
                await context.Channel.SendMessageAsync($"{context.User.Username} said Hello");
            }
        }
        [Command("poll")]
        [Cooldown(5,100,CooldownBucketType.Guild)] // after command is fired 5 times, cooldown of 100 seconds applied to all channels on the server (guild)
        public async Task Poll(CommandContext context, string option1, string option2, string option3, string option4, [RemainingText] string title)
        {

            var interact = Program.Client.GetInteractivity();
            var timeout = TimeSpan.FromSeconds(10);
            DiscordEmoji[] emojiList = { DiscordEmoji.FromName(Program.Client, ":one:"),
                                        DiscordEmoji.FromName(Program.Client, ":two:"),
                                        DiscordEmoji.FromName(Program.Client, ":three:"),
                                        DiscordEmoji.FromName(Program.Client, ":four:")
            };

            

            string[] options = { option1,
                                 option2,
                                 option3,
                                 option4 };

            var optionsDescription = string.Join("\n", emojiList.Select((emoji, index) => $"{emoji} | {options[index]}"));


            var pollMsg = new DiscordEmbedBuilder
            {
                Color = DiscordColor.Gold,
                Title = title,
                Description = optionsDescription,
            };

            var sentPoll = await context.Channel.SendMessageAsync(embed: pollMsg);
            foreach (var emoji in emojiList)
            
                {
                    await sentPoll.CreateReactionAsync(emoji);
                }
                var finalTotal = await interact.CollectReactionsAsync(sentPoll, timeout);

            int[] counts = new int[emojiList.Length];

            foreach (var emoji in finalTotal)
            {
                for (int i = 0; i < emojiList.Length; i++)
                {
                    if (emoji.Emoji == emojiList[i])
                    {
                        counts[i]++;
                        break;
                    }
                }
            }
            for (int i = 0; i < emojiList.Length; i++)
            {
                await context.Channel.SendMessageAsync($"{emojiList[i]}: {counts[i]} votes");
            }

        }
    }
}

