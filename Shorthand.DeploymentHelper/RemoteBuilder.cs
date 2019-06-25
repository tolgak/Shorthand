using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Shorthand
{
  public class RemoteBuilder
  {
    public string RemoteHost { get; set; }
    public int RemotePort { get; set; }


    private Action<string> _logger;

    public int BufferSize { get; set; }

    public RemoteBuilder()
    {
      this.BufferSize = 256;
    }

    public RemoteBuilder(string remoteHost, int remotePort) : this()
    {
      this.RemoteHost = remoteHost;
      this.RemotePort = remotePort;
    }

    public RemoteBuilder(string remoteHost, int remotePort, Action<string> logger = null) : this(remoteHost, remotePort)
    {
      _logger = logger ?? new Action<string> (x => Console.WriteLine(x) );
    }

    public void Build()
    {
      this.RemoteExec("build");
    }

    public void Ping()
    {
      this.RemoteExec("ping");
    }

    private void RemoteExec(string command)
    {
      try
      {
        var clientSocket = GetClientSocket(this.RemoteHost, this.RemotePort);
        // Make sure we have a valid socket before trying to use it
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
            Console.WriteLine($"Error: {err.Message}");
          }
        }
        else
        {
          Console.WriteLine("Client: Unable to establish connection to server!");
        }
      }
      catch (SocketException err)
      {
        Console.WriteLine($"Client: Socket error occurred: {err.Message}");
      }
    }

    private Socket GetClientSocket(string remoteName, int remotePort)
    {
      Socket clientSocket = null;

      // Try to resolve the remote host name or address
      var resolvedHost = Dns.GetHostEntry(remoteName);
      Console.WriteLine("Client: GetHostEntry() is OK...");

      // Try each address returned
      foreach (IPAddress addr in resolvedHost.AddressList)
      {
        // Create a socket corresponding to the address family of the resolved address
        clientSocket = new Socket(addr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        Console.WriteLine("Client: Socket() is OK...");
        try
        {
          // Create the endpoint that describes the destination
          var destination = new IPEndPoint(addr, remotePort);
          Console.WriteLine("Client: IPEndPoint() is OK. IP Address: {0}, server port: {1}", addr, remotePort);
          Console.WriteLine("Client: Attempting connection to: {0}", destination.ToString());

          clientSocket.Connect(destination);
          Console.WriteLine("Client: Connect() is OK...");
          break;
        }
        catch (SocketException)
        {
          // Connect failed, so close the socket and try the next address
          clientSocket.Close();
          Console.WriteLine("Client: Close() is OK...");
          clientSocket = null;
          continue;
        }
      }

      return clientSocket;
    }

    private string Receive(Socket clientSocket)
    {
      byte[] recvBuffer = new byte[this.BufferSize];

      try
      {
        while (true)
        {
          Array.Clear(recvBuffer, 0, recvBuffer.Length);
          var rc = clientSocket.Receive(recvBuffer);

          // Exit loop if server indicates shutdown
          if (rc == 0)
          {
            clientSocket.Close();
            Console.WriteLine("Client: Close() is OK...");
            break;
          }
          var response = Encoding.UTF8.GetString(recvBuffer.Where(x => x != '\0').ToArray());
//          Console.WriteLine($"response : {response}");
          _logger?.Invoke(response);
        }
      }
      catch (SocketException err)
      {
        Console.WriteLine("Client: Error occurred while sending or receiving data.");
        Console.WriteLine("   Error: {0}", err.Message);
      }

      return Encoding.UTF8.GetString(recvBuffer.Where(x => x != '\0').ToArray());
    }

    private void Send(Socket clientSocket, string request)
    {
      byte[] sendBuffer = Encoding.ASCII.GetBytes(request);

      var rc = clientSocket.Send(sendBuffer);
      Console.WriteLine("Client: send() is OK...TCP...");
      Console.WriteLine("Client: Sent request of {0} bytes", rc);

      clientSocket.Shutdown(SocketShutdown.Send);
      Console.WriteLine("Client: Shutdown() is OK...");
    }


  }




}
