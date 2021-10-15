using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamGameV5.Client
{
    public class MyValues
    {
        public int GameStarted { get; set; }
        public int PlayerIAm { get; set; }
        public int MyLobby { get; set; }
        public double P1Xcords { get; set; }
        public double P1Ycords { get; set; }
        public double P2Xcords { get; set; }
        public double P2Ycords { get; set; }
        public double P3Xcords { get; set; }
        public double P3Ycords { get; set; }
        public double P4Xcords { get; set; }
        public double P4Ycords { get; set; }
        
        public string ponezone { get; set; }
        public string ptwozone { get; set; }
        public string pthreezone { get; set; }
        public string pfourzone { get; set; }
        public int MyCurrentGame = 1;
        public int[] MyGameVars;
        public string MyCursorStyle { get; set; }
        public double mycursx { get; set; }
        public double mycursy { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public void MouseMoved(MouseEventArgs e)
        {
            //this sets curs to the coords around the center bascially
            mycursx = e.ClientX - (Width / 2);
            mycursy = e.ClientY - (Height / 2);
        }
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
        public int P1ingame { get; set; }
        public int P2ingame { get; set; }
        public int P3ingame { get; set; }
        public int P4ingame { get; set; }
        public int P1Health { get; set; }
        public int P2Health { get; set; }
        public int P3Health { get; set; }
        public int P4Health { get; set; }
        public string StartTime { get; set; }
        public bool P1Present { get; set; }
        public bool P2Present { get; set; }
        public bool P3Present { get; set; }
        public bool P4Present { get; set; }
    }
}
