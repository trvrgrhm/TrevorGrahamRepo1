using Models;
using Models.ViewModels;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class BusinessLogic
    {
        private readonly Repository _repository;
        private readonly Mapper _mapper;

        private int currentUserId;
        private int currentLocationId;

        public BusinessLogic(Repository repository, Mapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        //users
        //public List<CustomerViewModel> GetAllCustomerViewModels()
        //{
        //    List<Customer> allPlayers = _repository.GetAllCustomers();
        //    List<PlayerViewModel> allPlayerViewModels = new List<PlayerViewModel>();
        //    foreach (Player player in allPlayers)
        //    {
        //        var playerViewModel = _mapper.ConvertPlayerToPlayerViewModel(player);
        //        allPlayerViewModels.Add(playerViewModel);
        //    }
        //    return allPlayerViewModels;
        //}

        //products
        public List<InventoryViewModel> GetInventoryModelsForCurrentLocation()
        {
            List<Inventory> locationInventories = _repository.GetLocationInventories(currentLocationId);
            List<InventoryViewModel> inventoryViewModels = new List<InventoryViewModel>();
            foreach (Inventory player in locationInventories)
            {
                var inventoryViewModel = _mapper.ConvertInventoryToInventoryViewModel(player);
                inventoryViewModels.Add(inventoryViewModel);
            }
            return inventoryViewModels;
        }
        //locations
        public List<LocationViewModel> GetAllLocationViewModels()
        {
            List<Location> allLocations = _repository.GetAllLocations();
            List<LocationViewModel> locationViewModels = new List<LocationViewModel>();
            foreach (Location location in allLocations)
            {
                var locationViewModel = _mapper.ConvertLocationToLocationViewModel(location);
                locationViewModels.Add(locationViewModel);
            }
            return locationViewModels;
        }
        public bool SetCurrentLocation(int locationId)
        {
            if (_repository.LocationIsInDb(locationId))
            {
                currentLocationId = locationId;
                return true;
            }
            else return false;
        }
    }
}
