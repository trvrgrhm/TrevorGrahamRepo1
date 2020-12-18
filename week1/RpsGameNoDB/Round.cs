using System;
namespace RpsGameNoDB
{
    public class Round
    {
        public Guid RoundId { get;} = Guid.NewGuid();   
        public Choice Player1Choice { get; set; }
        public Choice Player2Choice { get; set; }
        public Player Player1 {get;set;} = null;
        public Player Player2 {get;set;} = null;
        public Player WinningPlayer {get;set;} = null;
    }
}