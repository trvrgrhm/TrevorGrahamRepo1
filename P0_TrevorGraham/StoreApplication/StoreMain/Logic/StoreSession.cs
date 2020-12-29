using System.Collections.Generic;
using System.Linq;
using StoreApplication.Models;

namespace StoreApplication.Logic
{
    public class StoreSession
    {
        //db access
        /// <summary>
        /// Provides access to the db; remember to save changes if you make any!
        /// </summary>
        /// <returns></returns>
        StoreDbContext db = new StoreDbContext();

        //session variables
        /// <summary>
        /// Customer that is currently logged in; null if nobody is logged in
        /// </summary>
        Customer currentCustomer = null;
        Location currentLocation = null;
        List<OrderLine> cart = new List<OrderLine>();

        /// <summary>
        /// Attempts find Customer with given username in database. If successful, assigns the Customer as the signed in user and returns true; otherwise, returns false
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool AttemptLogin(string username){        
            Customer loggingIn = db.Customers.SingleOrDefault(x => x.Username == username);
            currentCustomer = loggingIn;
            return currentCustomer != null;
            // return false;
        }
        /// <summary>
        /// Logs customer out if a customer is logged in.
        /// </summary>
        public void Logout(){
            currentCustomer = null;
        }


        #region add to db
        /// <summary>
        /// Adds a customer to the db; returns true if successful, and returns false if username already exists.
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public bool AddCustomerToDb(string username, string fName, string lName){
            //check if username already exists
            if(AttemptLogin(username)){
                //username already exists
                Logout();
                return false;
            }
            //username is not in db, add them!
            Customer newCustomer = new Customer(){
                Username = username,
                Fname = fName,
                LName = lName
            };
            db.Customers.Add(newCustomer);
            db.SaveChanges();
            return true;
        }
        #endregion

        #region info
        /// <summary>
        /// Returns true if a user is logged in and false if no user is logged in.
        /// </summary>
        /// <returns></returns>
        public bool IsLoggedIn(){
            return currentCustomer !=null;
        }
        /// <summary>
        /// Returns true if the cart is empty; returns false if the cart contains any items.
        /// </summary>
        /// <returns></returns>
        public bool CartIsEmpty(){
            return !cart.Any();
        }
        /// <summary>
        /// Returns current customer's first name if a user is logged in and null if no user is logged in.
        /// </summary>
        /// <returns></returns>
        public string GetCustomerFname(){
            if(IsLoggedIn()){
                return currentCustomer.Fname;
            }
            else{
                return null;
            }
        }
        /// <summary>
        /// Returns current customer's last name if a user is logged in and null if no user is logged in.
        /// </summary>
        /// <returns></returns>
        public string GetCustomerLname(){
            if(IsLoggedIn()){
                return currentCustomer.LName;
            }
            else{return null;}
        }
        #endregion
    }
}