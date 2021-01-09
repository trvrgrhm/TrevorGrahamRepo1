using Models;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class Mapper
    {


        /// <summary>
        /// Converts a customer model to a customer viewmodel
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public CustomerViewModel ConvertCustomerToCustomerViewModel(Customer customer)
        {
            CustomerViewModel customerViewModel = new CustomerViewModel()
            {
                UserId = customer.UserId,
                Username = customer.Username,
                Password = customer.Password,
                Fname = customer.Fname,
                LName = customer.LName,
                DefaultLocationId = customer.DefaultLocation.LocationId
            };
            return customerViewModel;
        }

        /// <summary>
        /// Converts inventory model to inventory viewmodel
        /// </summary>
        /// <param name="inventory"></param>
        /// <returns></returns>
        public InventoryViewModel ConvertInventoryToInventoryViewModel(Inventory inventory)
        {
            InventoryViewModel inventoryViewModel = new InventoryViewModel()
            {
                InventoryId = inventory.InventoryId,
                Quantity = inventory.Quantity,
                //foreign keys
                LocationId = inventory.Location.LocationId,
                ProductId = inventory.Product.ProductId,
                //product stuff
                ProductName = inventory.Product.ProductName,
                Price = inventory.Product.Price,
                Description = inventory.Product.Description

            };
            return inventoryViewModel;
        }

        /// <summary>
        /// Converts location model to location viewmodel
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public LocationViewModel ConvertLocationToLocationViewModel(Location location)
        {
            LocationViewModel locationViewModel = new LocationViewModel()
            {
                LocationId = location.LocationId,
                Name = location.Name

            };
            return locationViewModel;
        }
    }
}
