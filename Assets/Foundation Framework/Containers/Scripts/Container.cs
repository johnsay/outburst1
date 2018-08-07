using System;
using System.Collections.Generic;
using UnityEngine;

namespace FoundationFramework
{
    /// <summary>
    /// hold the list of items
    /// have logic for add /remove
    /// send events to other components ( ui /save )
    /// </summary>
    public class Container : MonoBehaviour
    {
        #region FIELDS

        public Action OnContentChanged;
        public ItemDatabase Db;
        [SerializeField] private ContainerUi _ui;
        
        public List<ItemCategory> AllowedCategories;
        public List<ItemEntry> Items;
        public int SlotCounts = 10;

        #endregion

        private void Awake()
        {
            InitializeContainer();
        }

        public void InitializeContainer()
        {
            if (Items.Count > 0) return;
            
            Db.Init();
            Items.Clear();
            InitializeList();
        }

        private void InitializeList()
        {
            for (int i = 0; i < SlotCounts; i++)
            {
                Items.Add(new ItemEntry(String.Empty, 0));
            }
        }

        public void OpenContainer()
        {
            _ui.Open(this);
        }

        #region Transfers

        public void RemoveSlot(int id)
        {
            Items[id].ItemCount = 0;
            Items[id].ItemName = string.Empty;
        }

        public void AddItemCount(int id, int count)
        {
            Items[id].ItemCount +=count;
        }

        public void CopySlotToOtherSlot(int id, Container to, int toId)
        {
            to.Items[toId].ItemCount =  Items[id].ItemCount;
            to.Items[toId].ItemName =  Items[id].ItemName;
            Items[id].ItemCount = 0;
            Items[id].ItemName = string.Empty;
        }

        public void SwapSlots(int id, Container to, int toId)
        {
            var fromItem = Items[id].ItemName;
            var fromItemCount = Items[id].ItemCount;
            var toItem = to.Items[toId].ItemName;
            var toItemCount = to.Items[toId].ItemCount;
            
            to.Items[toId].ItemName =  fromItem;
            to.Items[toId].ItemCount =  fromItemCount;
           
            Items[id].ItemName = toItem;
            Items[id].ItemCount = toItemCount;  
        }

        //move from slot to slot
        //move to other collection
        public void TryMoveSlot(int fromId, Container to, int toId)
        {
            var itemToMove = Items[fromId];
            var destinationItem = to.Items[toId];
            if (string.IsNullOrEmpty(itemToMove.ItemName)) return;
            
            var resolvedItemToMove = Item.ResolveItem(itemToMove.ItemName);
           
            //Check: Contain item
            if (destinationItem.ItemCount > 0)
            {
                var resolvedDestinationItem = Item.ResolveItem(destinationItem.ItemName);
                //Check: same slot
                if (this == to && fromId == toId)
                {
                    return;
                }

                //Check: Items can stack
                if (resolvedDestinationItem == resolvedItemToMove && resolvedItemToMove.Stackable)
                {
                    var count = Items[fromId].ItemCount;
                    RemoveSlot(fromId);
                    to.AddItemCount(toId,count);
                }
                else
                {
                    //Check: Can item be swaped
                    if (to.AllowedCategories.Contains(resolvedItemToMove.Category)
                        && AllowedCategories.Contains(resolvedDestinationItem.Category))
                    {
                        //can slot contain this item 
                        SwapSlots(fromId, to, toId);
                    }
                    else
                    {
                        //items cannot be swapped
                        return;
                    }
                }
            }
            else
            {
                //Check: Can item be moved
                if (to.AllowedCategories.Contains(resolvedItemToMove.Category))
                {
                    //TODO
                    //can slot contain this item 
                    CopySlotToOtherSlot(fromId, to, toId);
                }
                else
                {
                    //items cannot be moved
                    return;
                }
            }

            //UPDATE THE UIs LISTENING
            OnContentChanged.Invoke();
            if (to != this)
            {
                to.OnContentChanged.Invoke();  
            }
            
        }

        public bool CanAddItem(string itemName)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].ItemName == itemName || string.IsNullOrEmpty(Items[i].ItemName))
                    return true;
            }
            
            return false;
        }

        private int FindFirstFreeSlot()
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if(string.IsNullOrEmpty(Items[i].ItemName))
                    return i;
            }
            
            return -1;
        }

        private int FindSlotWithItem(string itemName)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if(Items[i].ItemName == itemName)
                    return i;
            }
            
            return -1;
        }

        public void AddItem(ItemEntry entry)
        {
            var hasItem = FindSlotWithItem(entry.ItemName);
            if (hasItem >= 0)
            {
                AddItemCount(hasItem, entry.ItemCount);
            }
            else
            {
                int freeSlot = FindFirstFreeSlot();
                if (freeSlot >= 0)
                {
                    Items[freeSlot] = entry;
                }
            }
        }

        public int RemoveItem(string itemName, int amountToRemove)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].ItemName == itemName)
                {
                    var itemCount = Items[i].ItemCount;

                    if (amountToRemove > itemCount)
                    {
                        Items[i].Reset();
                   
                        return itemCount;
                    }
                    else
                    {
                        
                        Items[i].ItemCount -= amountToRemove;
                        if(Items[i].ItemCount == 0)
                            Items[i].Reset(); 
                        
                        return amountToRemove;
                    }
                }
            }

            return 0;
        }

        #endregion
        
    }
}