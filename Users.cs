using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class Users
    {

        private uint m_userId;
        private string m_userName;
        private string m_userPassword;
        private static bool m_clientLoginStatus = false;
        private static bool m_newRegistration = false;
        private static bool m_adminLogin = false;
        private static uint m_userLoginID ;


        public uint UserId { get { return m_userId; } set { m_userId = value; } }
        public string UserPassword { get { return m_userPassword; } set { m_userPassword = value; } }
         
        public static bool ClientLoginStatus{ get { return m_clientLoginStatus; }   set { m_clientLoginStatus = value; }}
        public static bool NewRegistration { get { return m_newRegistration; } set { m_newRegistration  = value; } }
        public static bool AdminLogin { get { return m_adminLogin; } set { m_adminLogin = value; } }
        public  string UserName { get { return m_userName; } set { m_userName = value; } }
        public static uint UserLoginId { get { return m_userLoginID; } set { m_userLoginID = value; } }


    }
}
