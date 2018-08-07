using UnityEngine;
using UnityEngine.UI;

using System.Collections.Generic;


namespace FoundationFramework
{
    public class ContainerUi : MonoBehaviour 
    {
        #region FIELDS

        [SerializeField] private UiPanelBase _panel;
        [SerializeField] private DragDummy _dragDummy;
        private Container _currentContainer;
        [SerializeField] private int _maxSlotPreloaded;
        [SerializeField] private Slot _slotPrefab;
        [SerializeField] private LayoutGroup _layout;
       
        private List<Slot> _slots = new List<Slot>();
        
        #endregion

        private void Awake()
        {
            _dragDummy.Reset();
            PreloadSlots();
        }

        private void PreloadSlots()
        {
            for (var i = 0; i < _maxSlotPreloaded; i++)
            {
                var slot = Instantiate(_slotPrefab, _layout.transform);
                _slots.Add(slot);
            }
        }

        private void InitializeSlots(int slotTotal)
        {
            var current = 0;
            foreach (var slot in _slots)
            {
                if (current < slotTotal)
                {
                    slot.Initialize(_currentContainer, this, current);
                    slot.gameObject.SetActive(true);
                    current++;
                }
                else
                {
                    slot.gameObject.SetActive(false);
                }

               
            }
        }


        private void AssignItemsToSlots(IEnumerable<ItemEntry> data)
        {
            int index = 0;
            foreach (var entry in data)
            {
                var currentSlot = _slots[index];
                currentSlot.CurrentContainer = _currentContainer;
               
                if (!string.IsNullOrEmpty(entry.ItemName))
                {   
                    currentSlot.AssignItem(Item.ResolveItem(entry.ItemName),entry.ItemCount);
                }
                else
                {
                    currentSlot.Reset();
                }

                index++;
            }
        }
        public void Open(Container container)
        {
            _currentContainer = container;
            InitializeSlots(_currentContainer.Items.Count);
            AssignItemsToSlots(_currentContainer.Items);
            _currentContainer.OnContentChanged += OnContentChanged;
            _panel.Show();
        }

       
        public void Close()
        {
            _currentContainer.OnContentChanged -= OnContentChanged;
            _currentContainer = null;
            _panel.Hide();
        }

        private void OnContentChanged()
        {
            AssignItemsToSlots(_currentContainer.Items);
        }
        
        
        #region drag

        public DragDummy GetDragDummy
        {
            get { return _dragDummy; }
        }

        #endregion
    }
    
}
