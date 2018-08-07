using System;

[Serializable]
public class ItemEntry
{
	public string ItemName;
	public int ItemCount;

	public ItemEntry(string itemName, int itemCount)
	{
		ItemName = itemName;
		ItemCount = itemCount;
	}

	public void Reset()
	{
		ItemName = String.Empty;
		ItemCount = 0;
	}
}
