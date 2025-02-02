﻿using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Options;
using MQTTnet.Client.Receiving;
using Newtonsoft.Json;
using SEMS_APP.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SEMS_APP.Interface
{
    public class MqttClientRepository
    {
        Dictionary<string, MqttTopicFilter> _topicFilter;
        public static IMqttClient client;
        public IMqttClient Create(string server, int? port, string userName, string password, List<string> topics, string clientID)
        {
            _topicFilter = new Dictionary<string, MqttTopicFilter>();

            foreach (var topic in topics)
            {
                MqttTopicFilter topicFilter = new MqttTopicFilter
                {
                    Topic = topic,
                    QualityOfServiceLevel = MQTTnet.Protocol.MqttQualityOfServiceLevel.AtMostOnce

                };

                _topicFilter.Add(topic, topicFilter);

            }

            Task.Run(() => MqttClientRunAsync(server, port, userName, password, clientID)).Wait();

            foreach (var topic in topics)
            {
                client.ApplicationMessageReceivedHandler = new SubscribeCallback(_topicFilter);

            }

            return client;
        }

        private async Task MqttClientRunAsync(string server, int? port, string userName, string password, string clientID)
        {
            try
            {
                var options = new MqttClientOptions
                {
                    ClientId = clientID,
                    CleanSession = true,
                    ChannelOptions = new MqttClientTcpOptions
                    {
                        Server = server,
                        Port = port
                    },
                    Credentials = new MqttClientCredentials
                    {
                        Username = userName,
                        Password = Encoding.UTF8.GetBytes(password)
                    }
                };

                var factory = new MqttFactory();

                client = factory.CreateMqttClient();

                client.ConnectedHandler = new MqttConnectedHandler(_topicFilter, client);
                client.DisconnectedHandler = new MqttDisconnectedHandler(options, client);

                try
                {
                    await client.ConnectAsync(options);
                }
                catch //(Exception ex)
                {

                }

            }
            catch //(Exception ex)
            {

            }
        }
        public async static void PublibMessage(string topic, string content)
        {
            try
            {
                var messge = new MqttApplicationMessage();
                messge.Topic = topic;
                messge.Payload = Encoding.UTF8.GetBytes(content);
                await client.PublishAsync(messge);
            }
            catch (Exception ex)
            {
                //await new MessageBox("Thông Báo", ex.Message).Show();
            }

        }
    }

    public class MqttDisconnectedHandler : IMqttClientDisconnectedHandler
    {
        private IMqttClient _client;
        private MqttClientOptions _options;

        public MqttDisconnectedHandler(MqttClientOptions options, IMqttClient client)
        {
            _options = options;
            _client = client;
        }

        public async Task HandleDisconnectedAsync(MqttClientDisconnectedEventArgs eventArgs)
        {
            //await new MessageBox("Thông Báo", "Mất kết nối đến server").Show();
            await _client.ReconnectAsync();
        }
    }

    public class MqttConnectedHandler : IMqttClientConnectedHandler
    {
        private IMqttClient _client;
        private Dictionary<string, MqttTopicFilter> _topicFilter;

        public MqttConnectedHandler(Dictionary<string, MqttTopicFilter> topicFilter, IMqttClient client)
        {
            _topicFilter = topicFilter;
            _client = client;
        }

        public async Task HandleConnectedAsync(MqttClientConnectedEventArgs eventArgs)
        {
            await _client.SubscribeAsync(_topicFilter.Values.ToArray());
        }
    }

    public class SubscribeCallback : IMqttApplicationMessageReceivedHandler
    {
        private readonly Dictionary<string, MqttTopicFilter> _sessionPayedTopic;

        public SubscribeCallback(Dictionary<string, MqttTopicFilter> sessionPayedTopic)
        {
            _sessionPayedTopic = sessionPayedTopic;
        }

        public Task HandleApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs e)
        {
            string message = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);          
            string[] topic = e.ApplicationMessage.Topic.Split('/');
            if (topic[0] == "REALTIME")
            {
                Inverter data = JsonConvert.DeserializeObject<Inverter>(message);
                if (data.NGAYGIO >= DateTime.Now.AddMilliseconds(-10))
                {
                    clsGen.analyze_inverter(data, clsVaribles._dtInverter);
                    MessagingCenter.Send<SubscribeCallback>(this, "MQTTRev");
                }
                else
                {
                    return Task.CompletedTask;
                }    
            }
            else if (topic[0] == "PVSTRING")
            {
                ObservableCollection <PVString> data = JsonConvert.DeserializeObject<ObservableCollection<PVString>>(message);
                clsGen.analyze_pvstring(data, clsVaribles._dtPVString);
                MessagingCenter.Send<SubscribeCallback>(this, "MQTTPvstring");
            }
            else if (e.ApplicationMessage.Topic == clsVaribles._topic.phanhoi)
            {
                clsVaribles._dtPhanHoiMqtt = JsonConvert.DeserializeObject<ObservableCollection<clsVaribles.CAP_NHAT_CONG_SUAT>>(message); ;
                MessagingCenter.Send<SubscribeCallback>(this, "MQTTPhanhoi");
            }
            return Task.CompletedTask;
        }
    }
}
