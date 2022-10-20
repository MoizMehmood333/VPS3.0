using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using veichlePurchaseAndInventorySystem.Dashboards;
using VPSCache.CacheLayer;
using static System.Net.Mime.MediaTypeNames;

namespace veichlePurchaseAndInventorySystem
{
    public class Program
    {
        private static volatile bool cancelled = false;

        public static void Main(String[] args) {

           
            Screen.MainScreen();
            System.Environment.Exit(0);


        }
    }           
}
