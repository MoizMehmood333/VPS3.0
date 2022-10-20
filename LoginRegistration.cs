using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Repository;
using Repository.Repository;
using VPSCache.CacheLayer;

namespace veichlePurchaseAndInventorySystem
{
    public class LoginRegistration
    {
        public static Cache m_cacheInstance = Cache.GetCache;
        public static void LoginUser()
        {
            if (!File.Exists(@"C:\Users\mmoiz\Desktop\Practice\XMLfolder\AllUsers.xml"))
            {
                Console.WriteLine("There are no users present. To Use the application you'll first need to Register.");
                RegisterUser();
                LoginUser();
                return;
            }
            m_cacheInstance.DeSerializeXMLFiles();

            Console.WriteLine("Please Enter the User ID: ");
            uint nUserId = Convert.ToUInt32(Console.ReadLine());

            Console.WriteLine("Please Enter the User Login Password: ");
            string stPassword = Console.ReadLine();



            for (int i = 0; i < m_cacheInstance.LstUsers.Count; i++)
            {

                if (nUserId == m_cacheInstance.LstUsers[i].UserId && stPassword == m_cacheInstance.LstUsers[i].UserPassword)
                {
                    Users.ClientLoginStatus = true;
                    Users.AdminLogin = false;
                    Users.UserLoginId = nUserId;
                    
                    break;
                }
            }


            if (Users.ClientLoginStatus )
            {

                m_cacheInstance.DeSerializeXMLFiles();
            }
            else
            {
                Console.WriteLine("Not a valid User ID or Password");
                Console.WriteLine("If you want to register, Enter 1.\n If you want to Re Enter login Details Enter 2\n If you want to exit, Enter 0 ");
                uint nInput = Convert.ToUInt32(Console.ReadLine());
                if (nInput == 0)
                {
                    Console.WriteLine("Thanks for visiting the application");
                    return;
                }
                else if(nInput == 1)
                {
                    RegisterUser();
                    Console.WriteLine("To Enter the ,Application you will have to Re-Enter the Login Details");
                    LoginUser();

                }
                else if(nInput == 2)
                {
                    LoginUser();
                }
            }
        }


