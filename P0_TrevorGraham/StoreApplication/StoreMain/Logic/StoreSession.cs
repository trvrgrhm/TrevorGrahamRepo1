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

        public bool AttemptChooseStore(string storeName){
            currentLocation = db.Locations.SingleOrDefault(x => x.Name == storeName);
            return currentLocation != null;
        }


        #region add to db

        //Populates database if dummy data is needed;should be called very first
        public void PopulateDb(){
            if(db.Locations.Count()<1){
                for(int i = 0;i<3;i++){
                    AddLocationToDb("Store "+i);
                }
            }
            if(db.Products.Count()<1){
                for(int i = 0;i<3;i++){
                    AddLocationToDb("Product "+i);
                }
            }
            //restock stores
            foreach(Location store in db.Locations){
                if(store.InventoryItems.Count()<1){
                    foreach(Product product in db.Products){
                        AddInventoryItem(store,product,10);
                    }
                }
            }
        }

        public bool AddInventoryItem(Location store,Product product,int amount){
            //TODO: any checks to perform?
            Inventory stockItem = new Inventory{
                            Location = store,
                            Product = product,
                            Quantity = amount

                        };
            db.Inventories.Add(stockItem);
            db.SaveChanges();
            return true;
        }
        /// <summary>
        /// Adds location to db based on a name; returns false if it already exists in db
        /// </summary>
        /// <param name="storeName"></param>
        /// <returns></returns>
        public bool AddLocationToDb(string storeName){
            //TODO: any checks to perform?
            if(db.Locations.SingleOrDefault(x => x.Name ==storeName)!=null){
                //store already exists in db
                return false;
            }
            Location newStore = new Location(){
                Name = storeName
            };
            db.Locations.Add(newStore);
            db.SaveChanges();
            return true;
        }
        /// <summary>
        /// Adds product to db; returns false if product name already exists in db
        /// </summary>
        /// <param name="name">Product Name</param>
        /// <param name="price">Product Price</param>
        /// <param name="description">Product Description</param>
        /// <returns></returns>
        public bool AddProductToDb(string name, double price, string description){
            //TODO: any checks to perform?
            if(db.Products.SingleOrDefault(x => x.ProductName ==name)!=null){
                //product already exists in db
                return false;
            }
            Product newProduct = new Product(){
                ProductName = name,
                Price = price,
                Description = description,
            };
            db.Products.Add(newProduct);
            db.SaveChanges();
            return true;
        }
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
        /// Returns true if the customer has chosen a store; returns fals if not
        /// </summary>
        /// <returns></returns>
        public bool StoreIsChosen(){
            return currentLocation != null;
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
        /// <summary>
        /// Returns current store name; returns null if no store is currently chosen;
        /// </summary>
        /// <returns></returns>
        public string GetCurrentStoreName(){
            if(StoreIsChosen()){
                return currentLocation.Name;
            }
            else{
                return null;
            }
        }
        /// <summary>
        /// Returns a list containing all of the store names in the db
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllStoreNames(){
            List<string> storeNames = new List<string>();
            foreach(Location store in db.Locations){
                storeNames.Add(store.Name);
            }
            return storeNames;
        }
        #endregion
    }
}