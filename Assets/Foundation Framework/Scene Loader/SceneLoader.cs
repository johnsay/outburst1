
#if DOTWEEN
	using DG.Tweening;
	#endif
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FoundationFramework
{
	public class SceneLoader : MonoBehaviour
	{
		[SerializeField] private CanvasGroup _canvas;
		private const string BlankScene = "BlankScene";
		private const string PrefabName = "SceneLoader";
		private const float FadeTime = 0.25f;
		private const float DelayBeforeFadeOut = 1.5f;

		private static SceneLoader _instance;
		private readonly WaitForEndOfFrame _waitForEndOfFrame = new WaitForEndOfFrame();
#if DOTWEEN
		private Tween _tweener;
		#endif
		private bool _isLoading;
		private string _currentScene;

		public static SceneLoader Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = Instantiate(Resources.Load<SceneLoader>(PrefabName));
				}

				return _instance;
			}
		}

		#region Delegates

		public event Action OnStartLoadScene;
		public event Action<float> OnLoadingProgress; 
		public event Action OnFinishLoadScene;

		#endregion

		#region Monobehaviour

		private void Awake()
		{
			DontDestroyOnLoad(gameObject);
			_canvas.alpha = 0;
			_canvas.gameObject.SetActive(false);
		}

		#endregion

		public void LoadScene(string sceneName)
		{
			if (_isLoading) return;
			
			_currentScene = sceneName;
			_isLoading = true;
			if (OnStartLoadScene != null)
				OnStartLoadScene();
			UpdateProgressEvent(0);
			_canvas.gameObject.SetActive(true);
#if DOTWEEN
			_tweener = UiTween.SetCanvasFade(Ease.Linear, _canvas, true, FadeTime, 0);
			_tweener.OnComplete(() => { StartCoroutine(LoadSceneSequence()); }
			);
	#endif
		}

		private IEnumerator LoadSceneSequence()
		{
			yield return _waitForEndOfFrame;
			yield return LoadSceneAsync(BlankScene);
		
			yield return _waitForEndOfFrame;
			CleanMemory();
			UpdateProgressEvent(0.01f);
			yield return _waitForEndOfFrame;
			var asyncOperation = LoadSceneAsync(_currentScene);
			
			while (asyncOperation.isDone == false)
			{
				yield return _waitForEndOfFrame;
				UpdateProgressEvent(asyncOperation.progress);
			}
			
			yield return _waitForEndOfFrame;
			UpdateProgressEvent(1);
#if DOTWEEN
			_tweener = UiTween.SetCanvasFade(Ease.Linear, _canvas, false, FadeTime, DelayBeforeFadeOut);
			_tweener.OnComplete(() =>
				{
					_canvas.gameObject.SetActive(false);
					if (OnFinishLoadScene != null)
						OnFinishLoadScene();
					_isLoading = false;
				}
	
			);
	#endif
		}

		private void UpdateProgressEvent(float progress)
		{
			if (OnLoadingProgress != null)
			{
				OnLoadingProgress.Invoke(progress);
			}
		}

		private AsyncOperation LoadSceneAsync(string sceneName)
		{
			return SceneManager.LoadSceneAsync(sceneName);
		}

		private void CleanMemory()
		{
			GC.Collect();
			Resources.UnloadUnusedAssets();
		}

		private void OnDestroy()
		{
#if DOTWEEN
			_tweener.SafeKill();
	#endif
		}
	}
}

