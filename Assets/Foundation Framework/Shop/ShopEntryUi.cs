using System;
using UnityEngine;
using UnityEngine.UI;

namespace FoundationFramework
{
    public class ShopEntryUi : MonoBehaviour
    {
        #region [[ FIELDS ]]
        
        [SerializeField] private Image _icon;
        [SerializeField] private Text _itemName;
        [SerializeField] private Text _itemCount;
        [SerializeField] private Button _interactionButton;
        [SerializeField] private LayoutCostCurrencyUi _layoutCost;

        private Item _shopItem;
        private ShopUi _cachedUi;
        private bool _buy;
        
        #endregion

        public void Setup(ItemEntry item, ShopUi ui,bool buy)
        {
            var resolvedItem = Item.ResolveItem(item.ItemName);
            _buy = buy;
            _layoutCost.Initialize();
            _itemName.text = item.ItemName;
            
            _icon.sprite = resolvedItem.Icon;
            if (buy)
            {
                _layoutCost.SetupItemCosts(resolvedItem.BuyPrice);
                _interactionButton.interactable = resolvedItem.HasValidBuyPrice();
            }
            else
            {
                _layoutCost.SetupItemCosts(resolvedItem.SellPrice);
                _interactionButton.interactable = resolvedItem.HasValidSellPrice();
            }

            if (_itemCount)
            {
                _itemCount.text = "X " + item.ItemCount;
            }

            _shopItem = resolvedItem;
            _cachedUi = ui;
            
        }

        public void Setup(Item item, ShopUi ui,bool buy)
        {
          
            _buy = buy;
            _layoutCost.Initialize();
            _itemName.text = item.ItemName;
            
            _icon.sprite = item.Icon;
            if (buy)
            {
                _layoutCost.SetupItemCosts(item.BuyPrice);
                _interactionButton.interactable = item.HasValidBuyPrice();
            }
            else
            {
                _layoutCost.SetupItemCosts(item.SellPrice);
                _interactionButton.interactable = item.HasValidSellPrice();
            }

            if (_itemCount)
            {
                _itemCount.text = String.Empty;
            }

            _shopItem = item;
            _cachedUi = ui;
            
        }
       
        public void DoInteract()
        {
            if (_buy)
            {
                _cachedUi.OnCacheBuyItem(_shopItem, this);
                _interactionButton.interactable = false;
            }
            else
            {
                _cachedUi.OnCacheSellItem(_shopItem, this);
                _interactionButton.interactable = false;
            }

           
        }

        public void Reset()
        {
            _interactionButton.interactable = true;
        }
    }
}
