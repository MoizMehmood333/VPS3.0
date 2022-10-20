using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using Repository.Repository;
using veichlePurchaseAndInventorySystem.Manager;
using VPSCache.CacheLayer;

namespace veichlePurchaseAndInventorySystem.Dashboards
{
    internal class ClientDashboard
    {
        internal static void ClientScreen()
        {
            int nContinue = 0;
            int option;
            do
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("Welcome to Client Dashboard");
                    Console.WriteLine("Please select from the Following Options\n" +
                        "\tTo check the Account Balance, Enter 1\n" +
                        "\tTo check a Manufacturer cars, Enter 2\n" +
                        "\tTo check the all cars available in the Inventory, Enter 3\n" +
                        "\tTo check the cars under a particular Price Range, Enter 4\n" +
                        "\tTo check the cars of a model, Enter 5\n" +
                        "\n\tIf you want to exit press 0");
                    option = Convert.ToInt32(Console.ReadLine());
                    switch (option)
                    {
                        case 0:
                            Users.ClientLoginStatus = false;
                            Users.NewRegistration = false;
                            Users.UserLoginId = Convert.ToUInt32(null);
                            nContinue = 0;
                            Cache.GetCache.SerializeToXML(false);
                            break;

                        case 1:
                            AccountManager.CheckAccountBalance(Users.UserLoginId);
                            Console.WriteLine("");
                            //add continue condition if a user wans to continue or not! 

                            nContinue = ContinueUsingApplication();
                            break;
                        case 2:
                            CarManager.GetCarsWithManufacturerName();
                            //add continue condition if a user wans to continue or not! 
                            nContinue = ContinueUsingApplication();

                            break;

                        case 3:
                            CarManager.GetInventory();
                            //add continue condition if a user wans to continue or not! 
                            nContinue = ContinueUsingApplication();

                            break;

                        case 4:
                            CarManager.GetCarWithInPriceRange();
                            //add continue condition if a user wans to continue or not! 
                            nContinue = ContinueUsingApplication();

                            break;

                        case 5:
                            CarManager.GetCarsOfParticularModel();
                            //add continue condition if a user wans to continue or not! 
                            nContinue = ContinueUsingApplication();

                            break;

                        default:
                            Console.WriteLine("Choose a Option between 0 and 5");
                            nContinue = 1;
                            break;
                    }

                }
                catch (FormatException)
                {
                    Console.WriteLine("Enter a correct choice! Input should be in numbers");
                    nContinue = ContinueUsingApplication();
                    if (nContinue == 0)
                    {
                        return;
                    }
                    nContinue = 1;

                }
            } while (nContinue == 1);
        }
        protected static int ContinueUsingApplication()
        {
            Console.WriteLine("Do you still want to continue? 0 for No, 1 for yes");
            int nContinue = Convert.ToInt32(Console.ReadLine());
            if (nContinue == 0) {
                Users.ClientLoginStatus = false;
                Users.NewRegistration = false;
                Users.UserLoginId = Convert.ToUInt32(null);
                Cache.GetCache.SerializeToXML(false);
            }
            else if (nContinue == 1) {
            }
            else
            {
                Console.WriteLine("Select a correct option");
                nContinue = 1;
            }
            return nContinue;
        }

    }
}
