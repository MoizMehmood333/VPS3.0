using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security.AccessControl;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Repository;
using Repository.Repository;

namespace VPSCache.CacheLayer
{
    public class Cache
    {


        private List<Cars> m_lstCars;
        private List<Users> m_lstUsers;
        private List<Accounts> m_lstAcconts;
        public List<Cars> LstCars
        {
            get { return m_lstCars; }
            set { m_lstCars = value; }
        }
        public List<Users> LstUsers
        {
            get { return m_lstUsers; }
            set { m_lstUsers = value; }
        }
        public List<Accounts> LstAccounts
        {
            get { return m_lstAcconts; }
            set { m_lstAcconts=  value; }
        }

        private static Cache m_getCacheInstance;
        private  bool m_usersFileNotFound = false;
        private  bool m_accountsFileNotFound = false;
        private  bool m_carInventoryFileNotFound = false;

        public static Cache GetCache {
            get { if (m_getCacheInstance == null)
                {
                    m_getCacheInstance = new Cache();
                    return m_getCacheInstance;
                }
                return m_getCacheInstance;
            }
            set { m_getCacheInstance = value; }
        }

        public  bool UserFileNotFound
        {
            get { return m_usersFileNotFound; }
            set { m_usersFileNotFound = value; } 
        }
        public  bool AccountFileNotFound {
            get { return m_accountsFileNotFound; }
            set { m_accountsFileNotFound = value; }
        }
        public  bool CarInInventoryFileNotFound
        {
            get { return m_carInventoryFileNotFound; }
            set { m_carInventoryFileNotFound = value; }

        }
        public void DeSerializeXMLFiles()
        {


           if (!File.Exists(@"C:\Users\mmoiz\Desktop\Practice\XMLfolder\CarInInventory.xml"))
            {
                CarInInventoryFileNotFound = true;
                SerializeToXML(false);
            }
            if (Users.AdminLogin)
            {
                if (LstUsers == null)
                {
                    var userXmlSerializer = new XmlSerializer(typeof(List<Users>));

                    using (var reader = new StreamReader(@"C:\Users\mmoiz\Desktop\Practice\XMLfolder\AllUsers.xml"))
                    {
                        LstUsers = new List<Users>();
                        LstUsers = (List<Users>)userXmlSerializer.Deserialize(reader);

                    }
                }
                    if (LstAccounts == null)
                    {
                        var accountsXmlSerializer = new XmlSerializer(typeof(List<Accounts>));
                        using (var reader = new StreamReader(@"C:\Users\mmoiz\Desktop\Practice\XMLfolder\Accounts.xml"))
                        {
                            LstAccounts = new List<Accounts>();
                            LstAccounts = (List<Accounts>)accountsXmlSerializer.Deserialize(reader);


                    }
                    

                }
                if (LstCars == null) {
                    var carXmlSerializer = new XmlSerializer(typeof(List<Cars>));


                    using (var reader = new StreamReader(@"C:\Users\mmoiz\Desktop\Practice\XMLfolder\CarInInventory.xml"))
                    {
                        LstCars = new List<Cars>();
                        LstCars = (List<Cars>)carXmlSerializer.Deserialize(reader);

                    }
                }
                return;


            }

            if (LstUsers == null)
                {

                    var userXmlSerializer = new XmlSerializer(typeof(List<Users>));

                    using (var reader = new StreamReader(@"C:\Users\mmoiz\Desktop\Practice\XMLfolder\AllUsers.xml"))
                    {
                        LstUsers = new List<Users>();
                        LstUsers = (List<Users>)userXmlSerializer.Deserialize(reader);

                    }


                }
                bool boolValue = Users.ClientLoginStatus;
                if (Users.NewRegistration)
                {
                if (LstAccounts == null) {
                    var accountsXmlSerializer = new XmlSerializer(typeof(List<Accounts>));
                    using (var reader = new StreamReader(@"C:\Users\mmoiz\Desktop\Practice\XMLfolder\Accounts.xml"))
                    {
                        LstAccounts = new List<Accounts>();
                        LstAccounts = (List<Accounts>)accountsXmlSerializer.Deserialize(reader);

                    }

                }


            }
           

                if (!boolValue)
                    return;

                if (!File.Exists(@"C:\Users\mmoiz\Desktop\Practice\XMLfolder\CarInInventory.xml"))
                {
                    CarInInventoryFileNotFound = true;
                    SerializeToXML(false);
                }
                if (LstAccounts == null) {
                    var accountXmlSerializer = new XmlSerializer(typeof(List<Accounts>));
                    using (var reader = new StreamReader(@"C:\Users\mmoiz\Desktop\Practice\XMLfolder\Accounts.xml"))
                    {
                        LstAccounts = new List<Accounts>();
                        LstAccounts = (List<Accounts>)accountXmlSerializer.Deserialize(reader);

                    }
                }
    
            if (LstCars == null) {

                var carXmlSerializer = new XmlSerializer(typeof(List<Cars>));


                using (var reader = new StreamReader(@"C:\Users\mmoiz\Desktop\Practice\XMLfolder\CarInInventory.xml"))
                {
                    LstCars = new List<Cars>();
                    LstCars = (List<Cars>)carXmlSerializer.Deserialize(reader);

                }

            }

        }

        public void SerializeToXML(bool newUser){
        
            if (CarInInventoryFileNotFound)
            {
                List<Cars> lstCarsInInventory = new List<Cars>();
                var userXmlSerializer = new XmlSerializer(typeof(List<Cars>));
                using (var writer = new StreamWriter(@"C:\Users\mmoiz\Desktop\Practice\XMLfolder\CarInInventory.xml"))
                {
                    userXmlSerializer.Serialize(writer, lstCarsInInventory);
                }
                CarInInventoryFileNotFound = false;
                return;
            }

            if (newUser)
            {
                var userXmlSerializer = new XmlSerializer(typeof(List<Users>));
                using (var writer = new StreamWriter(@"C:\Users\mmoiz\Desktop\Practice\XMLfolder\AllUsers.xml"))
                {
                    userXmlSerializer.Serialize(writer, LstUsers);
                }
                var accountsXmlSerializer = new XmlSerializer(typeof(List<Accounts>));
                using (var writer = new StreamWriter(@"C:\Users\mmoiz\Desktop\Practice\XMLfolder\Accounts.xml"))
                {
                    accountsXmlSerializer.Serialize(writer, LstAccounts);
                }
            }
            else if (Accounts.AddedAccountDetails)
            {
                var accountsXmlSerializer = new XmlSerializer(typeof(List<Accounts>));
                using (var writer = new StreamWriter(@"C:\Users\mmoiz\Desktop\Practice\XMLfolder\Accounts.xml"))
                {
                    accountsXmlSerializer.Serialize(writer, LstAccounts);
                }
                Accounts.AddedAccountDetails = false;
            }
            else
            {
                var carsXmlSerializer = new XmlSerializer(typeof(List<Cars>));
                var accountsXmlSerializer = new XmlSerializer(typeof(List<Accounts>));
                using (var writer = new StreamWriter(@"C:\Users\mmoiz\Desktop\Practice\XMLfolder\CarInInventory.xml"))
                {
                    carsXmlSerializer.Serialize(writer, LstCars);
                }
                using (var writer = new StreamWriter(@"C:\Users\mmoiz\Desktop\Practice\XMLfolder\Accounts.xml"))
                {
                    accountsXmlSerializer.Serialize(writer, LstAccounts);
                }

            }
        }
       
      
    }
}

