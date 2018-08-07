using System;
using UnityEngine;

namespace FoundationFramework
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Foundation/Inventory/Item")]
    public class Item : ScriptableObject
    {
        #region [[ FIELDS ]]
        [Header("Info:")]
        public string ItemName = "New Item";
        public string ItemDescription;
        public bool Stackable;
        public Sprite Icon; 
        public ItemCategory Category;
        
        /// <summary>
        /// Sell Price: how much currencies user receive when sold
        /// </summary>
        [Header("Buy/Sell:")] 
        public CurrencyAmount[] SellPrice;
        
        /// <summary>
        /// How much currencies it cost to buy this item
        /// </summary>
        public CurrencyAmount[] BuyPrice;
       
        #endregion
        
        #region [[ Functions ]]
        public static Item ResolveItem(string itemName)
        {
            if (string.IsNullOrEmpty(itemName)) return null;
            return ItemDatabase.ResolveItem(itemName);
        }

        public String LocalizedItemName()
        {
            if (Language.Localization.IsReady)
            {
                return Language.Localization.Translate(ItemName);
            }

            return ItemName;
        }
        
        public String LocalizedItemDescription()
        {
            if (Language.Localization.IsReady)
            {
                return Language.Localization.Translate(ItemDescription);
            }

            return ItemDescription;
        }

        public bool HasValidBuyPrice()
        {
            if (BuyPrice.Length == 0) return false;
            
            
            for (var index = 0; index < BuyPrice.Length; index++)
            {
                if ( BuyPrice[index].Currency == null)
                    return false;
            }

            return true;
        }
        
        public bool HasValidSellPrice()
        {
            if (SellPrice.Length == 0) return false;
            
            
            for (var index = 0; index < SellPrice.Length; index++)
            {
                if ( SellPrice[index].Currency == null)
                    return false;
            }

            return true;
        }

        public bool HasEnoughCurrencies(CurrencyDataHandler dataHandler)
        {
            if (dataHandler == null) return false;

            for (int i = 0; i < BuyPrice.Length; i++)
            {
                var price = BuyPrice[i];
                if (dataHandler.HasEnoughCurrency(price.Currency.name, price.Count) == false)
                    return false;
            }
            
            return true;
        }

        #endregion
    }
}