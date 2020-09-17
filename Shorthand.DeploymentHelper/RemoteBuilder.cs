using AppBuilder.DTO;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Shorthand
{
  public class RemoteBuilder
  {
    public string RemoteHost { get; set; }
    public int RemotePort { get; set; }


    private Action<string> _textLogger;

    private Action<MessageWrapper> _messageLogger;

    public int BufferSize { get; set; }

    public RemoteBuilder()
    {
      this.BufferSize = 2048;
    }

    public RemoteBuilder(string remoteHost, int remotePort) : this()
    {
      this.RemoteHost = remoteHost;
      this.RemotePort = remotePort;
    }

    public RemoteBuilder(string remoteHost, int remotePort, Action<string> logger = null) : this(remoteHost, remotePort)
    {
      _textLogger = logger ?? new Action<string>(x => Console.WriteLine(x));
    }

    public RemoteBuilder(string remoteHost, int remotePort, Action<MessageWrapper> logger = null) : this(remoteHost, remotePort)
    {
      _messageLogger = logger ?? new Action<MessageWrapper>(x => Console.WriteLine(x.Message));
      _textLogger = new Action<string>(x => Console.WriteLine(x));
    }

    public void Build(params string[] args)
    {
      var trailer = args.Length > 0 ? string.Join(" ", args) : "";
      var command = "build " + trailer;
      this.RemoteExec2(command);
    }

    public async Task BuildAsync(params string[] args)
    {
      var trailer = args.Length > 0 ? string.Join(" ", args) : "";
      var command = "build " + trailer;
      await this.RemoteExecAsync(command).ConfigureAwait(false);
    }

    public void Ping()
    {
      this.RemoteExec2("ping");
    }

    public void Brew()
    {
      this.RemoteExec("brew");
    }


    private void RemoteExec2(string command)
    {
      var client = new TcpClient(this.RemoteHost, this.RemotePort);
      var sr = new StreamReader(client.GetStream());

      var stream = client.GetStream();
      var sw = new StreamWriter(stream);

      try
      {
        this.Send(sw, command);


        //var data = "";
        //while (!sr.EndOfStream)
        //{
        //  data += sr.ReadLine();          
        //}
        var response = this.Deserialize(client.GetStream());

        _messageLogger?.Invoke(response);

      }
      catch (Exception ex)
      {
        var message = $"received : {ex.AggregateExceptionMessages()}";
        _textLogger?.Invoke(message);
      }

      //sr.Close();
      //sw.Close();
      //client.Close();

    }


    //private string Receive2(TcpClient client)
    //{
    //  byte[] recvBuffer = new byte[this.BufferSize];
    //  var response = string.Empty;

    //  try
    //  {
    //    while (clientSocket.Available > 0)
    //    {
    //      Array.Clear(recvBuffer, 0, recvBuffer.Length);
    //      var rc = clientSocket.Receive(recvBuffer, SocketFlags.Partial);

    //      response += Encoding.UTF8.GetString(recvBuffer.Where(x => x != '\0').ToArray());
    //    }
    //  }
    //  catch (SocketException err)
    //  {
    //    _textLogger?.Invoke("Client: Error occurred while sending or receiving data.");
    //    _textLogger?.Invoke($"Error: {err.AggregateExceptionMessages()}");
    //  }

    //  clientSocket.Close();
    //  return response;
    //}



    private void RemoteExec(string command)
    {
      try
      {
        var clientSocket = this.GetClientSocket(this.RemoteHost, this.RemotePort);
        if (clientSocket?.RemoteEndPoint != null)
        {
          try
          {
            this.Send(clientSocket, command);
            var response = this.Receive(clientSocket);

            _textLogger?.Invoke(response);
          }
          catch (SocketException err)
          {
            Console.WriteLine("Client: Error occurred while sending or receiving data.");
            _textLogger?.Invoke($"Error: {err.AggregateExceptionMessages()}");
          }
        }
        else
        {
          Console.WriteLine("Client: Unable to establish connection to server!");
        }
      }
      catch (SocketException err)
      {
        Console.WriteLine($"Client: Socket error occurred: {err.AggregateExceptionMessages()}");
      }
    }

    private async Task RemoteExecAsync(string command)
    {
      try
      {
        var clientSocket = await GetClientSocketAsync(this.RemoteHost, this.RemotePort);
        if (clientSocket?.RemoteEndPoint != null)
        {
          try
          {
            Send(clientSocket, command);
            var response = Receive(clientSocket);
          }
          catch (SocketException err)
          {
            Console.WriteLine("Client: Error occurred while sending or receiving data.");
            _textLogger?.Invoke($"Error: {err.AggregateExceptionMessages()}");
          }
        }
        else
        {
          Console.WriteLine("Client: Unable to establish connection to server!");
        }
      }
      catch (SocketException err)
      {
        Console.WriteLine($"Client: Socket error occurred: {err.AggregateExceptionMessages()}");
      }
    }

    private Socket GetClientSocket(string remoteName, int remotePort)
    {
      Socket clientSocket = null;
      var resolvedHost = Dns.GetHostEntry(remoteName)
                            .AddressList
                            .Where(a => a.AddressFamily == AddressFamily.InterNetwork);

      foreach (IPAddress addr in resolvedHost)
      {
        try
        {
          clientSocket = new Socket(addr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
          var destination = new IPEndPoint(addr, remotePort);
          clientSocket.Connect(destination);
          break;
        }
        catch (SocketException ex)
        {
          clientSocket.Close();
          _textLogger($"Client returned : {ex.AggregateExceptionMessages()}");
          clientSocket = null;
          continue;
        }
      }
      return clientSocket;
    }

    private async Task<Socket> GetClientSocketAsync(string remoteName, int remotePort)
    {
      Socket clientSocket = null;
      var resolvedHost = await Dns.GetHostEntryAsync(remoteName)
        .ConfigureAwait(false);

      foreach (IPAddress addr in resolvedHost.AddressList.Where(a => a.AddressFamily == AddressFamily.InterNetwork))
      {
        try
        {
          clientSocket = new Socket(addr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
          var destination = new IPEndPoint(addr, remotePort);
          await clientSocket.ConnectAsync(destination)
            .ConfigureAwait(false);

          break;
        }
        catch (SocketException ex)
        {
          clientSocket.Close();
          _textLogger($"Client returned : {ex.AggregateExceptionMessages()}");
          clientSocket = null;
          continue;
        }
      }
      return clientSocket;
    }



    private string Receive(Socket clientSocket)
    {
      byte[] recvBuffer = new byte[this.BufferSize];
      var response = string.Empty;

      try
      {
        while (clientSocket.Available > 0)
        {
          Array.Clear(recvBuffer, 0, recvBuffer.Length);
          var rc = clientSocket.Receive(recvBuffer, SocketFlags.Partial);

          response += Encoding.UTF8.GetString(recvBuffer.Where(x => x != '\0').ToArray());
        }
      }
      catch (SocketException err)
      {
        _textLogger?.Invoke("Client: Error occurred while sending or receiving data.");
        _textLogger?.Invoke($"Error: {err.AggregateExceptionMessages()}");
      }

      clientSocket.Close();
      return response;
    }



    private void Send(TcpClient client, string command)
    {
      //byte[] sendBuffer = Encoding.ASCII.GetBytes(request);
      var stream = client.GetStream();
      using (StreamWriter sw = new StreamWriter(stream))
      {
        sw.WriteLine(command);
        sw.Flush();
      }

      Console.WriteLine("Client: send() is OK...");
    }

    private void Send(StreamWriter sw, string command)
    {
      //byte[] sendBuffer = Encoding.ASCII.GetBytes(request);
      sw.WriteLine(command);
      sw.Flush();

      Console.WriteLine("Client: send() is OK...");
    }

    private void Send(Socket socket, string request)
    {
      byte[] sendBuffer = Encoding.ASCII.GetBytes(request);
      var rc = socket.Send(sendBuffer);
      Console.WriteLine("Client: send() is OK...");
      socket.Shutdown(SocketShutdown.Send);
    }


    private MessageWrapper Deserialize(NetworkStream stream)
    {
      var bf = new BinaryFormatter();
      return (MessageWrapper) bf.Deserialize(stream);
    }


  }




}
