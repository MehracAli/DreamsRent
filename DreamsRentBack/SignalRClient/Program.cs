using Microsoft.AspNetCore.SignalR.Client;
using System;

namespace SignalRClient
{
    class Program
    {
        static HubConnection HubConnection;

        static async Task Main(string[] args)
        {
            HubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7260/chat")
                .Build();

            HubConnection.On<string>("send", message => Console.WriteLine($"Message from server: {message}"));

            await HubConnection.StartAsync();

            bool isExit = false;

            while (!isExit)
            {
                var message = Console.ReadLine();

                if (message != "exit")
                    await HubConnection.SendAsync("SendMessage", message);
                else
                    isExit = true;
            }

            Console.ReadLine();
        }
    }
}

