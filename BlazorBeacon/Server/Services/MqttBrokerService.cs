using BlazorBeacon.Server.Models;
using MessagePack;
using MQTTnet;
using MQTTnet.Server;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace BlazorBeacon.Server.Services
{
    public class MqttBrokerService
    {
        public static Gateway Gateway = null;
        private static IMqttServer mqttServer = null;

        public static async Task Start()
        {
            // Create the options for our MQTT Broker
            MqttServerOptionsBuilder options = new MqttServerOptionsBuilder()
                                                 // set endpoint to localhost
                                                 .WithDefaultEndpointBoundIPAddress(IPAddress.Parse("192.168.63.25"))
                                                 // port used will be 707
                                                 .WithDefaultEndpointPort(1883)
                                                 // handler for new connections
                                                 .WithConnectionValidator(OnNewConnection)
                                                 // handler for new messages
                                                 .WithApplicationMessageInterceptor(OnNewMessage);

            // creates a new mqtt server     
            mqttServer = new MqttFactory().CreateMqttServer();

            // start the server with options  
            await mqttServer.StartAsync(options.Build());
        }

        public static async Task Stop()
        {
            await mqttServer.StopAsync();
        }
        public static void OnNewConnection(MqttConnectionValidatorContext context)
        {
            //Log.Logger.Information(
            //        "New connection: ClientId = {clientId}, Endpoint = {endpoint}",
            //        context.ClientId,
            //        context.Endpoint);
        }

        public static void OnNewMessage(MqttApplicationMessageInterceptorContext context)
        {
            var gw = MessagePackSerializer.Deserialize<MpGateway>(context.ApplicationMessage.Payload);
            if (gw is not null && gw.devices.Any())
            {
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

                Gateway = new Gateway
                {
                    TimeStamp = DateTimeOffset.UtcNow,
                    Topic = context.ApplicationMessage.Topic,
                    Version = gw.v,
                    Mac = string.Join("-", Regex.Split(gw.mac, @"(?<=\G.{" + 2 + "})(?!$)")),
                    Ip = gw.ip,
                    Beacons = beacons
                };

            }
        }
    }
}
