namespace BlazorBeacon.Server.Services
{
    public interface IMqttClientService
    {
        Task GetGatewayDataByTopic(string topic);
    }
}
