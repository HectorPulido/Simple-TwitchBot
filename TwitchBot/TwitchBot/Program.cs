using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;

namespace TwitchBot
{
    public enum TypeCommand { Action, Text }

    public class Command
    {
        public string Text;
        public Action<string[]> commandScript;
        public TypeCommand typeCommand;
        public bool modOnly;
        public bool subOnly;

        public Command(string Text, bool modOnly = false, bool subOnly = false)
        {
            typeCommand = TypeCommand.Text;
            this.Text = Text;
            this.modOnly = modOnly;
            this.subOnly = subOnly;
        }
        public Command(Action<string[]> CommandScript, bool modOnly = false, bool subOnly = false)
        {
            typeCommand = TypeCommand.Action;
            this.commandScript = CommandScript;
            this.modOnly = modOnly;
            this.subOnly = subOnly;
        }

        public void Invoke(string[] args)
        {
            commandScript.Invoke(args);
        }
        public void Invoke()
        {
            commandScript.Invoke(new string[] { });
        }
    }

    public struct ChatClient
    {

        public string username, password, channel;
        public static string server = "irc.chat.twitch.tv";
        public static int port = 6667;
        Dictionary<string, Command> commands;

        public ChatClient(string username, string password, string channel, Dictionary<string, Command> commands)
        {
            this.username = username;
            this.password = password;
            this.channel = channel;
            this.commands = commands;
        }

        static void Send(StreamWriter writer, string message)
        {
            writer.WriteLine(message);
            writer.Flush();
        }
        static void SendMessage(StreamWriter writer, string channel, string message)
        {
            Send(writer, string.Format("PRIVMSG #{0} : {1}", channel, message));
        }

        public void StartBot(int maxRetry)
        {
            var retry = false;
            var retryCount = 0;
            do
            {
                try
                {
                    using (var irc = new TcpClient(ChatClient.server, ChatClient.port))
                    using (var stream = irc.GetStream())
                    using (var reader = new StreamReader(stream))
                    using (var writer = new StreamWriter(stream))
                    {

                        Send(writer, "CAP REQ :twitch.tv/tags twitch.tv/commands twitch.tv/membership");
                        Send(writer, string.Format("PASS oauth:{0}", password));
                        Send(writer, string.Format("NICK {0}", username));
                        Send(writer, string.Format("JOIN #{0}", channel));

                        while (true)
                        {

                            string inputLine;
                            while ((inputLine = reader.ReadLine()) != null)
                            {
                                if (inputLine.Contains("PING"))
                                {
                                    Send(writer, "PONG :tmi.twitch.tv");
                                }

                                if (inputLine.Contains("PRIVMSG"))
                                {
                                    // Mensaje en el chat
                                    string[] splitInput = inputLine.Split(new string[] { ";" }, StringSplitOptions.None);
                                    Dictionary<string, string> message = new Dictionary<string, string>();

                                    for (int i = 0; i < splitInput.Length; i++)
                                    {
                                        string[] splitZone = splitInput[i].Split('=');
                                        message.Add(splitZone[0], splitZone[1]);
                                    }

                                    string line = message["user-type"];
                                    line = line.Split(new string[] {"PRIVMSG"}, StringSplitOptions.None)[1];
                                    line = line.Split(new string[] {":"}, StringSplitOptions.None)[1];
                                    string user = message["display-name"];

                                    Console.WriteLine(string.Format("->{0} : {1}", user, line));

                                    var commandInLine = line.Split(' ')[0];
                                    var args = line.Split(' ');
                                    args[0] = "";

                                    if (commands.ContainsKey(commandInLine))
                                    {
                                        var command = commands[commandInLine];

                                        Console.WriteLine(message["mod"]);
                                        Console.WriteLine(message["subscriber"]);

                                        if ((command.modOnly && message["mod"] == "1") || !command.modOnly)
                                        {
                                            if ((command.subOnly && message["subscriber"] == "1") || !command.subOnly)
                                            {
                                                Console.WriteLine("Ejecutando comando " + commandInLine);

                                                if (command.typeCommand == TypeCommand.Action)
                                                {
                                                    command.Invoke(args);
                                                }
                                                else if (command.typeCommand == TypeCommand.Text)
                                                {
                                                    var text = command.Text.Replace("{username}", user);
                                                    SendMessage(writer, channel, text);
                                                }

                                            }
                                        }

                                    }
                                }
                                else
                                {
                                    Console.WriteLine(inputLine);
                                }
                            }

                        }
                    }
                }
                catch (Exception e)
                {
                    // shows the exception, sleeps for a little while and then tries to establish a new connection to the IRC server
                    Console.WriteLine(e.ToString());
                    Thread.Sleep(5000);
                    retry = ++retryCount <= maxRetry;
                }
            } while (retry);
        }
    }

    class Program
    {       

        static void Main(string[] args)
        {
            var commands = new Dictionary<string, Command>();
            commands.Add("!saludo", new Command("Holi mundo :3"));
            commands.Add("!saludame", new Command("Holi {username} :3"));
            commands.Add("!discord", new Command("Lindo servidor de Discord: https://discord.gg/pkNtfN"));
            commands.Add("!youtube", new Command("Lindo canal de Youtube: https://www.youtube.com/channel/UCS_iMeH0P0nsIDPvBaJckOw", true, false));

            var client = new ChatClient(
                "USERNAME", 
                "****************", 
                "CHANNEL",
                commands
                );
            client.StartBot(10);
        }
    }
}
