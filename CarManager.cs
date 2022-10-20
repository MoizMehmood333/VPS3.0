using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using Repository.Repository;
using VPSCache.CacheLayer;

namespace veichlePurchaseAndInventorySystem.Manager
{

    internal class CarManager : VeichleBase
    {
        public static Cache m_cacheInstance = Cache.GetCache;
        public static void AddCars()
        {
            uint nCarId;
            string stCarMake;
            string stCarModel;
            uint nCarPrice;
            bool boolCarExsists = false;

            int nContinue = 0;
            do
            {
                Console.WriteLine("To Add a car, Enter the following details: ");
                   
                Console.WriteLine("\nEnter the Car Id:");
                nCarId = Convert.ToUInt32(Console.ReadLine());
                for (int i = 0; i < m_cacheInstance.LstCars.Count; i++) {
                    if (nCarId == m_cacheInstance.LstCars[i].CarID) {
                        boolCarExsists = true;
                        break;
                    }
                }
                if (boolCarExsists)
                {
                    Console.WriteLine("This car already exsists, Please enter a new car ID:");
                    Console.WriteLine("To Re-Enter Enter 1\nTo Exit, Enter 0");
                    nContinue = Convert.ToInt32(Console.ReadLine());

                    if (nContinue == 0)
                    {
                    }
                    else if (nContinue == 1) { 
                        
                    }
                   else
                    {
                       Console.WriteLine("Select a correct option");
                        nContinue = 1;
                    }

                }
                else {
                    Console.WriteLine("Enter the Car Manufacturer Name: ");
                    stCarMake = Console.ReadLine();
                    Console.WriteLine("Enter the Car Model Name: ");
                    stCarModel= Console.ReadLine();
                    Console.WriteLine("Enter the Car Price: ");
                    nCarPrice= Convert.ToUInt32(Console.ReadLine());

                    m_cacheInstance.LstCars.Add(new Cars { CarID = nCarId, CarPrice = nCarId, Make = stCarMake, Model = stCarModel });
                    nContinue = 0;
                }

            } while (nContinue == 1);
        }
        public static void GetInventory()
        {
            foreach (Cars item in m_cacheInstance.LstCars)
            {
                Console.WriteLine($"Car ID: {item.CarID} \n\t Make: {item.Make}, Model: {item.Model} " + $" Price: {item.CarPrice} ");
            }
        }
        //overriding
        public override void CarWithID(uint ID)
        {
            int i = 0;
            bool checkCar = false;
            Console.WriteLine("\n\n");
            for (i = 0; i < m_cacheInstance.LstCars.Count; i++)
            {
                if (ID == m_cacheInstance.LstCars[i].CarID)
                {
                    Console.WriteLine($"Car ID: {m_cacheInstance.LstCars[i].CarID}\n\t   Make: {m_cacheInstance.LstCars[i].Make}, Model: {m_cacheInstance.LstCars[i].Model}, Price: {m_cacheInstance.LstCars[i].CarPrice} ");
                    checkCar = true;
                    break;
                }
            }

            if (!checkCar)
            {
                Console.WriteLine($"We don't have any car in the Inventory, with the ID : {ID} ");
            }
        }
        public static void GetCarsWithManufacturerName()
        {
            //newCode
            Console.Clear();
            Console.WriteLine("Please Enter the Manufacturer you want the cars off ");
            string stName = Console.ReadLine().ToLower();
            Console.WriteLine("\n");
            bool bCheckAvalibility = false;
            if (Enum.IsDefined(typeof(Repository.Repository.CarsWeDealIn), stName))
            {
                
                for (int i = 0; i < m_cacheInstance.LstCars.Count; i++)
                {
                    if (stName.ToLower() == m_cacheInstance.LstCars[i].Make.ToLower())
                    {
                        Console.WriteLine($"Car ID: {m_cacheInstance.LstCars[i].CarID}\n\t   Make: {m_cacheInstance.LstCars[i].Make}, Model: {m_cacheInstance.LstCars[i].Model}, Price: {m_cacheInstance.LstCars[i].CarPrice} ");
                        bCheckAvalibility = true;
                    }
                }
                if (!bCheckAvalibility) {
                    Console.WriteLine($"Currently we are out of Stock for {stName}");
                }

            }
            else {
                Console.WriteLine($"We don't deal in {stName}'s at the moment");
            }

            //
            
        }

