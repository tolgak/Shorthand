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
    public class AsyncEchoServer
    {
        private Action<string> _logger;

        private int _listeningPort;

        public AsyncEchoServer(int port)
        {
            _listeningPort = port;
        }

        public AsyncEchoServer(int port, Action<string> logger) : this(port)
        {
            _logger = logger;
        }

        public async void Start()
        {
            TcpListener listener = new TcpListener(IPAddress.Loopback, _listeningPort);
            listener.Start();
            _logger("Server is running");
            _logger($"Listening on port {_listeningPort}");

            while (true)
            {
                _logger("Waiting for connections...");
                try
                {
                    var tcpClient = await listener.AcceptTcpClientAsync();
                    HandleConnectionAsync(tcpClient);
                }
                catch (Exception e)
                {
                    _logger(e.ToString());
                }
            }
        }

        private async void HandleConnectionAsync(TcpClient tcpClient)
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
                    while (true)
                    {
                        var request = await reader.ReadLineAsync();
                        if (string.IsNullOrEmpty(request))                        
                            break;
                        
                        _logger(request);

                        var jenkins = new Jenkins();
                        var jobs = await jenkins.GetJobsAsync();

                        jobs.ForEach( x => _logger($"{x.Name} - {x.Color}") );

                        await writer.WriteLineAsync("FromServer - " + jobs?.AsJson() ?? "Empty Response");
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