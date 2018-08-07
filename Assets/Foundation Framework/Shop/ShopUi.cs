using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using FoundationFramework.UI;

namespace FoundationFramework
{
    public class ShopUi : MonoBehaviour 
    {
        #region FIELDS
       
        [SerializeField] private UiPanelBase _panel;
        [Header("To Buy:")]
        [SerializeField] private LayoutGroup _toBuyLayoutGroup;
        [SerializeField] private ShopEntryUi _toBuyEntryPrefab;
        private GenericUiList<Item> _toBuyEntries;
        [Header("to sell:")]
        [SerializeField] private LayoutGroup _toSellLayoutGroup;
        [SerializeField] private ShopEntryUi _toSellEntryPrefab;
        private GenericUiList<ItemEntry> _toSellEntries;
        
        [Header("Interaction Buy panel:")]
        [SerializeField] private Transform _selectedBuyRoot;
        [SerializeField] private Text _itemBuySelected;
        [SerializeField] private Text _itemBuySelectedDescription;
        [SerializeField] private Image _itemBuySelectedIcon;
        [SerializeField] private LayoutCostCurrencyUi _layoutCost;
        [SerializeField] private Button _buyButton;
        [SerializeField] private bool _purchaseRequireConfirmation;
        [SerializeField] private bool _displayOnPurchasePopup;
        [SerializeField] private bool _displayOnFailPurchasePopup;
        
        [Header("Interaction Sell panel:")]
        [SerializeField] private Transform _selectedSellRoot;
        [SerializeField] private Text _itemSellSelected;
        [SerializeField] private Text _itemSellSelectedDescription;
        [SerializeField] private Image _itemSellSelectedIcon;
        [SerializeField] private LayoutCostCurrencyUi _sellLayoutCost;
        [SerializeField] private Button _buyCategory;
        [SerializeField] private Button _sellCategory;
        [SerializeField] private CanvasGroup _buyGroup;
        [SerializeField] private CanvasGroup _sellGroup;
        
        
        private Item _cachedSelection;
        private ShopConfig _config;
        private Shop _shop;
        private ShopEntryUi _cachedEntry;
        private CurrencyDataHandler _currencyDataHandler;
        #endregion
        
        private void Awake()
        {
            _layoutCost.Initialize();
            _toBuyEntries = new GenericUiList<Item>(_toBuyEntryPrefab.gameObject,_toBuyLayoutGroup);
            
            _toSellEntries = new GenericUiList<ItemEntry>(_toSellEntryPrefab.gameObject,_toSellLayoutGroup);
        }

        #region [[ BUY IN SHOP ]]
        private void SetBuyList(IEnumerable<Item> data)
        {
            _toBuyEntries.Generate<ShopEntryUi>(data, (entry, item) => { item.Setup(entry,this,true); });
        }
        
        public void OnCacheBuyItem(Item itemCost,ShopEntryUi entryUi)
        {
            //reset previous
            if (_cachedEntry)
                _cachedEntry.Reset();
            
            //cache item
            _cachedEntry = entryUi;
            _cachedSelection = itemCost;
            
            _itemBuySelected.text = itemCost.LocalizedItemName();
            _itemBuySelectedDescription.text = itemCost.LocalizedItemDescription();
            _itemBuySelectedIcon.sprite = itemCost.Icon;
            _selectedBuyRoot.localScale = Vector3.one;
           
            //prices
            _layoutCost.SetupItemCosts(itemCost.BuyPrice);
            _buyButton.interactable = itemCost.HasValidBuyPrice() && itemCost.HasEnoughCurrencies(_currencyDataHandler);
        }
        #endregion

        #region [[ SELL IN SHOP ]] 
        private void SetSellList(IEnumerable<ItemEntry> data)
        {
            _toSellEntries.Generate<ShopEntryUi>(data, (entry, item) => { item.Setup(entry,this,false); });
        }
        
        public void OnCacheSellItem(Item itemCost,ShopEntryUi entryUi)
        {
            if (_cachedEntry)
                _cachedEntry.Reset();
            
            //cache item
            _cachedEntry = entryUi;
            _cachedSelection = itemCost;
            
            _itemSellSelected.text = itemCost.LocalizedItemName();
            _itemSellSelectedDescription.text = itemCost.LocalizedItemDescription();
            _itemSellSelectedIcon.sprite = itemCost.Icon;
            _selectedSellRoot.localScale = Vector3.one;
            
            //prices
            _sellLayoutCost.SetupItemCosts(itemCost.SellPrice);
           
        }

        private void CacheSellItemList()
        {
            if (_config.PurchaseItems)
            {
                SetSellList(_shop.SellableItemsEnumerable());
            }
        }
        #endregion

