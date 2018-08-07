using System.Collections.Generic;
using UnityEngine;

namespace FoundationFramework
{
    public class Shop : MonoBehaviour 
    {
        #region FIELDS

        [SerializeField] private ShopUi _shopUi;
        [SerializeField] private CurrencyDataHandler _currencyDataHandler;
        [SerializeField] private ContainerDataHandler _containerDataHandler;
        [SerializeField] private ShopConfig _config;
        
        #endregion

        public Item[] FilteredShopProducts()
        {
            return _config.ItemsOnSale;
        }

        public void TriggerOpenShop()
        {
            _shopUi.OnOpenShop(this, _config, _currencyDataHandler);
        }
  
        
        public PurchaseResult TryPurchaseItems(Item item, int count)
        {
            var purchaseResult = new PurchaseResult
            (
                _currencyDataHandler.HasEnoughCurrencies(item.BuyPrice),
                _containerDataHandler.GetContainer.CanAddItem(item.ItemName)
            );

            if (purchaseResult.isSuccess())
            {
                _currencyDataHandler.TrySpendCurrencies(item.BuyPrice);
                _containerDataHandler.GetContainer.AddItem(new ItemEntry(item.ItemName,count));
            }

            return purchaseResult;
        }

        public void SellItem(Item item, int count)
        {
            var soldCount = _containerDataHandler.GetContainer.RemoveItem(item.ItemName, count);
            if (soldCount > 0)
            {
                _containerDataHandler.SaveData();
            }

            for (int i = 0; i < soldCount; i++)
            {
                //add price for each object
                foreach (var price in item.SellPrice)
                {
                    _currencyDataHandler.AddCurrencyAmounts(price.Currency.name,price.Count);
                }
            }
        }
        

        public List<ItemEntry> SellableItemsEnumerable()
        {
            if (!_config.PurchaseItems)
            {
                return null;
            }
            var filteredList = new  List<ItemEntry>();
            
            for (int i = 0; i < _containerDataHandler.GetContainer.Items.Count; i++)
            {
                string itemName = _containerDataHandler.GetContainer.Items[i].ItemName;
                int count = _containerDataHandler.GetContainer.Items[i].ItemCount;
                if (string.IsNullOrEmpty(itemName) == false)
                {
                    var item = Item.ResolveItem(itemName);
                    if (item && item.HasValidSellPrice())
                    {
                        filteredList.Add(new ItemEntry(item.ItemName,count));
                    }
                }
            }
          
            return filteredList;
        }

    }
}
