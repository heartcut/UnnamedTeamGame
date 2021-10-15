using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamGameV5.Server.Data
{
    public class DataManager
    {
        
        public int LobbyNumber { get; set; }

        public int PlayersInLobby { get; set; }

        public int P1Game { get; set; }
        public int P1GameVar1 { get; set; }
        public int P1GameVar2 { get; set; }
        public int P1GameVar3 { get; set; }
        public int P1GameVar4 { get; set; }
        public int P2Game { get; set; }
        public int P2GameVar1 { get; set; }
        public int P2GameVar2 { get; set; }
        public int P2GameVar3 { get; set; }
        public int P2GameVar4 { get; set; }
        public int P3Game { get; set; }
        public int P3GameVar1 { get; set; }
        public int P3GameVar2 { get; set; }
        public int P3GameVar3 { get; set; }
        public int P3GameVar4 { get; set; }
        public int P4Game { get; set; }
        public int P4GameVar1 { get; set; }
        public int P4GameVar2 { get; set; }
        public int P4GameVar3 { get; set; }
        public int P4GameVar4 { get; set; }

        public double P1Xcords { get; set; }
        public double P1Ycords { get; set; }
        public double P2Xcords { get; set; }
        public double P2Ycords { get; set; }
        public double P3Xcords { get; set; }
        public double P3Ycords { get; set; }
        public double P4Xcords { get; set; }
        public double P4Ycords { get; set; }

        public int P1Health { get; set; }
        public int P2Health { get; set; }
        public int P3Health { get; set; }
        public int P4Health { get; set; }

        public int P1ingame { get; set; }
        public int P2ingame { get; set; }
        public int P3ingame { get; set; }
        public int P4ingame { get; set; }

        public int GameStarted { get; set; }

        public bool P1Present { get; set; }
        public bool P2Present { get; set; }
        public bool P3Present { get; set; }
        public bool P4Present { get; set; }

        public int P1HealthChange { get; set; }
        public int P2HealthChange { get; set; }
        public int P3HealthChange { get; set; }
        public int P4HealthChange { get; set; }

        public int GameTimeSeconds { get; set; }
        public string StartTime { get; set; }
    }
}