        #region [[ CLICK CALLS ]]
        public void OnCloseShop()
        {
            if (_cachedEntry)
                _cachedEntry.Reset();
            
            _currencyDataHandler.OnCurrencyChange -= OnCurrencyChange;
            _cachedSelection = null;
            _panel.Hide();
        }

        public void ClickSell()
        {
            if (_cachedSelection == null) return;

            if (_cachedSelection.HasValidSellPrice())
            {
                _shop.SellItem(_cachedSelection,1);

                var newList = _shop.SellableItemsEnumerable();
                  SetSellList(newList);

                bool hasItem = false;
                
                foreach (var item in newList)
                {
                    if (item.ItemName == _cachedSelection.ItemName)
                    {
                        hasItem = true;
                    }
                }
                if (hasItem == false)
                {
                    _cachedSelection = null;
                    _selectedSellRoot.localScale = Vector3.zero;
                }
            }
        }

        public void ClickBuy()
        {
            if (_cachedSelection == null) return;
            
            if (_cachedSelection.HasValidBuyPrice())
            {
                if (_purchaseRequireConfirmation)
                {
                    string translatedMsg = Language.Localization.Translate("Buy");
                    translatedMsg += " " + _cachedSelection.LocalizedItemName() + "?";
                    
                    var msg = DialogBoxData.CreateActionBox
                        (
                            translatedMsg,
                            () =>
                            {
                                TryBuySelection(1);
                            }, 
                            () =>
                            {
                                //cancel
                            },
                            Language.Localization.Translate("Buy")
                        );
                    
                    UiDialogBox.Instance.ShowDialog(msg);
                }
                else
                {
                    TryBuySelection(1);
                }
            }
        }
        
        public void ClickSellCategory()
        {
            CacheSellItemList();
            SwitchGroup(_sellGroup, true);
        }

        public void ClickBuyCategory()
        {
            SwitchGroup(_buyGroup, true);
        }

        #endregion
        
        public void OnOpenShop(Shop shop,ShopConfig config,CurrencyDataHandler currency)
        {
            _config = config;
            _shop = shop;
            _currencyDataHandler = currency;
            _currencyDataHandler.OnCurrencyChange += OnCurrencyChange;
            _cachedSelection = null;
            _selectedBuyRoot.localScale = Vector3.zero;
            _selectedSellRoot.localScale = Vector3.zero;
            SetBuyList(shop.FilteredShopProducts());
            
            _panel.Show();
            SwitchGroup(_buyGroup, true);
            _sellCategory.gameObject.SetActive(config.PurchaseItems);
        }

        private void OnCurrencyChange()
        {
           if(_cachedSelection)
               _buyButton.interactable = _cachedSelection.HasValidBuyPrice() && _cachedSelection.HasEnoughCurrencies(_currencyDataHandler);
        }

        private void TryBuySelection(int amount)
        {
            if (_cachedSelection == null) return;

            var result = _shop.TryPurchaseItems(_cachedSelection,amount);
            if (result.isSuccess() == false)
            {
                if (_displayOnFailPurchasePopup)
                {
                    if (result.AddedToContainer)
                    {
                        var notification = new UiNotice.NoticeMessage();
                        notification.Title = "Cant purchase:";
                        notification.Message = "not enough space";
                        UiNotice.Notify(notification);
                    }
                    
                    if (result.SpentCurrencies)
                    {
                        var notification = new UiNotice.NoticeMessage();
                        notification.Title = "Cant purchase:";
                        notification.Message = "not enough currencies";
                        UiNotice.Notify(notification);
                    }

                    return;
                }
            }

            if (result.AddedToContainer)
            {
                if (_displayOnPurchasePopup)
                {
                    var notification = new UiNotice.NoticeMessage();
                    notification.Title = "Item purchased:";
                    notification.Message = _cachedSelection.LocalizedItemName();
                   UiNotice.Notify(notification);
                }
                
            }
        }

        private void SwitchGroup(CanvasGroup group, bool instant)
        {
            _buyCategory.interactable = _buyGroup != group;
            _sellCategory.interactable = _sellGroup != group;
            var buyGroupAlpha = _buyGroup == group ? 1 : 0;
            var sellGroupAlpha = _sellGroup == group ? 1 : 0;
            _selectedBuyRoot.localScale = Vector3.zero;
            _selectedSellRoot.localScale = Vector3.zero;
            if (instant)
            {
                _buyGroup.interactable = _buyGroup == group;
                _buyGroup.blocksRaycasts = _buyGroup == group;
                _buyGroup.alpha = buyGroupAlpha;
                
                _sellGroup.interactable = _sellGroup == group;
                _sellGroup.blocksRaycasts = _sellGroup == group;
                _sellGroup.alpha =sellGroupAlpha;
            }
        } 
    }
}
