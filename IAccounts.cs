using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
namespace veichlePurchaseAndInventorySystem
{
    internal interface IAccounts
    {
        public abstract void GetAccountName(uint nUserId);
    }
}
