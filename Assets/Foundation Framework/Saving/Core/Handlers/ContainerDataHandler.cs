using System.Collections.Generic;
using UnityEngine;

namespace FoundationFramework
{
	public class ContainerDataHandler : UseProfileDataHandler
	{
		[SerializeField] private bool _saveOnChange;
		
		[SerializeField] private string _uniqueInventoryName;
		[SerializeField] private Container _container;
		[SerializeField] private List<ItemEntry> _defaultItems;
		public ContainerData ContainerData { get; private set; }

		public Container GetContainer
		{
			get { return _container; }
		}

		protected override void BuildDefaultData()
		{
			FileName = "Container_"+_uniqueInventoryName;
			ContainerData = new ContainerData();
		}

		public override void SaveData()
		{
			ContainerData.Items = _container.Items;
			DataContainer.Save(ContainerData);
		}

		public override void LoadData()
		{
			if (!IsLoaded)
			{
				
				IsLoaded = true;
				var tempData = DataContainer.Load<ContainerData>();
				if (tempData != null)
				{
					ContainerData = tempData;
				}
				_container.InitializeContainer();
				TryInitSave();
				
				
				_container.Items = ContainerData.Items;
			}    
		}

		private void TryInitSave()
		{
			if (ContainerData.IsInitialized == false)
			{
				
				ContainerData.IsInitialized = true;
				for (int i = 0; i < _defaultItems.Count; i++)
				{
					_container.Items[i] = _defaultItems[i];
				}
				
				ContainerData.Items = _container.Items;
				
				SaveData();
			}
		}

		private void OnEnable()
		{
			if(_saveOnChange)
			_container.OnContentChanged += SaveData;
		}

		private void OnDisable()
		{
			if(_saveOnChange)
			_container.OnContentChanged -= SaveData;
		}
	}
}

