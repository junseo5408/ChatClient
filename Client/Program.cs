using Client.Commands;
using System;
using System.Net.Sockets;

namespace Client
{
    class Program
    {
        public static NetWorker Worker;

        static void Main(string[] args)
        {
            string name = "";
            while (true)
            {
                if (name.Length == 0)
                {
                    Console.Write("Имя: ");
                    name = Console.ReadLine();
                }
                else { UserInfo.Name = name; break; }
            }

            string IP="";
            while (true)
            {
                if (IP.Length == 0)
                {
                    Console.Write("IPAdress: ");
                    IP = Console.ReadLine();
                }
                else { UserInfo.Ip = IP; break; }
            }

            Worker = new NetWorker();
            //Worker.SocketOpen();
            Worker.Connect();

            // После успешного подключения к серверу
            // Постоянно можем отправлять сообщения на серверSystem.NullReferenceException: '개체 참조가 개체의 인스턴스로 설정되지 않았습니다.'
            //Connected Message
            //Command
            bool Repeat = true;
            while (Repeat)
            {
                string userInput = Console.ReadLine();
                var command = Commands.Commands.GetCommand(userInput);

                switch (command.Type)
                {
                    case TypeCommand.Send:
                        //Console.WriteLine($"Sending message: {command.FullValue}");
                        CommandWorker.Send(command.FullValue);
                        break;
                    case TypeCommand.SetIp:
                        CommandWorker.SetIp(command.FullValue);
                        break;
                    case TypeCommand.SetName:
                        if (command.Values.Length > 1)
                        {
                            Console.WriteLine("ERROR New name can not be with spaced");
                            break; ;
                        }
                        CommandWorker.SetName(command.FullValue);
                        break;
                    case TypeCommand.Connect:
                        CommandWorker.Connect();
                        break;
                    case TypeCommand.Disconnect:
                        CommandWorker.Disconnect();
                        break;
                    case TypeCommand.Help:
                        CommandWorker.Help();
                        break;
                    case TypeCommand.Status:
                        CommandWorker.Status();
                        break;
                    case TypeCommand.Exit:
                        CommandWorker.Exit();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
