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
        /// Converts inventory model to inventory view model
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
