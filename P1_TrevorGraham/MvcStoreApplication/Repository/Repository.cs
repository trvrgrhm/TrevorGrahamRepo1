using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer
{
    public class Repository
    {
        private StoreDbContext _dbContext;
        public Repository(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region Users
        /// <summary>
        /// returns an IUser that contains the username if the password is correct. if the username or password is wrong, returns null
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public IUser AttemptSignInWithUsernameAndPassword(string username, string password)
        {
            Administrator admin = GetAdministratorByUsername(username);
            Customer cust = GetCustomerByUsername(username);
            //check customers
            if (cust != null)
            {
                if (cust.Password == password)
                {
                    //successful login
                    return cust;
                }
            }
            //check admins
            else if(admin != null)
            {
                if (admin.Password == password)
                {
                    //successful login
                    return admin;
                }
            }
            //login was not successful
            return null;
        }
        /// <summary>
        /// returns whether or not the username already exists in the db as an admin or customer
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool UsernameAlreadyExists(string username)
        {
            return CustomerIsInDb(username) || AdministratorIsInDb(username);
        }

        #endregion

        #region Customers
        /// <summary>
        /// Attempts to add customer to db and returns whether or not it was successful
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public bool AttemptAddCustomerToDb(Customer customer)
        {
            //if there is not a product in the db with the same id or name
            if (!CustomerIsInDb(customer)&&!UsernameAlreadyExists(customer.Username))
            {
                //add it
                _dbContext.Customers.Add(customer);
                _dbContext.SaveChanges();
                return CustomerIsInDb(customer);
            }
            else
            {
                //TODO:check if customer was already in db and give some sort of warning
            }
            return false;
            
        }
        /// <summary>
        /// Returns whether or not the requested user is a customer
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool UserIsCustomer(Guid userId)
        {
            return _dbContext.Customers.SingleOrDefault(x => x.UserId == userId) != null;
        }

        /// <summary>
        /// checks if the given customer is in the db based on id and username
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public bool CustomerIsInDb(Customer customer)
        {
            if (customer !=null)
            return _dbContext.Customers.ToList().Where(x => x.UserId == customer.UserId || x.Username == customer.Username).Count() > 0;
            return false;
        }
        /// <summary>
        /// Returns whether or not the requested guid is a customer in the db
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public bool CustomerIsInDb(Guid customerId)
        {
            return CustomerIsInDb(GetCustomerById(customerId));
        }
        /// <summary>
        /// Retunrs whether or not the requested username is a customer in the db
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool CustomerIsInDb(string username)
        {
            return CustomerIsInDb(GetCustomerByUsername(username));
        }

        /// <summary>
        /// returns a customer object based on the given customer id
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public Customer GetCustomerById(Guid customerId)
        {
            return _dbContext.Customers.Include(x=>x.DefaultLocation).FirstOrDefault(x => x.UserId == customerId);
        }
        /// <summary>
        /// returns a customer object based on the given username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Customer GetCustomerByUsername(string username)
        {
            return _dbContext.Customers.FirstOrDefault(x => x.Username == username);
        }

        /// <summary>
        /// returns a list of all of the customers
        /// </summary>
        /// <returns></returns>
        public List<Customer> GetAllCustomers()
        {
            return _dbContext.Customers.ToList();
        }
        #endregion
        #region Administrators
        /// <summary>
        /// Attempts to add admin to db and returns whether or not it was successful
        /// </summary>
        /// <param name="admin"></param>
        /// <returns></returns>
        public bool AttemptAddAdministratorToDb(Administrator admin)
        {
            //if there is not a product in the db with the same id or name
            if (!AdministratorIsInDb(admin) && !UsernameAlreadyExists(admin.Username))
            {
                //add it
                _dbContext.Administrators.Add(admin);
                _dbContext.SaveChanges();
                return AdministratorIsInDb(admin);
            }
            else
            {
                //TODO:check if admin was already in db and give some sort of warning
            }
            return false;
        }
        public bool UserIsAdministrator(Guid userId)
        {
            return _dbContext.Administrators.SingleOrDefault(x => x.UserId == userId) != null;
        }
        public bool AdministratorIsInDb(Administrator admin)
        {
            if(admin!=null)
                return _dbContext.Administrators.ToList().Where(x => x.UserId == admin.UserId || x.Username == admin.Username).Count() > 0;
            return false;
        }
        public bool AdministratorIsInDb(Guid adminId)
        {
            return AdministratorIsInDb(GetAdministratorById(adminId));
        }
        public bool AdministratorIsInDb(string username)
        {
            return AdministratorIsInDb(GetAdministratorByUsername(username));
        }
        public Administrator GetAdministratorById(Guid adminId)
        {
            return _dbContext.Administrators.FirstOrDefault(x => x.UserId == adminId);
        }
        public Administrator GetAdministratorByUsername(string username)
        {
            return _dbContext.Administrators.FirstOrDefault(x => x.Username == username);
        }

        public List<Administrator> GetAllAdministrators()
        {
            return _dbContext.Administrators.ToList();
        }

        #endregion

        #region Products
        /// <summary>
        /// attempts to add a product to the db, returns whether or not it was successful
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public bool AttemptAddProductToDb(Product product)
        {
            //if there is not a product in the db with the same id or name
            if(!ProductIsInDb(product))
            {
                //add it
                _dbContext.Products.Add(product);
                _dbContext.SaveChanges();
                return ProductIsInDb(product);
            }
            return false;
        }
        /// <summary>
        /// Attempts to add a product to an inventory, returns whether or not it was successful
        /// </summary>
        /// <param name="product"></param>
        /// <param name="location"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public bool AttemptAddProductToStore(Product product, Location location, int quantity)
        {
            if (ProductIsInDb(product) && LocationIsInDb(location))
            {
                Inventory inventory = new Inventory()
                {
                    Location = location,
                    Product = product,
                    Quantity = quantity
                    
                };
                _dbContext.Inventories.Add(inventory);
                _dbContext.SaveChanges();
                return (InventoryIsInDb(inventory));
            }
            return false;
        }
        /// <summary>
        /// returns whether or not the given inventory is in the db
        /// </summary>
        /// <param name="inventory"></param>
        /// <returns></returns>
        public bool InventoryIsInDb(Inventory inventory)
        {
            return _dbContext.Inventories.ToList().Where(x => x.InventoryId == inventory.InventoryId).Count()>0;
        }
        /// <summary>
        /// Checks if a product is in the db based on name and id
        /// </summary>
        /// <param name="product"></param>
        /// <returns>bool IsInDb</returns>
        public bool ProductIsInDb(Product product)
        {
            return (_dbContext.Products.ToList().Where(x => x.ProductId == product.ProductId || x.ProductName == product.ProductName).Count() > 0);
        }
        /// <summary>
        /// returns whether or not the given guid is related to a product in the db
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public bool ProductIsInDb(Guid productId)
        {
            return ProductIsInDb(GetProductById(productId));
        }
        /// <summary>
        /// Returns the product based on the product id or null if one doesn't exist
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public Product GetProductById(Guid productId)
        {
            return _dbContext.Products.FirstOrDefault(x => x.ProductId == productId);
        }
        /// <summary>
        /// Returns a list of all products in the db
        /// </summary>
        /// <returns></returns>
        public List<Product> GetAllProducts()
        {
            return _dbContext.Products.ToList();
        }
        /// <summary>
        /// returns the inventory in the db based on the id given
        /// </summary>
        /// <param name="inventoryId"></param>
        /// <returns></returns>
        public Inventory GetInventoryById(Guid inventoryId)
        {
            return _dbContext.Inventories.Include(x=>x.Location).Include(x=>x.Product).FirstOrDefault(x => x.InventoryId == inventoryId);
        }
        /// <summary>
        /// Returns a list of all inventories based on the location id given
        /// </summary>
        /// <param name="currentLocationId"></param>
        /// <returns></returns>
        public List<Inventory> GetLocationInventories(Guid currentLocationId)
        {
            List<Inventory> filteredInventories = new List<Inventory>();
            return _dbContext.Inventories.Include(x=>x.Product).Where(x => x.Location.LocationId == currentLocationId).ToList();
        }
        #endregion

        #region Locations
        /// <summary>
        /// checks if a location is in the db based on id and name, and adds it if it doesn't already exist
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public bool AttemptAddLocationToDb(Location location)
        {
            //if there is not a location in the db with the same id or name
            if (!LocationIsInDb(location))
            {
                _dbContext.Locations.Add(location);
                _dbContext.SaveChanges();
                return LocationIsInDb(location);
            }
            return false;
        }
        /// <summary>
        /// checks if a location is in the db based on id and name
        /// </summary>
        /// <param name="location"></param>
        /// <returns>bool IsInDb</returns>
        public bool LocationIsInDb(Location location)
        {
            return (_dbContext.Locations.ToList().Where(x => x.LocationId == location.LocationId || x.Name == location.Name).Count() > 0);
            
        }
        /// <summary>
        /// returns whether or not the requested location is in the db based on a guid
        /// </summary>
        /// <param name="locationId"></param>
        /// <returns></returns>
        public bool LocationIsInDb(Guid locationId)
        {
            return LocationIsInDb(GetLocationById(locationId));
        }
        /// <summary>
        /// Returns a location based on the location Id, and returns null if one doesn't exist
        /// </summary>
        /// <param name="locationId"></param>
        /// <returns></returns>
        public Location GetLocationById(Guid locationId)
        {
            return _dbContext.Locations.FirstOrDefault(x => x.LocationId == locationId);
        }
        /// <summary>
        /// returns all of the locations in the db
        /// </summary>
        /// <returns></returns>
        public List<Location> GetAllLocations()
        {
            return _dbContext.Locations.ToList();
        }
        /// <summary>
        /// Returns the first location in the db
        /// </summary>
        /// <returns></returns>
        public Location GetDefautLocation()
        {
            return _dbContext.Locations.FirstOrDefault();
        }
        #endregion

        #region orders

        /// <summary>
        /// gets an open order for the given customer id
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public Order GetOpenCartForCustomer(Guid customerId)
        {
            Order cart = _dbContext.Orders.Include(x=>x.Customer).Where(x=>x.Customer.UserId==customerId).Where(x => x.OrderIsComplete == false).FirstOrDefault();
            if (cart == null)
            {
                cart = new Order()
                {
                    OrderId = Guid.NewGuid(),
                    Date = DateTime.Now,
                    Customer = GetCustomerById(customerId),
                    OrderIsComplete = false
                };
                _dbContext.SaveChanges();
            }
            return cart;
        }

        /// <summary>
        /// returns a list of orderlines for the given order id
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public List<OrderLine> GetOrderLinesForOrder(Guid orderId)
        {
            return _dbContext.OrderLines.Include(x => x.Order).Include(x=>x.Inventory).Where(x => x.Order.OrderId == orderId).ToList();
        }
        /// <summary>
        /// adds order to order line, decrements inventories for order line, and then saves to the db; returns true if successful
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="orderLine"></param>
        /// <returns></returns>
        public bool AddItemToCart(Order cart, OrderLine orderLine)
        {
            if (cart.OrderIsComplete)
            {
                return false;
            }
            orderLine.Inventory.Quantity -= orderLine.Quantity;
            orderLine.Order = cart;
            _dbContext.OrderLines.Add(orderLine);
            _dbContext.SaveChanges();
            return true;
        }

        /// <summary>
        /// complete an order; returns false if there are no items in cart
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        public bool CompleteOrder(Order cart)
        {
            //if cart contains items
            if(OrderContainsOrderLines(cart))
            {
                cart.OrderIsComplete = true;
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }
        /// <summary>
        /// returns whether or not the given order is related to any orderlines in the db
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public bool OrderContainsOrderLines(Order order)
        {
            if(order!=null)
           return _dbContext.OrderLines.Include(x => x.Order).Where(x => x.Order.OrderId == order.OrderId).Count() > 0;
            return false;
           
        }
        /// <summary>
        /// removes an order line from the db; fails if line is not part of an order or if the order is already complete
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public bool RemoveOrderLineFromOrder(OrderLine line)
        {
            if(line == null)
            {
                return false;
            }
            if (line.Order == null)
                return false;
            if (line.Order.OrderIsComplete)
            {
                return false;
            }
            if (_dbContext.OrderLines.Contains(line))
            {
                //restore quantity to inventory
                line.Inventory.Quantity += line.Quantity;
                _dbContext.OrderLines.Remove(line);
                _dbContext.SaveChanges();

                return true;
            }
            return false;
        }
        /// <summary>
        /// returns an order based on the given guid id
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public Order GetOrderById(Guid orderId)
        {
            return _dbContext.Orders.Include(x=>x.Customer).FirstOrDefault(x => x.OrderId == orderId);
        }
        /// <summary>
        /// returns all of the orders in the db based on the given customer guid id
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public List<Order> GetAllCustomerOrders(Guid customerId)
        {
            return _dbContext.Orders.Include(x => x.Customer).Where(x => x.Customer.UserId == customerId).ToList();
        }

        #endregion
        /// <summary>
        /// used to populate the db if it is empty
        /// </summary>
        public void PopulateDb()
        {
            if (_dbContext.Locations.Count() < 1)
            {
                for (int i = 0; i < 3; i++)
                {
                    Location newLocation = new Location()
                    {
                        Name = $"Store + {i}"
                    };
                    AttemptAddLocationToDb(newLocation);
                }
            }
            if (_dbContext.Products.Count() < 1)
            {
                for (int i = 0; i < 3; i++)
                {
                    Product product = new Product()
                    {
                        ProductName = $"Product {i}",
                        Price = i + 1,
                        Description = "Something Special"
                    };
                    AttemptAddProductToDb(product);
                }
            }
            //Product newProduct = new Product()
            //{
            //    ProductName = $"Sandwich",
            //    Price = 4.65,
            //    Description = "A sandwich with ham, lettuce, tomato, and mayo."
            //};
            //AttemptAddProductToDb(newProduct);
            // newProduct = new Product()
            //{
            //    ProductName = $"Hammer",
            //    Price = 18.93,
            //    Description = "A tool that is used to whack various other objects.",
            //};
            //AttemptAddProductToDb(newProduct);
            //restock stores
            if (_dbContext.Inventories.Count() < 1)
            {
                foreach (Location store in _dbContext.Locations.ToList())
                {
                    //var storeInv = _dbContext.Inventories.Include(x=>x.Location).Select(x => x.Location.Equals(store)).ToList();
                    //if (storeInv.Count() < 1)
                // Store.Inventorys.Count()<1){
                    foreach (Product product in _dbContext.Products.ToList())
                    {
                        AttemptAddProductToStore(product, store, 10);
                    }
                }
            }
            //}
        }
    }
}
