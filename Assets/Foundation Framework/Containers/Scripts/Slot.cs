using FoundationFramework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	public Container CurrentContainer;
	public ContainerUi CurrentContainerUi;
	private int _slotId;
	private Item _attachedItem;
	private int _attachedItemCount;
	[SerializeField] private Image _icon;
	[SerializeField] private Text _count;
	
	#region Initialize

	public void Initialize(Container source, ContainerUi sourceUi, int slotId)
	{
		CurrentContainer = source;
		CurrentContainerUi = sourceUi;
		_slotId = slotId;
		SetupComponents();
	}


	private void SetupComponents()
	{
		_icon.raycastTarget = false;
		_count.raycastTarget = false;
	}

	#endregion
	#region UI

	private void SetColorOfContent(Color color)
	{
		_icon.color = color;
		_count.color = color;
	}

	#endregion
	#region ITEM LOGIC

	public int SlotId
	{
		get { return _slotId; }
	}

	public void AssignItem(Item item, int count)
	{
		_attachedItem = item;
		_icon.sprite = _attachedItem.Icon;
		_icon.enabled = true;
		_attachedItemCount = count;
		_count.text = _attachedItemCount.ToString();
		_count.enabled = true;
	}

	public void Reset()
	{
		_attachedItem = null;
		_icon.enabled = false;
		_attachedItemCount = 0;
		_count.enabled = false;
	}

	#endregion

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (_attachedItem == null) return;
		var dragDummy = CurrentContainerUi.GetDragDummy;
		dragDummy.Initialize(_icon.sprite);
		dragDummy.SetPosition(transform.position);
		SetColorOfContent(Color.clear);
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (_attachedItem == null) return;
		CurrentContainerUi.GetDragDummy.SetPosition(eventData.position);
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if (_attachedItem == null) return;
		if (eventData.pointerEnter)
		{
			var slot = eventData.pointerEnter.GetComponent<Slot>();

			if (slot != null)
			{
				CurrentContainer.TryMoveSlot(_slotId, slot.CurrentContainer, slot.SlotId);
			}
		}
		
		CurrentContainerUi.GetDragDummy.Reset();
		SetColorOfContent(Color.white);
	}
}