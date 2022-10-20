using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VPSCache.CacheLayer;

namespace veichlePurchaseAndInventorySystem.Dashboards
{
    public class Screen
    {
        public static void MainScreen()
        {

            int nContinue = 0;
            uint nOption = 0;

            Console.WriteLine("Welcome To Car Purchase and Information System");
            Console.WriteLine("________________________________________________\n\n");
            do
            {
            
                try
                {
                    Console.WriteLine("Are you Client or an Admin? \nFor Admin Press 1\nFor Client Press 2");
                    nOption = Convert.ToUInt32(Console.ReadLine());
                    
                    
                    if (nOption >= 1 && nOption <= 2)
                    {
                        switch (nOption)
                        {
                            case 1:
                                LoginRegistration.AdminLogin();
                                Console.Clear();
                                break;
                            case 2:
                                Console.WriteLine("Do you want to Login or Register? \nTo Login Press 1 \nTo Register Enter 2");
                                int nLoginRegister = Convert.ToInt32(Console.ReadLine());
                                if (nLoginRegister == 1)
                                {
                                    LoginRegistration.LoginUser();

                                }
                                else
                                {
                                    LoginRegistration.RegisterUser();
                                    Console.WriteLine("To  use the system please Re-Enter the login Details: ");
                                    LoginRegistration.LoginUser();
                                }
                                break;
                        }

                        //Admin Screen

                        if (Users.AdminLogin)
                        {
                            //add  admin screen logic here.

                            AdminDashboard.AdminScreen();

                            //End the admin screen logic.

                            Users.AdminLogin = false;
                            Console.WriteLine("Do you Want to close the application? \n If \"Yes\" Enter 0\n If \"No\" Enter 1");
                            nContinue = Convert.ToInt32(Console.ReadLine());
                            Console.Clear();
                        }

                        //Client Screen

                        else if (Users.ClientLoginStatus)
                        {
                            //add client Screen logic here. 

                            ClientDashboard.ClientScreen();


                            //logic ends here..

                            //nulling the Users.LoginId Value so that it can be updated in the next Login. 
                            Users.ClientLoginStatus = false;
                            Users.UserLoginId = Convert.ToUInt32(null);
                            Console.WriteLine("Do you Want to close the application ? \n If \"Yes\" Enter 0\n If \"No\" Enter 1");
                            nContinue = Convert.ToInt32(Console.ReadLine());
                            Console.Clear();
                        }
                        else
                        {
                            Console.WriteLine("Thanks for using the Application.");
                            Console.ReadKey();
                        }

                    }
                    else
                    {
                        Console.WriteLine("Please Enter a valid Choice.");
                        Console.WriteLine("Do you Want to close the application? \n If \"Yes\" Enter 0\n If \"No\" Enter 1");
                        nContinue = Convert.ToInt32(Console.ReadLine());
                        Console.Clear();
                    }

                }
                catch (Exception e)

                {
                    Console.WriteLine("An error occured!");
                    Console.WriteLine("Please Enter a valid input !! \n");
                    Console.WriteLine("Do you Want to close the application? \n If \"Yes\" Enter 0\n If \"No\" Enter 1");
                    nContinue = Convert.ToInt32(Console.ReadLine());
                }


            } while (nContinue == 1);



        }
    }

}