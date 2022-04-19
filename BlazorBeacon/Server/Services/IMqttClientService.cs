namespace BlazorBeacon.Server.Services
{
    public interface IMqttClientService
    {
        Task GetData(string topic);
    }
}
