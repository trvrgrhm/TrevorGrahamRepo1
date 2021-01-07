using Microsoft.Extensions.Logging;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer
{
    public class Repository
    {
        private readonly RpsDbContext _dbContext;

        private ILogger<Repository> _logger;
        public Repository(RpsDbContext rpsDbContext, ILogger<Repository> logger)
        {
            _dbContext = rpsDbContext;
            _logger = logger;
        }

        public Player LoginPlayer(Player player)
        {
            //check if player is in db
            Player player1 = _dbContext.players.FirstOrDefault(x => x.Fname == player.Fname && x.Lname == player.Lname);
            
            // if player is not in db, create a new player and add them to db
            if(player1 == null)
            {
                player1 = new Player()
                {
                    Fname = player.Fname,
                    Lname = player.Lname
                };
                _dbContext.players.Add(player1);
                _dbContext.SaveChanges();
                //check if player is in db
                try
                {
                    Player player2 = _dbContext.players.FirstOrDefault(x => x.playerId == player1.playerId);
                    return player2;
                }
                catch(ArgumentNullException ex)
                {
                    _logger.LogInformation($"Saving a player to the db threw an error, {ex}");
                }
            }


            //return the player
            return player1;
        }

        /// <summary>
        /// takes a modified player, adds it to the database, and returns the player from the database
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public Player EditPlayer(Player player)
        {
            //search db for player
            Player player1 = GetPlayerById(player.playerId);
            //transfer new values

            player1.Fname = player.Fname;
            player1.Lname = player.Lname;
            player1.numLosses = player.numLosses;
            player1.numWins = player.numWins;
            player1.ByteArrayImage = player.ByteArrayImage;

            //save changes
            _dbContext.SaveChanges();
            //search player again to verify that player is in db
            Player player2 = GetPlayerById(player.playerId);
            //return edited db
            return player2;
        }

        public Player GetPlayerById(Guid playerId)
        {
            Player player = _dbContext.players.FirstOrDefault(x => x.playerId == playerId);
            return player;
        }

        /// <summary>
        /// returns a list of all of the players in the db
        /// </summary>
        /// <returns></returns>
        public List<Player> GetAllPlayers()
        {
            return _dbContext.players.ToList();
        }


    }
}
