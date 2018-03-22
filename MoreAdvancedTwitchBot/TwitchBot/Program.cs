using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;

namespace TwitchBot
{
    using Addons;
    using Core;
    using System.Threading.Tasks;

    public static class Program
    {
        static void Main(string[] Args)
        {
            App.bot = new Bot(
                Credentials.Username, 
                Credentials.Oauth, 
                Credentials.Channel)
            {
                commands = Commands.commands
            };

            App.startTime = DateTime.Now;
            App.bot.whenNewMessage += (username, message) => Console.WriteLine($"{username}: {message}");
            App.bot.whenNewSystemMessage += (message) => Console.WriteLine($"System: {message}");
            App.bot.whenDisconnect += () => Console.WriteLine("Desconexion");
            App.bot.whenStart += () => Console.WriteLine("Conexion");
            App.bot.whenNewChater += (username) => App.bot.SendMessage($"{username}, bienvenido al stream!");
            
            
            Task.Factory.StartNew(() => App.bot.StartBot(3));

            while (true)
            {
                var line = Console.ReadLine();
                App.bot.SendMessage(line);
                Commands.TriggerCommand(line);

            }
        }
    }
}
