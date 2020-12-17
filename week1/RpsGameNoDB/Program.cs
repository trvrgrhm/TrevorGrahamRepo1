using System;
using System.Collections.Generic;

namespace RpsGameNoDB
{
    class Program
    {        
        static void Main(string[] args)
        {
            List<Player> players = new List<Player>();
            List<Match> matches = new List<Match>();
            List<Round> rounds = new List<Round>();

            Player player1 = null;

            Player player2 = new Player(){
                FName = "Computer",
                LName = "McGee",
            };


            while(true){

                Console.WriteLine("\nWelcome to the Rock-Paper-Scissors Game");
                Console.WriteLine("\nPlease Log In: ");
                //log in or create new player
                bool onlyTwoNames = false;
                do{
                Console.Write("Please enter your first and last name: ");
                string[] names = Console.ReadLine().Split(' ');
                //TODO: check to make sure only two names
                if(names.Length==2){

                    Console.WriteLine($"Welcome {names[0]} {names[1]}!");
                    onlyTwoNames = true;
                    player1 = new Player(){
                        FName = names[0],
                        LName = names[1],
                    };

                }
                else{
                    Console.WriteLine("Please use only two names, separated by a space.");
                }
                }while (!onlyTwoNames);

                bool okayInput=false;
                string userResponse = "";
                Choice playerChoice = 0;
                Choice computerChoice  = (Choice)new Random().Next(3);

                do{
                Console.WriteLine("Type \"stop\" if you want to stop.");
                Console.WriteLine("Please choose an option by typing a number: \n\t0. Rock\n\t1. Paper\n\t2. Scissors");
                Console.Write("\nType your answer here: ");


                userResponse = Console.ReadLine().ToLower();
                if(userResponse=="stop"){
                    break;
                }
                
                okayInput = Choice.TryParse(userResponse, out playerChoice);
                }while(!okayInput);
                Console.WriteLine($"player chose {playerChoice.ToString()}");
                Console.WriteLine($"computer chose {computerChoice.ToString()}");


                

                // if(userResponse=="1"||userResponse=="2"||userResponse=="3"){
                //     Dictionary<int,string> answers = new Dictionary<int,string>();
                //     //rock beats scissors
                //     answers.Add(1,"3");
                //     //paper beats rock
                //     answers.Add(2,"1");
                //     //scissors beats paper
                //     answers.Add(3,"2");

                //     Random rand = new Random();
                //     int computerChoice = rand.Next(3)+1;
                //     string computerResponse = "The computer chose ";

                //     if(userResponse==computerChoice.ToString()){
                //         Console.WriteLine("\nResult: You tied!");
                //     }
                //     else if(answers[computerChoice]==userResponse){
                //         switch (computerChoice){
                //             case 1: computerResponse+="Rock and you picked Scissors, so you lost...";
                //             break;
                //             case 2: computerResponse+="Paper and you picked Rock, so you lost...";
                //             break;
                //             case 3: computerResponse+="Scissors and you picked Paper, so you lost...";
                //             break;
                //         }
                //         Console.WriteLine(computerResponse);
                //         Console.WriteLine("\nResult: You fool! The computer won!\n");
                //     }
                //     else{
                //         switch (computerChoice){
                //             case 1: computerResponse+="Rock and you picked Paper, so you won!";
                //             break;
                //             case 2: computerResponse+="Paper and you picked Scissors, so you won!";
                //             break;
                //             case 3: computerResponse+="Scissors and you picked Rock, so you won!";
                //             break;
                //         }
                //         Console.WriteLine(computerResponse);
                        
                //         Console.WriteLine("\nResult: Good job! You won!\n");
                //     }

                // }
                // else{
                //     Console.WriteLine("Ummm, try again...\n\n");
                // }
            }
        }

        public void Menu(){

        }

        #region game logic
        public void Best2Of3(Player competitor1,Player competitor2){
                    
        }
        //returns "tie"
        public string OneMatch(string competitor1,string competitor2){
            string winner = "tie";
            
            return winner;
        }
        #endregion

            // Console.WriteLine("This is an unofficial batch rock-paper-scissors game!");

            // int userChoice;
            // bool successfullyGaveAnswer;
            // do{
            // Console.WriteLine("Please choose an option by typing a number: \n\t1. Rock\n\t2. Paper\n\t3. Scissors");
            // Console.WriteLine("Type your answer here: ");
            // successfullyGaveAnswer = int.TryParse(Console.ReadLine(),out userChoice);
            // if(userChoice>3||userChoice<1){
            //     successfullyGaveAnswer = false;
            // }
            // if(!successfullyGaveAnswer){
            //     Console.WriteLine("Umm... try again?\n");
            // }
            // }
            // while(!successfullyGaveAnswer);

            // PrintChoice("You",userChoice);    

            // //computer choice
            // Random rand = new Random();
            // int computerChoice = rand.Next(1,4);
            // PrintChoice("Computer",computerChoice);

            // if(userChoice == computerChoice){
            //     Console.WriteLine("The game was a tie!");
            // }
            // else if(userChoice==1&&computerChoice==3||userChoice==2&&computerChoice==1||userChoice==3&&computerChoice==2){
            //     Console.WriteLine("You defeated the computer!");
            // }
            // else{
            //     Console.WriteLine("You fool! The computer defeated you!");
            // }

        // }

    //     static void PrintChoice(string name, int choice){string response = $"{name} chose ";
    //         switch(choice){
    //             case 1: response+="Rock"; 
    //             break;
    //             case 2: response+="Paper";
    //             break;
    //             case 3: response+="Scissors";
    //             break;

    //         }
    //         response+="!";
    //         Console.WriteLine(response);
    //     }
    }

}
