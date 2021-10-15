using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamGameV5.Client.Components.Game.Minigames
{
    public static class MinigameGeneration
    {
        //this will take in what game it is and return an array of the proper variables for the game
        //will return in the form [var1,var2,var3,var4] depending on the game
        public static int[] GenerateVariables(int whatgame)
        {
            Random rndm = new Random();
            if (whatgame==0)
            {
                //sixninegame
                //just needs to generate 2 random numbers between 0-31 which will be where the 6s are
                int first = rndm.Next(0, 32);
                int second = rndm.Next(0, 32);
                while (first == second)
                {
                    second = rndm.Next(0, 32);
                }
                return new[] { first, second, 0, 0 };
            }
            if (whatgame == 1)
            {
                //make a squre
                //only returns 1-3 so no square starts not needing to be rotated
                return new[] { rndm.Next(1, 4), rndm.Next(1, 4), rndm.Next(1, 4), rndm.Next(1, 4) };
            }
            if (whatgame == 2)
            {
                //memorizecolornumber
                //[colors,numbers,questoin1or2,answer]
                return new[] { rndm.Next(1, 10), rndm.Next(1, 10), rndm.Next(0, 2), rndm.Next(0, 3) };
            }
            if (whatgame == 3)
            {
                //memorizeshapenumber
                //[colors,shapes,questoin1or2,answer]
                return new[] { rndm.Next(1, 10), rndm.Next(1, 10), rndm.Next(0, 2), rndm.Next(0, 4) };
            }
            if (whatgame == 4)
            {
                //matchcolorword
                return new[] { rndm.Next(0, 4), rndm.Next(0, 4), rndm.Next(0, 4),0 };
            }
            //else if (whatgame == 1)
            else
            {
                return new[] { rndm.Next(0, 32), rndm.Next(0, 32), rndm.Next(0, 32), rndm.Next(0, 32) };
            }
        }

        public static async Task PlayerFinishedGame(int player, bool won, int lobby, HubConnection hubConnection)
        {
            //need to start new game here
            await hubConnection.SendAsync("UpdateInGame", lobby, player,0);
            //below is to change player health
            if (won)
            {
                switch (player)
                {
                    case 1: await hubConnection.SendAsync("HealthChanged", lobby, 2, 3, true); break;
                    case 2: await hubConnection.SendAsync("HealthChanged", lobby, 4, 3, true); break;
                    case 3: await hubConnection.SendAsync("HealthChanged", lobby, 1, 3, true); break;
                    case 4: await hubConnection.SendAsync("HealthChanged", lobby, 3, 3, true); break;
                    default: break;
                }
                await Task.Delay(600);
            }
            else { }
            await MinigameGeneration.GenerateNewGame(player, lobby,hubConnection);
            //await Task.Delay(2000);
            await hubConnection.SendAsync("UpdateInGame", lobby, player, 1);
        }
        public static async Task GenerateNewGame(int player, int lobby,HubConnection hubConnection)
        {
            Random rndm = new Random();
            int whatgame = rndm.Next(0, 6);
            int[] tempvars = new int[4];
            tempvars = GenerateVariables(whatgame);
            await hubConnection.SendAsync("UpdateGameVars", lobby, player, tempvars[0], tempvars[1], tempvars[2], tempvars[3],whatgame);
        }
    }
}
