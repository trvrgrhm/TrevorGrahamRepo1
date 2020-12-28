using System;
using System.Collections.Generic;
using System.Linq;
using StoreApplication.Models;
using StoreApplication.Repository;

namespace StoreApplication.UI
{
    public class StoreCLI
    {
        StoreDbContext db = new StoreDbContext();
        Customer currentCustomer = null;
        #region menus
        public void Start(){
            Console.WriteLine("Welcome to the Store Application!");
            string[] startOptions = {"Log In","Exit"};
            switch(ChooseOptionFromList(options:startOptions)){
                case 0: SignInMenu();
                break;
                case 1: Environment.Exit(0);
                break;
            }
        }

        void MainMenu(){

        }

        void SignInMenu(){
            while(currentCustomer == null){

                Console.Write("Please enter your username: ");
                string username = Console.ReadLine();
                //check for username in db. if username exists, prompt for password
                if(!AttemptSignIn(username)){
                    string badUsername = ("\nLooks like the username you have entered doesn't exist. What would you like to do?\n");
                    string[] startOptions = {"Use a different username","Create an account","Exit"};
                    switch(ChooseOptionFromList(intro: badUsername, options:startOptions)){
                        case 0: AttemptSignIn(username);
                        break;
                        case 1: CreateNewUser(username);
                        break; 
                        case 2: Environment.Exit(0);
                        break;
                    }
                }                
            }
            MainMenu();

        }
        bool AttemptSignIn(string username){    
            Console.WriteLine("Attempting to sign in...");       
            Customer loggingIn = db.Customers.SingleOrDefault(x => x.Username == username);
            currentCustomer = loggingIn;
            return currentCustomer != null;
            // return false;
        }

        void CreateNewUser(string username){
            Console.WriteLine("Creating a new user");

        }

        #endregion
        #region choosing options
        /// <summary>
        /// Given a list of options in the form of a string array, this method will print out the options and return a valid number option or die trying.
        /// </summary>
        /// <returns></returns>
        public int ChooseOptionFromList(string intro = "Please choose an option: ",string[] options = null){
            if(options == null || options.Length == 0){
                throw new Exception("StoreCLI.cs: ChooseOptionFromList: something isn't giving options to this method!!! Give it a valid list of options!!!");
            }
            int userChoice = -1;
            bool validChoice = false;
            //keep pestering the user until they pick a valid choice
            while(!validChoice){
                Console.WriteLine(intro);
                for(int i =0;i<options.Length;i++){
                    Console.WriteLine(i+". "+options[i]);
                }
                Console.WriteLine("Select an option by typing in the number next to the desired option.");
                validChoice = IsValidNumberOption(Console.ReadLine(),options.Length, out userChoice);
                if(!validChoice){
                    Console.WriteLine("\nYou have tried to enter an invalid option.\nPlease choose a valid option.\n");
                }
            }
            return userChoice;
        }

        /// <summary>
        /// Like int.TryParse, this takes a string and converts it into an int, but it will also return false if the string is out of range of the number of options.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="numOptions"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        private bool IsValidNumberOption(string input, int numOptions, out int output){
            bool validOption = false;
            validOption = int.TryParse(input,out output);
            if(validOption && output>=0 && output<numOptions){
                validOption = true;
            }
            else{
                validOption =false;
                }
            return validOption;
        }
        #endregion
        
    }
}