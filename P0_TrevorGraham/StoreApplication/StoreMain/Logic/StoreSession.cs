using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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
        StoreDbContext db;// = new StoreDbContext();
        public StoreSession(){
            db = new StoreDbContext();
        }

        #region session variables
        /// <summary>
        /// Customer that is currently logged in; null if nobody is logged in
        /// </summary>
        Customer currentCustomer = null;
        Location currentLocation = null;
        Inventory currentProduct = null;
        List<OrderLine> cart = new List<OrderLine>();
        #endregion

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
        public bool AttemptChooseProduct(string productName){
            currentProduct = currentLocation.InventoryItems.SingleOrDefault(x => x.Product.ProductName == productName); 
            return currentProduct!=null;
        }

        public bool AttemptAddToCart(int quantity){
            OrderLine cartLine =new OrderLine(){
                Quantity = quantity,
                Inventory = currentProduct,
            };
            cart.Add(cartLine);
            return true;
        }

        /// <summary>
        /// Attempts to add cart to db as order; Any quantities that are too high are dropped
        /// </summary>
        /// <returns>true if successful</returns>
        public bool AttemptCheckout(){
            if(!IsLoggedIn()||!ProductIsChosen()||!StoreIsChosen()){
                return false;
            }
            //decrement inventories in db
            List<OrderLine> removeThese = new List<OrderLine>();
            foreach(OrderLine cartLine in cart){
                if(cartLine.Inventory.Quantity - cartLine.Quantity>=0){
                    cartLine.Inventory.Quantity -= cartLine.Quantity;
                }
                else{
                    removeThese.Add(cartLine);
                }
            }
            if(removeThese.Any()){
                foreach(OrderLine line in removeThese){
                    cart.Remove(line);
                }
            }
            if(cart.Any()){
                Order newOrder = new Order(){
                    Date = DateTime.Now,
                    Customer = currentCustomer,
                    OrderLines = cart
                };
                db.Orders.Add(newOrder);
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public double CartTotal(){
            return cart.Select(x=>x.Inventory.Product.Price*x.Quantity).Sum();
        }
        public double OrderTotal(int orderId){
            // Order order = db.Orders.Include(x=>x.OrderLines).SingleOrDefault(x=>x.OrderId==orderId);
            // if(order!=null)
            // return order.OrderLines.Select(x=>x.Inventory.Product.Price).Sum();
            return db.OrderLines.Include(x=>x.Inventory.Product).Where(x=>x.Order.OrderId==orderId).Select(x=>x.Inventory.Product.Price*x.Quantity).Sum();
            // return 0.0;
        }
        

        public bool RemoveAllItemsFromCart(){
            cart = null;
            return cart == null;
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
                    AddProductToDb("Product "+i,i+1,"Something Special");
                }
            }
            //restock stores
            if(db.Inventories.Count()<1){
            foreach(Location store in db.Locations.ToList()){
                //  if( db.Inventories.Select(x=>x.Location.Equals(store)).Count()<1){// Store.Inventorys.Count()<1){
                    foreach(Product product in db.Products.ToList()){
                        AddInventoryItem(store,product,10);
                    }
                //  }
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
            store.InventoryItems.Add(stockItem);
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
        /// Returns true if the customer has chosen a store; returns false if not
        /// </summary>
        /// <returns></returns>
        public bool StoreIsChosen(){
            return currentLocation != null;
        }
        /// <summary>
        /// Returns true if the customer has chosen a product; returns false if not
        /// </summary>
        /// <returns></returns>
        public bool ProductIsChosen(){
            return currentProduct != null;
        }
        /// <summary>
        /// Returns true if the cart is empty; returns false if the cart contains any items.
        /// </summary>
        /// <returns></returns>
        public bool CartIsEmpty(){
            if(cart==null)
            return false;
            return !cart.Any();
        }
        public string GetCustomerUsername(){
            if(IsLoggedIn()){
                return currentCustomer.Username;
            }
            else{return null;}
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
        public string GetCurrentProduct(){
            return currentProduct.Product.ProductName;
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
        /// <summary>
        /// Returns a list with the inventory of the current store.
        /// </summary>
        /// <returns></returns>
        public List<string> GetCurrentProductNames(){
            return db.Inventories.Where(x=>x.Location==currentLocation).Select(x=>x.Product.ProductName).ToList();
            // List<string> products = new List<string>();
            // foreach(Inventory inventory in db.Inventories.Where(x=>x.Location==currentLocation)){
            //     products.Add(inventory.Product.ProductName);
            // }
            // return products;
        }
        /// <summary>
        /// Returns the price as a double for the product in the current store location
        /// </summary>
        /// <param name="prductName"></param>
        /// <returns></returns>
        public double GetProductPrice(string productName){
            if(StoreIsChosen()){
                
                var inv = db.Inventories.Include(x=>x.Product).Where(x=>x.Location==currentLocation);
                var tinv = inv.FirstOrDefault(x=>x.Product.ProductName==productName);
                if(tinv!=null)
                return tinv.Product.Price;
                // return currentLocation.Inventories.SingleOrDefault(x => x.Product.ProductName == productName).Product.Price;
            }
            return 0.0;
        }
        /// <summary>
        /// Returns the quantity as a string for the product in the current store location
        /// </summary>
        /// <param name="prductName"></param>
        /// <returns></returns>
        public int GetProductQuantity(string prductName){
            if(StoreIsChosen()){
                return currentLocation.InventoryItems.SingleOrDefault(x => x.Product.ProductName ==prductName).Quantity;
            }
            return -1;
        }

        /// <summary>
        /// Returns a list with the inventory of the current store.
        /// </summary>
        /// <returns></returns>
        public List<string> GetCurrentInventory(){
            List<string> products = new List<string>();
            foreach(Inventory inventory in currentLocation.InventoryItems){
                products.Add($"\n{inventory.Product.ProductName}\nPrice: ${inventory.Product.Price}\n{inventory.Quantity} left in stock\n");
            }
            return products;
        }
        //order info
        public List<int> GetOrderIds(string customerName, string location){
            List<int> orderIds = new List<int>();
            foreach(Order order in db.Orders.ToList()){
                if(order.Customer.Username == customerName && db.OrderLines.FirstOrDefault(x=>x.Order==order).Inventory.Location.Name == location){
                    orderIds.Add(order.OrderId);
                }
            }
            return orderIds;
        }
        public List<int> GetOrderIds(string customerName){
            return db.Orders.Where(x=>x.Customer.Username==customerName).Select(x=>x.OrderId).ToList();
        }
        public List<int> GetOrderIds(){
            return db.Orders.Select(x=>x.OrderId).ToList();
        }
        public string GetOrderLocation(int orderId){
            var orderLine = db.OrderLines.Include(x=>x.Inventory.Location).FirstOrDefault(x=>x.Order.OrderId==orderId);
            if(orderLine != null){
                return orderLine.Inventory.Location.Name;
            }
            else {return null;}
            // return db.Orders.SingleOrDefault(x=>x.OrderId == orderId).OrderLines[0].Inventory.Location.Name;
        }
        public List<string> GetOrderLines(int orderId){
            return db.OrderLines.Include(x=>x.Inventory.Product).Where(x=>x.Order.OrderId==orderId).Select(x=>$"{x.Quantity} {x.Inventory.Product.ProductName}s: ${x.Inventory.Product.Price*x.Quantity}").ToList();
            // return db.Orders.SingleOrDefault(x=>x.OrderId ==orderId).OrderLines.Select(x=>$"{x.Quantity} {x.Inventory.Product}s: ${x.Inventory.Product.Price}").ToList();
        }
        #endregion
    }
}