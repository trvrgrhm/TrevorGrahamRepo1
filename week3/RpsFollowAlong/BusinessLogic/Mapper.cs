using Microsoft.AspNetCore.Http;
using ModelLayer;
using ModelLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class Mapper
    {
        public PlayerViewModel ConvertPlayerToPlayerViewModel(Player player)
        {
            PlayerViewModel playerViewModel = new PlayerViewModel()
            {
                playerId = player.playerId,
                Fname = player.Fname,
                Lname = player.Lname,
                numLosses = player.numLosses,
                numWins = player.numWins,
                JpegStringImage = ConvertByteArrayToString(player.ByteArrayImage)
            };
            return playerViewModel;
        }


        public byte[] ConvertIFormFileToByteArray(IFormFile iFormFile)
        {
            using (var ms = new MemoryStream())
            {
                iFormFile.CopyTo(ms);
                if(ms.Length > 2097152)
                {
                    return null;
                }
                else
                {
                    byte[] a = ms.ToArray();
                    return a;
                }

            }

        }
        private string ConvertByteArrayToString(byte[] byteArrayImage)
        {
            if (byteArrayImage != null)
            {
                string imageBase64 = Convert.ToBase64String(byteArrayImage, 0, byteArrayImage.Length);
                string imageDataURL = string.Format($"data:image/jpg;base64,{imageBase64}");
                return imageDataURL;
            }
            else
            {
                return null;
            }
        }



    }
}