        public static void RegisterUser()
        {
            int nContinue = 0; 
            Console.Clear();
            uint nUserId;
            string stUserPassword;
            string stUserName;
            bool boolNewUser = true;
            bool boolCreateLoginForAccount = false;
            //new code
            do {
                Console.Clear();
                boolCreateLoginForAccount = false;
                boolNewUser = true;
                bool boolInvalidIdDetails = false;
                Console.WriteLine("For Registration please enter the following Details: \n");
                Console.WriteLine("Enter User ID");
                nUserId = Convert.ToUInt32(Console.ReadLine());
                Console.WriteLine("\nEnter User Name");
                stUserName = Console.ReadLine();
                Console.WriteLine("\nEnter User Password");
                stUserPassword = Console.ReadLine();
                
                if (File.Exists(@"C:\Users\mmoiz\Desktop\Practice\XMLfolder\AllUsers.xml") && File.Exists(@"C:\Users\mmoiz\Desktop\Practice\XMLfolder\Accounts.xml"))
                {
                    m_cacheInstance.DeSerializeXMLFiles();
                    for (int i = 0; i < m_cacheInstance.LstUsers.Count; i++)
                    {
                        if (nUserId == m_cacheInstance.LstUsers[i].UserId)
                        {
                            boolNewUser = false;
                            break;
                        }

                    }
                    if (boolNewUser)
                    {
                        Users.NewRegistration = true;
                        m_cacheInstance.DeSerializeXMLFiles();
                        //Users.NewRegistration = false;
                        for (int i = 0; i < m_cacheInstance.LstAccounts.Count; i++)
                        {
                            if (nUserId == m_cacheInstance.LstAccounts[i].CustomerID && stUserName.ToLower() == m_cacheInstance.LstAccounts[i].CustomerName.ToLower())
                            {
                                Console.WriteLine("An account with this ID already exsists!\n " +
                                    "\tTo create a Login Id for Exsisting Account press 1 \n" +
                                    "\tTo  create New Account press 2");

                                int nOption = Convert.ToInt32(Console.ReadLine());
                                switch (nOption)
                                {
                                    case 1:
                                        Console.Clear();
                                        boolCreateLoginForAccount = true;
                                        Users.NewRegistration = false;
                                        nContinue = 0;
                                        break;
                                    case 2:
                                        Console.Clear();
                                        Users.NewRegistration = false;
                                        nContinue = 1;
                                        break;
                                    default:
                                        Console.WriteLine("Enter a correct Option");
                                        nContinue = 1;
                                        Users.NewRegistration = false;
                                        break;


                                }
                                if (boolCreateLoginForAccount)
                                {
                                    m_cacheInstance.LstUsers.Add(new Users { UserId = m_cacheInstance.LstAccounts[i].CustomerID, UserName = m_cacheInstance.LstAccounts[i].CustomerName, UserPassword = stUserPassword });
                                    Users.NewRegistration = false;
                                    m_cacheInstance.SerializeToXML(true);
                                    Console.WriteLine($"\n\n Your have Successfully created the user login ID for account number {m_cacheInstance.LstAccounts[i].CustomerID}");
                                    nContinue = 0;
                                }
                                break;
                            }
                            else if (nUserId == m_cacheInstance.LstAccounts[i].CustomerID && stUserName.ToLower() != m_cacheInstance.LstAccounts[i].CustomerName.ToLower()) {
                                Console.WriteLine("This User ID already exsists with a diffrent user name, kindly Enter a valid user ID to register.");
                                boolInvalidIdDetails = true;
                                nContinue = 1;
                                break;
                            }
                                                       
                        }
                        if (!boolCreateLoginForAccount && !boolInvalidIdDetails)
                        {
                            m_cacheInstance.LstUsers.Add(new Users { UserId = nUserId, UserName = stUserName, UserPassword = stUserPassword });
                            m_cacheInstance.LstAccounts.Add(new Accounts { CustomerID = nUserId, CustomerName = stUserName, Balance = 0, CustomerRating = 0 });
                            m_cacheInstance.SerializeToXML(true);
                            Users.NewRegistration = false;
                            Console.WriteLine("\n\n Your have registered Successfully!");
                            nContinue = 0;
                        }

                    }
                    else if (!boolNewUser) {
                        Console.WriteLine("This User ID already Exists");
                        Console.WriteLine("Please Enter a Valid User ID \nPress Enter to Continue");
                        boolNewUser = true;
                        Console.ReadKey();
                        Console.Clear();
                        nContinue = 1;
                    }

                }
                else if (!File.Exists(@"C:\Users\mmoiz\Desktop\Practice\XMLfolder\AllUsers.xml") && File.Exists(@"C:\Users\mmoiz\Desktop\Practice\XMLfolder\Accounts.xml"))
                {
                    m_cacheInstance.LstUsers = new List<Users>();
                    Users.NewRegistration = true;
                    m_cacheInstance.DeSerializeXMLFiles();
                    Users.NewRegistration = false;
                    for (int i = 0; i < m_cacheInstance.LstAccounts.Count; i++)
                    {
                        if (nUserId == m_cacheInstance.LstAccounts[i].CustomerID && stUserName.ToLower() == m_cacheInstance.LstAccounts[i].CustomerName.ToLower())
                        {
                            Console.WriteLine("An account with this ID already exsists!\n\n " +
                                "\tTo create a Login Id for Exsisting Account press 1 \n" +
                                "\tTo  create New Account press 2");

                            int nOption = Convert.ToInt32(Console.ReadLine());
                            switch (nOption)
                            {
                                case 1:
                                    Console.Clear();
                                    boolCreateLoginForAccount = true;
                                    Users.NewRegistration = false;
                                    nContinue = 0;
                                    break;
                                case 2:
                                    Console.Clear();
                                    RegisterUser();
                                    Users.NewRegistration = false;
                                    nContinue = 0;
                                    break;
                                default:
                                    Console.WriteLine("Enter a correct Option");
                                    nContinue = 1;
                                    Users.NewRegistration = false;
                                    break;


                            }
                            if (boolCreateLoginForAccount && !boolInvalidIdDetails)
                            {
                                m_cacheInstance.LstUsers.Add(new Users { UserId = m_cacheInstance.LstAccounts[i].CustomerID, UserName = m_cacheInstance.LstAccounts[i].CustomerName, UserPassword = stUserPassword });
                                Users.NewRegistration = false;
                                m_cacheInstance.SerializeToXML(true);
                                Console.WriteLine("\n\n Your have registered Successfully!");
                                nContinue = 0;
                            }
                            

                            break;
                        }
                        else if (nUserId == m_cacheInstance.LstAccounts[i].CustomerID && stUserName.ToLower() != m_cacheInstance.LstAccounts[i].CustomerName.ToLower())
                        {
                            Console.WriteLine("This User ID already exsists with a diffrent user name, kindly Enter a valid user ID to register.");
                            nContinue = 1;
                            break;

                        }

                    }
                }
                else if (!File.Exists(@"C:\Users\mmoiz\Desktop\Practice\XMLfolder\AllUsers.xml") && !File.Exists(@"C:\Users\mmoiz\Desktop\Practice\XMLfolder\Accounts.xml")) {
                    m_cacheInstance.LstUsers = new List<Users>();
                    m_cacheInstance.LstUsers.Add(new Users { UserId = nUserId, UserName = stUserName, UserPassword = stUserPassword });
                    m_cacheInstance.LstAccounts = new List<Accounts>();
                    m_cacheInstance.LstAccounts.Add(new Accounts { CustomerID = nUserId, CustomerName = stUserName, Balance = 0, CustomerRating = 0 });
                    Users.NewRegistration = false;
                    m_cacheInstance.SerializeToXML(true);
                    Console.WriteLine("\n\n Your have registered Successfully!");
                    nContinue = 0;
                }
            } while (nContinue == 1);
                       
        }


