using System;
using System.Diagnostics;
using PusherClient;
using Xamarin.Forms;

namespace testPusher
{
    public partial class MainPage : ContentPage
    {
        Pusher pusher;

        //CONFIGURATION
        string applicationKey = "APPLICATION_KEY";
        string cluster = "CLUSTER eg. eu, us";
        string channelName = "CHANNEL_NAME";
        string eventName = "EVENT_NAME";

        public MainPage()
        {
            InitializeComponent();

            Setup();
        }

        public async void Setup()
        {
            pusher = new Pusher(applicationKey, new PusherOptions()
            {
                Cluster = cluster,
                Encrypted = true
            });

            pusher.ConnectionStateChanged += Pusher_ConnectionStateChanged;
            pusher.Error += Pusher_Error;
            await pusher.ConnectAsync();

            var channel = await pusher.SubscribeAsync(channelName);
            channel.Bind(eventName, (dynamic data) =>
            {
                Console.WriteLine(data);
            });

        }

        private void Pusher_Error(object sender, PusherException error)
        {
            Console.WriteLine(error);
        }

        private void Pusher_ConnectionStateChanged(object sender, ConnectionState state)
        {
            Console.WriteLine(state);
        }
    }
}
