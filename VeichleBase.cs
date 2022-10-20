using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace veichlePurchaseAndInventorySystem
{
    abstract internal class VeichleBase
    {
        string m_make;
        string m_model;
        uint m_year;
        uint m_veichlePrice;
        public abstract void CarWithID(uint ID);
    }
}
