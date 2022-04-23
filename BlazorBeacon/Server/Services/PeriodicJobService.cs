namespace BlazorBeacon.Server.Services
{
    public class PeriodicJobService : IHostedService, IDisposable
    {
        private Timer _timer = null!;
        private readonly IMqttClientService _mqttClientService;
        public PeriodicJobService(IMqttClientService mqttClientService)
        {
            _mqttClientService = mqttClientService;

        }
        public void Dispose()
        {
            _timer?.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(15));
            return Task.CompletedTask;
        }

        private void DoWork(object? state)
        {
            //var ar = new string[] { "FB-A7-DC-2C-E8-3E" };
            //Task.Run(async () => await _mqttClientService.GetGatewayDataByTopic("gp1/202"));
            Task.Run(async () => await _mqttClientService.GetGatewayDataByTopic("gp1/250"));
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
    }
}
