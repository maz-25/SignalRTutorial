using Microsoft.AspNetCore.SignalR;

namespace SignalRSample.Hubs
{
    public class DeathlyHallowsHub : Hub
    {
        public Dictionary<string,int> GetRaceStatus() //metoda koja vraća status utrke
        {
            return SD.DealthyHallowRace;
        }
    }
}
