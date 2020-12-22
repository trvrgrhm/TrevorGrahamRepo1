using System;
using System.ComponentModel.DataAnnotations;

namespace RpsGame_WithDb
{
    public class Round
    {        
        // public string Id;
        // private Guid roundId = Guid.NewGuid();
        [Key]
        public Guid RoundId {get;set;}= Guid.NewGuid();
        public Choice Player1Choice { get; set; } // always the computer
        public Choice Player2Choice { get; set; } // always the user
        public Player WinningPlayer { get; set; } = new Player()
        {
            Fname = "TieGame",
            Lname = "TieGame"
        };
    }
}