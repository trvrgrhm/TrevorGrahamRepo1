
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace RpsGame_WithDb
{
    public class Match
    {
        // public string Id;
        // private Guid matchId 
        
        [Key]
        public Guid MatchId { get; set;} = Guid.NewGuid();

        public Player Player1 { get; set; } // always the computer
        public Player Player2 { get; set; } // always the user.

        public List<Round> Rounds = new List<Round>();

        public int P1RoundWins { get; set; } // ho many rounds has the player won?
        public int P2RoundWins { get; set; }
        public int Ties { get; set; }


        /// <summary>
        /// This is the description of the method called RoundWinner
        /// This methodtakes an optional Player object and increments the numnber of round wins for that player.
        /// no arguments means a tie.
        /// </summary>
        /// <param name="p"></param>
        public void RoundWinner(Player p = null)
        {
            if (p == null)
            {
                Ties++;
            }
            else if (p.PlayerId == Player1.PlayerId)
            {
                P1RoundWins++;
            }
            else if (p.PlayerId == Player2.PlayerId)
            {
                P2RoundWins++;
            }
        }

        public Player MatchWinner()
        {
            if (P1RoundWins == 2)
            {
                return Player1;
            }
            else if (P2RoundWins == 2)
            {
                return Player2;
            }
            else
            {
                return null;
            }
        }





    }


}
