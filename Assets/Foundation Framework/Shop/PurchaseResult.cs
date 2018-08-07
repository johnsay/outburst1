using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoundationFramework
{
    public class PurchaseResult
    {
        public bool SpentCurrencies;
        public bool AddedToContainer;

        public PurchaseResult(bool spentSuccess, bool addedSucces)
        {
            SpentCurrencies = spentSuccess;
            AddedToContainer = addedSucces;
        }

        public bool isSuccess()
        {
            return SpentCurrencies && AddedToContainer;
        }
    }
}
