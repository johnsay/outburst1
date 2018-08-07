using UnityEngine;

namespace FoundationFramework
{
	[CreateAssetMenu(fileName = "Shop_name", menuName = "Foundation/ShopConfig")]
	public class ShopConfig : ScriptableObject
	{
		public Item[] ItemsOnSale;

		public bool PurchaseItems;


	}
}
