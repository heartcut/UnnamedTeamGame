using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamGameV5.Server.Data;

namespace TeamGameV5.Server.Hubs
{
    public class MainHub : Hub
    {
        private readonly List<DataManager> datalist;

        
        public MainHub(List<DataManager> datalist)
        {
            datalist.Add(new DataManager() { LobbyNumber = 1, PlayersInLobby = 3, P1Present = false, P2Present = false, P3Present = false, P4Present = false });
            datalist.Add(new DataManager() { LobbyNumber = 2, PlayersInLobby = 0, P1Present = false, P2Present = false, P3Present = false, P4Present = false });
            datalist.Add(new DataManager() { LobbyNumber = 3, PlayersInLobby = 0, P1Present = false, P2Present = false, P3Present = false, P4Present = false });
            datalist.Add(new DataManager() { LobbyNumber = 4, PlayersInLobby = 0, P1Present = false, P2Present = false, P3Present = false, P4Present = false });
            datalist.Add(new DataManager() { LobbyNumber = 5, PlayersInLobby = 0, P1Present = false, P2Present = false, P3Present = false, P4Present = false });
            datalist.Add(new DataManager() { LobbyNumber = 6, PlayersInLobby = 0, P1Present = false, P2Present = false, P3Present = false, P4Present = false });
            datalist.Add(new DataManager() { LobbyNumber = 7, PlayersInLobby = 0, P1Present = false, P2Present = false, P3Present = false, P4Present = false });
            datalist.Add(new DataManager() { LobbyNumber = 8, PlayersInLobby = 0, P1Present = false, P2Present = false, P3Present = false, P4Present = false });
            datalist.Add(new DataManager() { LobbyNumber = 9, PlayersInLobby = 0, P1Present = false, P2Present = false, P3Present = false, P4Present = false });

            this.datalist = datalist;
        }

        public async Task UpdateInGame(int lobby, int player, int ingame)
        {

            await Clients.Group(lobby.ToString()).SendAsync("NeedToUpdateInGame", player, ingame);
        }

        public async Task UpdateGameVars(int lobby,int player,int var1, int var2, int var3,int var4,int whatgame)
        {
            await Clients.Group(lobby.ToString()).SendAsync("NeedToUpdateGameVars", player, var1, var2,var3,var4,whatgame);

        }
        public async Task StartGame(int lobby)
        {
            //updateplayers to be in game
            await Clients.Group(lobby.ToString()).SendAsync("NeedToUpdateInGame", 1, 1);
            await Clients.Group(lobby.ToString()).SendAsync("NeedToUpdateInGame", 2, 1);
            await Clients.Group(lobby.ToString()).SendAsync("NeedToUpdateInGame", 3, 1);
            await Clients.Group(lobby.ToString()).SendAsync("NeedToUpdateInGame", 4, 1);
            //update everyones health to 6
            await Clients.Group(lobby.ToString()).SendAsync("PlayerHealthChanged", 1, 6,true);
            await Clients.Group(lobby.ToString()).SendAsync("PlayerHealthChanged", 2, 6,true);
            await Clients.Group(lobby.ToString()).SendAsync("PlayerHealthChanged", 3, 6,true);
            await Clients.Group(lobby.ToString()).SendAsync("PlayerHealthChanged", 4, 6,true);
            //finally change gamestarted
            await Clients.Group(lobby.ToString()).SendAsync("NeedToStartGame");
        }

        public async Task CloseGame(int lobby)
        {
            
            datalist[lobby - 1].P1Present = false;
            datalist[lobby - 1].P2Present = false;
            datalist[lobby - 1].P3Present = false;
            datalist[lobby - 1].P4Present = false;
            
            datalist[lobby - 1].PlayersInLobby=0;
            int[] test = (from i in datalist select i.PlayersInLobby).ToArray();
            await Clients.Group("0").SendAsync("UpdateLobbyPlayers", test);
            await Clients.Group(lobby.ToString()).SendAsync("SomeoneLeft");

        }
        public async Task HealthChanged(int lobby,int player,int healthchange,bool healthgained)
        {
            await Clients.Group(lobby.ToString()).SendAsync("PlayerHealthChanged",player,healthchange,healthgained);
        }

        public async Task JoinGameGroup(int lobby)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, lobby.ToString());

            if (!datalist[lobby - 1].P1Present)
            {
                datalist[lobby - 1].P1Present = true;
                await LobbyJoined(lobby);
                await Groups.AddToGroupAsync(Context.ConnectionId, lobby.ToString()+"/"+"1");
                await Clients.Group(lobby.ToString() + "/" + "1").SendAsync("GetMyPlayerNumber", 1);
                //set p1present and add to another group of string: lobby + / + playernumber
                //send message to new group that is just one person and tell them the player number
            }
            else if (!datalist[lobby - 1].P2Present)
            {
                datalist[lobby - 1].P2Present = true;
                await LobbyJoined(lobby);
                await Groups.AddToGroupAsync(Context.ConnectionId, lobby.ToString() + "/" + "2");
                await Clients.Group(lobby.ToString() + "/" + "2").SendAsync("GetMyPlayerNumber", 2);
            }
            else if (!datalist[lobby - 1].P3Present)
            {
                datalist[lobby - 1].P3Present = true;
                await LobbyJoined(lobby);
                await Groups.AddToGroupAsync(Context.ConnectionId, lobby.ToString() + "/" + "3");
                await Clients.Group(lobby.ToString() + "/" + "3").SendAsync("GetMyPlayerNumber", 3);
            }
            else if (!datalist[lobby - 1].P4Present)
            {
                datalist[lobby - 1].P4Present = true;
                await LobbyJoined(lobby);
                await Groups.AddToGroupAsync(Context.ConnectionId, lobby.ToString() + "/" + "4");
                await Clients.Group(lobby.ToString() + "/" + "4").SendAsync("GetMyPlayerNumber", 4);
            }
            else
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, "full");
                await Clients.Group("full").SendAsync("AmIFull");
                //lobby full send signal saying to go to full screen
            }

            //await Clients.Group("0").SendAsync("UpdateLobbyPlayers");

        }
        
        public async Task Moved(int player, int lobby, double x, double y)
        {
            //this is what we are sending out
            await Clients.GroupExcept(lobby.ToString(), Context.ConnectionId).SendAsync("AMouseMoved", player, x, y);

        }
        public async Task SetStartTime(int lobby, string starttime)
        {
            //this is what we are sending out
            await Clients.Group(lobby.ToString()).SendAsync("StartTimeSet", starttime);

        }
        //-----------------lobby stuff-----------------------------------
        public async Task JoinLobbyGroup()
        {
            //this is what we are sending out
            await Groups.AddToGroupAsync(Context.ConnectionId, "0");

            int[] test = (from i in datalist select i.PlayersInLobby).ToArray();

            await Clients.Group("0").SendAsync("UpdateLobbyPlayers", test);
            //await Clients.Group("0").SendAsync("UpdateLobbyPlayers");

        }

        public async Task LobbyJoined(int lobby)
        {
            //this is what we are sending out
            //await Groups.AddToGroupAsync(Context.ConnectionId, "0");
            datalist[lobby-1].PlayersInLobby++;
            int[] test = (from i in datalist select i.PlayersInLobby).ToArray();
            await Clients.Group("0").SendAsync("UpdateLobbyPlayers",test);

        }
        public async Task LeaveLobbyGroup()
        {
            //this is what we are sending out

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "0");
            //await Clients.Group("0").SendAsync("UpdateLobbyPlayers");

        }
    }
}
