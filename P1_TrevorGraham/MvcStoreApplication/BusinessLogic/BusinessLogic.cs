
using Microsoft.AspNetCore.Http;
using Models;
using Models.ViewModels;
using Newtonsoft.Json;
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        //private Guid? currentUserId = null;
        //private Guid? currentLocationId = null;

        private const string currentUserKey = "current-user-key";
        private const string currentLocationKey = "current-location-key";
        //private const string notGuidValue = "not guid";


        public BusinessLogic(Repository repository, Mapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        #region users

        /// <summary>
        /// creates a customer in the db from the given customerViewModel
        /// </summary>
        /// <param name="customerViewModel"></param>
        /// <returns></returns>
        public bool CreateNewCustomer(CustomerViewModel customerViewModel)
        {
            if (UsernameExists(customerViewModel.Username))
            {
                return false;
            }

            Customer customer = _mapper.ConvertCustomerViewModelToCustomer(customerViewModel);
            if (_repository.AttemptAddCustomerToDb(customer))
            {
                if (AttemptSignIn(customerViewModel.Username, customerViewModel.Password))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// requests the repository to create a new Admin in the db based on a viewmodel
        /// </summary>
        /// <param name="adminViewModel"></param>
        /// <returns></returns>
        public bool CreateNewAdministrator(AdministratorViewModel adminViewModel)
        {
            if (UsernameExists(adminViewModel.Username))
            {
                return false;
            }
            Administrator admin = _mapper.ConvertAdministratorViewModelToAdministrator(adminViewModel);
            return _repository.AttemptAddAdministratorToDb(admin);
        }
        /// <summary>
        /// attempts to login based on a username and password, and if successful, sets the session values to the correct values
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool AttemptSignIn(string username, string password)
        {
            IUser user = _repository.AttemptSignInWithUsernameAndPassword(username, password);
            if (user == null)
            {
                return false;
            }
            //assign current location
            else if (user is Customer)
            {
                //var customer = _repository.GetCustomerById(user.UserId);

                //currentLocationId = _repository.GetCustomerById(user.UserId).DefaultLocation.LocationId;
                SetCurrentLocation(_repository.GetCustomerById(user.UserId).DefaultLocation.LocationId);
            }
            else
            {
                //currentLocationId = _repository.GetDefautLocation().LocationId;
                SetCurrentLocation(_repository.GetDefautLocation().LocationId);
            }
            //assign current user
            //currentUserId = user.UserId;
            SessionAssignCurrentUser(user.UserId);
            return true;
        }
        /// <summary>
        /// removes the current user's values from the session
        /// </summary>
        public void SignOut()
        {
            SessionClearCurrentUser();
            SessionClearCurrentLocation();
        }
        /// <summary>
        /// This method is used to check if a user is signed into the session
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool UserIsSignedIn()
        {
            //return currentUserId != null;
            return SessionGetCurrentUser() != null;
        }
        /// <summary>
        /// returns whether or not the username exists in the db
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool UsernameExists(string username)
        {
            return _repository.UsernameAlreadyExists(username);
        }

        /// <summary>
        /// Returns true if the current user is a customer and false if not
        /// </summary>
        /// <returns></returns>
        public bool CurrentUserIsCustomer()
        {
            if (UserIsSignedIn())
                //return _repository.UserIsCustomer((Guid)currentUserId);
                return _repository.UserIsCustomer((Guid)GetCurrentCustomer());
            else return false;
        }
        /// <summary>
        /// returns the session value for the current customer
        /// </summary>
        /// <returns></returns>
        public Guid? GetCurrentCustomer()
        {
            //return currentUserId;
            return SessionGetCurrentUser();
        }
        /// <summary>
        /// Returns true if the current user is an administrator and false if not
        /// </summary>
        /// <returns></returns>
        public bool CurrentUserIsAdministrator()
        {
            if (UserIsSignedIn())
                //return _repository.UserIsAdministrator((Guid)currentUserId);
                return _repository.UserIsAdministrator((Guid)SessionGetCurrentUser());
            else return false;
        }
        /// <summary>
        /// Returns a viewmodel for the requested customer based on id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CustomerViewModel GetCustomerViewModel(Guid id)
        {
            return _mapper.ConvertCustomerToCustomerViewModel(_repository.GetCustomerById(id));
        }
        /// <summary>
        /// Returns a viewmodel for the requested admin based on id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AdministratorViewModel GetAdministratorViewModel(Guid id)
        {
            return _mapper.ConvertAdministratorToAdministratorViewModel(_repository.GetAdministratorById(id));
        }

        /// <summary>
        /// Returns a list of viewmodels for every customer in the db
        /// </summary>
        /// <returns></returns>
        public List<CustomerViewModel> GetAllCustomerViewModels()
        {
            List<Customer> allCustomers = _repository.GetAllCustomers();
            List<CustomerViewModel> allCustomerViewModels = new List<CustomerViewModel>();
            foreach (Customer cust in allCustomers)
            {
                var customerViewModel = _mapper.ConvertCustomerToCustomerViewModel(cust);
                allCustomerViewModels.Add(customerViewModel);
            }
            return allCustomerViewModels;
        }
        /// <summary>
        /// creates viewmodels for all admins based on admins in the db
        /// </summary>
        /// <returns></returns>
        public List<AdministratorViewModel> GetAllAdminViewModels()
        {
            List<Administrator> allCustomers = _repository.GetAllAdministrators();
            List<AdministratorViewModel> allCustomerViewModels = new List<AdministratorViewModel>();
            foreach (Administrator admin in allCustomers)
            {
                var adminViewModel = _mapper.ConvertAdministratorToAdministratorViewModel(admin);
                allCustomerViewModels.Add(adminViewModel);
            }
            return allCustomerViewModels;
        }
        #endregion

        #region products
        //products
        public List<InventoryViewModel> GetInventoryModelsForLocation(Guid locationId)
        {
            List<Inventory> locationInventories = _repository.GetLocationInventories(locationId);
            List<InventoryViewModel> inventoryViewModels = new List<InventoryViewModel>();
            foreach (Inventory inventory in locationInventories)
            {
                var inventoryViewModel = _mapper.ConvertInventoryToInventoryViewModel(inventory);
                inventoryViewModels.Add(inventoryViewModel);
            }
            return inventoryViewModels;
        }
        public List<InventoryViewModel> GetInventoryModelsForCurrentLocation()
        {
            //if (currentLocationId != null)
            if (SessionGetCurrentLocation() != null)
                //return GetInventoryModelsForLocation((Guid)currentLocationId);
                return GetInventoryModelsForLocation((Guid)SessionGetCurrentLocation());
            else return GetInventoryModelsForLocation(_repository.GetDefautLocation().LocationId);
        }
        /// <summary>
        /// Returns a list of viewmodels based on all of the products in the db
        /// </summary>
        /// <returns></returns>
        public List<ProductViewModel> GetAllProductViewModels()
        {
            var allProducts = _repository.GetAllProducts();
            List<ProductViewModel> viewModels = new List<ProductViewModel>();
            foreach (Product item in allProducts)
            {
                viewModels.Add(_mapper.ConvertProductToProductViewModel(item));
            }
            return viewModels;
        }
        #endregion

        #region locations
        /// <summary>
        /// attempts to add a product to the inventory, and returns whether or not it was successful
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public bool AddProductToInventory(InventoryViewModel viewModel)
        {
            return _repository.AttemptAddProductToStore(
                _repository.GetProductById(viewModel.ProductId),
                _repository.GetLocationById(viewModel.LocationId),
                viewModel.Quantity);
        }
        /// <summary>
        /// returns viewmodels for inventory lines for the given location guid id
        /// </summary>
        /// <param name="locationId"></param>
        /// <returns></returns>
        public LocationWithInventoriesViewModel GetInventoryDetails(Guid locationId)
        {
            Location location = _repository.GetLocationById(locationId);
            if (location == null)
            {
                location = _repository.GetDefautLocation();
            }
            List<InventoryViewModel> inventory = GetInventoryModelsForLocation(locationId);
            LocationWithInventoriesViewModel locationInventory = new LocationWithInventoriesViewModel()
            {
                LocationId = location.LocationId,
                Name = location.Name,
                InventoryItems = inventory
            };
            return locationInventory;
        }
        /// <summary>
        /// returns a viewmodel for the request location guid id
        /// </summary>
        /// <param name="locationId"></param>
        /// <returns></returns>
        public LocationViewModel GetLocationViewModel(Guid locationId)
        {
            return _mapper.ConvertLocationToLocationViewModel(_repository.GetLocationById(locationId));
        }
        /// <summary>
        /// returns a list of viewmodels for all of the locations in the db
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// attempts to set the current location in the session and returns whether or not it was successful
        /// </summary>
        /// <param name="locationId"></param>
        /// <returns></returns>
        public bool SetCurrentLocation(Guid locationId)
        {
            if (_repository.LocationIsInDb(locationId))
            {
                //currentLocationId = locationId;
                if (SessionAssignCurrentLocation(locationId))
                    return true;
            }
            //if unsuccessful
            return false;
        }
        /// <summary>
        /// returns the current location from the session, if unsuccessful, returns the default location from the db
        /// </summary>
        /// <returns></returns>
        public Guid GetCurrentLocation()
        {
            if (SessionGetCurrentLocation() != null)
            {
                return (Guid)SessionGetCurrentLocation();
            }
            else
            {
                return _repository.GetDefautLocation().LocationId;
            }
        }
        /// <summary>
        /// attempts to create a new location in the db based on a viewmodel and returns whether or not it was successful
        /// </summary>
        /// <param name="newLocationViewModel"></param>
        /// <returns></returns>
        public bool CreateNewLocation(LocationViewModel newLocationViewModel)
        {
            Location newLocation = new Location()
            {
                Name = newLocationViewModel.Name
            };
            return _repository.AttemptAddLocationToDb(newLocation);
        }
        #endregion

        #region orders
        /// <summary>
        /// returns an order viewmodel based on a given inventory viewmodel that it is being created from
        /// </summary>
        /// <param name="inventoryViewModel"></param>
        /// <returns></returns>
        public OrderLineViewModel CreateOrderLine(InventoryViewModel inventoryViewModel)
        {
            OrderLineViewModel viewModel = new OrderLineViewModel()
            {
                InventoryId = inventoryViewModel.InventoryId,
                Price = inventoryViewModel.Price,
                ProductName = inventoryViewModel.ProductName,
                Quantity = 0,
                StoreName = inventoryViewModel.LocationName,
                TotalPrice = 0,
            };
            return viewModel;
        }
        /// <summary>
        /// attempts to add a line to a cart based on a given orderline viewmodel and returns whether or not it was successful
        /// </summary>
        /// <param name="orderLineViewModel"></param>
        /// <returns></returns>
        public bool AddToCart(OrderLineViewModel orderLineViewModel)
        {
            if (!CurrentUserIsCustomer())
            {
                return false;
            }
            if (orderLineViewModel == null)
            {
                return false;
            }
            //check if orderline is empty
            //check if order amount is greater than inventory amount
            var dbInv = _repository.GetInventoryById(orderLineViewModel.InventoryId);
            if (orderLineViewModel.Quantity <= 0 || orderLineViewModel.Quantity > dbInv.Quantity)
            {
                return false;
            }
            //get current cart and line
            var cart = _repository.GetOpenCartForCustomer((Guid)SessionGetCurrentUser());
            var line = _mapper.ConvertOrderLineViewModelToOrderLine(orderLineViewModel);
            //try to add to cart
            return _repository.AddItemToCart(cart, line);

            //return false;

        }
        /// <summary>
        /// returns an order viewmodel based on the given order id
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public OrderViewModel GetOrderViewModel(Guid orderId)
        {
            //if (!CurrentUserIsCustomer()) { return null; }
            //if (SessionGetCurrentUser() != null) { return null; }
            var order = _repository.GetOrderById(orderId);
            //var loc = (Guid)SessionGetCurrentLocation();
            var cust = (Guid)SessionGetCurrentUser();
            //var cart = _repository.GetOpenCartForCustomer(cust);
            var cartLines = _repository.GetOrderLinesForOrder(order.OrderId);
            var cartLineViewModels = new List<OrderLineViewModel>();
            double total = 0;
            foreach (OrderLine line in cartLines)
            {
                var viewLine = _mapper.ConvertOrderLineToOrderLineViewModel(line);
                total += viewLine.TotalPrice;
                cartLineViewModels.Add(viewLine);
            }

            OrderViewModel viewModel = new OrderViewModel()
            {
                Date = DateTime.Now,
                CustomerId = cust,
                CustomerName = _repository.GetCustomerById(cust).Fname,
                OrderId = order.OrderId,
                //StoreName = _repository.GetLocationById(loc).Name,
                TotalPrice = total,
                orderLines = cartLineViewModels

            };
            if (cartLines.Count() > 0)
            {
                viewModel.StoreName = cartLines[0].Inventory.Location.Name;
            }
            return viewModel;
        }
        /// <summary>
        /// returns a viewmodel based on the currently open cart if a user is logged in
        /// </summary>
        /// <returns></returns>
        public OrderViewModel GetCart()
        {
            if (!CurrentUserIsCustomer()) { return null; }
            //if (SessionGetCurrentUser() != null) { return null; }
            var loc = (Guid)SessionGetCurrentLocation();
            var cust = (Guid)SessionGetCurrentUser();
            var cart = _repository.GetOpenCartForCustomer(cust);
            var cartLines = _repository.GetOrderLinesForOrder(cart.OrderId);
            var cartLineViewModels = new List<OrderLineViewModel>();
            double total = 0;
            foreach (OrderLine line in cartLines)
            {
                var viewLine = _mapper.ConvertOrderLineToOrderLineViewModel(line);
                total += viewLine.TotalPrice;
                cartLineViewModels.Add(viewLine);
            }

            OrderViewModel viewModel = new OrderViewModel()
            {
                Date = DateTime.Now,
                CustomerId = cust,
                CustomerName = _repository.GetCustomerById(cust).Fname,
                OrderId = cart.OrderId,
                StoreName = _repository.GetLocationById(loc).Name,
                TotalPrice = total,
                orderLines = cartLineViewModels

            };
            return viewModel;
        }
        /// <summary>
        /// returns whether or not the given quantity is greater than the given inventory viewmodel based on the db
        /// </summary>
        /// <param name="quantity"></param>
        /// <param name="inventoryViewModel"></param>
        /// <returns></returns>
        public bool AmountIsGreaterThanInventory(int quantity, InventoryViewModel inventoryViewModel)
        {
            if (inventoryViewModel != null)
            {
                var dbInv = _repository.GetInventoryById(inventoryViewModel.InventoryId);
                if (dbInv != null)
                {
                    if (quantity < dbInv.Quantity)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// returns order viewmodels for every order in the db for the currently logged in user
        /// </summary>
        /// <returns></returns>
        public List<OrderViewModel> GetAllOrderViewModels()
        {
            if (!CurrentUserIsCustomer()) { return null; }
            var orders = _repository.GetAllCustomerOrders((Guid)SessionGetCurrentUser());
            var viewModels = new List<OrderViewModel>();
            foreach (Order order in orders)
            {
                var viewModel = GetOrderViewModel(order.OrderId);
                viewModels.Add(viewModel);
            }
            return viewModels;
        }
        /// <summary>
        /// attempts to checkout and returns whether or not it was successful
        /// </summary>
        /// <returns></returns>
        public bool Checkout()
        {
            if (CurrentUserIsCustomer())
            {
                var cart = _repository.GetOpenCartForCustomer((Guid)SessionGetCurrentUser());
                return _repository.CompleteOrder(cart);

            }
            return false;
        }

        #endregion
        //session----------------------------------------------
        #region session
        /// <summary>
        /// Saves the given guid as the current user in the session; returns true if successful
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private bool SessionAssignCurrentUser(Guid userId)
        {
            var id = JsonConvert.SerializeObject(userId);
            _session.SetString(currentUserKey, id);
            if (SessionGetCurrentUser() != null)
                return true;
            return false;
        }
        /// <summary>
        /// Saves the given guid as the current location in the session; returns true if successful
        /// </summary>
        /// <param name="locId"></param>
        /// <returns></returns>
        private bool SessionAssignCurrentLocation(Guid locId)
        {
            var id = JsonConvert.SerializeObject(locId);
            _session.SetString(currentLocationKey, id);
            if (SessionGetCurrentLocation() != null)
                return true;
            return false;
        }
        /// <summary>
        /// sets the current user in session to an invalid value, so getcurrentuser will return null
        /// </summary>
        /// <returns></returns>
        private bool SessionClearCurrentUser()
        {
            _session.SetString(currentUserKey, "-");
            if (SessionGetCurrentUser() == null)
                return true;
            return false;
        }
        /// <summary>
        /// sets the curren location in session to an invalid value, so getcurrentuser will return null
        /// </summary>
        /// <returns></returns>
        private bool SessionClearCurrentLocation()
        {
            _session.SetString(currentLocationKey, "-");
            if (SessionGetCurrentUser() == null)
                return true;
            return false;
        }

        /// <summary>
        /// returns the guid of the current user in the session; returns null if a guid is not saved
        /// </summary>
        /// <returns></returns>
        private Guid? SessionGetCurrentUser()
        {
            try
            {
                var id = JsonConvert.DeserializeObject<Guid>(_session.GetString(currentUserKey));
                return id;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// returns the guid of the current location in the session; retunrs null if a guid is not saved
        /// </summary>
        /// <returns></returns>
        private Guid? SessionGetCurrentLocation()
        {
            try
            {
                var id = JsonConvert.DeserializeObject<Guid>(_session.GetString(currentLocationKey));
                return id;
            }
            catch
            {
                return null;
            }
        }
        #endregion


        //--------------------------------------------------------------Remember to delete this method please-----------------------------------------------------------
        /// <summary>
        /// used to populate db; shouldn't need this more than once
        /// </summary>
        public void TempMethodDeleteMePlease()
        {
            _repository.PopulateDb();
        }


    }
}
