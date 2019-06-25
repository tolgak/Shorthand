using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace DemoClient
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
    }

    private async void button1_Click(object sender, EventArgs e)
    {
      try
      {
        string server = "127.0.0.1";
        int port = 3390;

        //string server = "10.20.10.79";
        //int port = 6200;
        string method = "minimum"; 
        string data = "6 17 9 5"; 

        Task<string> tsResponse = SendRequest(server, port, method, data);
        listBox1.Items.Add("Sent request, waiting for response");
        await tsResponse;

        //double dResponse = double.Parse(tsResponse.Result);
        listBox1.Items.Add("Received response: " + tsResponse.Result.ToString());
      }
      catch (Exception ex)
      {
        listBox1.Items.Add(ex.Message);
      }
    }

    private static async Task<string> SendRequest(string server, int port, string method, string data)
    {
      try
      {
        IPAddress ipAddress = null;
        IPHostEntry ipHostInfo = Dns.GetHostEntry(server);
        for (int i = 0; i < ipHostInfo.AddressList.Length; ++i)
        {
          if (ipHostInfo.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
          {
            ipAddress = ipHostInfo.AddressList[i];
            break;
          }
        }
        if (ipAddress == null)
          throw new Exception("No IPv4 address for server");

        ipAddress = IPAddress.Loopback;
        TcpClient client = new TcpClient();
        await client.ConnectAsync(ipAddress, port);
        NetworkStream networkStream = client.GetStream();
        StreamWriter writer = new StreamWriter(networkStream);
        StreamReader reader = new StreamReader(networkStream);

        writer.AutoFlush = true;
        string requestData = "build"; // 'End-of-request'
        await writer.WriteLineAsync(requestData);
        string response = await reader.ReadLineAsync();
        client.Close();
        return response;
      }
      catch (Exception ex)
      {
        return ex.Message;
      }
    }



    private void button2_Click(object sender, EventArgs e)
    {
      //Soket bağlantısını oluştur
      Socket istemciBaglantisi = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

      //Bağlantıyı gerçekleştir
      //istemciBaglantisi.Connect(IPAddress.Parse("10.20.10.79"), 6200);
      istemciBaglantisi.Connect(IPAddress.Loopback, 3390);

      //veri iletişimi için akış nesnelerini oluştur
      NetworkStream agAkisi = new NetworkStream(istemciBaglantisi);
      BinaryReader binaryOkuyucu = new BinaryReader(agAkisi);
      BinaryWriter binaryYazici = new BinaryWriter(agAkisi);

      //sunucuya bir string metin yolla
      var textToSend = textBox1.Text;
      binaryYazici.Write(textToSend);

      //Sunucudan bir string metin oku

      string alinanMetin = binaryOkuyucu.ReadString();

      //alınan metini ekranda göster

      Console.WriteLine(alinanMetin);

      //Bağlantıyı sonlandır

      istemciBaglantisi.Close();

      //Enter'a basılana kadar bekle

      Console.ReadLine();

    }
  }
}
