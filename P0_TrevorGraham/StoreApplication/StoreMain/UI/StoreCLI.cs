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
        StoreSession session;// = new StoreSession();
        Menu currentMenu = Menu.Welcome;

        public StoreCLI(){
            session = new StoreSession();
            session.PopulateDb();

        }
        public void Start(){
            //make sure usable data exists
            //start program loop
            while(true){
                switch(currentMenu){
                    case Menu.Welcome: WelcomeMenu();
                    break;
                    case Menu.SignIn: SignInMenu();
                    break;
                    case Menu.Signup: SignUpMenu();
                    break;
                    case Menu.ChooseStore: ChooseStore();
                    break;
                    case Menu.ViewProducts: ViewProducts();
                    break;
                    case Menu.ProductAmount: ProductAmount();
                    break;
                    case Menu.Checkout: CheckoutMenu();
                    break;
                    case Menu.Main: MainMenu();
                    break;
                    case Menu.ViewPurchaseHistory: PurchaseHistory();
                    break;
                    // case Menu.ViewPurchaseHistoryByStore:
                    // break;
                }
            }
        }
        #region menus
        void WelcomeMenu(){
            Console.WriteLine("\nWelcome to the Store Application!\n");
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
                CartDisplay();

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

        void ChooseStore(){
            //check if user is signed in?
            if(!session.IsLoggedIn()){
                Console.WriteLine("Something must have gone wrong, returning to first menu...");
                currentMenu = Menu.Welcome;
            }
            else{
                //successfully logged in
                Console.WriteLine($"\nWelcome {session.GetCustomerFname()}!\n");
                //
                CartDisplay();

                string makeChoice = ("\nWhat would you like to do?\n");
                List<string> options = new List<string>();
                options.Add("Exit");
                options.Add("Go Back to Main Menu");
                string[] storeNames = session.GetAllStoreNames().ToArray();
                foreach(string storeName in storeNames){
                    options.Add($"Go to the {storeName} Shop");
                }
                int choice = ChooseOptionFromList(intro: makeChoice, options:options.ToArray());
                switch(choice){
                    //if sign-in is successful, move to main menu, otherwise, stay in sign in menu
                    case 0:Environment.Exit(0);
                    break;
                    case 1:currentMenu = Menu.Main;
                    break; 
                    default: //attempt to choose store;if successful, go to products menu
                        if(session.AttemptChooseStore(storeNames[choice-2])){
                        currentMenu = Menu.ViewProducts;}
                    break;
                }
            }

        }
        void ViewProducts(){
            //check if user is signed in?
            if(!session.IsLoggedIn()){
                Console.WriteLine("Something must have gone wrong, returning to first menu...");
                currentMenu = Menu.Welcome;
                return;
            }
            if(!session.StoreIsChosen()){
                Console.WriteLine("Something must have gone wrong, returning to first menu...");
                currentMenu = Menu.Welcome;
                return;
            }
            else{
                //successfully logged in
                Console.WriteLine($"\n{session.GetCustomerFname()}, welcome to the {session.GetCurrentStoreName()} store!\n");
                CartDisplay();
                string makeChoice = ("\nWhat would you like to do?\n");
                List<string> options = new List<string>();
                options.Add("Exit");
                options.Add("Go Back to Main Menu");
                options.Add("View Order History");
                options.Add("Checkout");
                string[] products = session.GetCurrentProductNames().ToArray();
                foreach(string product in products){
                    options.Add($"Add {product} to Cart\n{product} costs ${session.GetProductPrice(product)}");
                }
                int choice = ChooseOptionFromList(intro: makeChoice, options:options.ToArray());
                switch(choice){
                    //if sign-in is successful, move to main menu, otherwise, stay in sign in menu
                    case 0:Environment.Exit(0);
                    break;
                    case 1:currentMenu = Menu.Main;
                    break; 
                    case 2: currentMenu = Menu.ViewPurchaseHistory;
                    break;
                    case 3: currentMenu = Menu.Checkout;
                    break;
                    default: //attempt to choose store;if successful, go to products menu
                        if(session.AttemptChooseProduct(products[choice-4])){currentMenu = Menu.ProductAmount;}
                    break;
                }
            }

        }
        void ProductAmount(){
            //check if user is signed in?
            if(!session.IsLoggedIn()){
                Console.WriteLine("Something must have gone wrong, returning to first menu...");
                currentMenu = Menu.Welcome;
                return;
            }
            if(!session.StoreIsChosen()){
                Console.WriteLine("Something must have gone wrong, returning to first menu...");
                currentMenu = Menu.Welcome;
                return;
            }
            if(!session.ProductIsChosen()){
                Console.WriteLine("Something must have gone wrong, returning to first menu...");
                currentMenu = Menu.Welcome;
                return;
            }
            string currentProduct = session.GetCurrentProduct();
            Console.WriteLine($"{currentProduct} was selected.\nThere are {session.GetProductQuantity(currentProduct)} in stock.");
            Console.WriteLine("How many would you like to buy?");
            int desiredAmount = 0;
            if(IsValidNumberOption(Console.ReadLine(),session.GetProductQuantity(currentProduct),out desiredAmount)){
                if(session.AttemptAddToCart(desiredAmount)){
                    Console.WriteLine($"You have successfully added {desiredAmount} {currentProduct}s to your cart.");
                    currentMenu = Menu.ViewProducts;
                    return;
                }
            }
            Console.WriteLine("Something must have gone wrong, returning to first menu...");
            currentMenu = Menu.Welcome;
            return;
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

        void CheckoutMenu(){
            //check if user is signed in?
            if(!session.IsLoggedIn()){
                Console.WriteLine("Something must have gone wrong, returning to first menu...");
                currentMenu = Menu.Welcome;
                return;
            }
            if(!session.StoreIsChosen()){
                Console.WriteLine("Something must have gone wrong, returning to first menu...");
                currentMenu = Menu.Welcome;
                return;
            }
            if(!session.ProductIsChosen()){
                Console.WriteLine("Something must have gone wrong, returning to first menu...");
                currentMenu = Menu.Welcome;
                return;
            }
            CartDisplay();

            string checkoutIntro = ("\nWould you like to check out now?\n");
            string[] checkoutOptions = {"Add More Items to Chart","Empty Cart and Return To First Menu","Checkout","Exit"};
            switch(ChooseOptionFromList(intro: checkoutIntro, options:checkoutOptions)){
                //if sign-in is successful, move to main menu, otherwise, stay in sign in menu
                case 0: currentMenu = Menu.ViewProducts;
                break;//stays in current menu
                case 1: if(session.RemoveAllItemsFromCart()){
                    currentMenu = Menu.Main;}
                    else{
                        Console.WriteLine("Something must have gone wrong, returning to first menu...");
                        currentMenu = Menu.Welcome;
                return;
                    }
                break; 
                case 2:if(session.AttemptCheckout()){
                    Console.WriteLine($"Checkout successful! for a total of ${session.CartTotal()}, you purchased:\n");
                    session.RemoveAllItemsFromCart();
                    
                        currentMenu = Menu.Welcome;
                                    
                }
                else{Console.WriteLine("Something must have gone wrong, returning to first menu...");
                currentMenu = Menu.Welcome;
                return;}
                break;
                case 3: Environment.Exit(0);
                break;
            }

        }

        void PurchaseHistory(){
            if(session.IsLoggedIn()){
                // PurchaseHistoryByStore(session.GetCurrentStoreName());
                foreach(int orderId in session.GetOrderIds(session.GetCustomerUsername())){
                OrderDisplay(orderId);
            }
                // session.GetOrderIds(session.GetCustomerUsername());
            }
            else{
                Console.WriteLine("Something must have gone wrong, returning to first menu...");
                currentMenu = Menu.Welcome;
                return;
            }
        }
        void PurchaseHistoryByStore(string storeName){
            Console.WriteLine($"Orders from {storeName}");
            foreach(int orderId in session.GetOrderIds(session.GetCustomerUsername(),storeName)){
                OrderDisplay(orderId);
            }
        }
        void OrderDisplay(int orderId){
            session.GetOrderLocation(orderId);
            session.OrderTotal(orderId);
            OrderLineDisplay(orderId);
            currentMenu = Menu.Main;
        }
        void OrderLineDisplay(int orderId){
            foreach(string line in session.GetOrderLines(orderId)){
                Console.WriteLine(line);
            }
        }
        void CartDisplay(){
            Console.WriteLine();
            if(session.CartIsEmpty()){
                Console.WriteLine("You don't have anything in your cart.");
            }
            else{
                Console.WriteLine("You have stuff in your cart");
            }
            Console.WriteLine();
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
                    Console.WriteLine(i+". "+options[i]+"\n");
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
        ChooseStore,
        ViewProducts,
        ProductAmount,
        Checkout,
        //implemented ^
        ViewPurchaseHistory,
        ViewPurchaseHistoryByStore,
        
    }
}