        public static void GetCarWithInPriceRange()
        {
            int nPriceFrom, nPriceTo;
            Console.Clear();
            Console.WriteLine("To Check the Cars with in a price Range, Enter the Following Details");
            Console.WriteLine("\tPrice From");
            nPriceFrom = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\tPrice to");
            nPriceTo = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\n\n");

            bool checkAvailability = false;
            foreach (Cars car in m_cacheInstance.LstCars)
            {
                if (car.CarPrice >= nPriceFrom && car.CarPrice <= nPriceTo)
                {
                    checkAvailability = true;
                    Console.WriteLine($" Car ID: {car.CarID},\n\t  Make: {car.Make}, Model: {car.Model}, Price: {car.CarPrice} ");
                }
            }
            if (!checkAvailability)
            {
                Console.WriteLine("Sorry! We don't have any cars in this particular Price Range");
            }
        }

        public static void GetCarsOfParticularModel()
        {
            Console.WriteLine("To Check the Car of a particular Model, enter the following details");
            string stModel = Console.ReadLine().ToLower();
            bool checkAvailability = false;
            Console.WriteLine("\n\n");
            foreach (Cars car in m_cacheInstance.LstCars)
            {
                if (stModel.ToLower() == car.Model.ToLower())
                {
                    checkAvailability = true;
                    Console.WriteLine($" Car ID: {car.CarID},\n\t  Make: {car.Make}, Model: {car.Model}, Price: {car.CarPrice} ");
                }
            }
            if (!checkAvailability)
            {
                Console.WriteLine($"Sorry! We don't have {stModel} in our Inventory.");
            }
        }

        public static void PurchaseCar()
        {
            Console.WriteLine("Enter the Customer ID that wants to purchase the Car: ");
            uint nCustomerID = Convert.ToUInt32(Console.ReadLine()); 

            Console.WriteLine("Enter the Car ID that customer wants to purchase: ");
            uint nCarId = Convert.ToUInt32(Console.ReadLine());



            int nCustomerIndex = -1;
            int nCarIndex = -1;

            for (int i = 0; i < m_cacheInstance.LstCars.Count; i++)
            {
                if (nCarId == m_cacheInstance.LstCars[i].CarID)
                {
                    nCarIndex = i;
                    break;
                }
            }
            for (int i = 0; i < m_cacheInstance.LstAccounts.Count; i++)
            {
                if (nCustomerID == m_cacheInstance.LstAccounts[i].CustomerID)
                {
                    nCustomerIndex = i;
                    break;
                }
            }
            if (nCustomerIndex == -1 || nCarIndex == -1)
            {
                Console.WriteLine("Enter Valid Customer ID" + " or Car ID");
            }
            else
            {
                if (m_cacheInstance.LstCars[nCarIndex].CarPrice <= m_cacheInstance.LstAccounts[nCustomerIndex].Balance)
                {
                    m_cacheInstance.LstAccounts[nCustomerIndex].Balance -= m_cacheInstance.LstCars[nCarIndex].CarPrice;
                    m_cacheInstance.LstCars.RemoveAt(nCarIndex);
                    Console.WriteLine($"Remaining balance for Customer ID:{m_cacheInstance.LstAccounts[nCustomerIndex].CustomerID} is: {m_cacheInstance.LstAccounts[nCustomerIndex].Balance}");
                }
                else
                {
                    Console.WriteLine("You have insufficient Balance");
                }
            }
        }
        public static void CheckCarIDInIventory() {
            int nContinue = 0;
            int nCarId;
            bool boolCheck = false;
            do {
                Console.WriteLine("Enter the car ID you are looking for: ");
                nCarId = Convert.ToInt32(Console.ReadLine());
                for (int i = 0; i < m_cacheInstance.LstCars.Count; i++) {
                    if (nCarId == m_cacheInstance.LstCars[i].CarID) {
                        Console.WriteLine("\nThe Car Details are:\n" +
                            $"\tCar ID: {m_cacheInstance.LstCars[i].CarID}\n" +
                            $"\tCar Make: {m_cacheInstance.LstCars[i].Make}\n" +
                            $"\tCar Model: {m_cacheInstance.LstCars[i].Model}\n" +
                            $"\tCar Price: {m_cacheInstance.LstCars[i].CarPrice}\n");
                        boolCheck = true;
                        nContinue = 0;
                     }
                }
                if (!boolCheck) {
                    Console.WriteLine("No car with this ID number exsists in the inventory\n" +
                        "\tTo Re-Enter the ID, Enter 1\n" +
                        "\tTo Exit, Enter 0");

                    nContinue = Convert.ToInt32(Console.ReadLine());
                    if (nContinue == 0)
                    {
                    }
                    else if (nContinue == 1)
                    {

                    }
                    else
                    {
                        Console.WriteLine("Select a correct option");
                        nContinue = 1;
                    }
                }
            } while (nContinue == 1);
            

        }
    }
}
