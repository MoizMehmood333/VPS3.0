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
    internal class AdminDashboard
    {
        internal static void AdminScreen()
        {
            int nContinue = 0;
            int option;
            do
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("Welcome to Admin Dashboard");
                    Console.WriteLine("Please select from the Following Options\n" +
                        "\nAccounts Related Options\n" +
                        "\tTo check the Account Details, Enter 1\n" +
                        "\tTo Add a new Account Enter 2\n" +
                        "\tTo check all the accounts, Enter 3\n" +
                        "\tTo add Amount into an Account , Enter 4\n" +
                        "\tTo find all the accounts from a name that we have in the System, Enter 5\n" +
                        "\tTo Add a login Id for an exsisting account, Enter 6\n" +
                        "\nCar Related Options\n" +
                        "\tTo check a Manufacturer cars, Enter 7\n" +
                        "\tTo check the all cars available in the Inventory, Enter 8\n" +
                        "\tTo check the cars under a particular Price Range, Enter 9\n" +
                        "\tTo check Car with the ID, Enter 10\n" +
                        "\tTo make a Car sell, Enter 11\n" +
                        "\tTo make Add a car, Enter 12\n" +
                        "\n\tIf you want to Sign out  press 0");
                    option = Convert.ToInt32(Console.ReadLine());
                    switch (option)
                    {
                        case 0:
                            nContinue = 0;
                            Users.AdminLogin = false;
                            Cache.GetCache.SerializeToXML(false);
                            break;

                        case 1:
                            AccountManager.GetAccountDetails();
                            nContinue = ContinueUsingApplication();
                            break;
                        case 2:
                            AccountManager.AddAccount();
                               
                            nContinue = ContinueUsingApplication();

                            break;

                        case 3:
                            AccountManager.GetAccounts();
                            nContinue = ContinueUsingApplication();

                            break;

                        case 4:
                            AccountManager.AddBalanceToAccount();
                            nContinue = ContinueUsingApplication();

                            break;

                        case 5:
                            AccountManager.GetAllTheAccountsWithName();
                            nContinue = ContinueUsingApplication();

                            break;
                        case 6:
                            AccountManager.AddLoginIDToAnExsistingAccount();
                            nContinue = ContinueUsingApplication();

                            break;

                        case 7:
                            CarManager.GetCarsWithManufacturerName();
                            nContinue = ContinueUsingApplication();

                            break;
                        case 8:
                            CarManager.GetInventory();
                            nContinue = ContinueUsingApplication();

                            break;
                        case 9:
                            CarManager.GetCarWithInPriceRange();
                            nContinue = ContinueUsingApplication();

                            break;
                        case 10:
                            CarManager.CheckCarIDInIventory();
                            nContinue = ContinueUsingApplication();

                            break;
                        case 11:
                            CarManager.PurchaseCar();
                            nContinue = ContinueUsingApplication();

                            break;
                        case 12:
                            CarManager.AddCars();
                            nContinue = ContinueUsingApplication();

                            break;
                        default:
                            Console.WriteLine("Choose a Option between 0 and 12");
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
        public static int ContinueUsingApplication() {
            Console.WriteLine("Do you still want to continue? 0 for No, 1 for yes");
            int nContinue = Convert.ToInt32(Console.ReadLine());
            if (nContinue == 0)
            {
                Users.ClientLoginStatus = false;
                Users.NewRegistration = false;
                Users.UserLoginId = Convert.ToUInt32(null);
                Users.AdminLogin = false;
                Cache.GetCache.SerializeToXML(false);
            }
            else if (nContinue == 1)
            {
            }
            else
            {
                Console.WriteLine("Select a correct option");
                nContinue = 1;
            }
         
            Console.Clear();
            return nContinue;
        }


    }
}
