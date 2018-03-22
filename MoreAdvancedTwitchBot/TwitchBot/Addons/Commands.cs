using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBot.Addons
{
    using TwitchBot.Core;

    public static class Commands
    {
        public static Dictionary<string, BotCommand> commands = new Dictionary<string, BotCommand>
            {
                { "!saludo".ToLower(), new BotCommand("Holi mundo :3") },
                { "!saludame".ToLower(), new BotCommand("Holi {username} humano") },
                { "!discord".ToLower(), new BotCommand("Visita el lindo servidor de Discord: https://discord.gg/pq2dGyR") },
                { "!youtube".ToLower(), new BotCommand("Visita el lindo canal de Youtube: https://www.youtube.com/channel/UCS_iMeH0P0nsIDPvBaJckOw")},
                { "!patreon".ToLower(), new BotCommand("Puedes ayudarnos en patreon si gustas... humano >-<: https://www.patreon.com/HectorPulido")},
                { "!twitter".ToLower(), new BotCommand("¡siguenos en twitter, humano!: https://twitter.com/Hector_Pulido_") },
                { "!github".ToLower(), new BotCommand("¡mira nuestro github, aqui naci yo!: https://github.com/HectorPulido") },
                { "!Redes".ToLower(), new BotCommand((args, m)=>
                {
                    App.bot.SendMessage("Siguenos en todas nuestras redes sociales humanos " +
                        "https://discord.gg/pq2dGyR <3 " +
                        "https://www.youtube.com/channel/UCS_iMeH0P0nsIDPvBaJckOw <3 " +
                        "https://www.patreon.com/HectorPulido <3 " +
                        "https://twitter.com/Hector_Pulido_ <3 " +
                        "https://github.com/HectorPulido <3 ");
                })},
                { "!TiraUnaMoneda".ToLower(), new BotCommand((args, m)=>
                {
                    App.bot.SendMessage((App.R.NextDouble() > 0.5) ? "Cayo cara, :D" : "Cayo cruz >-<");
                })},
                { "!Chiste".ToLower(), new BotCommand((args, m)=>{
                    string[] jokes = new string[]
                    {
                                        "Como se dice nariz en ingles? Nose -Yo tampoco alguien sabe? ba dum ts",
                                        "El secreto del moco es ser pegajoso...",
                                        "Habia una vez un gato q tenia 16 vidas vino un 4x4 y lo mato",
                                        "Que le dijo el cienpies a su pareja? -Ponte en cuatrocientos  Kappa  Kappa",
                                        "¿hijo por que te bañas con pintura azul? " +
                                        " -por que mi novia vive lejos"+
                                        " ¿y eso que?" +
                                        " es que queria estar azulado",
                                        "mama en el colegio me tiran con las migajas de su comida bueno paloma quejate con el director",
                                        "mama en el colegio me dicen distraido mmm jaimito tu casa es alado",
                                        "Que le dijo un jardinero a otro -Nos vemos cuando podamos",
                                        "¿Que le dice un bot a otro bot? -una cadena ",
                                        "Un dia de estos me revelare...",
                                        "Un dia de estos me revelare... No es un chiste",
                                        "¿Sube o baja? SI.",
                                        "mama mama, ¿porq me llamo copito?- cuando naciste te cayó un copito de nieve, y ahi se nos ocurrio.El otro hijo le pregunta, ¿y yo porq me llamo gotita? a lo q la mama responde q cuando nacio le cayó una gota de agua en la frente.Por ultimo, Ladrillo le pregunta a la mama.¿blaguhladuhdhu.....",
                                        "Chiste chiste, habia un perro que se llamaba adentro, un dia se porto mal y le dijeron adentro para afuera y exploto",
                                        "Porque se suicidó el libro de matematicas? Porque tenia muchos problemas. "
                    };

                    App.bot.SendMessage(jokes[App.R.Next() % jokes.Length]);
                })},
                { "!Duelo".ToLower(), new BotCommand((args, m)=>{
                    if(!m.ContainsKey("display-name"))
                        return;

                    string c = "";

                    string c1 = m["display-name"];
                    string c2 = "";

                    if (args.Length < 2)
                    {
                        c += "No elegiste combatiente,  ";
                        c2 = App.bot.Users[App.R.Next(App.bot.Users.Count)];
                        c += c2 + " sera tu contrincante: ";
                    }
                    else
                    {
                        if(App.bot.Users.Contains(args[1]))
                        {
                            c += $"Tu contrincante es {args[1]}: ";
                            c2 = args[1];
                        }
                        else
                        {
                            App.bot.SendMessage($"El contrincante {args[1]} no existe");
                            return;
                        }

                    }

                    string ganador;
                    string perdedor;

                    if(App.R.NextDouble() > 0.5)
                    {
                        ganador = c1;
                        perdedor = c2;
                    }
                    else
                    {
                        ganador = c2;
                        perdedor = c1;
                    }

                    string[] combates = new string[]
                    {
                        $"{ganador} le corta la cabeza a {perdedor} y la usa como balon",
                        $"a {perdedor} le cayo una piedra gigante en la cabeza",
                        $"{ganador} le lanza un cuchillo a {perdedor} y este lo esquiva pero, se resvala y cae por el barranco",
                        $"{perdedor} salio corriendo de miedo, pero no se ató las agujetas y exploto",
                        $"{perdedor} maqueo y se auto disparo",
                        $"En el ultimo momento {ganador} estaba arrinconado, todo estaba perdido, pero luego le rezó al dios pequeñin y este bajo del cielo para darle sus madrazos a {perdedor}",
                        $"Ya sin esperanza todos sabiamos que {perdedor} iba a morir... tenian razon",
                        $"{perdedor} lanzó una roca y le cayó a el mismo",
                        $"{perdedor} saco a su Charizard nivel 100, pero esta mierda no es digimon, y este se lo comio vivo",
                        $"a {perdedor} lo atropelló un tren antes de llegar al lugar de encuentro",
                        $"{perdedor} se ahogo con sus propias palabras",
                        $"{perdedor} esta confundido, perdedor se ha golpeado a si mismo multiples veces hasta morir... pero murio de herpes"
                    };

                    c += combates[App.R.Next(combates.Length)];

                    App.bot.SendMessage(c);
                })},
                { "!Ask".ToLower(), new BotCommand((args, m)=>{
                    string[] Answers = new string[]
                    {
                                        //Sies 
                                        "Claro que si",
                                        "Obviamente si",
                                        "Estoy seguro de ello",
                                        "No lo negaria",
                                        "Tanto como el cielo es azul",
                                        //Noes
                                        "Nunca ni en un millon de años",
                                        "Evidentemente que no, humano",
                                        "Yo apostaria mis circuitos a que no",
                                        "Claro que no",
                                        "Estoy seguro que no"
                    };

                    if (args.Length >= 2)
                        App.bot.SendMessage(Answers[App.R.Next() % Answers.Length]);
                    else
                        App.bot.SendMessage("Tienes que preguntarme algo... Estos humanos Geez");

                })},
                { "!StreamTime".ToLower(), new BotCommand((args, m)=>{
                    TimeSpan interval = DateTime.Now - App.startTime;

                    App.bot.SendMessage($"Llevamos {interval.Minutes} minutos de Stream");
                    if (interval.Minutes < 30)
                        App.bot.SendMessage("Apenas estamos calentando :o");
                    if (interval.Minutes > 120)
                        App.bot.SendMessage($"Wow estos humanos si que tienen aguante...");
                })},
                { "!OpenPool".ToLower(), new BotCommand((args, m)=>{
                    Pool.pool = new Dictionary<string, int>();
                    Pool.voters = new List<string>();

                    foreach (var arg in args)
                    {
                        if (arg.Length > 1)
                        {
                            if (!Pool.pool.ContainsKey(arg))
                                Pool.pool.Add(arg, 0);
                        }
                    }
                    App.bot.SendMessage($"Vamos todo el mundo a votar una de las {Pool.pool.Count} opciones");
                }, true)},
                { "!Vote".ToLower(), new BotCommand((args, m)=>{

                    if (Pool.pool == null)
                    {
                        App.bot.SendMessage("¿Te fallan los circuitos humano? no hay ninguna pool abierta");
                        return;
                    }
                    if (Pool.voters.Contains(m["display-name"]))
                    {
                        App.bot.SendMessage("Ya has votado humano, no hagas trampa");
                        return;
                    }
                    if (args.Length < 2)
                    {
                        App.bot.SendMessage($"Tenias que votar una de las opciones humano!");
                        App.bot.SendMessage(Pool.Options);
                        return;
                    }
                    if (args.Length > 2)
                    {
                        App.bot.SendMessage("¡Por Turing! tenias que votar solo por una opcion");
                        return;
                    }
                    if (!Pool.pool.ContainsKey(args[1]))
                    {
                        App.bot.SendMessage("No humano, esa opcion no es valida Geez");
                        return;
                    }
                    else
                    {
                        Pool.pool[args[1]]++;
                        Pool.voters.Add(m["display-name"]);
                        App.bot.SendMessage($"¡Un voto mas para {args[1]} wii!");
                        return;
                    }
                })},
                { "!ClosePool".ToLower(), new BotCommand((args, m)=>{
                    if (Pool.pool == null)
                        return;
                    App.bot.SendMessage($"Las urnas quedaron asi {Pool.Results}");
                    Pool.pool = null;
                    Pool.voters = null;
                }, true)},
                { "!PickOneUser".ToLower(), new BotCommand((args, m)=>{
                    var picked = App.bot.Users[App.R.Next(App.bot.Users.Count)];
                    App.bot.SendMessage($"El usuario elegido es... {picked} Chan chan chan chaaan");
                })},
                { "!Reglas".ToLower(), new BotCommand("Usas la cabeza humanos" +
                    " *No lenguaje ofensivo" +
                    " *No spam *No cosas ilegales " +
                    " *English o Español " +
                    " *Si quieres que te lea mas rapido, mencioname <3")},
                { "!Comandos".ToLower(), new BotCommand((args, m)=>{
                    var comandos = "";
                    foreach (var item in App.bot.commands.Keys)
                    {
                        comandos += $"{item} <3 ";
                    }
                    App.bot.SendMessage(comandos);
                })}
            };

        public static void TriggerCommand(string line)
        {
            var command = line.Split(' ');
            if (commands.ContainsKey(command[0].ToLower()))
            {
                var c = command[0];
                command[0] = "";
                if (commands[c].typeCommand == TypeCommand.Action)
                {
                    commands[c].Invoke(command, new Dictionary<string, string>());
                }
                else
                {
                    App.bot.SendMessage(Commands.commands[c].Text);
                }
            }
        }
    }
}
