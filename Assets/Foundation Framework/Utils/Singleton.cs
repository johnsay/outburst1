using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Component
{

	#region Fields

	private  static bool _isApplicationQuitting;

	[SerializeField] private bool _dontDestroy;
	/// <summary>
	/// The instance.
	/// </summary>
	private static T _instance;

	#endregion

	#region Properties

	/// <summary>
	/// Gets the instance.
	/// </summary>
	/// <value>The instance.</value>
	public static T Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType<T>();
				if (_instance == null)
				{
					if (_isApplicationQuitting) return null;
					var obj = Instantiate(Resources.Load<T>(typeof(T).Name));
					if (obj == null)
					{
						Debug.LogError("could not find the singleton object "+typeof(T).Name);
						Debug.LogError("be sure to place it in Resources folder and use the class name on the object");
						return null;
					}

					obj.name = typeof(T).Name;
					_instance = obj;
				}
			}
			return _instance;
		}
	}

	#endregion

	#region Methods

	/// <summary>
	/// Use this for initialization.
	/// </summary>
	protected virtual void Awake()
	{
		if (_instance == null)
		{
			_instance = this as T;
			if(_dontDestroy)
			DontDestroyOnLoad(gameObject);
		}
	}

	private void OnDestroy()
	{
		_instance = null;
	}

	void OnApplicationQuit ()
	{
		_isApplicationQuitting = true;
	}
	#endregion
}