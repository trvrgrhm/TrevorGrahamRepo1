using ModelLayer;
using ModelLayer.ViewModels;
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

        public BusinessLogic(Repository repository, Mapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// takes a LoginPlayerView model instance and retunrs a PlayerViewModel instance
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public PlayerViewModel LoginPlayer(LoginPlayerViewModel loginPlayerViewModel)
        {
            //have all logic confined to this business layer
            Player player = new Player()
            {
                Fname = loginPlayerViewModel.Fname,
                Lname = loginPlayerViewModel.Lname
            };
            Player player1 = _repository.LoginPlayer(player);

            //convert Player to PlayerViewModel

            PlayerViewModel playerViewModel = _mapper.ConvertPlayerToPlayerViewModel(player1);

            return playerViewModel;
        }
        public PlayerViewModel GetPlayerViewById(Guid playerId)
        {
            //call method in repository to get player by id
            Player player = _repository.GetPlayerById(playerId);
            PlayerViewModel playerViewModel  = _mapper.ConvertPlayerToPlayerViewModel(player);
            return playerViewModel;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="playerViewModel"></param>
        /// <returns></returns>
        public PlayerViewModel EditedPlayer(PlayerViewModel playerViewModel)
        {
            //get instance of a player and insert the changed details
            Player player = _repository.GetPlayerById(playerViewModel.playerId);
            player.Fname = playerViewModel.Fname;
            player.Lname = playerViewModel.Lname;
            player.numLosses = playerViewModel.numLosses;
            player.numWins = playerViewModel.numWins;
            //call mapper method to convert iformfile to byte[]
            if(playerViewModel.IFormFileImage != null)
            player.ByteArrayImage = _mapper.ConvertIFormFileToByteArray(playerViewModel.IFormFileImage);
            //create a player and insert the changed details; make sure to convert the byte[] to a jpg string image
            Player player1 = _repository.EditPlayer(player);

            //convert player back to view model
            PlayerViewModel playerViewModel1 = _mapper.ConvertPlayerToPlayerViewModel(player1);

            return playerViewModel1;
        }

        public bool DeletePlayer(Guid playerId)
        {
            return _repository.DeletePlayer(playerId);
        }

        /// <summary>
        /// returns a list of playerviewmodels for every player in the db
        /// </summary>
        /// <returns></returns>
        public List<PlayerViewModel> GetAllPlayerViewModels()
        {
            List<Player> allPlayers = _repository.GetAllPlayers();
            List<PlayerViewModel> allPlayerViewModels = new List<PlayerViewModel>();
            foreach(Player player in allPlayers)
            {
                var playerViewModel = _mapper.ConvertPlayerToPlayerViewModel(player);
                allPlayerViewModels.Add(playerViewModel);
            }
            return allPlayerViewModels;
        }
    }
}
