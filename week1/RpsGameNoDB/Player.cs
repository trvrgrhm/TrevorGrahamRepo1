using System;
using System.Collections.Generic;
namespace RpsGameNoDB
{
    public class Player
    {
        private Guid playerId = Guid.NewGuid();
        public Guid PlayerId
        {
            get { return playerId; }
        }

//TODO: check values when changing wins and losses
        public int Wins { get; set; }
        public int Losses { get; set; }

        private string fName;
        public string FName
        {
            get { return fName; }
            set { fName = value; }
        }private string lName;
        public string LName
        {
            get { return lName; }
            set { lName = value; }
        }
        // public List<Match> matches { get; set; }

        // public int getWins(){
        //     int numWins=0;
        //     return numWins;
        // }public int getLosses(){
        //     int numLosses=0;
        //     return numLosses;
        // }


    }
}