using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace FoundationFramework
{
    [CreateAssetMenu(fileName = "ItemDatabase", menuName = "Foundation/Inventory/ItemDatabase")]
    public class ItemDatabase : ScriptableObject
    {
       public List<Item> Items = new List<Item>();

        public void Init()
        {
            Instance = this;
            _cache = Instance.Items.ToDictionary
            (
                item => item.name, item => item
            );
        }

       
        public static ItemDatabase Instance;
        private static Dictionary<string, Item> _cache;

        public static Dictionary<string, Item> Entries
        {
            get
            {
                return _cache ?? (_cache = Instance.Items.ToDictionary
                           (
                               item => item.name, item => item
                           )
                       );
            }
        }

        public static Item ResolveItem(string itemName)
        {
            Item item;
            Entries.TryGetValue(itemName, out item);
            return item;
        }
    }
}