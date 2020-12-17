using System;
using System.Collections.Generic;

namespace RpsGameNoDB
{
    class Match
    {
        public Guid MatchId {get;} = Guid.NewGuid();

        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public List<Round> Rounds { get; set; } = new List<Round>();
        public Player Winner { get; set; }

        public Match(Player player1, Player player2){
            Player1 = player1;
            Player2 = player2;
        }


        //used to check for wins; returns winner, returns null if no winner yet
        /// <summary>
        /// This method checks if there is a winner yet. Two round wins are required to win the match. If there is no winner yet, the method will return null.
        /// </summary>
        /// <returns>bool gameIsOver</returns>
        public bool CheckWins(){
            Player winner  = null;
            int p1Wins = 0;
            int p2Wins = 0;
            foreach (Round round in Rounds){
                if(round.WinningPlayer == Player1){
                    p1Wins++;
                }
                else if(round.WinningPlayer == Player2){
                    p2Wins++;
                }
            }
            if(p1Wins>=2){
                winner = Player1;
                return true;
            }
            else if(p2Wins>=2){
                winner = Player2;
                return true;
            }
            return false;
        }

        //returns the winner of the round
        public Player PlayRound(Choice p1Choice,Choice p2Choice){
            
            if(p1Choice == p2Choice){
                //tie game
                Winner = null;
            }
            else if ((p1Choice==Choice.Rock&&p2Choice==Choice.Scissors)||(p1Choice==Choice.Paper&&p2Choice==Choice.Rock)||(p1Choice==Choice.Scissors&&p2Choice==Choice.Paper)){
                //p1 wins
                Winner = Player1;
                Player1.Wins++;
                Player2.Losses++;
            }
            else{
                //p2 wins
                Winner = Player2;
                Player2.Wins++;
                Player1.Losses++;
            }
            Round currentRound = new Round(){
                Player1 = this.Player1,
                Player1Choice = p1Choice,
                Player2 = this.Player2,
                Player2Choice = p2Choice,
                WinningPlayer = Winner,
            };
            return Winner;
        }
        public void EndMatch(){
            
        }
    }
}