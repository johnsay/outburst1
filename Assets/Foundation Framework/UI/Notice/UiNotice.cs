using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FoundationFramework.Pools;

namespace FoundationFramework.UI
{
	public class UiNotice : MonoBehaviour
	{
		public class NoticeMessage
		{
			public string Title;
			public string Message;
		}

		#region Fields
		[SerializeField] private UiNoticeObject _objectPrefab;
		[SerializeField] private VerticalLayoutGroup _layout;

		private static UiNotice _instance;
		private float _timeLeft;
		private readonly List<NoticeMessage> _queue = new List<NoticeMessage> ();
		private const int MaxConcurentEntries = 4;
		#endregion

		private void Awake()
		{
			_instance = this;
		}

		public static void Notify(NoticeMessage entry)
		{
			_instance._queue.Add (entry);
		}

		private void Update()
		{
			if(_queue.Count>0)
			{
				if (_timeLeft <= 0 && _layout.transform.childCount <MaxConcurentEntries) 
				{
					var entry = _queue[0];
					_queue.RemoveAt (0);
					var instance = GetNext();//Instantiate(_objectPrefab,_layout.transform);
					instance.Setup (entry.Title,entry.Message);
					_timeLeft = 0.5f;
				}
			}
			_timeLeft -= Time.deltaTime;
		}

		private UiNoticeObject GetNext()
		{
			return PoolManager.Spawn(_objectPrefab.gameObject,Vector3.zero,Quaternion.identity,_layout.transform).GetComponent<UiNoticeObject>();
		}


	}
}

