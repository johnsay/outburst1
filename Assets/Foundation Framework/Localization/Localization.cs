using System.IO;
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
		private const string CodeTag = "[TAG] \"";
		private const string CodeMsg = "[MSG] \"";
		private const string CodeLineBreak = "[LB]";
		private const string Utf8LineBreak = "\n";
		private const string MissingTranslation = "[Missing]";

		#endregion

		#region [[ PRIVATE ]]

		private void Awake()
		{
			Initialize();
		}

		private void OnDestroy()
		{
			_instance = null;
		}

		private void AddEntries(IEnumerable<TextAsset> source, IDictionary<string, string> destination)
		{
			foreach (var file in source)
			{
				if (file.name.Contains(_currentLanguage.ToString()))
				{
					var result = new StringReader(file.text);
					string currentLine;
					var msg = string.Empty;

					while ((currentLine = result.ReadLine()) != null)
					{
						if (currentLine.StartsWith(CodeTag))
						{
							var tagLenght = currentLine.Length - CodeTag.Length - 1;
							var tagTitle = currentLine.Substring(CodeTag.Length, tagLenght);
							currentLine = result.ReadLine();

							if (currentLine != null)
							{
								if (currentLine.StartsWith(CodeMsg))
								{
									var msgLength = currentLine.Length - CodeMsg.Length - 1;
									msg = currentLine.Substring(CodeMsg.Length, msgLength).Replace(CodeLineBreak, Utf8LineBreak);
								}
							}

							if (string.IsNullOrEmpty(msg))
							{
								msg = string.Empty;
							}
							else
							{
								destination.Add(tagTitle, msg);
							}
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

