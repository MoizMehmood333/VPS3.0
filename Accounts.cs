using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Repository.Repository
{
        public class Accounts
        {
        

            //member variables
            private uint m_customerID;
            private uint m_balance;
            private string m_customerName;
            private uint m_customerRatings;
            private static bool m_addedAccountDetails = false;  
            



        //Properties for private variables
        public uint CustomerID
            {
                get { return m_customerID; }
                set { m_customerID = value; }
            }
            public uint Balance
            {
                get { return m_balance; }
                set { m_balance = value; }
            }
    
            public string CustomerName
            {
                get { return m_customerName; }
                set { m_customerName = value; }
            }
            
        public uint CustomerRating { 
            get { return m_customerRatings; }
            set { m_customerRatings = value; } 
        }

        public static bool AddedAccountDetails
        {
            get { return m_addedAccountDetails; }
            set { m_addedAccountDetails = value; }
        }
        
        
        }      
}





