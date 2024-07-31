﻿using DiscordBot.commands;
using DiscordBot.config;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Threading.Tasks;

namespace DiscordBot
{
    public sealed class Program  // sealed = cannot be extended/inherited 
        
    {
        public static DiscordClient Client { get; set; }
        public static CommandsNextExtension Commands { get; set; }
        static async Task Main(string[] args)
        { 
            var jsonReader = new JSONReader();
            await jsonReader.ReadJSON();

            var discordConfig = new DiscordConfiguration()
            {
                Intents = DiscordIntents.All, // relates to intents privellges - see discord dev bot page for more details
                Token = jsonReader.token,
                TokenType = TokenType.Bot, // specifies the token is for a bot entity
                AutoReconnect = true, // will attempt to reconnect in the event of the bot crashing
            };

            Client = new DiscordClient(discordConfig);
            Client.UseInteractivity(new DSharpPlus.Interactivity.InteractivityConfiguration()
            {
                Timeout =TimeSpan.FromMinutes(2) //commands that interact with read will timeout after 2 mins by default
            });


            Client.Ready += Client_Ready; //client_ready is added to client.ready class as a usable function.
            Client.MessageCreated += Client_MessageCreated;
            

            var commandsConfig = new CommandsNextConfiguration()
            {
                StringPrefixes = new string[] {jsonReader.prefix},
                EnableMentionPrefix = true,
                EnableDms = true,
                EnableDefaultHelp = false,
            };
            Commands = Client.UseCommandsNext(commandsConfig);
            Commands.RegisterCommands<TestCommands>();

            await Client.ConnectAsync();
            await Task.Delay(-1); //Delay-1 keeps the bot running indefinetly 
        }


        private static async Task Client_MessageCreated(DiscordClient sender, DSharpPlus.EventArgs.MessageCreateEventArgs e)
        {
            await e.Message.CreateReactionAsync(DiscordEmoji.FromName(Program.Client, ":robot:"));
        }

        private static Task Client_Ready(DiscordClient sender, DSharpPlus.EventArgs.ReadyEventArgs e)
        {
            return Task.CompletedTask; // resets the bot after a task has completed, making the client ready to take on a new task
        }
    }
}
