using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Repository.Repository
{
    public enum CarsWeDealIn {
        honda,
        suzuki,
        bmw,
        mercedes,
        porsche,
        toyota
    }

    public class Cars
    {

        private uint m_carID;
        private string m_make;
        private string m_model;
        private uint m_carPrice;

        //properties
        public uint CarID
        {
            get { return m_carID; }
            set { m_carID = value; }
        }
        public string Make
        {
            get { return m_make; }
            set { m_make = value; }

        }
        public string Model
        {
            get { return m_model; }
            set { m_model = value; }
        }
        public uint CarPrice
        {
            get { return m_carPrice; }
            set { m_carPrice = value; }
        }
        
    }
}
