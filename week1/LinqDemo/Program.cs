using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Player> players = new List<Player>();
            for(int i = 0;i<25;i++){
                players.Add(new Player(){Fname="First"+i,Lname="Last"+i});
            }
            foreach(Player player in players){
                Console.WriteLine($"{player.Fname} {player.Lname}");
            }

            Console.WriteLine("the following names have 2's in them.");
            foreach(Player player in players.Where(x => x.Fname.Contains("2"))){
                Console.WriteLine($"{player.Fname} {player.Lname}");
            };
        }
    }
}
