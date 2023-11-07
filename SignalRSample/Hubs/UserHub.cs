using Microsoft.AspNetCore.SignalR;

namespace SignalRSample.Hubs
{
    //ovaj Hub nam služi za brojanje ukupnog broja općenitih i trenutnih pregleda
    public class UserHub : Hub
    {
        public static int TotalViews { get; set; } = 0;
        public static int TotalUsers { get; set; } = 0;

        //ovo su built inane metode signalR-a pa ih zato overrideamo, pozivaju se automatski kada
        //se spojimo i odspojimo na hub
        public override Task OnConnectedAsync()
        {
            TotalUsers++;
            Clients.All.SendAsync("updateTotalUsers", TotalUsers).GetAwaiter().GetResult();
            //Clients.Caller.SendAsync šalje samo onom tko je pozvao metodu sa client sidea A-> A, B,C,D,E
            //Clients.Other.SendAsync šalje svima osim onom tko je pozvao metodu sa client sidea A-> B,C,D,E
            //Clients.Client("Connection Id - A").SendAsync šalje specifičnom preko connection id-ja B->A
            //Clients.Client("Connection Id - A, Connection Id - C") B-> A,C
            //Clients.AllExcept("Connection Id - A, Connection Id - C") B-> svima osim A i C
            //što ako npr. user sa specificnim mailom ima vise otvorenih prozora ->
            //Clients.User("mislav@gmail.com").SendAsync
            //Clients.Users("mislav@gmail.com",marko@gmail.com).SendAsync
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            TotalUsers--;
            Clients.All.SendAsync("updateTotalUsers", TotalUsers).GetAwaiter().GetResult();
            return base.OnDisconnectedAsync(exception);
        }

        //on je tu dodao name da bi pokazao nesto svoje nebitno, nemoj da te to buni!!!
        public async Task<string> NewWindowLoaded(string name)
        {
            TotalViews++;
            //send update to all clients that total views have been updated
            /*metoda updateTotalUsers se nalazi u Client sideu,
            server kaže kada je novi window loadan na hubu želim updateat totalViews i pozvat tu metodu na
            client sideu, ali moramo mu i proslijedit TotalViews (logično) */
            await Clients.All.SendAsync("updateTotalViews", TotalViews);
            return $"total views from {name} - {TotalViews}";
        }

    }
}
