using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using Repository;
using Repository.Repository;
using VPSCache.CacheLayer;

namespace veichlePurchaseAndInventorySystem.Manager
{
    internal class AccountManager : IAccounts
    {
        public static Cache m_cacheInstance = Cache.GetCache;
        public static void GetAccounts()
        {

            foreach (Accounts account in m_cacheInstance.LstAccounts)
            {
                Console.WriteLine($"Account ID: {account.CustomerID}\n\tCustomer Name: {account.CustomerName}, Account Balance:{account.Balance}");
            }
        }


        public static void CheckAccountBalance(uint nUserId)
        {
            bool boolCheckAccountExistence = false; 
            for (int i = 0; i < m_cacheInstance.LstAccounts.Count; i++)
            {
                if (nUserId == m_cacheInstance.LstAccounts[i].CustomerID)
                {
                    boolCheckAccountExistence = true;
                    Console.WriteLine($"Your Account Balance is: {m_cacheInstance.LstAccounts[i].Balance}");
                    break;
                }
                
            }
            if (!boolCheckAccountExistence) { 
                    Console.WriteLine("Enter a Valid User ID");
            }
        }

        public static void GetAccountDetails()
        {
            Console.WriteLine("Enter the User ID you want Account Details of: ");
            uint nUserId = Convert.ToUInt32(Console.ReadLine());
            bool bCheck = false;
            int nIndex = -1;
            for (int i = 0; i < m_cacheInstance.LstAccounts.Count; i++)
            {
                if (nUserId == m_cacheInstance.LstAccounts[i].CustomerID)
                {
                    nIndex = i;
                    bCheck = true;

                    break;
                }
                else
                {
                    bCheck = false;
                }
            }
            if (bCheck)
            {
                Console.WriteLine($"Account ID: {m_cacheInstance.LstAccounts[nIndex].CustomerID}\n\t" +
                    $" Account Holder Name: {m_cacheInstance.LstAccounts[nIndex].CustomerName}, Balance: {m_cacheInstance.LstAccounts[nIndex].Balance}");
            }
            else
            {
                Console.WriteLine("Enter a Valid User ID");
            }
        }


        //method overloading
        public static void GetAccountDetails(string stUserName)
        {
            bool bCheck = false;
            int nIndex = -1;
            for (int i = 0; i < m_cacheInstance.LstAccounts.Count; i++)
            {
                stUserName = stUserName.ToLower();
                if (stUserName.Equals(m_cacheInstance.LstAccounts[i].CustomerName.ToLower()))
                {
                    bCheck = false;
                    nIndex = i;
                    break;
                }
                else
                {
                    bCheck = false;
                }
            }
            if (bCheck)
            {
                Console.WriteLine($"Account ID: {m_cacheInstance.LstAccounts[nIndex].CustomerID}\n\t" +
                    $" Account Holder Name: {m_cacheInstance.LstAccounts[nIndex].CustomerName}, Balance: {m_cacheInstance.LstAccounts[nIndex].Balance}");
            }
            else
            {
                Console.WriteLine("Enter a Valid User ID");
            }
        }

        public static void GetAllTheAccountsWithName() {
            Console.WriteLine("\nEnter the Name you want all the accounts of: ");
            string name = Console.ReadLine().ToLower();
            bool checkAccountsCount = false;

            foreach (Accounts account in m_cacheInstance.LstAccounts) {
                if (name == account.CustomerName)
                {
                    Console.WriteLine($"Account ID: {account.CustomerID}\n\t" +
                    $" Account Holder Name: {account.CustomerName}, Balance: {account.Balance}");
                    checkAccountsCount = true;
                }
            }

            if (!checkAccountsCount) {
                Console.WriteLine("We have no Accounts associated with this name");
            }

        }

