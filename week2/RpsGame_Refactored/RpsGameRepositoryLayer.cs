using System;
using System.Collections.Generic;
using System.Linq;

namespace RpsGame_Refactored
{
    public class RpsGameRepositoryLayer
    {
        List<Player> players = new List<Player>();
        List<Match> matches = new List<Match>();
        List<Round> rounds = new List<Round>();

        Random randomNumber = new Random((int)DateTime.Now.Millisecond); // create a random number object

        /// <summary>
        /// Creates a player after verifying that the player doesn't already exist.
        /// </summary>
        /// <returns>Player player</returns>
        public Player CreatePlayer(string fname = "null", string lname = "null"){
            Player p1 = players.Where(x => x.Fname == fname && x.Lname == lname).FirstOrDefault();

            if(p1==null)  {
            p1 = new Player()
            {
                Fname = fname,
                Lname = lname
            };
            players.Add(p1);
            }
            return p1;
        }   

        /// <summary>
        /// Converts string input to int. If unsuccessful, returns -1
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public int StringToInt(string input){
            int output;
            bool logInOrQuitBool = int.TryParse(input, out output);
            if(logInOrQuitBool==false)
            return -1;
            return output;
        }


        
    }
}