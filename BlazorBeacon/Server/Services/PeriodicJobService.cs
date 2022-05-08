using BlazorBeacon.Server.Models;

namespace BlazorBeacon.Server.Services
{
    public class PeriodicJobService : IHostedService, IDisposable
    {
        private Timer _timer = null!;

        private readonly ICacheService _cacheService;
        public PeriodicJobService(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }
        public void Dispose()
        {
            _timer?.Dispose();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await MqttBrokerService.Start();
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(15));
            //return Task.CompletedTask;
        }

        private void DoWork(object? state)
        {
            SaveToCache(MqttBrokerService.Gateway);            

            //Task.Run(async () => await _mqttClientService.GetGatewayDataByTopic("gp1/202"));
            //Task.Run(async () => await _mqttClientService.GetGatewayDataByTopic("gp1/250"));
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await MqttBrokerService.Stop();
            _timer?.Change(Timeout.Infinite, 0);
            //return Task.CompletedTask;
        }

        private void SaveToCache(Gateway gateway)
        {
            _cacheService.CachedBeacons.Clear();
            var timeStamp = DateTimeOffset.Now;
            while (_cacheService.CachedBeacons.Count < 6)
            {
                if (gateway is not null && gateway.Beacons.Any())
                {
                    foreach (var beacon in gateway.Beacons)
                    {
                        var distance = CalculateAccuracy(beacon.TxPower, beacon.Rssi);
                        if (_cacheService.CachedBeacons is not null)
                        {
                            _cacheService.CachedBeacons.Add(new CachedBeacon { TimeStamp = timeStamp, Mac = beacon.Mac, Distance = distance, GwMac = gateway.Mac, GwTopic = gateway.Topic });
                        }
                    }
                }
            }
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
