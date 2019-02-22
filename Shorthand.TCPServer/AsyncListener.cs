using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shorthand.TCPServer
{
    public class AsyncListener
    {
        private bool _listening;
        public bool Listening => _listening;

        private Action<string> _logger;

        private int _listeningPort;

        public AsyncListener(int port)
        {
            _listeningPort = port;
        }

        public AsyncListener(int port, Action<string> logger) : this(port)
        {
            _logger = logger;
        }

 

        public Task Start(CancellationToken cancellationToken)
        {
            _logger($"Starting on port {_listeningPort}.");
            var listener = new TcpListener(IPAddress.Broadcast, _listeningPort);
            listener.Start();

            return Task.Run(async () =>
            {
                var clients = new List<Task>();
                try
                {
                    while (!cancellationToken.IsCancellationRequested)
                    {
                        var tcpClient = await listener.AcceptTcpClientAsync()
                                                      .ContinueWith(t => t.Result, cancellationToken);
                        clients = clients.Where(task => !task.IsCompleted).ToList();
                        clients.Add(HandleConnectionAsync(tcpClient, cancellationToken));
                    }
                }
                finally
                {
                    await Task.WhenAll(clients.ToArray());
                }
            }, cancellationToken);
        }

        private async Task HandleConnectionAsync(TcpClient tcpClient, CancellationToken cancellationToken)
        {
            string clientInfo = tcpClient.Client.RemoteEndPoint.ToString();
            _logger($"Connection request from {clientInfo}");

            try
            {
                using (var networkStream = tcpClient.GetStream())
                using (var reader = new StreamReader(networkStream))
                using (var writer = new StreamWriter(networkStream))
                {
                    writer.AutoFlush = true;
                    while (!cancellationToken.IsCancellationRequested && tcpClient.Connected)
                    {
                        var request = await reader.ReadLineAsync();
                        if (string.IsNullOrEmpty(request))                        
                            break;
                        
                        _logger(request);

                        var jenkins = new Jenkins();
                        var jobs = await jenkins.GetJobsAsync();

                        jobs.ForEach( x => _logger($"{x.Name} - {x.Color}") );

                        await writer.WriteLineAsync("Jenkins Report - " + jobs?.AsJson() ?? "Empty Response");
                    }
                }
            }
            catch (Exception exp)
            {
                _logger(exp.Message);
            }
            finally
            {
                _logger($"Closing the client connection - {clientInfo}");
                tcpClient.Close();
            }
        }


    }

}