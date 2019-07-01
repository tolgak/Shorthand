using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace Shorthand.DataScraper.WebDataProvider
{
  public static class FileDownload
  {
    public static int DownloadFile(string remoteFilename, string localFilename)
    {
      return DownloadFile(null, remoteFilename, localFilename);
    }

    public static int DownloadFile(ICookieManager cm, string remoteFilename, string localFilename)
    {
      // Function will return the number of bytes processed to the caller. Initialize to 0 here.
      int bytesProcessed = 0;

      // Assign values to these objects here so that they can
      // be referenced in the finally block

      // Use a try/catch/finally block as both the WebRequest and Stream classes throw exceptions upon 
      // Create a request for the specified remote file name
      WebRequest request = WebRequest.Create(remoteFilename);
      if (request == null)
        return 0;

      if(cm != null)
        cm.PublishCookies((HttpWebRequest)request);

      // Send the request to the server and retrieve the WebResponse object
      using (WebResponse response = request.GetResponse())
      {
        // Once the WebResponse object has been retrieved, get the stream object associated with the response's data
        using (Stream remoteStream = response.GetResponseStream())
        {
          // Create the local file
          using (Stream localStream = File.Create(localFilename))
          {
            // Allocate a 1k buffer
            byte[] buffer = new byte[1024];
            int bytesRead;

            // Simple do/while loop to read from stream until no bytes are returned
            do
            {
              // Read data (up to 1k) from the stream
              bytesRead = remoteStream.Read(buffer, 0, buffer.Length);

              // Write the data to the local file
              localStream.Write(buffer, 0, bytesRead);

              // Increment total bytes processed
              bytesProcessed += bytesRead;
            } while (bytesRead > 0);

          }
        }
      }
      

      // Return total bytes processed to caller.
      return bytesProcessed;
    }
  }
}
