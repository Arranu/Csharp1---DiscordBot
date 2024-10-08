﻿
using DiscordBot.Commands.prefix;
using DiscordBot.Commands.slash;
using DiscordBot.config;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext.Exceptions;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.Lavalink;
using DSharpPlus.Net;
using DSharpPlus.SlashCommands;
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
            var lavalink = Client.UseLavalink();
            var endpoint = new ConnectionEndpoint
            {
                Hostname = "127.0.0.1", // Localhost (***CHANGE LATER***)
                Port = 433             // Must match the port in application.yml
            };
            var lavalinkConfig = new LavalinkConfiguration
            {
                Password = "youshallnotpass", // Must match the password in application.yml
                RestEndpoint = endpoint,
                SocketEndpoint = endpoint
            };
            
            
            var commandsConfig = new CommandsNextConfiguration()
            {
                StringPrefixes = new string[] {jsonReader.prefix},
                EnableMentionPrefix = true,
                EnableDms = true,
                EnableDefaultHelp = false,
            };
            Commands = Client.UseCommandsNext(commandsConfig);
            var slashCommandsConfig =Client.UseSlashCommands(); // enables use of slash commands

            Commands.RegisterCommands<TestCommands>();  //prefix command register
            slashCommandsConfig.RegisterCommands<TestSlashCommands>(); // slash command register
            Commands.CommandErrored += ErrorHandler;
            

            await Client.ConnectAsync();
            // await lavalink.ConnectAsync(lavalinkConfig); //- issue with lavalink connection, consult lavalink docs
            await Task.Delay(-1); //Delay-1 keeps the bot running indefinetly 
        }

        private static async Task ErrorHandler(CommandsNextExtension sender, CommandErrorEventArgs e)
        {
                if(e.Exception is ChecksFailedException exception)
            {
                string timeLeft=string.Empty;

                foreach(var check in exception.FailedChecks)
                {
                    var coolDown = (CooldownAttribute)check;
                     timeLeft = coolDown.GetRemainingCooldown(e.Context).ToString(@"hh\:mm\:ss"); // gets teh remaining time before cooldown clears in hours\minutes\seconds format
                }
                var coolDownMessage = new DiscordEmbedBuilder
                {
                    Color = DiscordColor.Red,
                    Title = "Please wait for the cooldown to end",
                    Description = $"Remaining: {timeLeft}"
                };
                await e.Context.Channel.SendMessageAsync(embed: coolDownMessage);
            }
        }

        private static async Task Client_MessageCreated(DiscordClient sender, DSharpPlus.EventArgs.MessageCreateEventArgs e)
        {
            await e.Message.CreateReactionAsync(DiscordEmoji.FromName(Program.Client, ":robot:"));
        }

        private static Task Client_Ready(DiscordClient sender, DSharpPlus.EventArgs.ReadyEventArgs e)
        {
            Console.WriteLine("Bot is connected!");
            return Task.CompletedTask; // resets the bot after a task has completed, making the client ready to take on a new task
        }
    }
}
