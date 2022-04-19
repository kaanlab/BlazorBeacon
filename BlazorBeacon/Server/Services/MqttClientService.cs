﻿using BlazorBeacon.Server.Models;
using MessagePack;
using MQTTnet;
using MQTTnet.Client.Options;
using MQTTnet.Client.Receiving;
using MQTTnet.Extensions.ManagedClient;
using System.Text.RegularExpressions;

namespace BlazorBeacon.Server.Services
{
    public class MqttClientService : IMqttClientService
    {
        private readonly ICacheService _cacheService;
        private Gateway gateway;

        public MqttClientService(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }
        public async Task GetData(string topic)
        {
            var options = TcpMqttClientOptions("mqtt.bconimg.com");
            var mqttClient = new MqttFactory().CreateManagedMqttClient();

            await mqttClient.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic(topic).Build());
            mqttClient.UseApplicationMessageReceivedHandler(new MqttApplicationMessageReceivedHandlerDelegate(e => MqttClient_ApplicationMessageReceived(e)));

            //mqttClient.UseConnectedHandler((arg) => Console.WriteLine("Establish connection " + arg.ConnectResult.ResultCode));

            await mqttClient.StartAsync(options);

            while (true)
            {
                if (gateway is not null && gateway.Beacons.Count > 0)
                {
                    foreach (var beacon in gateway.Beacons)
                    {
                        var distance = CalculateAccuracy(beacon.TxPower, beacon.Rssi);
                        if (_cacheService.CachedBeacons is not null)
                        {
                            _cacheService.CachedBeacons.Add(new CachedBeacon { TimeStamp = DateTimeOffset.UtcNow, Mac = beacon.Mac, Distance = distance.ToString("F"), GatewayMac = gateway.Mac });
                        }
                    }
                }
            }

            await mqttClient.StopAsync();
        }

        private static ManagedMqttClientOptions TcpMqttClientOptions(string url)
        {
            return new ManagedMqttClientOptionsBuilder()
                //.WithAutoReconnectDelay(TimeSpan.FromSeconds(10))
                .WithClientOptions(
                    new MqttClientOptionsBuilder()
                        .WithClientId("EMQX_")
                        .WithTcpServer(url, 1883)
                        //.WithCredentials("user", "pass")
                        .WithCleanSession()
                        .Build()
                )
                .Build();
        }

        private void MqttClient_ApplicationMessageReceived(MqttApplicationMessageReceivedEventArgs e)
        {
            var gw = MessagePackSerializer.Deserialize<MpGateway>(e.ApplicationMessage.Payload);
            var beacons = new List<Beacon>();
            foreach (var device in gw.devices)
            {
                beacons.Add(new Beacon
                {
                    Adv = device[0].ToString("X2"),
                    Mac = BitConverter.ToString(device.Skip(1).Take(6).ToArray()),
                    Rssi = int.Parse((device[7] - 256).ToString()),
                    Uuid = BitConverter.ToString(device.Skip(18).Take(16).ToArray()),
                    Major = BitConverter.ToString(device.Skip(34).Take(2).ToArray()),
                    Minor = BitConverter.ToString(device.Skip(36).Take(2).ToArray()),
                    TxPower = int.Parse((device[37] - 256).ToString())
                });
            }

            gateway = new Gateway
            {
                TimeStamp = DateTimeOffset.UtcNow,
                Topic = e.ApplicationMessage.Topic,
                Version = gw.v,
                Mac = string.Join("-", Regex.Split(gw.mac, @"(?<=\G.{" + 2 + "})(?!$)")),
                Ip = gw.ip,
                Beacons = beacons
            };

        }

        private static double CalculateAccuracy(int txPower, double rssi)
        {
            if (rssi == 0)
            {
                return -1.0; // if we cannot determine accuracy, return -1.
            }

            double ratio = rssi * 1.0 / txPower;
            if (ratio < 1.0)
            {
                return Math.Pow(ratio, 10);
            }
            else
            {
                double accuracy = (0.89976) * Math.Pow(ratio, 7.7095) + 0.111;
                return accuracy;
            }
        }
    }
}
