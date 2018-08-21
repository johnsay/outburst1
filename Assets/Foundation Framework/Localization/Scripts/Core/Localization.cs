using System.Collections.Generic;
using UnityEngine;

namespace FoundationFramework.Language
{
	public class Localization : MonoBehaviour
	{
		#region [[ FIELDS ]]
		private static Localization _instance;
		[SerializeField] private LocalizationDataHandler _dataHandler;
		[SerializeField] private TextAsset[] _persistantFiles;
		public delegate void OnLocalizationUpdated();
		public static OnLocalizationUpdated OnUpdated;
		private Language _currentLanguage;
		private readonly Dictionary<string, string> _persistantEntries = new Dictionary<string, string>();
		private readonly Dictionary<string, string> _localEntries = new Dictionary<string, string>();
		private ISerializer _serializer;
		private const string MissingTranslation = "[Missing]";

		#endregion

		#region [[ PRIVATE ]]

		private void Awake()
		{
			_serializer = new JsonSerializer();
			Initialize();
		}

		private void OnDestroy()
		{
			_instance = null;
		}

		//can load remotely the files from preloading scene then save to a file which are loaded here
		private void AddEntries(IEnumerable<TextAsset> source, IDictionary<string, string> destination)
		{
			foreach (var file in source)
			{
				var content = _serializer.Unserialize<Dictionary<string, Dictionary<Language, string>>>(file.text);
				if (content != null)
				{
					foreach (var entry in content)
					{
						if (string.IsNullOrEmpty(entry.Key) == false)
						{
							destination.Add(entry.Key, entry.Value[_currentLanguage]);
						}
					}
				}

			}
	
		}
		#endregion 
		
		#region [[ PUBLIC ]]

		public void Initialize()
		{
			if (_instance != null)
			{
				if(_instance !=this)
					Destroy(gameObject);
				
				return;
			}
			DontDestroyOnLoad(gameObject);
			_instance = this;
			_dataHandler.Initialize();
			_dataHandler.LoadData();
			_currentLanguage = _dataHandler.FindLanguage;
			
			AddEntries(_persistantFiles, _persistantEntries);
			if (OnUpdated != null) OnUpdated();
		}

		public static bool IsReady
		{
			get { return _instance != null; }
		}

		public static void ClearLocalEntries()
		{
			_instance._localEntries.Clear();
		}

		public static void AddLocalEntries(IEnumerable<TextAsset> entries)
		{
			_instance.AddEntries(entries, _instance._localEntries);
			if (OnUpdated != null) OnUpdated();
		}

		public static void ChangeLanguage(Language language)
		{
			_instance._currentLanguage = language;
			_instance._dataHandler.SaveLanguage(language);
			_instance._persistantEntries.Clear();
			_instance.AddEntries(_instance._persistantFiles, _instance._persistantEntries);
			if (OnUpdated != null) OnUpdated();
		}

		public static Language CurrentLanguage
		{
			get { return _instance._currentLanguage; }
		}

		public static string Translate(string key)
		{
			if (_instance._persistantEntries.ContainsKey(key))
			{
				return _instance._persistantEntries[key];
			}

			if (_instance._localEntries.ContainsKey(key))
			{
				return _instance._localEntries[key];
			}
		
			//uncomment if you want to display key when the translation is missing
			//return key;
			
			return MissingTranslation;
		}

		#endregion
	}
}

