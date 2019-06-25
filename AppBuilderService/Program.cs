using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace AppBuilderService
{
  class Program
  {
    //static void Main(string[] args)
    //{

    //  try
    //  {
    //    AsyncService service = new AsyncService(24185);
    //    service.Run();

    //    Console.ReadLine();
    //  }
    //  catch (Exception ex)
    //  {
    //    Console.WriteLine(ex.Message);
    //    Console.ReadLine();
    //  }
    //}

    // https://www.winsocketdotnetworkprogramming.com/clientserversocketnetworkcommunication8b.html
    // https://www.winsocketdotnetworkprogramming.com/clientserversocketnetworkcommunication8b_1.html


    static void Main(string[] args)
    {
      //string remoteName = "localhost";
      //int remotePort = 3390;
      string remoteName = "eoyuktas-ESPRIMO-p900";
      int remotePort = 6200;

      Console.WriteLine();

      try
      {
        var clientSocket = GetClientSocket(remoteName, remotePort);
        // Make sure we have a valid socket before trying to use it
        if (clientSocket?.RemoteEndPoint != null)
        {
          try
          {
            var command = "build";
            Send(clientSocket, command);

            var response = Receive(clientSocket);
            //Console.WriteLine($"response : {response}");
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

        Console.ReadLine();
      }
      catch (SocketException err)
      {
        Console.WriteLine($"Client: Socket error occurred: {err.Message}");
      }




    }

    static Socket GetClientSocket(string remoteName, int remotePort)
    {
      Socket clientSocket = null;
      //IPEndPoint destination = null;

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

    static string Receive(Socket clientSocket)
    {
      byte[] recvBuffer = new byte[256];

      try
      {
        while (true)
        {
          Array.Clear(recvBuffer, 0, recvBuffer.Length);
          var rc = clientSocket.Receive(recvBuffer);
          //Console.WriteLine("Client: Receive() is OK...");
          //Console.WriteLine("Client: Read {0} bytes", rc);
         
          // Exit loop if server indicates shutdown
          if (rc == 0)
          {
            clientSocket.Close();
            Console.WriteLine("Client: Close() is OK...");
            break;
          }
          var response = Encoding.UTF8.GetString(recvBuffer.Where(x => x != '\0').ToArray());
          Console.WriteLine($"response : {response}");
        }
      }
      catch (SocketException err)
      {
        Console.WriteLine("Client: Error occurred while sending or receiving data.");
        Console.WriteLine("   Error: {0}", err.Message);
      }

      return Encoding.UTF8.GetString(recvBuffer.Where( x => x!= '\0').ToArray());
    }

    static void Send(Socket clientSocket, string request)
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





