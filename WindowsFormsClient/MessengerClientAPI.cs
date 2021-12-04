﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyMessenger
{
    class MessengerClientAPI
    {
        private static readonly HttpClient client = new HttpClient();

        public void TestNewtonsoftJson()
        {
            //Тест JSon SerializeObject NewtonSoft
            Message msg = new Message("Mikhail", "Hello", DateTime.UtcNow);
            string output = JsonConvert.SerializeObject(msg);
            Console.WriteLine(output);
            Message deserializedMsg = JsonConvert.DeserializeObject<Message>(output);
            Console.WriteLine(deserializedMsg);
        }
        public Message GetMessage(int MessageId)
        {
            WebRequest request = WebRequest.Create("http://localhost:5000/api/Messenger/" + MessageId.ToString());
            request.Method = "Get";
            WebResponse response = request.GetResponse();
            string status = ((HttpWebResponse)response).StatusDescription;
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            reader.Close();
            dataStream.Close();
            response.Close();
            if ((status.ToLower() == "ok") && (responseFromServer != "Not Found"))
            {
                Message deserializedMsg = JsonConvert.DeserializeObject<Message>(responseFromServer);
                return deserializedMsg;
            }
            return null;
        }
        public async Task<Message> GetMessageHTTPAsync(int MessageID)
        {
            var responseString = await client.GetStringAsync("http://localhost:5000/api/Messenger/");
            if (responseString != null)
            {
                Message deserializedMsg = JsonConvert.DeserializeObject<Message>(responseString);
                return deserializedMsg;
            }
            return null;
        }
        public bool SendMessage(Message msg)
        {
            WebRequest request = WebRequest.Create("http://localhost:5000/api/Messenger/");
            request.Method = "POST";
            //Message msg = new Message("Mikhail", "Hello", DateTime.Now);
            string postData = JsonConvert.SerializeObject(msg);
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse response = request.GetResponse();
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            dataStream.Close();
            response.Close();
            return true;
        }
    }
}