        //adds account to the list
        
        
        
        
        
        
        
        
        
        
        
        
        public static void AddAccount()
        {
            int nContinue = 0;
            Console.Clear();
            uint nAccountId;
            string stAccountPassword;
            string stAccountHolderName;
            uint nBalance = 0;
            uint nRating = 0;



            bool boolNewUser = true;
            do
            {
                Console.WriteLine("To add a new Account please Enter the Following Details:\n");

                Console.WriteLine("Enter Account ID");
                nAccountId= Convert.ToUInt32(Console.ReadLine());
                Console.WriteLine("\nEnter Account Holder Name");
                stAccountHolderName= Console.ReadLine();
                Console.WriteLine("\nEnter Account Password");
                stAccountPassword= Console.ReadLine();
                Console.WriteLine("\nEnter Account Rating");
                nRating = Convert.ToUInt32(Console.ReadLine());
                Console.WriteLine("Enter Account Balance");
                nBalance= Convert.ToUInt32(Console.ReadLine());


                if (nAccountId!= null && stAccountPassword!= null && stAccountHolderName != null && stAccountHolderName != null )
                {
                    for (int i = 0; i < m_cacheInstance.LstAccounts.Count; i++) {
                        if (nAccountId == m_cacheInstance.LstAccounts[i].CustomerID) {
                            boolNewUser = false;
                        }
                    }
                    if (!boolNewUser)
                    {
                        Console.WriteLine("This User already exsists\n\tKindly enter a new user ID");
                        nContinue = 1;
                        
                    }
                    else {
                        m_cacheInstance.LstAccounts.Add(new Accounts {CustomerID = nAccountId , Balance = nBalance, CustomerName = stAccountHolderName, CustomerRating = nRating} );
                        m_cacheInstance.LstUsers.Add(new Users {UserId = nAccountId , UserName= stAccountHolderName, UserPassword = stAccountPassword} );
                        m_cacheInstance.SerializeToXML(true);
                    }

                }
                else
                {
                    Console.WriteLine("User ID or User Password is Blank\n Please Enter a valid Input ");
                    nContinue = 1;
                }
            }
            while (nContinue == 1);
            
        }
        public static void AddBalanceToAccount() {

           
            int nContinue = 0;
            do {
                Console.Clear();
                Console.WriteLine("Enter the user ID you want to add the Balance in: ");
                uint nAccountId = Convert.ToUInt32(Console.ReadLine());
                bool boolCheckUserAvailibility = false;
                Console.WriteLine("Write the Amount you want to add in the ");
                uint nAccountBalance = Convert.ToUInt32(Console.ReadLine());
                for (int i = 0; i < m_cacheInstance.LstAccounts.Count; i++)
                {
                    if (nAccountId == m_cacheInstance.LstAccounts[i].CustomerID)
                    {
                        boolCheckUserAvailibility = true;
                        m_cacheInstance.LstAccounts[i].Balance += nAccountBalance;
                        Console.WriteLine($"\n The Updated Account Balance is: " +
                            $"\n\t Account ID: {m_cacheInstance.LstAccounts[i].CustomerID}" +
                            $"\n\t Account Holder Name: {m_cacheInstance.LstAccounts[i].CustomerName}" +
                            $"\n\t Account Balance: {m_cacheInstance.LstAccounts[i].Balance}");
                        nContinue = 0;
                        break;
                    }
                }
                if (!boolCheckUserAvailibility)
                {
                    Console.WriteLine("This User ID Doesn't exisits.");
                    Console.WriteLine("To Re-Enter the Account ID enter 1: \n" +
                        "To Exit enter 0");
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
            }
            while (nContinue == 1);
            
        }

        public static void AddLoginIDToAnExsistingAccount() {
            uint nAccountID; 
            int nAccountBalance= 0;
            bool boolAccountExsists = false;
            bool boolUserIDForAccountExsists = false;
            int nUserIDIndex = -1;
            int nAccountIDIndex = -1;
            int nContinue = 0;
            string stAccountPassword;
            do
            {
                Console.WriteLine("Enter the Account ID you want to create the Login ID for. ");
                nAccountID = Convert.ToUInt32(Console.ReadLine());
                for (int i = 0; i < m_cacheInstance.LstAccounts.Count; i++)
                {
                    if (nAccountID == m_cacheInstance.LstAccounts[i].CustomerID) {
                        boolAccountExsists = true;
                        nAccountIDIndex = i;
                        for (int j = 0; j < m_cacheInstance.LstUsers.Count; j++)
                        {
                            if (nAccountID == m_cacheInstance.LstUsers[i].UserId) {
                                boolUserIDForAccountExsists = true;
                                nUserIDIndex = j;
                                break;
                            }
                        }
                        break;
                    }
                }
                if (nUserIDIndex == -1 && nAccountIDIndex == -1)
                {
                    Console.WriteLine("\nThis user doen't exsists. \n" +
                        "\tWant to Re-Enter the User ID, Enter 1\n" +
                        "\tWant to Exit, Enter 0\n");
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
                    Console.Clear();
                }
                else if (nAccountIDIndex != -1 && nUserIDIndex == -1)
                {
                    Console.WriteLine("Enter the Account Password");
                    stAccountPassword = Console.ReadLine();
                    m_cacheInstance.LstUsers.Add(new Users { UserId = m_cacheInstance.LstAccounts[nUserIDIndex].CustomerID, UserName = m_cacheInstance.LstAccounts[nUserIDIndex].CustomerName, UserPassword = stAccountPassword });
                    m_cacheInstance.SerializeToXML(true);
                    Console.WriteLine($"\n\n Your have Successfully created the user login ID for account number {m_cacheInstance.LstAccounts[nUserIDIndex].CustomerID}");
                    nContinue = 0;

                }
                else if (nUserIDIndex != -1) { 
                    Console.WriteLine("User Login ID already exsists for this account\n" +
                        "\tIf you still want to Continue Create new Login ID for this user, Enter 1\n" +
                        "\tIf you want to exit press 0\n ");
                    int nOption = Convert.ToInt32(Console.ReadLine());
                    if (nOption == 0)
                    {
                        nContinue = 0;
                    }
                    else if (nOption == 1)
                    {

                        Console.WriteLine("Enter the Account Password");
                        stAccountPassword = Console.ReadLine();
                        m_cacheInstance.LstUsers[nUserIDIndex] = new Users { UserId = m_cacheInstance.LstAccounts[nUserIDIndex].CustomerID, UserName = m_cacheInstance.LstAccounts[nUserIDIndex].CustomerName, UserPassword = stAccountPassword };
                        m_cacheInstance.SerializeToXML(true);
                        Console.WriteLine($"\n\n Your have Successfully created the user login ID for account number {m_cacheInstance.LstAccounts[nUserIDIndex].CustomerID}");
                        nContinue = 0;
                    }
                    else {
                        Console.WriteLine("Select a correct option");
                        nContinue = 1;
                    }
                    Console.Clear();
                }
            }
            while (nContinue == 1);
        }

        //overriding
        public void GetAccountName(uint nUserId)
        {

        }

    }
}
