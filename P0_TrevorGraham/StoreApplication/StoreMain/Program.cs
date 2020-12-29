using System;
using StoreApplication.UI;

namespace StoreApplication
{
    class Program
    {
        // public event EventHandler ProcessExit;
        static void Main(string[] args)
        {
            StoreCLI ui = new StoreCLI();
            ui.Start();
            
        }

    }
}
