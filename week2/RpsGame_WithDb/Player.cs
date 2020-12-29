using System;
using System.ComponentModel.DataAnnotations;

namespace RpsGame_WithDb
{
    public class Player
    {
        public Player(string fname = "null", string lname = "null")
        {
            this.Fname = fname;
            this.Lname = lname;
        }
        // public string Id;
        // private Guid playerId = Guid.NewGuid();
        [Key]
        public Guid PlayerId{get;set;}= Guid.NewGuid();
        // {
        //     get
        //     {
        //         return playerId;
        //     }
        // }

        public int NumWins{get;set;}
        public int NumLosses{get;set;}
        // private int numWins;
        // private int numLosses;
        private string fName;
        public string Fname
        {
            get { return fName; }
            set
            {
                if (value is string && value.Length < 20 && value.Length > 0)
                {
                    fName = value;
                }
                else
                {
                    throw new Exception("The player name you sent is not valid");
                }
            }
        }

        private string lName;
        public string Lname
        {
            get { return lName; }
            set
            {
                if (value is string && value.Length < 20 && value.Length > 0)
                {
                    lName = value;
                }
                else
                {
                    throw new Exception("The player name you sent is no valid");
                }
            }
        }

        //below is methods
        /// <summary>
        /// This method inrements the Wins or the player
        /// </summary>
        public void AddWin()
        {
            NumWins++;
        }

        /// <summary>
        /// This methods increments the wins of the player by the passed integer amount.
        /// </summary>
        /// <param name="x"></param>
        public void AddWin(int x)
        {
            NumWins += x;
        }

        public void AddLoss()
        {
            NumLosses++;
        }

        public int[] GetWinLossRecord()
        {
            int[] winsAndLosses = new int[2]; // create an array to hole the num of wins and losses

            winsAndLosses[0] = NumWins; // put in the wins and losses
            winsAndLosses[1] = NumLosses;

            return winsAndLosses; // return the array.
        }





    }
}