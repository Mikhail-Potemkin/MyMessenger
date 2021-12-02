using System;
using Newtonsoft.Json;

namespace MyMessenger
{
    class Program
    {
        static void Main(string[] args)
        {
            Message msg = new Message("Mikhail", "Hello", DateTime.UtcNow);
            string output = JsonConvert.SerializeObject(msg);
            Console.WriteLine(output);
            Message deserializedMsg = JsonConvert.DeserializeObject<Message>(output);
            Console.WriteLine(deserializedMsg);
            //{"UserName":"Mikhail","MessageText":"Hello","TimeStamp":"2021-12-02T20:19:47.3175332Z"}
            //Mikhail < 02.12.2021 20:19:47 >: Hello
        }
    }
}
