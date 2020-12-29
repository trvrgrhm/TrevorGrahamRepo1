using System;
using System.Collections.Generic;
using StoreApplication.Logic;

namespace StoreApplication.UI
{
    public class StoreCLI
    {
        //db access
        // StoreDbContext db = new StoreDbContext();

        // //session variables
        // Customer currentCustomer = null;
        // Location currentLocation = null;
        // List<OrderLine> cart = new List<OrderLine>();
        //state
        StoreSession session = new StoreSession();
        Menu currentMenu = Menu.Welcome;
        public void Start(){
            while(true){
                switch(currentMenu){
                    case Menu.Welcome: WelcomeMenu();
                    break;
                    case Menu.SignIn: SignInMenu();
                    break;
                    case Menu.Signup: SignUpMenu();
                    break;
                    case Menu.Main: MainMenu();
                    break;
                }
            }
        }
        #region menus
        void WelcomeMenu(){
            Console.WriteLine("Welcome to the Store Application!");
            string[] startOptions = {"Log In","Register New User","Exit"};
            switch(ChooseOptionFromList(options:startOptions)){
                case 0: currentMenu = Menu.SignIn;
                break;
                case 1: currentMenu = Menu.Signup;
                break;
                case 2: Environment.Exit(0);
                break;
            }

        }

        void MainMenu(){
            //check if user is signed in?
            if(!session.IsLoggedIn()){
                Console.WriteLine("Something must have gone wrong, returning to first menu...");
                currentMenu = Menu.Welcome;
            }
            else{
                //successfully logged in
                Console.WriteLine($"\nWelcome {session.GetCustomerFname()}!\n");
                //
                if(session.CartIsEmpty()){
                    Console.WriteLine("You don't have anything in your cart.");
                }

                string makeChoice = ("\nWhat would you like to do?\n");
                string[] options = {"Choose a Store to Shop at","Checkout with Current Cart","View Purchase History","Logout","Exit"};
                switch(ChooseOptionFromList(intro: makeChoice, options:options)){
                    //if sign-in is successful, move to main menu, otherwise, stay in sign in menu
                    case 0:currentMenu = Menu.ChooseStore;
                    break;
                    case 1:currentMenu = Menu.Checkout;
                    break;
                    case 2:currentMenu = Menu.ViewPurchaseHistory;
                    break;
                    case 3: session.Logout();
                    currentMenu = Menu.Welcome;
                    break; 
                    case 4: Environment.Exit(0);
                    break;
                }

            }
        }

        void SignInMenu(){
            Console.Write("Please enter your username: ");
            string username = Console.ReadLine();
            //check for username in db. if username exists, prompt for password
            if(!session.AttemptLogin(username)){
                string badUsername = ("\nLooks like the username you have entered doesn't exist. What would you like to do?\n");
                string[] badUsernameOptions = {"Use a Different Username","Create an Account","Exit"};
                switch(ChooseOptionFromList(intro: badUsername, options:badUsernameOptions)){
                    //if sign-in is successful, move to main menu, otherwise, stay in sign in menu
                    case 0:break;//stays in current menu
                    case 1: currentMenu = Menu.Signup;
                    break; 
                    case 2: Environment.Exit(0);
                    break;
                }
            }
            else{
                //sign-in was successful
                currentMenu = Menu.Main;
            }               
        }
        void SignUpMenu(){
                Console.Write("Please enter the username you would like to use: ");
                string username = Console.ReadLine();

                if(session.AttemptLogin(username)){
                    //username was in database, unassign currentCustomer and ask for options
                    session.Logout();
                    string usernameTaken = ("\nLooks like that username is already taken, what yould you like to do?\n");
                    string[] usernameTakenOptions = {"Use a Different Username","Login Instead","Go Back","Exit"};
                    switch(ChooseOptionFromList(usernameTaken,usernameTakenOptions)){
                        case 0: break;//stays in current menu
                        case 1: currentMenu = Menu.SignIn;
                        break;
                        case 2: currentMenu = Menu.Welcome;
                        break;
                        case 3: Environment.Exit(0);
                        break;
                    }
                }
                else{
                    //nobody in db with chosen username
                    if (CreateNewUser(username)){
                        Console.WriteLine($"New user {username} created successfully!");
                        currentMenu = Menu.Main;
                    }
                }
        }
        
        bool CreateNewUser(string username){
            Console.WriteLine("Creating a new user");
            //make sure nobody is logged in
            if(session.IsLoggedIn()){
                Console.WriteLine("You are already logged in!");
                //TODO: go to password validation
                currentMenu = Menu.Main;
                return false;
            }
            //check if username is in db
            if(session.AttemptLogin(username)){
                session.Logout();
                Console.WriteLine("There is already somebody with that username!");
                return false;
            }

            //name validation?
            Console.Write("Please enter your first name: ");
            string fName = Console.ReadLine();
            Console.Write("Please enter your last name: ");
            string lName = Console.ReadLine();

            session.AddCustomerToDb(username,fName,lName);

            if(session.AttemptLogin(username)){
                //TODO: go to password validation
                currentMenu = Menu.Main;
                return true;
            }
            else{
                Console.WriteLine("Something has gone wrong! Returning to start menu");
                currentMenu = Menu.Welcome;
                return false;
            }
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

    public enum Menu{
        Welcome,
        SignIn,
        Signup,
        Main,
        //implemented ^
        ViewPurchaseHistory,
        ViewPurchaseHistoryByStore,
        ChooseStore,
        ViewProducts,
        ProductAmount,
        Checkout
    }
}