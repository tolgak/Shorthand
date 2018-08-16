using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shorthand.TCPServer
{
    public static class TcpServer
    {
        private const int SleepTimeout = 50;// miliseconds

        public static Task Run(int port, CancellationToken cancellation, Action<List<string>, TcpClient, CancellationToken> onLineReceived)
        {
            return Accept(port, cancellation, (client, token) => { var receivedLines = new List<string>();
                                                                    while (!cancellation.IsCancellationRequested && client.Connected)
                                                                    {
                                                                        var available = client.Available;
                                                                        if (available == 0)
                                                                        {
                                                                            Thread.Sleep(SleepTimeout);
                                                                            continue;
                                                                        }

                                                                        var buffer = new byte[available];
                                                                        client.GetStream().Read(buffer, 0, available);

                                                                        var newData = Encoding.UTF8.GetString(buffer);
                                                                        bool newLine;
                                                                        AppendLines(receivedLines, newData, out newLine);

                                                                        if (newLine)
                                                                          onLineReceived(receivedLines, client, cancellation);
                                                                    }

                                                                    client.Close();
                                                                });
        }

        private static Task Accept(int port, CancellationToken cancellation, Action<TcpClient, CancellationToken> onClientAccepted)
        {
            var childTasks = new List<Task>();
            var listener   = new TcpListener(IPAddress.Any, port);
            listener.Server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);
            listener.Start();

            return Task.Factory.StartNew(() => { while (!cancellation.IsCancellationRequested)
                                                 {
                                                     if (!listener.Pending())
                                                     {
                                                         Thread.Sleep(SleepTimeout);
                                                         continue;
                                                     }

                                                     var client = listener.AcceptTcpClient();

                                                     var childTask = new Task( () => onClientAccepted(client, cancellation) );
                                                     childTasks.Add(childTask);
                                                     childTask.ContinueWith(t => childTasks.Remove(t));
                                                     childTask.Start(); } }, cancellation)
                               .ContinueWith(t => { Task.WaitAll(childTasks.ToArray()); listener.Stop(); });
        }


        private static void AppendLines(List<string> lines, string newData, out bool newLine)
        {
            int i;
            int pos = 0;
            string line;
            newLine = false;

            for (i = pos; i < newData.Length; i++)
            {
                if (newData[i] == '\n')
                {
                    line = (i > 0 && newData[i - 1] == '\r') ?
                                    newData.Substring(pos, i - pos - 1) :
                                    newData.Substring(pos, i - pos);

                    if (lines.Count == 0)
                        lines.Add(line);
                    else
                    {
                        if (newLine)
                            lines.Add(line);
                        else
                            lines[lines.Count - 1] = lines[lines.Count - 1] + line;
                    }

                    newLine = true;
                    pos = i + 1;
                }
            }

            line = newData.Substring(pos);
            if (!string.IsNullOrEmpty(line))
                lines[lines.Count - 1] = lines[lines.Count - 1] + line;
        }
    }
}
