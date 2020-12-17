using System;
using System.Collections.Generic;

namespace Group_Rps
{
    class Program
    {        
        static Dictionary<string,Player> players = new Dictionary<string,Player>();
        static List<Match> matches = new List<Match>();
        static List<Round> rounds = new List<Round>();

        static Player currentPlayer;
        static void Main(string[] args)
        {

            SignIn();

            while(true){
            //menu here
            Console.WriteLine("Here are your options: \n0. Play Rock-Paper-Scissors\n1. View Stats\n2. Sign out\n3. Quit game");
            string startmenu;
            startmenu = Console.ReadLine();
            if(startmenu == "0"){
                GameLoop();
            }
            else if(startmenu =="1"){
                ViewStats();
            }
            else if(startmenu == "2"){
                SignIn();
                //sign in
            }
            else if(startmenu == "3"){
                break;
            }
            }
        }

        public static void GameLoop(){
            #region game loop
            //while wins < 2
            int playerWins = 0;
            int compWins = 0;
            int roundNumber = 0;//new match
                Match currentMatch = new Match(){
                    Player1 = players["Karen Plankton"],
                    Player2 = currentPlayer
                };
            while(playerWins<2&&compWins<2){
                
                Console.WriteLine("\nWelcome to the Rock-Paper-Scissors Game");
                Console.WriteLine("Please choose an option by typing a number: \n\t1. Rock\n\t2. Paper\n\t3. Scissors");
                Console.Write("Type your answer here: ");
                string userResponse = Console.ReadLine().ToLower();
                if(userResponse=="1"||userResponse=="2"||userResponse=="3"){
                    //new round
                    Round round = new Round();
                    roundNumber++;
                    Dictionary<int,string> answers = new Dictionary<int,string>();
                    //rock beats scissors
                    answers.Add(1,"3");
                    //paper beats rock
                    answers.Add(2,"1");
                    //scissors beats paper
                    answers.Add(3,"2");
                    Random rand = new Random();
                    int computerChoice = rand.Next(3)+1;
                    string response = "The computer chose ";

                    round.Player1Choice = (Choice)(computerChoice-1);
                    //this is safe
                    round.Player2Choice = (Choice)(int.Parse(userResponse)-1);



                    if(userResponse==computerChoice.ToString()){
                        Console.WriteLine("\nResult: You tied!");
                        currentMatch.RoundWinner();
                        //create tie round
                    }
                    else if(answers[computerChoice]==userResponse){
                        switch (computerChoice){
                            case 1: response+="Rock and you picked Scissors, so you lost...";
                            break;
                            case 2: response+="Paper and you picked Rock, so you lost...";
                            break;
                            case 3: response+="Scissors and you picked Paper, so you lost...";
                            break;
                        }
                        Console.WriteLine(response);
                        Console.WriteLine("\nResult: You fool! The computer won!\n");
                        
                        round.WinningPlayer = currentMatch.Player1;
                        
                        currentMatch.RoundWinner(currentMatch.Player1);
                        compWins++;

                    }
                    else{
                        switch (computerChoice){
                            case 1: response+="Rock and you picked Paper, so you won!";
                            break;
                            case 2: response+="Paper and you picked Scissors, so you won!";
                            break;
                            case 3: response+="Scissors and you picked Rock, so you won!";
                            break;
                        }
                        Console.WriteLine(response);
                        Console.WriteLine("\nResult: Good job! You won!\n");
                        round.WinningPlayer = currentMatch.Player2;
                        currentMatch.RoundWinner(currentMatch.Player2);
                        playerWins++;
                    }
                    rounds.Add(round);
                    currentMatch.Rounds.Add(round);
                    Console.WriteLine($"{currentMatch.Player2.Fname} has {playerWins}.{currentMatch.Player1.Fname} has {compWins}. ");
                }
                else{
                    Console.WriteLine("Ummm, try again...\n\n");
                }
            }
            Console.WriteLine($"{currentMatch.MatchWinner().Fname} won the game!!!");

            #endregion
        }

        public static void ViewStats(){
            Console.WriteLine("view stats");
            //TODO: implement this
        }

        public static void SignIn(){
            Player p1 = new Player()
            {
                Fname = "Karen",
                Lname = "Plankton"
            };
            players.Add(p1.Fname+" "+p1.Lname, p1);

            Console.WriteLine("This is The Official Batch Rock-Paper-Scissors Game");



            #region name stuff

            string[] userNamesArray;
            do
            {
                Console.WriteLine("\nPlease enter your first name.\n If you enter unique first and last name I will create a new player.\n");
                string userNames = Console.ReadLine();
                userNamesArray = userNames.Split(' ');
            } while (userNamesArray[0] == "");

            Player p2 = new Player();

            //is the user unputted jsut one name
            if (userNamesArray.Length == 1)
            {
                p2.Fname = userNamesArray[0];
            }

            // if the user inputted 2 or more names.
            if (userNamesArray.Length > 1)
            {
                p2.Fname = userNamesArray[0];
                p2.Lname = userNamesArray[1];
            }
#endregion

        }

    }

}