        public static void AdminLogin() {
            string stAdminID = "MuhammadMoiz";
            string stAdminPassword = "03332308865";
            string stInputAdminID;
            string stInputAdminPassword;
           
            int nContinue = 0;
            do {
                Console.Clear();
                Console.WriteLine("Please Enter the Admin ID: ");
                stInputAdminID = Console.ReadLine();
                Console.WriteLine("Please Enter the Admin Password: ");
                stInputAdminPassword = Console.ReadLine();

                if (stInputAdminID == stAdminID && stInputAdminPassword == stAdminPassword)
                {
                    Users.AdminLogin = true;
                    Users.ClientLoginStatus = false;
                    if (!File.Exists(@"C:\Users\mmoiz\Desktop\Practice\XMLfolder\AllUsers.xml") && !File.Exists(@"C:\Users\mmoiz\Desktop\Practice\XMLfolder\Accounts.xml")) {
                        m_cacheInstance.LstUsers = new List<Users>();
                        m_cacheInstance.LstAccounts = new List<Accounts>();
                        Users.NewRegistration = false;
                        m_cacheInstance.SerializeToXML(true);
                        m_cacheInstance.DeSerializeXMLFiles();

                        nContinue = 0;
                    }
                    else if (!File.Exists(@"C:\Users\mmoiz\Desktop\Practice\XMLfolder\AllUsers.xml") && File.Exists(@"C:\Users\mmoiz\Desktop\Practice\XMLfolder\Accounts.xml")) {
                        m_cacheInstance.LstUsers = new List<Users>();
                        m_cacheInstance.DeSerializeXMLFiles();
                        m_cacheInstance.SerializeToXML(true);

                    }
                    else if (File.Exists(@"C:\Users\mmoiz\Desktop\Practice\XMLfolder\AllUsers.xml") && File.Exists(@"C:\Users\mmoiz\Desktop\Practice\XMLfolder\Accounts.xml")) {
                        m_cacheInstance.DeSerializeXMLFiles();
                    }
                    if (!File.Exists(@"C:\Users\mmoiz\Desktop\Practice\XMLfolder\CarInInventory.xml")) {
                        m_cacheInstance.LstCars = new List<Cars>();
                        m_cacheInstance.DeSerializeXMLFiles();
                        m_cacheInstance.SerializeToXML(false);

                    }
                    nContinue = 0;
                }
                else
                {
                    Console.WriteLine("Entered the wrong Admin ID or Admin Password");
                    Console.WriteLine("Enter 1 to Re-Enter Admin Login Details\nEnter 0 to Exit");
                    int nOption = Convert.ToInt32(Console.ReadLine());

                    if (nOption == 1)
                    {
                        nContinue = 1;
                    }
                    else if (nOption == 0)
                    {
                        nContinue = 0;
                    }

                }
            }
            while (nContinue == 1);

           
        }
        public static void LoginIDForExsistingAccount(int nIndexOfAccountsList ) { 

        }
    }
}
