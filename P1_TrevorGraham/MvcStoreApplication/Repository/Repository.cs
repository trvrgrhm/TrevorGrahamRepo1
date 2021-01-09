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

        #region Customers

        public bool AttemptAddCustomerToDb(Customer customer)
        {
            //if there is not a product in the db with the same id or name
            if (!CustomerIsInDb(customer))
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
        public bool CustomerIsInDb(Customer customer)
        {
            return _dbContext.Customers.ToList().Where(x => x.UserId == customer.UserId||x.Username == customer.Username).Count() > 0;
        }
        public bool CustomerIsInDb(int customerId)
        {
            return CustomerIsInDb(GetCustomerById(customerId));
        }
        public Customer GetCustomerById(int customerId)
        {
            return _dbContext.Customers.FirstOrDefault(x => x.UserId == customerId);
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
        public bool ProductIsInDb(int productId)
        {
            return ProductIsInDb(GetProductById(productId));
        }
        /// <summary>
        /// Returns the product based on the product id or null if one doesn't exist
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public Product GetProductById(int productId)
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
        /// Returns a list of all inventories based on the location id given
        /// </summary>
        /// <param name="currentLocationId"></param>
        /// <returns></returns>
        public List<Inventory> GetLocationInventories(int currentLocationId)
        {
            List<Inventory> filteredInventories = new List<Inventory>();
            return _dbContext.Inventories.Where(x => x.Location.LocationId == currentLocationId).ToList();
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
        public bool LocationIsInDb(int locationId)
        {
            return LocationIsInDb(GetLocationById(locationId));
        }
        /// <summary>
        /// Returns a location based on the location Id, and returns null if one doesn't exist
        /// </summary>
        /// <param name="locationId"></param>
        /// <returns></returns>
        public Location GetLocationById(int locationId)
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
    }
}
