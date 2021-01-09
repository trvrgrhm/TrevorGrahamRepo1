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
        public List<Customer> GetAllCustomers()
        {
            return _dbContext.Customers.ToList();
        }
        #endregion

        #region Products
        public bool AddProductToDb(Product product)
        {
            //if there is not a product in the db with the same id
            if(!(_dbContext.Products.ToList(). Where(x=>x.ProductId==product.ProductId).Count()>0))
            {
                _dbContext.Products.Add(product);
                return true;
            }
            return false;
        }
        public List<Product> GetAllProducts()
        {
            return _dbContext.Products.ToList();
        }
        public List<Inventory> GetLocationInventories(int currentLocationId)
        {
            List<Inventory> filteredInventories = new List<Inventory>();
            return _dbContext.Inventories.Where(x => x.Location.LocationId == currentLocationId).ToList();
        }

        #endregion

        #region Locations

        public bool AddLocationToDb(Location location)
        {
            //if there is not a product in the db with the same id
            if (!(_dbContext.Locations.ToList().Where(x => x.LocationId == location.ProductId).Count() > 0))
            {
                _dbContext.Products.Add(location);
                return true;
            }
            return false;
        }
        /// <summary>
        /// returns all of the locations in the db
        /// </summary>
        /// <returns></returns>
        public List<Location> GetAllLocations()
        {
            return _dbContext.Locations.ToList();
        }
        public bool DoesLocationExist(int locationId)
        {
            if(_dbContext.Locations.Any(x=>x.LocationId == locationId))
            {
                return true;
            }
            else 
            { 
                return false; 
            }
        }
        public Location GetDefautLocation()
        {
            return _dbContext.Locations.FirstOrDefault();
        }
        #endregion
    }
}
